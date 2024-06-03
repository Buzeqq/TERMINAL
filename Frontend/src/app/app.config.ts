import { ApplicationConfig, isDevMode } from '@angular/core';
import { provideRouter } from '@angular/router';

import { routes } from './app.routes';
import { provideState, provideStore } from '@ngrx/store';
import { provideAnimationsAsync } from '@angular/platform-browser/animations/async';
import { identityReducer, identityFeatureKey } from "../core/state/identity/identity.reducer";
import { provideEffects } from '@ngrx/effects';
import { provideStoreDevtools } from "@ngrx/store-devtools";
import { userLoggedInEffect } from "../core/state/identity/identity.effect";
import { provideHttpClient, withFetch, withInterceptors } from "@angular/common/http";
import { credentialsInterceptor } from "../core/interceptors/credentials.interceptor";
import { provideComponentStore } from "@ngrx/component-store";
import { LoginStore } from "../pages/login/login.store";

export const appConfig: ApplicationConfig = {
  providers: [
    provideRouter(routes),
    provideStore(),
    provideComponentStore(LoginStore),
    provideStoreDevtools({
      maxAge: 25,
      logOnly: !isDevMode(),
      autoPause: true,
      trace: false,
      traceLimit: 75,
      connectInZone: true
    }),
    provideState({
        name: identityFeatureKey, reducer: identityReducer
    }),
    provideAnimationsAsync(),
    provideEffects({
      userLoggedInEffect,
    }),
    provideHttpClient(
      withFetch(),
      withInterceptors([credentialsInterceptor]))
  ]
};
