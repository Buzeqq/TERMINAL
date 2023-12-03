import { Component } from '@angular/core';
import { Observable, tap } from "rxjs";
import { SamplesService } from "../../core/services/samples/samples.service";
import { Sample } from "../../core/models/samples/sample";
import { SelectedItem } from "../../core/models/items/selected-item";
import { BreakpointObserver } from '@angular/cdk/layout';
import { Router } from '@angular/router';

@Component({
  selector: 'app-dashboard',
  templateUrl: './dashboard.component.html',
  styleUrls: ['./dashboard.component.scss']
})
export class DashboardComponent {
  displayedColumns: string[] = ['code', 'project', 'created'];
  selectedSample?: SelectedItem;
  recentSamples$: Observable<Sample[]> = this.samplesService.getRecentSamples(10)
    .pipe(tap(r => this.selectedSample = {type: 'Sample', id: r[0].id, config: {init: true}}));

  constructor(
    private readonly samplesService: SamplesService,
    private readonly breakpointObserver: BreakpointObserver,
    private readonly router: Router
    ) {
  }

  selectSample(m: Sample) {
    this.selectedSample = {type: 'Sample', id: m.id};

    this.breakpointObserver.observe('(max-width: 768px)').subscribe(result => {
      if (result.matches) {
        this.router.navigate(['samples', m.id]);
      }
    });
  }
}
