import { Component, computed, inject, OnInit, Signal } from '@angular/core';
import {
  MatCard,
  MatCardFooter,
  MatCardHeader,
  MatCardTitle,
} from '@angular/material/card';
import { BasePageComponent } from '../../core/components/base-page/base-page.component';
import { MatIcon } from '@angular/material/icon';
import {
  MatCell,
  MatCellDef,
  MatColumnDef,
  MatHeaderCell,
  MatHeaderCellDef,
  MatHeaderRow,
  MatHeaderRowDef,
  MatRow,
  MatRowDef,
  MatTable,
} from '@angular/material/table';
import { TimeAgoPipe } from '../../core/pipes/time-ago.pipe';
import { MatRipple } from '@angular/material/core';
import { DashboardStore } from './dashboard.store';
import { MatButton } from '@angular/material/button';
import { Sample } from '../../core/samples/sample.model';
import { Subject } from 'rxjs';
import { MatProgressBar } from '@angular/material/progress-bar';
import { BasePageHeaderComponent } from '../../core/components/base-page/base-page-header/base-page-header.component';
import { BasePageContentComponent } from '../../core/components/base-page/base-page-content/base-page-content.component';
import { BasePageFooterComponent } from '../../core/components/base-page/base-page-footer/base-page-footer.component';
import { SampleDetailsComponent } from '../../core/components/sample-details/sample-details.component';
import { AsyncPipe } from '@angular/common';
import { HintComponent } from '../../core/components/hint/hint.component';

@Component({
  selector: 'app-dashboard',
  standalone: true,
  imports: [
    MatCard,
    MatCardTitle,
    BasePageComponent,
    BasePageHeaderComponent,
    BasePageContentComponent,
    BasePageFooterComponent,
    MatCardHeader,
    MatIcon,
    MatTable,
    MatHeaderCellDef,
    MatHeaderCell,
    MatColumnDef,
    MatCell,
    MatCellDef,
    TimeAgoPipe,
    MatHeaderRow,
    MatHeaderRowDef,
    MatRow,
    MatRipple,
    MatRowDef,
    MatButton,
    MatCardFooter,
    MatProgressBar,
    SampleDetailsComponent,
    AsyncPipe,
    HintComponent,
  ],
  templateUrl: './dashboard.component.html',
  styleUrl: './dashboard.component.scss',
})
export class DashboardComponent implements OnInit {
  readonly displayedColumns = ['code', 'project', 'created'];
  readonly selectedSample$ = new Subject<Sample>();
  private readonly dashboardStore = inject(DashboardStore);
  readonly recentSamples = this.dashboardStore.selectSignal(
    (state) => state.recentSamples,
  );
  readonly areThereAnyRecentSamples: Signal<boolean> = computed(
    () => this.recentSamples().length > 0,
  );
  readonly isLoading = this.dashboardStore.selectSignal(
    (state) => state.isLoading,
  );
  readonly sampleDetails$ = this.dashboardStore.select(
    (state) => state.selectedSample,
  );

  ngOnInit() {
    if (!this.areThereAnyRecentSamples()) {
      this.dashboardStore.patchState({ isLoading: true });
    }
    this.dashboardStore.loadRecentSamples();
    this.dashboardStore.selectSample(this.selectedSample$);
  }
}
