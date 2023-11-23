import {Component, ElementRef, Input, OnInit, ViewChild} from '@angular/core';
import {
  BehaviorSubject,
  catchError,
  combineLatestWith,
  debounceTime,
  EMPTY, filter,
  map,
  Observable,
  startWith, switchMap,
  tap
} from "rxjs";
import {Project} from "../../../core/models/projects/project";
import {ParametersService} from "../../../core/services/parameters/parameters.service";
import {SearchService} from "../../../core/services/search/search.service";
import {ProjectsService} from "../../../core/services/projects/projects.service";
import {TagsService} from "../../../core/services/tags/tags.service";
import {FormBuilder, Validators} from "@angular/forms";
import {MeasurementsService} from "../../../core/services/measurements/measurements.service";
import {NotificationService} from "../../../core/services/notification/notification.service";
import {Router} from "@angular/router";
import {MatAutocompleteSelectedEvent} from "@angular/material/autocomplete";
import {MeasurementDetails} from "../../../core/models/measurements/measurementDetails";
import {MatSnackBar} from "@angular/material/snack-bar";
import {ProjectDetails} from "../../../core/models/projects/project-details";

@Component({
  selector: 'app-measurement-edit',
  templateUrl: './measurement-edit.component.html',
  styleUrls: ['./measurement-edit.component.scss']
})
export class MeasurementEditComponent implements OnInit {
  loading: 'determinate' | 'indeterminate' | 'buffer' | 'query' = 'query';

  measurementForm = this.formBuilder.group({
    project: ['', [Validators.required]],
    recipe: ['', [Validators.required]],
    tags: [''],
    comment: [''],
    // steps: this.formBuilder.array([]) // TODO
  });

  private _measurementId?: string;
  measurementDetails$: Observable<MeasurementDetails> = new Observable<MeasurementDetails>();

  projectDetails$: Observable<ProjectDetails> = new Observable<ProjectDetails>();
  projects$: Observable<Project[]> = new Observable<Project[]>();

  private chosenTags = new BehaviorSubject<string[]>([]);
  get chosenTags$() {
    return this.chosenTags.asObservable();
  }
  recentTags$ = this.tagsService.getTags(0, 5).pipe(
    combineLatestWith(this.chosenTags$),
    map(([recentTags, chosenTags]) => recentTags.filter(t1 => !chosenTags.find(t2 => t1.name === t2))),
  );
  filteredTags$: Observable<string[]> = new Observable<string[]>();
  @ViewChild('tagInput') tagInput?: ElementRef<HTMLInputElement>;

  constructor(protected readonly parameterService: ParametersService,
              private readonly searchService: SearchService,
              private readonly projectService: ProjectsService,
              private readonly tagsService: TagsService,
              private readonly formBuilder: FormBuilder,
              private readonly measurementService: MeasurementsService,
              private readonly notificationService: NotificationService,
              private readonly router: Router,
              private readonly snackBar: MatSnackBar
  ) {  }

  ngOnInit(): void {
    this.filteredTags$ = this.measurementForm.controls.tags.valueChanges.pipe(
      startWith(''),
      debounceTime(500),
      filter(phrase => !!phrase),
      switchMap(phrase => this.searchService.searchTags(phrase!, 0, 10)),
      combineLatestWith(this.recentTags$),
      map(([filteredTags, chosenTags]) => filteredTags.filter(t1 => chosenTags.find(t2 => t1 === t2.name))),
    );
  }

  @Input()
  get measurementId(): string | undefined {
    return this._measurementId;
  }

  set measurementId(id: string | undefined) {
    this.measurementDetails$ = this.measurementService.getMeasurementDetails(id!)
      .pipe(
        catchError((err, _) => {
          console.log(err);
          this.snackBar.open('Failed to load measurement', 'Close', {duration: 3000});
          return EMPTY;
        }),
        tap(measurement => {
          this.initForm(measurement);
          this.loading = 'determinate';
        })
      );
  }

  private initForm(m: MeasurementDetails) {
    // set initial values in form controls

    this.projectDetails$ = this.projectService.getProject(m.projectId)
      .pipe(tap(p => this.measurementForm.controls.project.setValue(p.name)));
    this.projects$ = this.projectService.getProjects(0, 30); // TODO get all projects for a dropdown list?

    this.measurementForm.controls.recipe.setValue('None') // TODO

    this.chosenTags.next(m.tags);
    this.measurementForm.controls.comment.setValue(m.comment);
  }

  selectedTag(event: MatAutocompleteSelectedEvent) {
    const newTag = event.option.viewValue;
    if (this.chosenTags.value.find(t => t === newTag)) {
      return;
    }

    this.chosenTags.next([newTag ,...this.chosenTags.value]);
    this.tagInput!.nativeElement.value = '';
    this.measurementForm.controls.tags.setValue('');
  }

  removeTag(tag: string) {
    const index = this.chosenTags.value.indexOf(tag);

    if (index >= 0) {
      this.chosenTags.value.splice(index, 1)
      this.chosenTags.next(this.chosenTags.value);
    }
  }

}
