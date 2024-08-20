import { Actions, createEffect, ofType } from '@ngrx/effects';
import { inject } from '@angular/core';
import { IdentityService } from '../services/identity.service';
import { IdentityActions } from './identity.actions';
import { map, switchMap, tap } from 'rxjs';
import { Router } from '@angular/router';
import { LoginStore } from '../../../pages/login/login.store';

export const userLoggedInEffect = createEffect(
  (
    actions$ = inject(Actions),
    identityService = inject(IdentityService),
    loginStore = inject(LoginStore),
    router = inject(Router),
  ) => {
    return actions$.pipe(
      ofType(IdentityActions.userLoggedIn),
      switchMap(() => identityService.getUserInfo()),
      map((identity) => IdentityActions.userLoaded({ identity })),
      tap(async () => {
        await router.navigate(['/']);
        loginStore.patchState({ isLoading: false });
      }),
    );
  },
  { functional: true },
);

export const userLoggedOutEffect = createEffect(
  (
    actions$ = inject(Actions),
    identityService = inject(IdentityService),
    loginStore = inject(LoginStore),
    router = inject(Router),
  ) => {
    return actions$.pipe(
      ofType(IdentityActions.tryToLogOut),
      switchMap(() => identityService.logOut()),
      map(() => IdentityActions.userLoggedOut()),
      tap(async () => {
        await router.navigate(['/login']);
        loginStore.patchState({ isLoading: false });
      }),
    );
  },
  { functional: true },
);
