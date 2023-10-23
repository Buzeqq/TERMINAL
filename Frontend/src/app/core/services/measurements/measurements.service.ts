import { Injectable } from '@angular/core';
import { ApiService } from "../api-service";
import { HttpClient } from "@angular/common/http";
import { map, Observable, tap } from "rxjs";
import { RecentMeasurement } from "../../models/measurements/recentMeasurement";

@Injectable({
  providedIn: 'root'
})
export class MeasurementsService extends ApiService {
  constructor(http: HttpClient) {
    super(http);
  }

  getRecentMeasurements(length: number): Observable<RecentMeasurement[]> {
    return this.get<{ recentMeasurements: {
        measurementId: string,
        code: string,
        project: string,
        createdAtUtc: string
      }[] }>(`measurements/recent?len=${length}`)
      .pipe(
        map(r => r.recentMeasurements),
        map(r => r.map(measurement => ({
          ...measurement,
          createdAtUtc: new Date(measurement.createdAtUtc)
        })))
      );
  }
}
