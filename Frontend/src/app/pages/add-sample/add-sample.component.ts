import { Component, OnDestroy, OnInit } from '@angular/core';
import { ParametersService } from "../../core/services/parameters/parameters.service";
import { FormArray, FormControl, FormGroup, Validators } from "@angular/forms";
import {
  BehaviorSubject, catchError,
  debounceTime, EMPTY,
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
import { Router } from "@angular/router";
import {
  AddSampleFormData, CommentFormControl, ComplexTypeFormControl,
  DateFormControl, ProjectFormControl,
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
    tags: new ComplexTypeFormControl<Tag[], string[]>([], [], [Validators.required]) as TagsFormControl,
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
              private readonly setupFormService: SetupFormService) {
  }

  private readonly subscriptions: Subscription[] = [];
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
}
