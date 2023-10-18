import { Component } from '@angular/core';
import { RecentMeasurement } from "../core/models/measurements/recentMeasurement";

@Component({
  selector: 'app-dashboard',
  templateUrl: './dashboard.component.html',
  styleUrls: ['./dashboard.component.scss']
})
export class DashboardComponent {
  recentMeasurement: RecentMeasurement[];
  searchExpression?: string;
  displayedColumns: string[] = ['code', 'project', 'created'];

  constructor() {
    this.recentMeasurement = [];
    const currentDate = Date.now();

    for (let i = 1; i <= 10; i++) {
      const olderDate = new Date(currentDate);
      olderDate.setMinutes(olderDate.getMinutes() - 5 * i);
      this.recentMeasurement.push({ code: `AX-12${i}`, project: `Project${i}`, created: olderDate });
    }
  }
}
