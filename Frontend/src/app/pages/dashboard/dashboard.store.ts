import { ComponentStore } from "@ngrx/component-store";
import { Sample, SampleDetails } from "../../core/samples/sample.model";
import { inject, Injectable } from "@angular/core";
import { SamplesService } from "../../core/samples/samples.service";
import { catchError, combineLatestWith, distinctUntilChanged, EMPTY, map, Observable, switchMap, tap } from "rxjs";
import { FailedToLoadSampleDetailsError, FailedToLoadSamplesError } from "../../core/errors/errors";
import { NotificationService } from "../../core/services/notification.service";
import { BreakpointObserver } from "@angular/cdk/layout";
import { Router } from "@angular/router";

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
      recentSamples: [],
    });
  }

  private readonly sampleService = inject(SamplesService);
  private readonly notificationService = inject(NotificationService);
  private readonly router = inject(Router);
  private readonly shouldNavigateToSampleDetails$ = inject(BreakpointObserver)
    .observe('(max-width: 760px)')
    .pipe(
      map(result => result.matches)
    );

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
          combineLatestWith(this.shouldNavigateToSampleDetails$),
          distinctUntilChanged(),
          map(([sample, shouldNavigateToSampleDetails]) => {
            if (shouldNavigateToSampleDetails) {
              this.patchState({ selectedSample: undefined });
              this.router.navigate(['/samples', sample.id])
                .catch(() => this.notificationService.notifyError('Failed to navigate to sample details page'));
            }

            return sample;
          }),
          tap(selectedSample => {
            this.patchState({ isLoading: false, selectedSample });
          }),
          catchError((err: FailedToLoadSampleDetailsError) => {
            this.patchState({ isLoading: false });
            this.notificationService.notifyError(err.detail ?? err.title);
            return EMPTY;
          })
        ))
    );
  });
}
