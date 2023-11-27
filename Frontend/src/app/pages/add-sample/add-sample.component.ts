import { Component, ElementRef, OnInit, ViewChild } from '@angular/core';
import { ParametersService } from "../../core/services/parameters/parameters.service";
import {
  FormArray,
  FormBuilder,
  FormControl,
  FormGroup,
  Validators
} from "@angular/forms";
import {
  BehaviorSubject, catchError,
  combineLatestWith,
  debounceTime, EMPTY,
  filter,
  map,
  Observable,
  startWith,
  switchMap
} from "rxjs";
import { SearchService } from "../../core/services/search/search.service";
import { Project } from "../../core/models/projects/project";
import { ProjectsService } from "../../core/services/projects/projects.service";
import { TagsService } from "../../core/services/tags/tags.service";
import { MatAutocompleteSelectedEvent } from "@angular/material/autocomplete";
import { isNumeric, isText, Parameter, TextParameter } from "../../core/models/parameters/parameter";
import { AddSample, ParameterValue, Step } from "../../core/models/samples/addSample";
import { SamplesService } from "../../core/services/samples/samples.service";
import { NotificationService } from "../../core/services/notification/notification.service";
import { Router } from "@angular/router";
import { Tag } from "../../core/models/tags/tag";

@Component({
  selector: 'app-add-sample',
  templateUrl: './add-sample.component.html',
  styleUrls: ['./add-sample.component.scss']
})
export class AddSampleComponent implements OnInit {
  parameters: Parameter[] = [];
  recentProjects$ = this.projectService.getProjects(0, 5);
  filteredProjects$: Observable<Project[]> = new Observable<Project[]>();

  private chosenTags = new BehaviorSubject<Tag[]>([]);
  selectedProject?: Project;
  get chosenTags$() {
    return this.chosenTags.asObservable();
  }
  recentTags$ = this.tagsService.getTags(0, 5).pipe(
    combineLatestWith(this.chosenTags$),
    map(([recentTags, chosenTags]) => recentTags.filter(t1 => !chosenTags.find(t2 => t1.id === t2.id))),
  );
  filteredTags$: Observable<Tag[]> = new Observable<Tag[]>();

  // TODO recentRecipes$: Observable<Recipes[]>
  // TODO filteredRecipes$: Observable<Recipes[]> = new Observable<Recipes[]>();
  selectedDate: Date = new Date();
  projectInputValue = new BehaviorSubject<string>('');
  recipeChosen$ = new Observable<boolean>();

  sampleForm = this.formBuilder.group({
    dateTime: [new Date(), [Validators.required]],
    project: ['', [Validators.required]],
    recipe: ['', [Validators.required]],
    tags: [''],
    comment: [''],
    steps: this.formBuilder.array([])
  });

  get parameterControls() {
    return this.sampleForm.get('steps') as FormArray<FormGroup<Record<string, FormControl>>>;
  }

  @ViewChild('tagInput') tagInput?: ElementRef<HTMLInputElement>;
  tabs: StepTab[] = [{}];

  constructor(protected readonly parameterService: ParametersService,
              private readonly searchService: SearchService,
              private readonly projectService: ProjectsService,
              private readonly tagsService: TagsService,
              private readonly formBuilder: FormBuilder,
              private readonly samplesService: SamplesService,
              private readonly notificationService: NotificationService,
              private readonly router: Router) {
  }

  ngOnInit(): void {
    this.filteredProjects$ = this.projectInputValue.pipe(
      startWith(''),
      filter(phrase => !!phrase && phrase!.length > 3),
      switchMap(phrase => this.searchService.searchProjects(phrase!, 0, 10)),
    );

    this.recipeChosen$ = this.sampleForm.controls.recipe.valueChanges.pipe(
      map(s => s !== 'None')
    );

    this.filteredTags$ = this.sampleForm.controls.tags.valueChanges.pipe(
      startWith(''),
      debounceTime(500),
      filter(phrase => !!phrase),
      switchMap(phrase => this.searchService.searchTags(phrase!, 0, 10)),
      combineLatestWith(this.recentTags$),
      map(([filteredTags, chosenTags]) => filteredTags.filter(t1 => chosenTags.find(t2 => t1.id === t2.id))),
    );

    this.parameterService.getParameters().subscribe(p => {
      this.parameters = p;
      const stepControl: Record<string, FormControl<number | string | null>> = {};
      for (const parameter of p) {
        let initialValue = isText(parameter) ? '' : 0;

        stepControl[parameter.name] = new FormControl(initialValue, [Validators.required]);
      }
      stepControl['comment'] = new FormControl('', []);

      const stepsControl = this.parameterControls;
      const firstStep = new FormGroup(stepControl);
      stepsControl.push(firstStep);
    });

    this.sampleForm.controls.recipe.setValue('None');
  }

  selectedTag(event: MatAutocompleteSelectedEvent) {
    const newTag = event.option.value;
    console.log(newTag);
    if (this.chosenTags.value.find(t => t.id === newTag)) {
      return;
    }

    this.chosenTags.next([newTag ,...this.chosenTags.value]);
    this.tagInput!.nativeElement.value = '';
    this.sampleForm.controls.tags.setValue('');
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

  addStep(i: number) {
    this.tabs.push({ });
    let step = this.copyStep(this.parameterControls.at(i));
    this.parameterControls.push(step);
  }

  deleteStep(i: number) {
    this.tabs.splice(i, 1);
    this.parameterControls.removeAt(i);
  }

  getLabel = (i: number) => `Step ${i + 1}`;

  getInputTypeOfParameter(name: string) {
    if (name === 'comment') return 'comment';
    const parameter = this.parameters.find(p => p.name === name);
    if (!parameter) return 'text';
    return isNumeric(parameter) ? 'number' : 'text';
  }

  getParameterUnit(name: string) {
    if (name === 'comment') return undefined;
    const parameter = this.parameters.find(p => p.name === name);
    if (!parameter) return undefined;
    return isNumeric(parameter) ? parameter.unit : undefined;
  }

  getGroupForTab(tabIndex: number): Record<string, FormControl<string | number | null>> | undefined {
    return this.parameterControls.controls.at(tabIndex)?.controls;
  }

  getAllowedOptions(name: string) {
    if (name === 'comment') return [];
    const parameter = this.parameters.find(p => p.name === name);
    if (!parameter && !isText(parameter!)) return [];
    return (parameter as TextParameter).allowedValues;
  }

  addSample() {
    let form = {...this.sampleForm.value} as SampleForm;
    form.tagIds = this.chosenTags.value.map(t => t.id);
    form.project = this.selectedProject!.id;
    console.log(form.tagIds);
    const addSample: AddSample = {
      projectId: form.project,
      recipeId: null,
      tagIds: form.tagIds,
      comment: form.comment,
      steps: form.steps!.map(s => this.mapToStep(s))
    };

    this.samplesService.addSample(addSample).pipe(
      catchError((err, _) => {
        this.notificationService.notifyError(err);
        return EMPTY;
      })
    ).subscribe(_ => {
      this.router.navigate(['/samples'])
        .then(_ => this.notificationService.notifySuccess('Sample added!'));
    });
  }
  private mapToStep(s: Record<string, string | number | null>): Step {
    let parameters: ParameterValue[] = [];

    for (let [name, value] of Object.entries(s)) {
      if (name === 'comment') continue;

      const parameter = this.parameters.find(p => p.name === name);
      if (!parameter) continue;

      parameters.push({
        $type: parameter.$type,
        id: parameter.id,
        value: value!
      })
    }
    console.log(s);
    return {
      parameters,
      comment: s['comment'] as string
    }
  }


  selectProject(event: MatAutocompleteSelectedEvent) {
    this.selectedProject = event.option.value;
    this.sampleForm.controls.project.setValue(this.selectedProject!.name);
  }

  private copyStep(formGroup: FormGroup<Record<string, FormControl>>): FormGroup<Record<string, FormControl>> {
    const fg = this.formBuilder.group({});
    for (const [name, control] of Object.entries(formGroup.controls)) {
      fg.addControl(name, control);
    }
    return fg;
  }
}

export interface StepTab {

}

export type SampleForm =  Partial<{
  dateTime: Date | null,
  project: string | null,
  recipe: string | null,
  tagIds: string[] | null,
  comment: string | null,
  steps: Record<string, string | number | null>[]}>
