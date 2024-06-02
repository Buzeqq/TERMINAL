import { createActionGroup, emptyProps, props } from "@ngrx/store";
import { Identity, LoginForm } from "../../identity/identity.model";

export const IdentityActions = createActionGroup({
  source: 'Identity actions',
  events: {
    'Try to log in': props<{ form: LoginForm }>(),
    'User logged in': props<{ identity: Identity }>(),
    'User logged out': emptyProps(),
    'Failed to log in': emptyProps()
  }
});
