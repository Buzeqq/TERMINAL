import { inject, Injectable } from '@angular/core';
import { HttpClient } from "@angular/common/http";
import { map, Observable } from "rxjs";
import { Identity, LoginForm } from "../../identity/identity.model";
import { environment } from "../../../environments/environment";

@Injectable({
  providedIn: 'root'
})
export class IdentityService {
  private readonly http = inject(HttpClient);
  private readonly baseUrl = environment.apiUrl + 'api/identity';

  getUserInfo(): Observable<Identity> {
    return this.http.get<{
      id: string,
      email: string,
      isEmailConfirmed: boolean,
      roles: string[]
    }>(this.baseUrl + '/account/info')
      .pipe(map(r => ({
        email: r.email,
        isAuthenticated: true,
        role: 'administrator'
      })));
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
    });
  }
}
