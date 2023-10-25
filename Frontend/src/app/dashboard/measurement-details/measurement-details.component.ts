import { Component, Input } from '@angular/core';
import { Observable, tap } from 'rxjs';
import { MeasurementDetails } from 'src/app/core/models/measurements/measurementDetails';
import { MeasurementsService } from 'src/app/core/services/measurements/measurements.service';

@Component({
  selector: 'app-measurement-details',
  templateUrl: './measurement-details.component.html',
  styleUrls: ['./measurement-details.component.scss']
})
export class MeasurementDetailsComponent {
  constructor(private readonly measurementService: MeasurementsService) {
  }

  @Input()
  get measurementId(): string | undefined {
    return this._measurementId;
  }

  set measurementId(id: string | undefined) {
    this._measurementId = id;
    this.measurementDetails$ = this.measurementService.getMeasurementDetails(id!)
      .pipe(tap(_ => this.loaded()));
  }

  private _measurementId?: string;
  measurementDetails$?: Observable<MeasurementDetails>;

  loading: 'determinate' | 'indeterminate' | 'buffer' | 'query' = 'query';
  loaded(): void {
    this.loading = 'determinate';
  }
}
