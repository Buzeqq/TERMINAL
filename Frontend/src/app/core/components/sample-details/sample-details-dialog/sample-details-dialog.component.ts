import { Component, Inject } from '@angular/core';
import {
  MAT_DIALOG_DATA,
  MatDialogContent,
  MatDialogModule,
  MatDialogTitle,
} from '@angular/material/dialog';
import { DatePipe, JsonPipe } from '@angular/common';
import { SampleDetails } from '../../../samples/sample.model';
import { HintComponent } from '../../hint/hint.component';
import { MatButton } from '@angular/material/button';
import { MatChip, MatChipSet } from '@angular/material/chips';
import { MatIcon } from '@angular/material/icon';
import { MatList, MatListItem } from '@angular/material/list';
import { MatTab, MatTabGroup } from '@angular/material/tabs';

@Component({
  selector: 'app-sample-details-dialog',
  standalone: true,
  imports: [
    MatDialogModule,
    MatDialogContent,
    MatDialogTitle,
    JsonPipe,
    DatePipe,
    HintComponent,
    MatButton,
    MatChip,
    MatChipSet,
    MatIcon,
    MatList,
    MatListItem,
    MatTab,
    MatTabGroup,
  ],
  templateUrl: './sample-details-dialog.component.html',
  styleUrl: './sample-details-dialog.component.scss',
})
export class SampleDetailsDialogComponent {
  constructor(
    @Inject(MAT_DIALOG_DATA) public data: { sample: SampleDetails },
  ) {}
}
