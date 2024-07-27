import { inject, Injectable } from "@angular/core";
import { ComponentStore } from "@ngrx/component-store";
import { catchError, EMPTY, Observable, switchMap, tap } from "rxjs";
import { LoginForm } from "../../core/identity/identity.model";
import { IdentityService } from "../../core/identity/services/identity.service";
import { Store } from "@ngrx/store";
import { IdentityActions } from "../../core/identity/state/identity.actions";
import { TerminalError } from "../../core/errors/errors";
import { NotificationService } from "../../core/services/notification.service";

export interface LoginState {
  isLoading: boolean;
}

@Injectable()
export class LoginStore extends ComponentStore<LoginState> {
  constructor() {
    super({
      isLoading: false
    });
  }

  private readonly service = inject(IdentityService);
  private readonly store = inject(Store);
  private readonly notificationService = inject(NotificationService);

  readonly tryToLogIn = this.effect((loginForm$: Observable<LoginForm>) => {
    return loginForm$.pipe(
      tap(() => this.patchState({ isLoading: true })),
      switchMap((form) => this.service.login(form).pipe(
        tap(() => {
          this.patchState({ isLoading: false });
          this.store.dispatch(IdentityActions.userLoggedIn());
        })
      )),
      catchError((err: TerminalError) => {
        this.patchState({ isLoading: false });
        this.notificationService.notifyError(err.detail ?? err.title);
        this.store.dispatch(IdentityActions.failedToLogIn());
        return EMPTY;
      })
    );
  })

  readonly tryToLoadUser = this.effect(() => {
    this.patchState({ isLoading: true });

    return this.service.getUserInfo().pipe(
      tap((identity) => {
        this.patchState({ isLoading: false });
        this.store.dispatch(IdentityActions.userLoaded({ identity }));
      }),
      catchError((_: TerminalError) => {
        this.patchState({ isLoading: false });
        this.store.dispatch(IdentityActions.failedToLoadUser());
        return EMPTY;
      })
    );
  })
}
