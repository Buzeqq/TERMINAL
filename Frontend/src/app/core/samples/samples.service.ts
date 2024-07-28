import { inject, Injectable } from '@angular/core';
import { HttpClient, HttpErrorResponse, HttpParams } from "@angular/common/http";
import { environment } from "../../../environments/environment";
import { Sample } from "./sample.model";
import { catchError, map, Observable } from "rxjs";
import { FailedToLoadSamplesError } from "../errors/errors";

@Injectable({
  providedIn: 'root'
})
export class SamplesService {
  private readonly http = inject(HttpClient);
  private readonly baseUrl = environment.apiUrl + '/samples';


  getRecentSamples(length: number): Observable<Sample[]> {
    return this.http
      .get<{ recentSamples: Sample[] }>(this.baseUrl + '/recent', {
        params: new HttpParams({ fromObject: { length } })
      })
      .pipe(
        catchError((err: HttpErrorResponse) => {
          throw new FailedToLoadSamplesError(err.error);
        }),
        map(r => r.recentSamples),
        map(r => r.map(sample => ({
          ...sample,
          createdAtUtc: new Date(sample.createdAtUtc)
        }))),
        map(r => r
          .sort((a, b) =>
            b.createdAtUtc.getTime() - a.createdAtUtc.getTime()))
      );
  }
}
