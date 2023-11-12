import { Injectable } from '@angular/core';
import {ApiService} from "../api-service";
import {HttpClient} from "@angular/common/http";
import {BehaviorSubject, of, tap} from "rxjs";
import * as moment from "moment";
import {NotificationService} from "../notification/notification.service";

@Injectable({
  providedIn: 'root'
})
export class AuthService extends ApiService {
  private loginButtonSubject  = new BehaviorSubject<string>('Sign In');
  public loginButtonLabel$ = this.loginButtonSubject.asObservable();
  private usernameLabelSubject  = new BehaviorSubject<string>('');
  public usernameLabel$ = this.usernameLabelSubject.asObservable();

  constructor(
    http: HttpClient,
    private readonly notificationService: NotificationService
  ) {
    super(http)
    // TODO AD
    // reload issue, if we reload the page a new Auth Service is created so we need to set labels again
    if (this.isLoggedIn()) {
      let username = localStorage.getItem('username');
      this.usernameLabelSubject.next(username ?? '');
      this.loginButtonSubject.next('Sign Out');
    }
  }

  login(username: string, password: string ){
    // no login endpoint available for now
    return of({expiresIn: 3000, idToken:'dummyToken-xxxyyyzzz123', role: 'user', username: 'John Doe'})
      .pipe(
        tap(r => this.successfulLogin(r, username))
      );

    // TODO AD: success -> setSession, failure -> send info back, (login form might need it)
/*    return this.http.post<JwtToken>('/api/login', {email, password})
      .pipe(
        tap(r => this.successfulLogin(r, username)),
        shareReplay() // prevents double form submit
      )*/
  }

  private successfulLogin(authResult: JwtToken, username: string) {
    this.setSession(authResult);
    this.updateStatusBar(username, 'Sign Out');
    this.notificationService.notifySuccess('Logged in successfully');
  }

  private setSession(authResult: JwtToken) {
    const expiresAt = moment().add(authResult.expiresIn,'second');
    localStorage.setItem('id_token', authResult.idToken);
    localStorage.setItem('user_role', authResult.role);
    localStorage.setItem('username', authResult.username);
    localStorage.setItem("expires_at", JSON.stringify(expiresAt.valueOf()));
  }

  logout() {
    localStorage.removeItem("id_token");
    localStorage.removeItem("expires_at");
    localStorage.removeItem("user_role");
    localStorage.removeItem("username");
    this.updateStatusBar('', 'Sign In');
    this.notificationService.notifySuccess('Logged out successfully');
    // TODO AD: return info about success?
  }

  public isLoggedIn() {
    return moment().isBefore(this.getExpiration());
  }

  isLoggedOut() {
    return !this.isLoggedIn();
  }

  getExpiration() {
    const expiration = localStorage.getItem("expires_at");
    const expiresAt = expiration ? JSON.parse(expiration) : null;
    return moment(expiresAt);
  }

  private updateStatusBar(username: string, buttonLabel: string) {
    this.usernameLabelSubject.next(username);
    this.loginButtonSubject.next(buttonLabel);
  }
}

export interface JwtToken {
  expiresIn: number,
  idToken: string // jwtBearerToken
  role: string // TODO AD: create an Enum for roles
  username: string
}

