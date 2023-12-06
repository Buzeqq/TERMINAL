import Dexie, { Table } from 'dexie';
import {SampleEntity} from "./tables/sample-entity";
import {ProjectEntity} from "./tables/project-entity";
import {RecipeEntity} from "./tables/recipe-entity";

export class TerminalDB extends Dexie {
  samples!: Table<SampleEntity, string>;
  projects!: Table<ProjectEntity, string>;
  recipes!: Table<RecipeEntity, string>;

  constructor() {
    super('terminalDB'); /* database name */
    /*
    fields provided in schema are indexed,
    other fields included in tables are not indexed, just stored
    */
    this.version(1).stores(
      {
        samples: 'id, &code, createdAtUtc, projectId, projectName',
        projects: 'id, &name, isActive',
        recipes: 'id, &name',
      }
      );
  }
}

export const db = new TerminalDB();
