import { Injectable } from '@angular/core';
import { ApiService } from "../api-service";
import { HttpClient, HttpParams } from "@angular/common/http";
import { map, Observable } from "rxjs";
import { Measurement } from "../../models/measurements/measurement";

@Injectable({
  providedIn: 'root'
})
export class MeasurementsService extends ApiService {
  constructor(http: HttpClient) {
    super(http);
  }

  getMeasurements(pageNumber: number, pageSize: number): Observable<Measurement[]> {
    return this.get<{ measurements: Measurement[] }>('measurements', new HttpParams({
      fromObject: {
        pageNumber,
        pageSize
      }
    }))
      .pipe(
        map(m => m.measurements)
      );
  }

  getRecentMeasurements(length: number): Observable<Measurement[]> {
    return this.get<{ recentMeasurements: Measurement[] }>(`measurements/recent`, new HttpParams({
      fromObject: {
        length
      }
    }))
      .pipe(
        map(r => r.recentMeasurements),
        map(r => r.map(measurement => ({
          ...measurement,
          createdAtUtc: new Date(measurement.createdAtUtc)
        })))
      );
  }

  getMeasurementDetails(id: string): Observable<any> {
    return this.get<any>(`measurements/${id}`);
  }
}
