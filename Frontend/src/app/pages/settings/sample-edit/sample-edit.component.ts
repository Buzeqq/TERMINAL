import {Component, Input} from '@angular/core';
import {
  catchError,
  EMPTY,
  Observable,
  tap
} from "rxjs";
import {SamplesService} from "../../../core/services/samples/samples.service";
import {NotificationService} from "../../../core/services/notification/notification.service";
import {SampleDetails} from "../../../core/models/samples/sampleDetails";
import {DeleteDialogComponent} from "../../../core/components/dialogs/delete-dialog/delete-dialog.component";
import {MatDialog} from "@angular/material/dialog";

@Component({
  selector: 'app-sample-edit',
  templateUrl: './sample-edit.component.html',
  styleUrls: ['./sample-edit.component.scss']
})
export class SampleEditComponent {
  loading: 'determinate' | 'indeterminate' | 'buffer' | 'query' = 'query';

  _sampleId?: string;
  sampleDetails$: Observable<SampleDetails> = new Observable<SampleDetails>();
  sampleDetails?: SampleDetails;

  constructor (
    private readonly samplesService: SamplesService,
    private readonly notificationService: NotificationService,
    private readonly dialog: MatDialog,
  ) {  }


  @Input()
  get sampleId(): string | undefined {
    return this._sampleId;
  }

  set sampleId(id: string | undefined) {
    this._sampleId = id;
    this.sampleDetails$ = this.samplesService.getSampleDetails(id!)
      .pipe(
        catchError((err, _) => {
          console.log(err);
          this.notificationService.notifyError('Failed to load sample');
          return EMPTY;
        }),
        tap(sample => {
          this.sampleDetails = sample;
          this.loading = 'determinate';
        })
      );
  }

  deleteSample() {
    const dialogRef = this.dialog.open(DeleteDialogComponent, {
      data: {
        title: `Delete Sample ${this.sampleDetails?.code}`,
        message: 'Attention! This action is irreversible.'
      }});
    dialogRef.afterClosed().subscribe(deleteConfirmed => {
      if (deleteConfirmed)
        this.samplesService.deleteSample(this._sampleId!, this.sampleDetails!.code).subscribe();
    })
  }
}
