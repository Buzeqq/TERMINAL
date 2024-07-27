import { Actions, createEffect, ofType } from "@ngrx/effects";
import { inject } from "@angular/core";
import { IdentityService } from "../services/identity.service";
import { IdentityActions } from "./identity.actions";
import { map, switchMap } from "rxjs";

export const userLoggedInEffect = createEffect(
  (actions$ = inject(Actions), identityService = inject(IdentityService)) => {
    return actions$.pipe(
      ofType(IdentityActions.userLoggedIn),
      switchMap(() => identityService.getUserInfo()),
      map((identity) => IdentityActions.userLoaded({ identity })),
    );
  },
  { functional: true }
);

export const userLoggedOutEffect = createEffect(
  (actions$ = inject(Actions), identityService = inject(IdentityService)) => {
    return actions$.pipe(
      ofType(IdentityActions.tryToLogOut),
      switchMap(() => identityService.logOut()),
      map(() => IdentityActions.userLoggedOut())
    );
  },
  { functional: true }
);
