import {Component, Input} from '@angular/core';
import {catchError, EMPTY, Observable, tap} from "rxjs";
import {Tag} from "../../../core/models/tags/tag";
import {TagDetails} from "../../../core/models/tags/tag-details";
import {FormControl, Validators} from "@angular/forms";
import {whitespaceValidator} from "../../../core/components/validators/whitespaceValidator";
import {TagsService} from "../../../core/services/tags/tags.service";
import {DeleteDialogComponent} from "../../../core/components/dialogs/delete-dialog/delete-dialog.component";
import {MatDialog} from "@angular/material/dialog";
import {NotificationService} from "../../../core/services/notification/notification.service";

@Component({
  selector: 'app-tag-edit',
  templateUrl: './tag-edit.component.html',
  styleUrls: ['./tag-edit.component.scss']
})
export class TagEditComponent {
  private _tagId?: string;
  tagDetails$: Observable<Tag> = new Observable<Tag>();
  private tagDetails?: TagDetails;
  loading: 'determinate' | 'indeterminate' | 'buffer' | 'query' = 'query';

  // TODO move min max to some config file
  private min = 3;
  private max = 50;
  tagNameFormControl = new FormControl('',[
    Validators.required,
    Validators.minLength(this.min),
    Validators.maxLength(this.max),
    whitespaceValidator])
  isActiveToggleButton = new FormControl(true);

  constructor(
    private readonly tagsService: TagsService,
    private readonly notificationService: NotificationService,
    private readonly dialog: MatDialog,
  ) {  }

  @Input()
  get tagId(): string | undefined {
    return this._tagId;
  }

  set tagId(id: string | undefined) {
    this._tagId = id;
    this.tagDetails$ = this.tagsService.getTag(this._tagId!)
      .pipe(
        catchError((err, _) => {
          console.log(err);
          this.notificationService.notifyError('Failed to load tag');
          return EMPTY;
        }),
        tap(r => {
          this.tagDetails = r;
          this.resetForm();
          this.loading = 'determinate';
        })
      );
  }

  resetForm() {
    this.tagNameFormControl.setValue(this.tagDetails!.name);
    this.isActiveToggleButton.setValue(this.tagDetails!.isActive);
  }

  readyToSubmit() {
    return this.dirtyForm() && this.tagNameFormControl.valid;
  }

  dirtyForm() {
    return this.tagDetails!.name !== this.tagNameFormControl.value
      || this.tagDetails!.isActive !== this.isActiveToggleButton.value
  }

  editTag() {
    // TODO send a request with new form values
  }

  deleteTag() {
    const dialogRef = this.dialog.open(DeleteDialogComponent, {
      data: {
        title: `Delete Tag ${this.tagDetails?.name}`,
        message: 'Attention! This action is irreversible. Deleting a tag removes it from every possible sample.'
      }});
    dialogRef.afterClosed().subscribe(deleteConfirmed => {
      if (deleteConfirmed)
        this.tagsService.deleteTag(this._tagId!, this.tagDetails!.name).subscribe();
    })
  }
}
