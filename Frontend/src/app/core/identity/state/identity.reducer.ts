import { createReducer, on } from "@ngrx/store";

import { IdentityActions } from "./identity.actions";
import { Identity } from "../identity.model";

export const identityFeatureKey = 'Identity';

export const initialState: Readonly<Identity> = {
  isAuthenticated: false,
  email: null,
  role: 'guest'
};

export const identityReducer = createReducer(
  initialState,
  on(IdentityActions.userLoaded, (state, { identity }): Identity => {
    if (state.isAuthenticated) return state;

    return identity;
  }),
  on(IdentityActions.userLoggedOut, (_): Identity => ({
    email: null,
    isAuthenticated: false,
    role: 'guest'
  }))
);
