import { Component, Output, EventEmitter } from '@angular/core';
import { Observable, tap } from 'rxjs';
import { Measurement } from 'src/app/core/models/measurements/measurement';
import { MeasurementsService } from 'src/app/core/services/measurements/measurements.service';

@Component({
  selector: 'app-measurement-views',
  templateUrl: './measurement-views.component.html',
  styleUrls: ['./measurement-views.component.scss']
})
export class MeasurementViewsComponent {
  displayedColumns: string[] = ['code', 'project', 'created'];
  selectedItemId: string | undefined;

  measurements$: Observable<Measurement[]> = this.measurementService.getMeasurements(0, 10)
    .pipe(tap(r => {
      this.selectedItemIdChangeEvent.emit(r[0].id);
      this.selectedItemId = r[0].id;
    }));

  constructor(
    private readonly measurementService: MeasurementsService,
  ) {
  }

  @Output() selectedItemIdChangeEvent = new EventEmitter<string>();

  selectMeasurement(row: Measurement) {
    this.selectedItemIdChangeEvent.emit(row.id);
    this.selectedItemId = row.id;
  }
}
