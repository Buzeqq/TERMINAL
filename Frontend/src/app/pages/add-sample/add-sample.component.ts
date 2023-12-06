import { Component, OnDestroy, OnInit } from '@angular/core';
import { ParametersService } from "../../core/services/parameters/parameters.service";
import { FormArray, FormControl, FormGroup, Validators } from "@angular/forms";
import {
  BehaviorSubject, catchError,
  debounceTime, EMPTY, filter,
  map,
  Observable,
  startWith, Subscription,
  switchMap, tap
} from "rxjs";
import { SearchService } from "../../core/services/search/search.service";
import { ProjectsService } from "../../core/services/projects/projects.service";
import { MatAutocompleteSelectedEvent } from "@angular/material/autocomplete";
import { Parameter } from "../../core/models/parameters/parameter";
import { SamplesService } from "../../core/services/samples/samples.service";
import { NotificationService } from "../../core/services/notification/notification.service";
import { ActivatedRoute, Params, Router } from "@angular/router";
import {
  AddSampleFormData, CommentFormControl, ComplexTypeFormControl,
  DateFormControl, ParameterFormControl, ProjectFormControl,
  RecipeFormControl, SampleForm, StepFormArray,
  TagsFormControl
} from "./types/addSampleTypes";
import { RecipesService } from "../../core/services/recipes/recipes.service";
import { AddSample } from "../../core/models/samples/addSample";
import { environment } from "../../../environments/environment";
import { Project } from "../../core/models/projects/project";
import { Tag } from "../../core/models/tags/tag";
import { Recipe } from "../../core/models/recipes/recipe";
import { SetupFormService } from "../../core/services/setup-form/setup-form.service";
import { ReplicateQuery, ReplicationData, ReplicationService } from "../../core/services/steps/replication.service";

@Component({
  selector: 'app-add-sample',
  templateUrl: './add-sample.component.html',
  styleUrls: ['./add-sample.component.scss'],
})
export class AddSampleComponent implements OnInit, OnDestroy {
  parameters: Parameter[] = [];
  private sampleFormData = new BehaviorSubject<AddSampleFormData>({
    projects: [],
    recipes: [],
    tags: []
  });
  public formData$: Observable<AddSampleFormData> = this.sampleFormData.asObservable();

  sampleForm = new FormGroup({
    date: new FormControl<Date>(new Date(), [Validators.required, Validators.nullValidator]) as DateFormControl,
    project: new ComplexTypeFormControl<Project | null, string | null>(null, null, [Validators.required]) as ProjectFormControl,
    tags: new ComplexTypeFormControl<Tag[], string[]>([], []) as TagsFormControl,
    recipe: new ComplexTypeFormControl<Recipe | null, string | null>( null,null, [Validators.required]) as RecipeFormControl,
    comment: new FormControl<string | null>(null, [Validators.maxLength(1024)]) as CommentFormControl,
    steps: new FormArray<StepFormArray>([])
  }) as SampleForm;

  constructor(protected readonly parameterService: ParametersService,
              private readonly searchService: SearchService,
              private readonly projectService: ProjectsService,
              private readonly samplesService: SamplesService,
              private readonly notificationService: NotificationService,
              private readonly recipesService: RecipesService,
              private readonly router: Router,
              private readonly activatedRoute: ActivatedRoute,
              private readonly setupFormService: SetupFormService,
              private readonly replicationService: ReplicationService) {
  }

  private readonly subscriptions: Subscription[] = [];

  replicationData$ = this.activatedRoute.queryParams.pipe(
    map((p: Params): ReplicateQuery | undefined => {
      const recipeId = p['recipeId'];
      if (recipeId) return {
        type: 'Recipe',
        id: recipeId,
      };

      const sampleId = p['sampleId'];
      if (sampleId) return {
        type: 'Sample',
        id: sampleId
      };

      return undefined;
    }),
    filter(q => q !== undefined),
    tap(_ => {
      this.sampleForm.controls.recipe.disable();
    }),
    switchMap(q => this.replicationService.getReplicationData(q!)),
    tap(d => {
      this.fillForm(d);
    })
  );


  ngOnInit(): void {
    this.subscriptions.push(this.sampleForm.controls.project.valueChanges.pipe(
      startWith(''),
      debounceTime(500),
      switchMap(phrase => {
        if (phrase === '') {
          return this.projectService.getProjects(0, 10);
        } else {
          return this.searchService.searchProjects(phrase!, 0, 10);
        }
      }),
      tap(projects => {
        this.sampleFormData.next({
          ...this.sampleFormData.value,
          projects
        });
      })
    ).subscribe());

    this.subscriptions.push(this.sampleForm.controls.recipe.valueChanges.pipe(
      startWith(''),
      debounceTime(500),
      switchMap(phrase => {
          if (phrase === '') {
            return this.recipesService.getRecipes(0, 10);
          } else {
            return this.searchService.searchRecipe(phrase!, 0, 10);
          }
        }),
      tap(recipes => {
        this.sampleFormData.next({
          ...this.sampleFormData.value,
          recipes
        });
      })
    ).subscribe());

    this.parameterService.getParameters()
      .pipe(
        map(parameters => parameters.sort((p1, p2) => p1.order - p2.order)),
      ).subscribe(parameters => {
        this.parameters = parameters;

        const steps = this.sampleForm.controls.steps;
        const firstStep = this.setupFormService.getFirstStep(parameters);

        this.subscriptions.push(...this.setupFormService.setParents(firstStep.controls.parameters, this.parameters));

        steps.push(firstStep);
    });

    this.subscriptions.push(this.sampleForm.controls.recipe.valueChanges
      .pipe(
        filter(_ => !!this.sampleForm.controls.recipe.item),
        map(_ => this.sampleForm.controls.recipe.item),
        switchMap(r => this.replicationService.getReplicationData({
          id: r!.id,
          type: 'Recipe'
        }).pipe(
          catchError((_) => {
            this.notificationService.notifyError('Failed to load recipe!');
            return EMPTY;
          })
        )),
        tap(d => this.fillForm(d))
      )
      .subscribe());
  }



  ngOnDestroy(): void {
    this.subscriptions.forEach(s => s.unsubscribe());
  }

  onRecipeSelect(event: MatAutocompleteSelectedEvent) {
    if (event.option.value === null) {
      this.sampleForm.controls.recipe.setItem(null, 'None');
      return;
    }
    this.sampleForm.controls.recipe.setItem(event.option.value, event.option.value.name);
  }

  addSample() {
    const addSample = {
      projectId: this.sampleForm.controls.project.item?.id,
      recipeId: this.sampleForm.controls.recipe.item?.id,
      steps: this.setupFormService.getStepsDto(this.sampleForm.controls.steps),
      tagIds: this.sampleForm.controls.tags.value,
      comment: this.sampleForm.controls.comment.value ?? '',
      saveAsRecipe: this.saveRecipeFormGroup.controls.saveAsRecipe.value,
      recipeName: this.saveRecipeFormGroup.controls.recipeName.value,
    } as AddSample;

    this.samplesService.addSample(addSample)
      .pipe(catchError((err, _) => {
        this.notificationService.notifyError(err);
        return EMPTY;
      }))
      .subscribe(_ => {
        this.router.navigate(['/samples'])
          .then(_ => this.notificationService.notifySuccess('Sample added!'));
      });
  }

  public saveRecipeFormGroup = new FormGroup({
    saveAsRecipe: new FormControl<boolean>(false),
    recipeName: new FormControl<string | null>('')
  })
  protected readonly environment = environment;

  private fillForm(d: ReplicationData) {
    this.sampleForm.controls.steps.clear();
    for (const s of d.steps) {
      const step = new FormGroup<{comment: CommentFormControl, parameters: FormArray<ParameterFormControl>}>({
        comment: new FormControl(d.comment),
        parameters: new FormArray<ParameterFormControl>(s.parameters.sort((pv1, pv2) => {
          const p1 = this.parameters.find(p => p.name === pv1.name)!;
          const p2 = this.parameters.find(p => p.name === pv2.name)!;

          return p1?.order - p2?.order;
        })
          .map(p1 => new ComplexTypeFormControl<Parameter>(
            this.parameters.find(p2 => p1.name === p2.name)!, p1.value
          )))
      });
      this.sampleForm.controls.steps.push(step);

      this.subscriptions.push(...this.setupFormService.setParents(step.controls.parameters, this.parameters));
    }
  }
}
