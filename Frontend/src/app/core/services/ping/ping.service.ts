import { Injectable } from '@angular/core';
import { ApiService } from "../api-service";
import { HttpClient } from "@angular/common/http";
import { catchError, map, Observable, of } from "rxjs";

@Injectable({
    providedIn: 'root'
})
export class PingService extends ApiService {
    constructor(http: HttpClient) {
        super(http);
    }

    get isOnline$(): Observable<boolean> {
        return this.ping()
            .pipe(
                map(_ => true),
                catchError(_ => of(false)),
            );
    }

    private ping(): Observable<boolean> {
        return this.get<boolean>("ping");
    }
}
