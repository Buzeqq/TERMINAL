import { Component, inject, OnInit } from '@angular/core';
import { MatCard, MatCardHeader, MatCardTitle } from "@angular/material/card";
import {BasePageComponent} from "../../core/components/base-page/base-page.component";
import { MatIcon } from "@angular/material/icon";
import {
  MatCell,
  MatCellDef,
  MatColumnDef,
  MatHeaderCell,
  MatHeaderCellDef,
  MatHeaderRow, MatHeaderRowDef, MatRow, MatRowDef,
  MatTable
} from "@angular/material/table";
import { TimeAgoPipe } from "../../core/pipes/time-ago.pipe";
import { MatRipple } from "@angular/material/core";
import { DashboardStore } from "./dashboard.store";
import { MatButton } from "@angular/material/button";

@Component({
  selector: 'app-dashboard',
  standalone: true,
  imports: [
    MatCard,
    MatCardTitle,
    BasePageComponent,
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
    MatButton
  ],
  templateUrl: './dashboard.component.html',
  styleUrl: './dashboard.component.scss'
})
export class DashboardComponent implements OnInit {
  readonly displayedColumns = ['code', 'project', 'created'];
  private readonly dashboardStore = inject(DashboardStore);
  readonly recentSamples = this.dashboardStore.selectSignal(state => state.recentSamples);

  ngOnInit() {
    this.dashboardStore.loadRecentSamples();
  }

  showSampleDetails(row: unknown) {
    console.log(row);
  }
}
