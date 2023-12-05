import { Injectable } from '@angular/core';
import { ApiService } from "../api-service";
import { HttpClient, HttpParams } from "@angular/common/http";
import {map, Observable, tap} from "rxjs";
import { Sample } from "../../models/samples/sample";
import { AddSample } from "../../models/samples/addSample";
import { SampleDetails } from "../../models/samples/sampleDetails";
import {PingService} from "../ping/ping.service";
import {IndexedDbService} from "../indexed-db/indexed-db.service";

@Injectable({
  providedIn: 'root'
})
export class SamplesService extends ApiService {

  private online = false;

  constructor(
    http: HttpClient,
    private readonly pingService: PingService,
    private readonly idbService: IndexedDbService
  ) { super(http); this.pingService.isOnline$.subscribe(r => this.online = r); }

  getSamples(pageNumber: number, pageSize: number, orderBy = "CreatedAtUtc", desc = true): Observable<Sample[]> {
    if (this.online) return this.get<{ samples: Sample[] }>('samples', new HttpParams({
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
    else return this.idbService.getSamples(pageNumber, pageSize, orderBy, desc)
      .pipe(map(s => s.map(sample => ({
        ...sample, createdAtUtc: new Date(sample.createdAtUtc)
      }))))
  }

  getRecentSamples(length: number): Observable<Sample[]> {
    if (this.online) return this.get<{ recentSamples: Sample[] }>(`samples/recent`, new HttpParams({
      fromObject: {
        length
      }
    }))
      .pipe(
        map(r => r.recentSamples),
        map(r => r.map(sample => ({
          ...sample,
          createdAtUtc: new Date(sample.createdAtUtc)
        }))),
        tap(_ => console.log('online /samples'))
      );
    else {
      console.log("offline /samples")
      return this.idbService.getRecentSamples(length).pipe(map(s => s.map(sample => ({
        ...sample, createdAtUtc: new Date(sample.createdAtUtc)
      }))));
    }
  }

  getSampleDetails(id: string): Observable<SampleDetails> {
    if (this.online) return this.get<SampleDetails>(`samples/${id}`)
      .pipe(
        map(sample => ({
          ...sample,
            createdAtUtc: new Date(sample.createdAtUtc)
        })),
        tap(_ => console.log('online /samples/{id}'))
      );
    else {
      console.log('offline /samples/{id}')
      return this.idbService.getSample(id).pipe(map(
        sample => ({
          ...sample, createdAtUtc: new Date(sample.createdAtUtc)
        })
      ));
    }
  }

  addSample(form: AddSample) {
    return this.post<never>(`samples`, form);
  }

  getSamplesAmount(): Observable<number> {
    if (this.online) return this.get<number>('samples/amount')
      .pipe(tap(_ => console.log('online /samples/amount')));
    else {
      console.log('offline /samples/amount')
      return this.idbService.getSamplesAmount();
    }
  }
}
