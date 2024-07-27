import { inject, Injectable } from '@angular/core';
import { HttpClient, HttpErrorResponse } from "@angular/common/http";
import { catchError, map, Observable } from "rxjs";
import { Identity, LoginForm } from "../identity.model";
import { environment } from "../../../../environments/environment";
import { FailedToLoginError } from "../../errors/errors";

@Injectable({
  providedIn: 'root'
})
export class IdentityService {
  private readonly http = inject(HttpClient);
  private readonly baseUrl = environment.apiUrl + '/identity';

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
          roles: r.roles
        } as Identity)),
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
      catchError((response: HttpErrorResponse) => {
        throw new FailedToLoginError(response.error);
      })
    );
  }

  logOut() {
    return this.http.post<undefined>(this.baseUrl + '/logout', {});
  }
}
