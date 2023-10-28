import { Component } from '@angular/core';
import { Observable, tap } from 'rxjs';
import { Measurement } from 'src/app/core/models/measurements/recentMeasurement';
import { MeasurementsService } from 'src/app/core/services/measurements/measurements.service';

@Component({
  selector: 'app-measurement-views',
  templateUrl: './measurement-views.component.html',
  styleUrls: ['./measurement-views.component.scss']
})
export class MeasurementViewsComponent {
  displayedColumns: string[] = ['code', 'project', 'created'];
  selectedMeasurementId?: string;
  
  measurements$: Observable<Measurement[]> = this.measurementService.getAllMeasurements()
  .pipe(tap(r => this.selectedMeasurementId = r[0].id));

  constructor(private readonly measurementService: MeasurementsService) {
  }

  selectMeasurement(row: Measurement) {
    this.selectedMeasurementId = row.id;
  }
}
