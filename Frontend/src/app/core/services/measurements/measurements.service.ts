import { Injectable } from '@angular/core';
import { ApiService } from "../api-service";
import { HttpClient, HttpParams } from "@angular/common/http";
import { map, Observable } from "rxjs";
import { Measurement } from "../../models/measurements/measurement";
import {MeasurementDetails} from "../../models/measurements/measurementDetails";

@Injectable({
  providedIn: 'root'
})
export class MeasurementsService extends ApiService {
  constructor(http: HttpClient) {
    super(http);
  }

  getMeasurements(pageNumber: number, pageSize: number): Observable<Measurement[]> {
    return this.get<{measurements: Measurement[]}>('measurements', new HttpParams({
      fromObject: {
        pageNumber,
        pageSize
      }
    }))
      .pipe(
        map(m => m.measurements),
        map(r => r.map(measurement => ({
          ...measurement,
          createdAtUtc: new Date(measurement.createdAtUtc)
        })))
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

  getMeasurementDetails(id: string): Observable<MeasurementDetails> {
    return this.get<MeasurementDetails>(`measurements/${id}`)
      .pipe(
        map(measurement => ({
          ...measurement,
            createdAtUtc: new Date(measurement.createdAtUtc)
        }))
      );
  }
}
