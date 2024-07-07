import { inject, Injectable } from "@angular/core";
import { ComponentStore } from "@ngrx/component-store";
import { delay, finalize, Observable, switchMap, tap } from "rxjs";
import { LoginForm } from "../../core/identity/identity.model";
import { IdentityService } from "../../core/services/identity/identity.service";

export interface LoginState {
  isLoading: boolean;
}

@Injectable()
export class LoginStore extends ComponentStore<LoginState> {
  constructor() {
    super({
      isLoading: false
    });
  }

  private readonly service = inject(IdentityService);
  readonly isLoading$ = this.select((state) => state.isLoading);

  readonly tryToLogIn = this.effect((loginForm$: Observable<LoginForm>) => {
    return loginForm$.pipe(
      tap(() => this.patchState({ isLoading: true })),
      switchMap((form) => this.service.login(form).pipe(
        tap(() => this.patchState({ isLoading: false }))
      )),
    );
  })

  readonly tryToLoadUser = this.effect(() => {
    this.patchState({ isLoading: true });

    return this.service.getUserInfo().pipe(
      delay(2000),
      tap(() => console.log('123')),
      finalize(() => this.patchState({ isLoading: false })),
    );
  })
}
