import { Component } from '@angular/core';
import { Observable, tap } from "rxjs";
import { MeasurementsService } from "../core/services/measurements/measurements.service";
import { Measurement } from "../core/models/measurements/measurement";
import { FiltersState } from "../core/types/types";
import { Router } from "@angular/router";

@Component({
  selector: 'app-dashboard',
  templateUrl: './dashboard.component.html',
  styleUrls: ['./dashboard.component.scss']
})
export class DashboardComponent {
  displayedColumns: string[] = ['code', 'project', 'created'];
  selectedMeasurementId?: string;
  recentMeasurement$: Observable<Measurement[]> = this.measurementService.getRecentMeasurements(10)
    .pipe(tap(r => this.selectedMeasurementId = r[0].id));

  constructor(private readonly measurementService: MeasurementsService,
              private readonly router: Router) {
  }

  selectMeasurement(row: Measurement) {
    this.selectedMeasurementId = row.id;
  }

  async onSearchClick(event: { searchPhrase: string; filtersState: FiltersState }) {
    const { filtersState, searchPhrase} = event;
    await this.router.navigate(['/search'], {
      queryParams: {
        searchPhrase,
        ...filtersState
      }
    });
  }
}
