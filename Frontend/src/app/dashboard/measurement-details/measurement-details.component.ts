import { Component, Input } from '@angular/core';
import { MatSnackBar } from '@angular/material/snack-bar';
import { EMPTY, Observable, catchError, tap } from 'rxjs';
import { MeasurementDetails } from 'src/app/core/models/measurements/measurementDetails';
import { MeasurementsService } from 'src/app/core/services/measurements/measurements.service';

@Component({
  selector: 'app-measurement-details',
  templateUrl: './measurement-details.component.html',
  styleUrls: ['./measurement-details.component.scss']
})
export class MeasurementDetailsComponent {
  constructor(
    private readonly measurementService: MeasurementsService,
    private readonly snackBar: MatSnackBar) {
  }

  @Input()
  get measurementId(): string | undefined {
    return this._measurementId;
  }

  set measurementId(id: string | undefined) {
    this._measurementId = id;
    this.measurementDetails$ = this.measurementService.getMeasurementDetails(id!)
      .pipe(
        catchError((err, caught) => {
          console.log(err);
          this.snackBar.open('Failed to load measurement', 'Close', {
            duration: 3000
          });
          return EMPTY;
        }),
        tap(_ => {
          this.loading = 'determinate';
        })
      );
  }

  private _measurementId?: string;
  measurementDetails$?: Observable<MeasurementDetails> = undefined;

  loading: 'determinate' | 'indeterminate' | 'buffer' | 'query' = 'query';
}
