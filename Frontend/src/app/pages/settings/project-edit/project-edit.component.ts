import {Component, Input} from '@angular/core';
import {catchError, EMPTY, Observable, tap} from "rxjs";
import {FormControl, Validators} from "@angular/forms";
import {whitespaceValidator} from "../../../core/components/validators/whitespaceValidator";
import {Project} from "../../../core/models/projects/project";
import {ProjectDetails} from "../../../core/models/projects/project-details";
import {ProjectsService} from "../../../core/services/projects/projects.service";
import {MatDialog} from "@angular/material/dialog";
import {NotificationService} from "../../../core/services/notification/notification.service";
import {DeleteDialogComponent} from "../../../core/components/dialogs/delete-dialog/delete-dialog.component";

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
    private readonly notificationService: NotificationService,
    private readonly dialog: MatDialog,
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
          this.notificationService.notifyError('Failed to load project');
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

  deleteProject() {
    const dialogRef = this.dialog.open(DeleteDialogComponent, {
      data: {
        title: `Delete project ${this.projectDetails?.name}`,
        message: 'Attention! This action is irreversible. Deleting a project also deletes all included samples.'
      }});
    dialogRef.afterClosed().subscribe(deleteConfirmed => {
      if (deleteConfirmed)
        this.projectService.deleteProject(this._projectId!, this.projectDetails!.name).subscribe();
    })
  }
}
