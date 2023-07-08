import {Injectable} from '@angular/core';
import {HttpClient, HttpErrorResponse} from "@angular/common/http";
import {catchError, map, Observable, of} from "rxjs";
import {ChangeType, db} from "./db";
import {liveQuery} from "dexie";
import {environment} from "../environments/environment";
import {v4 as uuidv4} from 'uuid';
import {StatusService} from "./status.service";

@Injectable({
  providedIn: 'root'
})
export class MeasurementsService {
  apiUrl = environment.apiUrl;
  constructor(private readonly http: HttpClient,
              private readonly statusService: StatusService) {
    this.getAllFromRemote().subscribe(r => {
      db.measurements.bulkAdd(r);
    });

    this.statusService.isOnline$.subscribe(r => {
      if (r) this.saveUncommittedChanges().then(() => console.log("done")).catch(() => console.log("failed"));
    });
  }

  private async saveUncommittedChanges() {
    let changedRecordsIds = await db.uncommittedChanges.toCollection().sortBy('type');
    for (const change of changedRecordsIds) {
      let measurement = change.measurement;
      if (!measurement) {
        console.log(`Cant find measurement with key: ${change.measurement.id}`);
        return;
      }

      switch (change.type) {
        case ChangeType.Created: {
          this.addToRemote(measurement!).subscribe();
          break;
        }
        case ChangeType.Updated: {
          this.updateToRemote(measurement!).subscribe();
          break;
        }
        case ChangeType.Deleted:
          break;

      }
      await db.uncommittedChanges.where({
        id: change.id
      }).delete();
    }
  }

  private getAllFromRemote(): Observable<Measurement[]> {
    return this.http.get<RawMeasurement[]>(this.apiUrl + "/api/measurements")
      .pipe(
        map(measurements => measurements.map<Measurement>((m) => {
          return {
            id: m.id,
            value: m.value,
            createdOnUtc: new Date(m.createdOnUtc),
          };
        }))
      );
  }

  private addToRemote(measurement: Measurement): Observable<never> {
    return this.http.post<never>(this.apiUrl + "/api/measurements", measurement);
  }

  private updateToRemote(measurement: Measurement): Observable<never> {
    return this.http.put<never>(this.apiUrl + `/api/measurements/${measurement.id}`, measurement);
  }

  async getAll() {
    return db.measurements.toArray();
  }

  async addMeasurement(measurement: RawMeasurement) {
    const newMeasurement = {
      id: measurement.id,
      value: measurement.value,
      createdOnUtc: new Date(measurement.createdOnUtc),
    };
    await db.measurements.add(newMeasurement);
    return this.http.post<never>(this.apiUrl + "/api/measurements", measurement)
      .pipe(catchError(await this.handleRemoteUnavailable<never>(newMeasurement, ChangeType.Created)));
  }

  async updateMeasurement(measurement: Measurement) {
    console.log(measurement);
    await db.measurements.update(measurement, { value: measurement.value });
    return this.http.put<never>(this.apiUrl + `/api/measurements/${measurement.id}`, measurement)
      .pipe(catchError(await this.handleRemoteUnavailable<never>(measurement, ChangeType.Updated)));
  }

  private async handleRemoteUnavailable<T>(measurement: Measurement, change: ChangeType, result?: T) {
    return async (error: HttpErrorResponse): Promise<Observable<T>> => {
      console.log(error.status);
      console.log(measurement);

      if (error.ok) return of(result as T);
      console.log('adding to uncommitted changes');
      await db.uncommittedChanges.add({
        id: uuidv4(),
        measurement: measurement,
        type: change
      })

      return of(result as T);
    }
  }

  get measurements$() {
    return liveQuery<Measurement[]>(() => this.getAll())
  }
}

export interface RawMeasurement {
  id: string,
  value: number,
  createdOnUtc: string
}

export interface Measurement {
  id: string,
  value: number,
  createdOnUtc: Date,
}
