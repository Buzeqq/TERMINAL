import { Component } from '@angular/core';
import { BreakpointObserver } from "@angular/cdk/layout";
import { Observable, tap } from "rxjs";
import { MeasurementsService } from "../core/services/measurements/measurements.service";
import { RecentMeasurement } from "../core/models/measurements/recentMeasurement";

@Component({
  selector: 'app-dashboard',
  templateUrl: './dashboard.component.html',
  styleUrls: ['./dashboard.component.scss']
})
export class DashboardComponent {
  displayedColumns: string[] = ['code', 'project', 'created'];
  selectedMeasurementId?: string;
  recentMeasurement$: Observable<RecentMeasurement[]> = this.measurementService.getRecentMeasurements(10)
    .pipe(tap(r => this.selectedMeasurementId = r[0].id));

  constructor(private readonly breakpointObserver: BreakpointObserver, private readonly measurementService: MeasurementsService) {
  }

  selectMeasurement(row: RecentMeasurement) {
    this.selectedMeasurementId = row.id;
  }
}