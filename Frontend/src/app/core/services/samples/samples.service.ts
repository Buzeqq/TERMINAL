import { Injectable } from '@angular/core';
import { ApiService } from "../api-service";
import { HttpClient, HttpParams } from "@angular/common/http";
import {map, Observable} from "rxjs";
import { Sample } from "../../models/samples/sample";
import { AddSample } from "../../models/samples/addSample";
import { SampleDetails } from "../../models/samples/sampleDetails";

@Injectable({
  providedIn: 'root'
})
export class SamplesService extends ApiService {
  constructor(http: HttpClient) {
    super(http);
  }

  getSamples(pageNumber: number, pageSize: number, orderBy = "CreatedAtUtc", desc = true): Observable<Sample[]> {
    return this.get<{ samples: Sample[] }>('samples', new HttpParams({
      fromObject: {
        pageNumber,
        pageSize,
        orderBy,
        desc
      }
    }))
      .pipe(
        map(s => s.samples),
        map(s => s.map(sample => ({
          ...sample,
          createdAtUtc: new Date(sample.createdAtUtc)
        })))
      );
  }

  getRecentSamples(length: number): Observable<Sample[]> {
    return this.get<{ recentSamples: Sample[] }>(`samples/recent`, new HttpParams({
      fromObject: {
        length
      }
    }))
      .pipe(
        map(r => r.recentSamples),
        map(r => r.map(sample => ({
          ...sample,
          createdAtUtc: new Date(sample.createdAtUtc)
        })))
      );
  }

  getSampleDetails(id: string): Observable<SampleDetails> {
    return this.get<SampleDetails>(`samples/${id}`)
      .pipe(
        map(sample => ({
          ...sample,
            createdAtUtc: new Date(sample.createdAtUtc)
        }))
      );
  }

  addSample(form: AddSample) {
    return this.post<never>(`samples`, form);
  }

  getSamplesAmount(): Observable<number> {
    return this.get<number>('samples/amount');
  }
}
