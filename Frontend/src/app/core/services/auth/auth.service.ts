import { Injectable } from '@angular/core';
import {ApiService} from "../api-service";
import {HttpClient, HttpErrorResponse} from "@angular/common/http";
import {BehaviorSubject, catchError, shareReplay, Subject, tap, throwError, timer} from "rxjs";
import * as moment from "moment";
import {jwtDecode} from "jwt-decode";

@Injectable({
  providedIn: 'root'
})
export class AuthService extends ApiService {
  private loggedOut = new BehaviorSubject<boolean>(true);
  alertBefore = 120 * 1000 // 2 minutes expressed in milliseconds
  private sessionWarningTimer = new Subject();
  get sessionWarningTimer$() {
    return this.sessionWarningTimer.asObservable();
  }

  constructor(
    http: HttpClient,
  ) {
    super(http)
    /* reload issue, if we reload the page a new Auth Service is created so we need to set labels again */
    if (this.isLoggedIn()) {
      this.loggedOut.next(false);
    }
  }

  login(email: string, password: string ){
    return this.post<successfulLoginResponse>('users/login', {email, password})
      .pipe(
        catchError(this.handleAuthError),
        tap(r => this.manageResponse(r)),
        shareReplay() /* prevents from double POST request */
      )
  }

  private handleAuthError(err: HttpErrorResponse) {
    return throwError(() => Error('Invalid credentials.'))
  }

  private manageResponse(r: successfulLoginResponse) {
    const decoded = jwtDecode(r.token) as decodedJWT;
    this.setSession(decoded, r.token);
    this.loggedOut.next(false);
  }

  private setSession(payload: decodedJWT, token: string) {
    localStorage.setItem('token', token);
    localStorage.setItem("expires_at", JSON.stringify(payload.exp * 1000));
    localStorage.setItem('user_role', payload["http://schemas.microsoft.com/ws/2008/06/identity/claims/role"]);

    const expiresIn = moment(payload.exp*1000).subtract(moment().valueOf()); // milliseconds
    timer(expiresIn.valueOf() - this.alertBefore).subscribe(this.sessionWarningTimer);
  }

  renewSession() {
    localStorage.setItem(
      "expires_at", JSON.stringify(
        this.getExpiration().add(1, 'hour').valueOf()
      )
    );
  }

  logout() {
    localStorage.removeItem("token");
    localStorage.removeItem("expires_at");
    localStorage.removeItem("user_role");
    this.loggedOut.next(true)
  }

  isLoggedIn() {
    return moment().isBefore(this.getExpiration());
  }

  isLoggedOut() {
    return this.loggedOut.asObservable();
  }

  getExpiration() {
    const expiration = localStorage.getItem("expires_at");
    const expiresAt = expiration ? JSON.parse(expiration) : null;
    return moment(expiresAt);
  }
}

export interface successfulLoginResponse {
  token: string
}

export interface decodedJWT {
  sub: string // token
  email: string
  "http://schemas.microsoft.com/ws/2008/06/identity/claims/role": string // role
  exp: number // expiration time - UTC timestamp in seconds
  iss: string // terminal
  aud: string // terminal-clients
}

