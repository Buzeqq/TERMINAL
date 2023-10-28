import { Injectable } from '@angular/core';
import { ApiService } from "../api-service";
import { HttpClient } from "@angular/common/http";
import { catchError, map, Observable, tap } from "rxjs";
import { Measurement } from "../../models/measurements/recentMeasurement";

@Injectable({
  providedIn: 'root'
})
export class MeasurementsService extends ApiService {
  constructor(http: HttpClient) {
    super(http);
  }

  // TODO: /api/measurements endpoint needed?
  getAllMeasurements(): Observable<Measurement[]> {
    return this.get<{ recentMeasurements: Measurement[] }>('measurements/recent?length=100')
      .pipe(
        catchError(this.handleError),
        map(r => r.recentMeasurements),
        map(r => r.map(measurement => ({
          ...measurement,
          createdAtUtc: new Date(measurement.createdAtUtc)
        })))
      );
  }

  getRecentMeasurements(length: number): Observable<Measurement[]> {
    return this.get<{ recentMeasurements: Measurement[] }>(`measurements/recent?length=${length}`)
      .pipe(
        map(r => r.recentMeasurements),
        map(r => r.map(measurement => ({
          ...measurement,
          createdAtUtc: new Date(measurement.createdAtUtc)
        })))
      );
  }

  getMeasurementDetails(id: string): Observable<any> {
    return this.get<any>(`measurements/${id}`).pipe(tap(r => console.log(r)));
  }
}
