import { Actions, createEffect, ofType } from "@ngrx/effects";
import { inject } from "@angular/core";
import { IdentityService } from "../../services/identity/identity.service";
import { IdentityActions } from "./identityActions";
import { catchError, exhaustMap, map, of, switchMap } from "rxjs";

export const loginEffect = createEffect(
  (actions$ = inject(Actions), identityService = inject(IdentityService)) => {
    return actions$.pipe(
      ofType(IdentityActions.tryToLogIn),
      exhaustMap((props) =>
        identityService.login(props.form).pipe(
          switchMap(() => identityService.getUserInfo()),
          map((result) => IdentityActions.userLoggedIn({ identity: result })),
          catchError(() => of(IdentityActions.failedToLogIn()))
        ))
    );
  },
  { functional: true }
);
