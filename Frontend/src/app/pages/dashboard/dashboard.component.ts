import { Component } from '@angular/core';
import { Observable, tap } from "rxjs";
import { SamplesService } from "../../core/services/samples/samples.service";
import { Sample } from "../../core/models/samples/sample";
import { SelectedItem } from "../../core/models/items/selected-item";

@Component({
  selector: 'app-dashboard',
  templateUrl: './dashboard.component.html',
  styleUrls: ['./dashboard.component.scss']
})
export class DashboardComponent {
  displayedColumns: string[] = ['code', 'project', 'created'];
  selectedSample?: SelectedItem;
  recentSamples$: Observable<Sample[]> = this.samplesService.getRecentSamples(10)
    .pipe(tap(r => this.selectedSample = {type: 'Sample', id: r[0].id}));

  constructor(private readonly samplesService: SamplesService) {
  }

  selectSample(m: Sample) {
    this.selectedSample = {type: 'Sample', id: m.id};
  }
}
