import { Injectable } from '@angular/core';
import {ApiService} from "../api-service";
import {HttpClient, HttpErrorResponse} from "@angular/common/http";
import {BehaviorSubject, catchError, shareReplay, Subject, tap, throwError, timer} from "rxjs";
import * as moment from "moment";
import {jwtDecode} from "jwt-decode";
import {decodedJWT, successfulLoginResponse} from "../../models/auth/auth";

@Injectable({
  providedIn: 'root'
})
export class AuthService extends ApiService {
  private loggedOut = new BehaviorSubject<boolean>(true);
  alertBefore = 120 * 1000 /* 2 minutes expressed in milliseconds */
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
        tap(r => this.setSession(r)),
        shareReplay() /* prevents from double POST request */
      )
  }

  private setSession(r: successfulLoginResponse) {
    const decoded = jwtDecode(r.token) as decodedJWT;

    localStorage.setItem('token', r.token);
    localStorage.setItem("expiresAt", JSON.stringify(decoded.exp * 1000));

    const expiresIn = moment(decoded.exp*1000).subtract(moment().valueOf()); // milliseconds
    timer(expiresIn.valueOf() - this.alertBefore).subscribe(this.sessionWarningTimer);

    this.loggedOut.next(false);
  }

  renewSession() {
    // FIXME send request to backend for a new token
    // localStorage.setItem(
    //   "expiresAt", JSON.stringify(
    //     this.getExpiration().add(1, 'hour').valueOf()
    //   )
    // );
  }

  logout() {
    localStorage.removeItem("token");
    localStorage.removeItem("expiresAt");
    this.loggedOut.next(true)
  }

  isLoggedIn() {
    return moment().isBefore(this.getExpiration());
  }

  isLoggedOut() {
    return this.loggedOut.asObservable();
  }

  isAdmin() {
    return this.isRole("Administrator");
  }

  isModerator() {
    return this.isRole("Moderator");
  }

  isAdminOrMod() {
    return this.isAdmin() || this.isModerator();
  }

  private isRole(role: string) {
    const token = localStorage.getItem('token');
    if (token) {
      const decoded = jwtDecode(token) as decodedJWT;
      return decoded["http://schemas.microsoft.com/ws/2008/06/identity/claims/role"] == role;
    } else return false;
  }

  private getExpiration() {
    const expiration = localStorage.getItem("expiresAt");
    const expiresAt = expiration ? JSON.parse(expiration) : null;
    return moment(expiresAt);
  }

  private handleAuthError(err: HttpErrorResponse) {
    return throwError(() => Error('Invalid credentials.'))
  }
}
