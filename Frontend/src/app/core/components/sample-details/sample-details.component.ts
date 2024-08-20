import { Component, Input, OnInit } from '@angular/core';
import { AsyncPipe, DatePipe, JsonPipe } from '@angular/common';
import { SampleDetails } from '../../samples/sample.model';
import {
  catchError,
  EMPTY,
  filter,
  map,
  merge,
  Observable,
  switchMap,
} from 'rxjs';
import { MatIcon } from '@angular/material/icon';
import { MatChip, MatChipSet } from '@angular/material/chips';
import { HintComponent } from '../hint/hint.component';
import { MatButton } from '@angular/material/button';
import { MatTab, MatTabGroup } from '@angular/material/tabs';
import { MatList, MatListItem } from '@angular/material/list';
import { ActivatedRoute } from '@angular/router';
import { SamplesService } from '../../samples/samples.service';
import { NotificationService } from '../../services/notification.service';
import { FailedToLoadSampleDetailsError } from '../../errors/errors.model';

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
    MatListItem,
  ],
  templateUrl: './sample-details.component.html',
  styleUrl: './sample-details.component.scss',
})
export class SampleDetailsComponent implements OnInit {
  @Input({ alias: 'sample' }) sampleInput$: Observable<
    SampleDetails | undefined
  > = new Observable<SampleDetails | undefined>();

  protected sampleDetails$ = new Observable<SampleDetails>();

  constructor(
    private readonly route: ActivatedRoute,
    private readonly sampleService: SamplesService,
    private readonly notificationService: NotificationService,
  ) {}

  ngOnInit() {
    this.sampleDetails$ = merge(
      this.sampleInput$.pipe(filter((sample) => !!sample)),
      this.route.params.pipe(
        map((params) => params['id']),
        filter((id) => !!id),
        switchMap((id) => this.sampleService.getSampleDetails(id)),
      ),
    ).pipe(
      catchError((err: FailedToLoadSampleDetailsError) => {
        this.notificationService.notifyError(err.detail ?? err.title);
        return EMPTY;
      }),
    );
  }
}
