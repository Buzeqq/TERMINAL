import { inject, Injectable } from '@angular/core';
import {
  HttpClient,
  HttpErrorResponse,
  HttpParams,
} from '@angular/common/http';
import { environment } from '../../../environments/environment';
import { Sample, SampleDetails } from './sample.model';
import { catchError, map, Observable } from 'rxjs';
import {
  FailedToLoadSampleDetailsError,
  FailedToLoadSamplesError,
} from '../errors/errors.model';
import { SortDirection } from '@angular/material/sort';

type SamplesResponse = { samples: Sample[]; totalCount: number };

@Injectable({
  providedIn: 'root',
})
export class SamplesService {
  private readonly http = inject(HttpClient);
  private readonly baseUrl = environment.apiUrl + '/samples';

  getRecentSamples(length: number): Observable<Sample[]> {
    return this.http
      .get<{ recentSamples: Sample[] }>(this.baseUrl + '/recent', {
        params: new HttpParams({ fromObject: { length } }),
      })
      .pipe(
        map(r => r.recentSamples),
        map(r =>
          r.map(sample => ({
            ...sample,
            createdAtUtc: new Date(sample.createdAtUtc),
          }))
        ),
        map(r =>
          r.sort((a, b) => b.createdAtUtc.getTime() - a.createdAtUtc.getTime())
        ),
        catchError((err: HttpErrorResponse) => {
          throw new FailedToLoadSamplesError(err.error);
        })
      );
  }

  getSampleDetails(id: string): Observable<SampleDetails> {
    return this.http
      .get<SampleDetails>(environment.apiUrl + `/samples/${id}`)
      .pipe(
        map(sample => ({
          ...sample,
          createdAtUtc: new Date(sample.createdAtUtc),
        })),
        catchError((err: HttpErrorResponse) => {
          throw new FailedToLoadSampleDetailsError(err.error);
        })
      );
  }

  getSamples(
    pageNumber: number,
    pageSize: number,
    sortDirection: SortDirection,
    searchPhrase?: string
  ): Observable<SamplesResponse> {
    const params: Record<string, number | string | boolean> = {
      pageNumber,
      pageSize,
      desc: sortDirection === 'desc',
    };

    if (searchPhrase) {
      params['searchPhrase'] = searchPhrase;
    }

    return this.http
      .get<SamplesResponse>(this.baseUrl + '/', {
        params: {
          ...params,
        },
      })
      .pipe(
        map(r => {
          r.samples.map(s => {
            s.createdAtUtc = new Date(s.createdAtUtc);
          });
          return r;
        }),
        catchError((error: HttpErrorResponse) => {
          throw new FailedToLoadSamplesError(error.error);
        })
      );
  }
}
