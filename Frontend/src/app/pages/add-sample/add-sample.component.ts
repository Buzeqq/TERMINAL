import { Component, OnDestroy, OnInit } from '@angular/core';
import { ParametersService } from "../../core/services/parameters/parameters.service";
import { FormArray, FormControl, FormGroup, Validators } from "@angular/forms";
import {
  BehaviorSubject,
  debounceTime,
  map,
  Observable,
  startWith, Subscription,
  switchMap, tap
} from "rxjs";
import { SearchService } from "../../core/services/search/search.service";
import { ProjectsService } from "../../core/services/projects/projects.service";
import { MatAutocompleteSelectedEvent } from "@angular/material/autocomplete";
import { NumericParameter, Parameter, TextParameter } from "../../core/models/parameters/parameter";
import { SamplesService } from "../../core/services/samples/samples.service";
import { NotificationService } from "../../core/services/notification/notification.service";
import { Router } from "@angular/router";
import {
  AddSampleFormData, CommentFormControl,
  DateFormControl, ParameterFormControl,
  ProjectFormControl,
  RecipeFormControl, SampleForm, StepFormArray,
  TagsFormControl
} from "./types/addSampleTypes";
import { RecipesService } from "../../core/services/recipes/recipes.service";
import { AddSample, ParameterValue, Step } from "../../core/models/samples/addSample";

@Component({
  selector: 'app-add-sample',
  templateUrl: './add-sample.component.html',
  styleUrls: ['./add-sample.component.scss']
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
    project: new FormControl<string | null>(null, [Validators.required]) as ProjectFormControl,
    tags: new FormControl<string[]>([], [Validators.required]) as TagsFormControl,
    recipe: new FormControl<string | null>(null, [Validators.required]) as RecipeFormControl,
    comment: new FormControl<string | null>(null, [Validators.maxLength(1024)]) as CommentFormControl,
    steps: new FormArray<StepFormArray>([])
  }) as SampleForm;

  constructor(protected readonly parameterService: ParametersService,
              private readonly searchService: SearchService,
              private readonly projectService: ProjectsService,
              private readonly samplesService: SamplesService,
              private readonly notificationService: NotificationService,
              private readonly recipesService: RecipesService,
              private readonly router: Router) {
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
      const steps = this.sampleForm.controls.steps;
      const firstStep: StepFormArray = new FormGroup<{comment: CommentFormControl; parameters: FormArray<ParameterFormControl>}>({
        comment: new FormControl<string | null>(''),
        parameters: new FormArray<ParameterFormControl>([])
      });

      for (const parameter of parameters) {
        switch (parameter.$type) {
          case "decimal":
          case "integer": {
            const numericParameter = parameter as NumericParameter;
            firstStep.controls.parameters.push(new ParameterFormControl<string | number | null>(numericParameter, numericParameter.defaultValue,
              [Validators.required]));
            break;
          }
          case "text": {
            const textParameter = parameter as TextParameter;
            const defaultValue = textParameter.defaultValue ?
              textParameter.allowedValues[textParameter.defaultValue] : null;
            firstStep.controls.parameters.push(new ParameterFormControl<string | number | null>(textParameter, defaultValue,
              [Validators.required]));
            break;
          }
        }
      }

      steps.push(firstStep);
    });

    this.sampleForm.controls.recipe.setValue('None');
  }
  ngOnDestroy(): void {
    this.subscriptions.forEach(s => s.unsubscribe());
  }

  getUnit(parameter: Parameter): string | undefined {
    if (parameter.$type !== 'text') {
      return (parameter as NumericParameter).unit;
    }
    return undefined;
  }

  getStep(parameter: Parameter) {
    if (parameter.$type !== 'text') {
      return (parameter as NumericParameter).step;
    }

    return undefined;
  }

  getOptions(parameter: Parameter) {
    if (parameter.$type === 'text') {
      return (parameter as TextParameter).allowedValues;
    }

    return [];
  }

  selectedTabIndex: number = 0;
  removeTab(i: number) {
    this.sampleForm.controls.steps.removeAt(i);
  }
  addTab(selectedTabIndex: number) {
    const t = this.sampleForm.controls.steps.at(selectedTabIndex).controls.parameters as FormArray<ParameterFormControl>;
    const parameterControls = new FormArray<ParameterFormControl>([]);
    for (const p of t.controls) {
      parameterControls.push(new ParameterFormControl<number | string | null>(
        p.parameter,
        p.value,
        p.validator
      ));
    }
    this.sampleForm.controls.steps.insert(selectedTabIndex, new FormGroup<{comment: CommentFormControl; parameters: FormArray<ParameterFormControl>}>({
      comment: new FormControl<string | null>(''),
      parameters: parameterControls
    }));
  }

  onRecipeSelect(event: MatAutocompleteSelectedEvent) {
    // todo: populate form with fetch steps
  }

  addSample() {
    const data = this.sampleFormData.value;
    const addSample = {
      projectId: data.projects.find(p => p.name === this.sampleForm.controls.project.value)?.id,
      recipeId: data.recipes.find(r => r.name === this.sampleForm.controls.recipe.value)?.id,
      steps: this.getStepsDto(),
      tagIds: data.recipes.filter(t => this.sampleForm.controls.tags.value.includes(t.name))
        .map(t => t.id),
      comment: this.sampleForm.controls.comment.value,
      saveAsRecipe: this.saveRecipeFormGroup.controls.saveAsRecipe.value,
      recipeName: this.saveRecipeFormGroup.controls.recipeName.value,
    } as AddSample;

    this.samplesService.addSample(addSample)
      .subscribe(_ => {
        this.router.navigate(['/samples'])
          .then(_ => this.notificationService.notifySuccess('Sample added!'));
      });
  }

  getStepsDto() {
    const steps: Step[] = [];
    const stepsControls = this.sampleForm.controls.steps;
    for (const stepControls of stepsControls.controls) {
      steps.push({
        parameters: stepControls.controls.parameters.controls.map(c => ({
          $type: c.parameter.$type,
          id: c.parameter.id,
          value: c.value
        } as ParameterValue)),
        comment: stepControls.controls.comment.value
      });
    }

    return steps;
  }

  public saveRecipeFormGroup = new FormGroup({
    saveAsRecipe: new FormControl<boolean>(false),
    recipeName: new FormControl<string | null>('')
  })
}
