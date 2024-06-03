import { Actions, createEffect, ofType } from "@ngrx/effects";
import { inject } from "@angular/core";
import { IdentityService } from "../../services/identity/identity.service";
import { IdentityActions } from "./identityActions";
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
