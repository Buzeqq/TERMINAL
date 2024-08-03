import { Component, inject, Input, OnInit } from '@angular/core';
import { AsyncPipe, DatePipe, JsonPipe } from "@angular/common";
import { SampleDetails } from "../../samples/sample.model";
import { combineLatestWith, map, Observable } from "rxjs";
import { BreakpointObserver } from "@angular/cdk/layout";
import { SampleDetailsDialogComponent } from "./sample-details-dialog/sample-details-dialog.component";
import { MatIcon } from "@angular/material/icon";
import { MatDialog } from "@angular/material/dialog";
import { MatChip, MatChipSet } from "@angular/material/chips";
import { HintComponent } from "../hint/hint.component";
import { MatButton } from "@angular/material/button";
import { MatTab, MatTabGroup } from "@angular/material/tabs";
import { MatList, MatListItem } from "@angular/material/list";

@Component({
  selector: 'app-sample-details',
  standalone: true,
  imports: [
    JsonPipe,
    AsyncPipe,
    MatIcon,
    DatePipe,
    MatChipSet,
    MatChip,
    HintComponent,
    MatButton,
    MatTabGroup,
    MatTab,
    MatList,
    MatListItem
  ],
  templateUrl: './sample-details.component.html',
  styleUrl: './sample-details.component.scss'
})
export class SampleDetailsComponent implements OnInit {
  @Input({ required: true, alias: 'sample' }) inputSample$: Observable<SampleDetails | undefined> = new Observable<SampleDetails | undefined>();

  private readonly breakpointObserver = inject(BreakpointObserver);
  private readonly dialog = inject(MatDialog);

  readonly shouldOpenAsDialog$ = this.breakpointObserver.observe('(max-width: 760px)')
    .pipe(
      map(result => result.matches)
    );

  componentData$: Observable<{ sample: SampleDetails | undefined, shouldOpenAsDialog: boolean }> = new Observable()

  ngOnInit() {
    this.componentData$ = this.inputSample$.pipe(
      combineLatestWith(this.shouldOpenAsDialog$),
      map(([sample, shouldOpenAsDialog]) => {
        if (shouldOpenAsDialog && sample) {
          this.dialog.open(SampleDetailsDialogComponent, {
            data: { sample }
          });
        }

        return { sample, shouldOpenAsDialog };
      })
    );
  }
}
