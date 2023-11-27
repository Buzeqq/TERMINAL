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
import {SamplesService} from "../../../core/services/samples/samples.service";
import {NotificationService} from "../../../core/services/notification/notification.service";
import {Router} from "@angular/router";
import {MatAutocompleteSelectedEvent} from "@angular/material/autocomplete";
import {SampleDetails} from "../../../core/models/samples/sampleDetails";
import {MatSnackBar} from "@angular/material/snack-bar";
import {ProjectDetails} from "../../../core/models/projects/project-details";
import { Tag } from "../../../core/models/tags/tag";

@Component({
  selector: 'app-sample-edit',
  templateUrl: './sample-edit.component.html',
  styleUrls: ['./sample-edit.component.scss']
})
export class SampleEditComponent implements OnInit {
  loading: 'determinate' | 'indeterminate' | 'buffer' | 'query' = 'query';

  sampleForm = this.formBuilder.group({
    project: ['', [Validators.required]],
    recipe: ['', [Validators.required]],
    tags: [''],
    comment: [''],
    // steps: this.formBuilder.array([]) // TODO
  });

  private _sampleId?: string;
  sampleDetails$: Observable<SampleDetails> = new Observable<SampleDetails>();

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
  filteredTags$: Observable<Tag[]> = new Observable<Tag[]>();
  @ViewChild('tagInput') tagInput?: ElementRef<HTMLInputElement>;

  constructor(protected readonly parameterService: ParametersService,
              private readonly searchService: SearchService,
              private readonly projectService: ProjectsService,
              private readonly tagsService: TagsService,
              private readonly formBuilder: FormBuilder,
              private readonly samplesService: SamplesService,
              private readonly notificationService: NotificationService,
              private readonly router: Router,
              private readonly snackBar: MatSnackBar
  ) {  }

  ngOnInit(): void {
    this.filteredTags$ = this.sampleForm.controls.tags.valueChanges.pipe(
      startWith(''),
      debounceTime(500),
      filter(phrase => !!phrase),
      switchMap(phrase => this.searchService.searchTags(phrase!, 0, 10)),
      combineLatestWith(this.recentTags$),
      map(([filteredTags, chosenTags]) => filteredTags.filter(t1 => chosenTags.find(t2 => t1.id === t2.id))),
    );
  }

  @Input()
  get sampleId(): string | undefined {
    return this._sampleId;
  }

  set sampleId(id: string | undefined) {
    this.sampleDetails$ = this.samplesService.getSampleDetails(id!)
      .pipe(
        catchError((err, _) => {
          console.log(err);
          this.snackBar.open('Failed to load sample', 'Close', {duration: 3000});
          return EMPTY;
        }),
        tap(sample => {
          this.initForm(sample);
          this.loading = 'determinate';
        })
      );
  }

  private initForm(m: SampleDetails) {
    // set initial values in form controls

    this.projectDetails$ = this.projectService.getProject(m.projectId)
      .pipe(tap(p => this.sampleForm.controls.project.setValue(p.name)));
    this.projects$ = this.projectService.getProjects(0, 30); // TODO get all projects for a dropdown list?

    this.sampleForm.controls.recipe.setValue('None') // TODO

    this.chosenTags.next(m.tags);
    this.sampleForm.controls.comment.setValue(m.comment);
  }

  selectedTag(event: MatAutocompleteSelectedEvent) {
    const newTag = event.option.viewValue;
    if (this.chosenTags.value.find(t => t === newTag)) {
      return;
    }

    this.chosenTags.next([newTag ,...this.chosenTags.value]);
    this.tagInput!.nativeElement.value = '';
    this.sampleForm.controls.tags.setValue('');
  }

  removeTag(tag: string) {
    const index = this.chosenTags.value.indexOf(tag);

    if (index >= 0) {
      this.chosenTags.value.splice(index, 1)
      this.chosenTags.next(this.chosenTags.value);
    }
  }

}
