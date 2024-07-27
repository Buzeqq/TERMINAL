import { ComponentStore } from "@ngrx/component-store";
import { Sample } from "../../core/samples/sample.model";
import { inject, Injectable } from "@angular/core";
import { SamplesService } from "../../core/samples/samples.service";
import { catchError, EMPTY, tap } from "rxjs";
import { FailedToLoadSamplesError } from "../../core/errors/errors";
import { NotificationService } from "../../core/services/notification.service";

export interface DashboardState {
  isLoading: boolean;
  recentSamples: Sample[];
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
    this.patchState({ isLoading: true });

    return this.sampleService.getRecentSamples(10).pipe(
      tap(recentSamples => {
        this.patchState({ recentSamples, isLoading: false });
      }),
      catchError((err: FailedToLoadSamplesError) => {
        this.notificationService.notifyError(err.detail ?? err.title);
        return EMPTY;
      })
    );
  });
}
