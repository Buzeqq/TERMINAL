import { Component } from '@angular/core';
import { BreakpointObserver } from "@angular/cdk/layout";
import { map, Observable, tap } from "rxjs";
import { MeasurementsService } from "../core/services/measurements/measurements.service";
import { RecentMeasurement } from "../core/models/measurements/recentMeasurement";

@Component({
  selector: 'app-dashboard',
  templateUrl: './dashboard.component.html',
  styleUrls: ['./dashboard.component.scss']
})
export class DashboardComponent {
  displayedColumns: string[] = ['code', 'project', 'created'];
  showDetails$: Observable<boolean> = this.breakpointObserver.observe('(max-width: 768px)').pipe(map(result => !result.matches));
  selectedMeasurementId?: string;
  recentMeasurement$: Observable<RecentMeasurement[]> = this.measurementService.getRecentMeasurements(10);

  constructor(private readonly breakpointObserver: BreakpointObserver, private readonly measurementService: MeasurementsService) {
  }
}
