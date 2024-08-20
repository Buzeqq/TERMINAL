import { CanActivateFn, Router } from '@angular/router';
import { Store } from '@ngrx/store';
import { inject } from '@angular/core';
import { selectIdentity } from '../identity/state/identity.selectors';

export const loggedInGuard: CanActivateFn = async (_route, state) => {
  const store = inject(Store);
  const router = inject(Router);
  const isVisitingLoginComponent = state.url.includes('/login');
  const identity = store.selectSignal(selectIdentity)();

  if (isVisitingLoginComponent && identity.isAuthenticated) {
    await router.navigate(['/']);
  }

  if (!isVisitingLoginComponent && !identity.isAuthenticated) {
    await router.navigate(['/login']);
  }

  return isVisitingLoginComponent || identity.isAuthenticated;
};
