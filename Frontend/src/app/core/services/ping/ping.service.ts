import { Injectable } from '@angular/core';
import { ApiService } from "../api-service";
import { HttpClient } from "@angular/common/http";
import { BehaviorSubject, catchError, map, Observable, of } from "rxjs";

@Injectable({
  providedIn: 'root'
})
export class PingService extends ApiService {
  constructor(http: HttpClient) {
    super(http);
    this.ping()
      .pipe(
        map(_ => true),
        catchError(_ => of(false))
      ).subscribe(r => this.status.next(r));
  }
  get isOnline$(): Observable<boolean> {
    return this.status.asObservable();
  }

  private ping(): Observable<boolean> {
    return this.get<boolean>("ping");
  }
  private status = new BehaviorSubject<boolean>(window.navigator.onLine);
}
