import { Component, ElementRef, OnDestroy, OnInit, ViewChild } from '@angular/core';
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
import { TagsService } from "../../core/services/tags/tags.service";
import { MatAutocompleteSelectedEvent } from "@angular/material/autocomplete";
import { NumericParameter, Parameter, TextParameter } from "../../core/models/parameters/parameter";
import { SamplesService } from "../../core/services/samples/samples.service";
import { NotificationService } from "../../core/services/notification/notification.service";
import { Router } from "@angular/router";
import { Tag } from "../../core/models/tags/tag";
import {
  AddSampleFormData, CommentFormControl,
  DateFormControl, ParameterFormControl,
  ProjectFormControl,
  RecipeFormControl, SampleForm, StepFormArray,
  TagsFormControl
} from "./types/addSampleTypes";
import { RecipesService } from "../../core/services/recipes/recipes.service";

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
              private readonly tagsService: TagsService,
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

    this.subscriptions.push(this.tagFormControl.valueChanges.pipe(
      startWith(''),
      debounceTime(500),
      switchMap(phrase => {
        if (phrase === '') {
          return this.tagsService.getTags(0, 10);
        } else {
          return this.searchService.searchTags(phrase!, 0, 10);
        }
      }),
      tap(r => {
        console.log(r);
        return r;
      }),
      // combineLatestWith(this.chosenTags$),
      // map(([filteredTags, chosenTags]) =>
      //   filteredTags.filter(t1 => chosenTags.find(t2 => t1.id === t2.id))),
      tap(tags => this.sampleFormData.next({
        ...this.sampleFormData.value,
        tags
      }))
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

  public tagFormControl = new FormControl<string>('');
  @ViewChild('tagInput') tagInput?: ElementRef<HTMLInputElement>;
  private chosenTags = new BehaviorSubject<Tag[]>([]);
  public readonly chosenTags$ = this.chosenTags.asObservable();

  selectedTag(event: MatAutocompleteSelectedEvent) {
    const newTag = event.option.value;
    if (this.chosenTags.value.find(t => t.id === newTag)) {
      return;
    }

    this.chosenTags.next([newTag ,...this.chosenTags.value]);
    this.tagInput!.nativeElement.value = '';
    this.tagFormControl.setValue('');
  }

  removeTag(tagId: string) {
    const tag = this.chosenTags.value.find(t => t.id == tagId);
    if (!tag) return;
    const index = this.chosenTags.value.indexOf(tag);

    if (index >= 0) {
      this.chosenTags.value.splice(index, 1)
      this.chosenTags.next(this.chosenTags.value);
    }
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

  addSample() {

  }
  saveAsRecipeFormControl = new FormControl(false);
  recipeNameFormControl = new FormControl('')

  selectedTabIndex: number = 0;
  onRecipeSelect(event: MatAutocompleteSelectedEvent) {

  }
}
