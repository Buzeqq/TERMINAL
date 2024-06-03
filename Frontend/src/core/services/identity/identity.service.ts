import { inject, Injectable } from '@angular/core';
import { HttpClient } from "@angular/common/http";
import { catchError, EMPTY, map, Observable, tap } from "rxjs";
import { Identity, LoginForm } from "../../identity/identity.model";
import { environment } from "../../../environments/environment";
import { Store } from "@ngrx/store";
import { IdentityActions } from "../../state/identity/identityActions";

@Injectable({
  providedIn: 'root'
})
export class IdentityService {
  private readonly http = inject(HttpClient);
  private readonly store = inject(Store);
  private readonly baseUrl = environment.apiUrl + 'api/identity';

  getUserInfo(): Observable<Identity> {
    return this.http.get<{
      id: string,
      email: string,
      isEmailConfirmed: boolean,
      roles: string[]
    }>(this.baseUrl + '/account/info')
      .pipe(
        map(r => ({
          email: r.email,
          isAuthenticated: true,
          role: 'administrator'
        } as Identity)),
        tap({
          next: (identity) => this.store.dispatch(IdentityActions.userLoaded({ identity })),
          error: () => this.store.dispatch(IdentityActions.failedToLoadUser())
        })
      );
  }

  login(form: LoginForm): Observable<undefined> {
    const { email, password, rememberMe } = form;
    return this.http.post<undefined>(this.baseUrl + '/login', {
      email,
      password,
    },
  {
      params: {
        useCookies: true,
        useSessionCookies: !rememberMe
      }
    }).pipe(
      tap({
        next: () => this.store.dispatch(IdentityActions.userLoggedIn()),
        error: () => this.store.dispatch(IdentityActions.failedToLogIn())
      }),
      catchError(() => EMPTY)
    );
  }
}
