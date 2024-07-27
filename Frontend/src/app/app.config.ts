import { ApplicationConfig, isDevMode } from '@angular/core';
import { provideRouter } from '@angular/router';

import { routes } from './app.routes';
import { provideState, provideStore } from '@ngrx/store';
import { provideAnimationsAsync } from '@angular/platform-browser/animations/async';
import { provideEffects } from '@ngrx/effects';
import { provideStoreDevtools } from "@ngrx/store-devtools";
import { provideHttpClient, withFetch, withInterceptors } from "@angular/common/http";
import { provideComponentStore } from "@ngrx/component-store";
import { LoginStore } from "./pages/login/login.store";
import { identityFeatureKey, identityReducer } from "./core/identity/state/identity.reducer";
import { userLoggedInEffect, userLoggedOutEffect } from "./core/identity/state/identity.effect";
import { credentialsInterceptor } from "./core/interceptors/credentials.interceptor";
import { globalErrorHandlerInterceptor } from "./core/interceptors/global-error-handler.interceptor";

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
      userLoggedOutEffect
    }),
    provideHttpClient(
      withFetch(),
      withInterceptors([credentialsInterceptor, globalErrorHandlerInterceptor]))
  ]
};
