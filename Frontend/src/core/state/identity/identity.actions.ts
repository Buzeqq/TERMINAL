import { createActionGroup, emptyProps, props } from "@ngrx/store";
import { Identity, LoginForm } from "../../identity/identity.model";

export const IdentityActions = createActionGroup({
  source: 'Identity actions',
  events: {
    'Try to log in': props<{ form: LoginForm }>(),
    'Try to log out': emptyProps(),
    'User logged in': emptyProps(),
    'User logged out': emptyProps(),
    'Failed to log in': emptyProps(),
    'User loaded': props<{ identity: Identity }>(),
    'Failed to load user': emptyProps()
  }
});
