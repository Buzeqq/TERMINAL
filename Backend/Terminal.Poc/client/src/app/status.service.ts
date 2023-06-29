import { Injectable } from '@angular/core';
import {BehaviorSubject, catchError, map, Observable, of} from "rxjs";
import {HttpClient} from "@angular/common/http";
import {environment} from "../environments/environment";

@Injectable({
  providedIn: 'root'
})
export class StatusService {
  apiUrl = environment.apiUrl;

  private _isOnline$ = new BehaviorSubject<boolean>(window.navigator.onLine);
  // baseUrl = environment.apiUrl;
  constructor(private readonly http: HttpClient) {
    this.listenToOnlineStatus();
  }

  listenToOnlineStatus(): void {
    window.addEventListener('online', () => {
      this.ping()
        .subscribe(status => {
          this._isOnline$.next(status);

        });
    });
    window.addEventListener('offline', () => {
      this.ping()
        .subscribe(status => this._isOnline$.next(status));
    });
  }

  ping(): Observable<boolean> {
    return this.http.get(this.apiUrl + "/api/ping")
      .pipe(
        map(_ => true),
        catchError(_ => of(false))
      )
  }

  get isOnline$(): Observable<boolean> {
    return this._isOnline$.asObservable();
  }
}
