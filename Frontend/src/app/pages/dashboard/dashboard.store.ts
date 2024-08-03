import { ComponentStore } from "@ngrx/component-store";
import { Sample, SampleDetails } from "../../core/samples/sample.model";
import { inject, Injectable } from "@angular/core";
import { SamplesService } from "../../core/samples/samples.service";
import { catchError, EMPTY, Observable, switchMap, tap } from "rxjs";
import { FailedToLoadSampleDetailsError, FailedToLoadSamplesError } from "../../core/errors/errors";
import { NotificationService } from "../../core/services/notification.service";

export interface DashboardState {
  isLoading: boolean;
  recentSamples: Sample[];
  selectedSample?: SampleDetails;
}

@Injectable()
export class DashboardStore extends ComponentStore<DashboardState> {
  constructor() {
    super({
      isLoading: false,
      recentSamples: []
    });
  }

  private readonly sampleService = inject(SamplesService);
  private readonly notificationService = inject(NotificationService);

  readonly loadRecentSamples = this.effect(() => {
    return this.sampleService.getRecentSamples(10).pipe(
      tap(recentSamples => {
        this.patchState({ recentSamples, isLoading: false });
      }),
      catchError((err: FailedToLoadSamplesError) => {
        this.patchState({ isLoading: false });
        this.notificationService.notifyError(err.detail ?? err.title);
        return EMPTY;
      })
    );
  });

  readonly selectSample = this.effect((sample: Observable<Sample>) => {
    return sample.pipe(
      tap(() => this.patchState({ isLoading: true })),
      switchMap(sample => this.sampleService.getSampleDetails(sample.id)
        .pipe(
          tap(selectedSample => {
            this.patchState({ isLoading: false, selectedSample });
          }),
          catchError((err: FailedToLoadSampleDetailsError) => {
            this.patchState({ isLoading: false });
            this.notificationService.notifyError(err.detail ?? err.title);
            return EMPTY;
          })))
    );
  });
}
