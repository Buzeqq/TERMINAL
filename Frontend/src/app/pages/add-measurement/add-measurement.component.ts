import { Component, ElementRef, OnInit, ViewChild } from '@angular/core';
import { ParametersService } from "../../core/services/parameters/parameters.service";
import { FormArray, FormBuilder, FormControl, FormGroup, Validators } from "@angular/forms";
import {
  BehaviorSubject,
  combineLatestWith,
  debounceTime,
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
import { isNumeric, isText, Parameter } from "../../core/models/parameters/parameter";

@Component({
  selector: 'app-add-measurement',
  templateUrl: './add-measurement.component.html',
  styleUrls: ['./add-measurement.component.scss']
})
export class AddMeasurementComponent implements OnInit {
  parameters: Parameter[] = [];
  recentProjects$ = this.projectService.getProjects(0, 5);
  filteredProjects$: Observable<Project[]> = new Observable<Project[]>();

  private chosenTags = new BehaviorSubject<string[]>([]);
  get chosenTags$() {
    return this.chosenTags.asObservable();
  }
  recentTags$ = this.tagsService.getTags(0, 5).pipe(
    combineLatestWith(this.chosenTags$),
    map(([recentTags, chosenTags]) => recentTags.filter(t1 => !chosenTags.find(t2 => t1 === t2))),
  );
  filteredTags$: Observable<string[]> = new Observable<string[]>();

  // TODO recentRecipes$: Observable<Recipes[]>
  // TODO filteredRecipes$: Observable<Recipes[]> = new Observable<Recipes[]>();
  selectedDate: Date = new Date();
  projectInputValue = new BehaviorSubject<string>('');
  recipeChosen$ = new Observable<boolean>();

  timeDateControl = new FormControl(this.selectedDate, [Validators.required]);
  projectControl = new FormControl('', [Validators.required]);
  recipeControl = new FormControl('', [Validators.required]);
  tagControl = new FormControl('');

  measurementForm = this.formBuilder.group({
    dateTime: [new Date(), [Validators.required]],
    project: ['', [Validators.required]],
    recipe: ['', [Validators.required]],
    tags: [''],
    comment: [''],
    steps: this.formBuilder.array([])
  });

  get parameterControls() {
    return this.measurementForm.get('steps') as FormArray<FormGroup<Record<string, FormControl>>>;
  }

  @ViewChild('tagInput') tagInput?: ElementRef<HTMLInputElement>;
  tabs: StepTab[] = [{}];

  constructor(protected readonly parameterService: ParametersService,
              private readonly searchService: SearchService,
              private readonly projectService: ProjectsService,
              private readonly tagsService: TagsService,
              private readonly formBuilder: FormBuilder) {
  }

  ngOnInit(): void {
    this.filteredProjects$ = this.projectInputValue.pipe(
      startWith(''),
      filter(phrase => !!phrase && phrase!.length > 3),
      switchMap(phrase => this.searchService.searchProjects(phrase!, 0, 10)),
    );

    this.recipeChosen$ = this.recipeControl.valueChanges.pipe(
      map(s => s !== 'None')
    );

    this.filteredTags$ = this.tagControl.valueChanges.pipe(
      startWith(''),
      debounceTime(500),
      filter(phrase => !!phrase),
      switchMap(phrase => this.searchService.searchTags(phrase!, 0, 10)),
      combineLatestWith(this.recentTags$),
      map(([filteredTags, chosenTags]) => filteredTags.filter(t1 => chosenTags.find(t2 => t1 === t2))),
    );

    this.parameterService.getParameters().subscribe(p => {
      this.parameters = p;
      const stepControl: Record<string, FormControl<number | string | null>> = {};
      for (const parameter of p) {
        let initialValue = isText(parameter) ? '' : 0;

        stepControl[parameter.name] = new FormControl(initialValue, [Validators.required]);
      }

      const stepsControl = this.parameterControls;
      const firstStep = new FormGroup(stepControl);
      stepsControl.push(firstStep);
    });
  }

  selectedTag(event: MatAutocompleteSelectedEvent) {
    const newTag = event.option.viewValue;
    if (this.chosenTags.value.find(t => t === newTag)) {
      return;
    }

    this.chosenTags.next([newTag ,...this.chosenTags.value]);
    this.tagInput!.nativeElement.value = '';
    this.tagControl.setValue('');
  }

  removeTag(tag: string) {
    const index = this.chosenTags.value.indexOf(tag);

    if (index >= 0) {
      this.chosenTags.value.splice(index, 1)
      this.chosenTags.next(this.chosenTags.value);
    }
  }

  addStep(i: number) {
    this.tabs.push({ });
    this.parameterControls.push(Object.create(this.parameterControls.controls[i]));
  }

  deleteStep(i: number) {
    this.tabs.splice(i, 1);
    this.parameterControls.removeAt(i);
  }

  getLabel = (i: number) => `Step ${i + 1}`;

  getInputTypeOfParameter(name: string) {
    const parameter = this.parameters.find(p => p.name === name);
    if (!parameter) return 'text';
    return isNumeric(parameter) ? 'number' : 'text';
  }

  getGroupForTab(tabIndex: number): Record<string, FormControl<string | number | null>> | undefined {
    return this.parameterControls.controls.at(tabIndex)?.controls;
  }
}

export interface StepTab {

}
