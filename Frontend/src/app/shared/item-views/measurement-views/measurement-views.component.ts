import { Component, Output, EventEmitter } from '@angular/core';
import { Observable, tap } from 'rxjs';
import { Measurement } from 'src/app/core/models/measurements/recentMeasurement';
import { MeasurementsService } from 'src/app/core/services/measurements/measurements.service';
import { ItemViewsComponent } from '../item-views.component';

@Component({
  selector: 'app-measurement-views',
  templateUrl: './measurement-views.component.html',
  styleUrls: ['./measurement-views.component.scss']
})
export class MeasurementViewsComponent {
  displayedColumns: string[] = ['code', 'project', 'created'];
  selectedItemId: string | undefined;

  measurements$: Observable<Measurement[]> = this.measurementService.getAllMeasurements()
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
