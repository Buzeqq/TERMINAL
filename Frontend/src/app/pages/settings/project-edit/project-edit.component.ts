import {Component, Input} from '@angular/core';
import {catchError, EMPTY, Observable, tap} from "rxjs";
import {MatSnackBar} from "@angular/material/snack-bar";
import {FormControl, Validators} from "@angular/forms";
import {whitespaceValidator} from "../../../core/components/validators/whitespaceValidator";
import {Project} from "../../../core/models/projects/project";
import {ProjectDetails} from "../../../core/models/projects/project-details";
import {ProjectsService} from "../../../core/services/projects/projects.service";

@Component({
  selector: 'app-project-edit',
  templateUrl: './project-edit.component.html',
  styleUrls: ['./project-edit.component.scss']
})
export class ProjectEditComponent {
  private _projectId?: string;
  projectDetails$: Observable<Project> = new Observable<Project>();
  private projectDetails?: ProjectDetails;
  loading: 'determinate' | 'indeterminate' | 'buffer' | 'query' = 'query';

  // TODO move min max to some config file
  private min = 3;
  private max = 50;
  projectNameFormControl = new FormControl('',[
    Validators.required,
    Validators.minLength(this.min),
    Validators.maxLength(this.max),
    whitespaceValidator])
  isActiveToggleButton = new FormControl(true);

  constructor(
    private readonly projectService: ProjectsService,
    private readonly snackBar: MatSnackBar,
  ) {  }

  @Input()
  get projectId(): string | undefined {
    return this._projectId;
  }

  set projectId(id: string | undefined) {
    this._projectId = id;
    this.projectDetails$ = this.projectService.getProject(this._projectId!)
      .pipe(
        catchError((err, _) => {
          console.log(err);
          this.snackBar.open('Failed to load project', 'Close', {
            duration: 3000
          });
          return EMPTY;
        }),
        tap(r => {
          this.projectDetails = r;
          this.resetForm();
          this.loading = 'determinate';
        })
      );
  }

  resetForm() {
    this.projectNameFormControl.setValue(this.projectDetails!.name);
    this.isActiveToggleButton.setValue(this.projectDetails!.isActive);
  }

  readyToSubmit() {
    return this.dirtyForm() && this.projectNameFormControl.valid;
  }

  dirtyForm() {
    return this.projectDetails!.name !== this.projectNameFormControl.value
      || this.projectDetails!.isActive !== this.isActiveToggleButton.value
  }

  editProject() {
    // TODO send a request with new form values
  }
}
