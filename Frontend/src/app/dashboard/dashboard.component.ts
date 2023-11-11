import { Component } from '@angular/core';
import { Observable, tap } from "rxjs";
import { MeasurementsService } from "../core/services/measurements/measurements.service";
import { Measurement } from "../core/models/measurements/measurement";
import {SelectedItem} from "../core/models/items/selected-item";

@Component({
  selector: 'app-dashboard',
  templateUrl: './dashboard.component.html',
  styleUrls: ['./dashboard.component.scss']
})
export class DashboardComponent {
  displayedColumns: string[] = ['code', 'project', 'created'];
  selectedMeasurement?: SelectedItem;
  recentMeasurement$: Observable<Measurement[]> = this.measurementService.getRecentMeasurements(10)
    .pipe(tap(r => this.selectedMeasurement = {type: 'Measurement', id: r[0].id}));

  constructor(private readonly measurementService: MeasurementsService) {
  }

  selectMeasurement(m: Measurement) {
    this.selectedMeasurement = {type: 'Measurement', id: m.id};
  }
}
