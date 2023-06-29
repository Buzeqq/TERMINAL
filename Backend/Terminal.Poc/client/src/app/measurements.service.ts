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
    let changedRecordsIds = await db.uncommittedChanges.toArray();
    for (const change of changedRecordsIds) {
      const measurement = await db.measurements.where({
        id: change.recordChanged
      }).first();
      console.log(measurement);
      switch (change.type) {
        case ChangeType.Created: {
          this.addToRemote(measurement!).subscribe();
          await db.measurements.where({
            id: change.recordChanged
          }).delete();
          break;
        }
        case ChangeType.Updated:
          break;
        case ChangeType.Deleted:
          break;

      }
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
      .pipe(catchError(await this.handleRemoteUnavailable<never>(newMeasurement)));
  }

  private async handleRemoteUnavailable<T>(measurement: Measurement, result?: T) {
    return async (error: HttpErrorResponse): Promise<Observable<T>> => {
      console.log(error.status);

      if (!error.ok) {
        console.log('adding to uncommitted changes');
        await db.uncommittedChanges.add({
          id: uuidv4(),
          recordChanged: measurement.id,
          type: ChangeType.Created
        })
      }
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
