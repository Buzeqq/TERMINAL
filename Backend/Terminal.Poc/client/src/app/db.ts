import Dexie, { Table } from 'dexie';
import { Measurement } from "./measurements.service";

class AppDB extends Dexie {
  measurements!: Table<Measurement, number>;
  uncommittedChanges!: Table<Change, number>;

  constructor() {
    super('ngdexieliveQuery');
    this.version(5).stores({
      measurements: '&id',
      uncommittedChanges: '&id, recordChanged'
    });
    console.log(this._dbSchema);
  }
}
export interface Change {
  id: string,
  measurement: Measurement
  type: ChangeType
}

export enum ChangeType {
  Created,
  Updated,
  Deleted
}
export const db = new AppDB();
