import Dexie, { Table } from 'dexie';
import {SampleEntity} from "./tables/sample-entity";
import {ProjectEntity} from "./tables/project-entity";
import {RecipeEntity} from "./tables/recipe-entity";
import {TagEntity} from "./tables/tag-entity";

export class TerminalDB extends Dexie {
  samples!: Table<SampleEntity, string>;
  projects!: Table<ProjectEntity, string>;
  recipes!: Table<RecipeEntity, string>;
  tags!: Table<TagEntity, string>;
  // sampleTags!: Table<SampleTagAT, string>;
  // steps!: Table<Step, string>;

  constructor() {
    super('terminalDB'); /* database name as parameter */
    /*
    fields provided in schema are indexed,
    other fields included in tables are not indexed, just stored
    */
    this.version(5).stores(
      {
        samples: 'id, code, createdAtUtc, projectId, projectName',
        projects: 'id, &name, isActive',
        recipes: 'id, name',
        tags: 'id, &name, isActive',
      }
      );
  }
}

export const db = new TerminalDB();
