import {Injectable} from '@angular/core';
import {Sample} from "../../../models/samples/sample";
import {db} from "../../../db/terminal-db";
import {SampleEntity} from "../../../db/tables/sample-entity";
import {SampleDetails} from "../../../models/samples/sampleDetails";
import {NotFoundError} from "rxjs";
import {ProjectEntity} from "../../../db/tables/project-entity";

@Injectable({
  providedIn: 'root'
})
export class IdbSamplesService {

  constructor( ) { }

  async getSamples(pageIndex: number, pageSize: number, orderBy: string, desc = true) {
    orderBy = this.fieldMapper(orderBy as 'Code' | 'Project.Name' | 'CreatedAtUtc');
    let query = db.samples.orderBy(orderBy);
    if (desc) query = query.reverse();
    const entities = await query
      .offset(pageIndex * pageSize)
      .limit(pageSize)
      .toArray();
    return this.entitiesToSamples(entities, orderBy, desc);
  }

  async getRecentSamples(length: number) {
    const entities = await db.samples
      .orderBy('createdAtUtc')
      .reverse()
      .limit(length)
      .toArray();
    return this.entitiesToSamples(entities);
  }

  async getSample(id: string) {
    const entity = await db.samples.where('id').equals(id).first();
    if (entity == undefined) throw NotFoundError;
    return this.entityToSample(entity);
  }

  async getSamplesAmount() {
    return db.samples.count();
  }

  async addSamples(samples: Sample[]) {
    const projects = await Promise.all(samples.map(s => this.getProjectByName(s.project)));
    await db.samples.bulkPut(this.samplesToEntities(samples, projects));
  }

  async addSample(sample: SampleDetails) {
    const entity = await this.sampleDetailsToEntity(sample);
    db.samples.put(entity);
  }

  private async getProjectByName(name: string) {
    const project = await db.projects.where('name').equals(name).first();
    if (project == undefined) throw NotFoundError;
    else return project;
  }

  private async getProjectById(id: string) {
    const project = await db.projects.where('id').equals(id).first();
    if (project == undefined) throw NotFoundError;
    else return project;
  }

  private async sampleDetailsToEntity(s: SampleDetails): Promise<SampleEntity> {
    const project = await this.getProjectById(s.projectId);
    return {...s, projectName: project.name};
  }

  private entityToSample(e: SampleEntity): SampleDetails {
    return {
      id: e.id,
      code: e.code,
      comment: e.comment,
      createdAtUtc: e.createdAtUtc,
      projectId: e.projectId,
      recipe: e.recipe,
      steps: e.steps,
      tags: e.tags
    }
  }

  private async entitiesToSamples(entities: SampleEntity[], orderBy = '', desc = true): Promise<Sample[]> {
    const projects = await Promise.all(entities.map(e => this.getProjectById(e.projectId)))
    let samples = entities.map((e, index) => ({id: e.id, code: e.code, project: projects[index].name, createdAtUtc: e.createdAtUtc}))
    // almost proper sorting by code
    if (orderBy == 'code') {
      samples = samples.sort((a, b) => {
        const codeA = +a.code.replace(/\D+/g, '');
        const codeB = +b.code.replace(/\D+/g, '');
        const comparatorResult = (codeA < codeB) ? -1 : 1;
        return comparatorResult * (desc ? -1 : 1);
      });
    }
    return samples;
  }

  private samplesToEntities(samples: Sample[], projects: ProjectEntity[]): SampleEntity[] {
    return samples.map((s, index) => {
      return {
        id: s.id,
        code: s.code,
        createdAtUtc: s.createdAtUtc,
        projectId: projects[index].id,
        projectName: s.project,
        comment: null,
        recipe: null,
        steps: [], // TODO
        tags: []
      }
    })
  }

  private fieldMapper(field: 'Code' | 'Project.Name' | 'CreatedAtUtc') {
    return {
      'Code': 'code',
      'Project.Name': 'projectName',
      'CreatedAtUtc': 'createdAtUtc'
    }[field]
  }
}

/*
  sample details
*   id: string;
  code: string;
  recipe: Recipe | null;
  createdAtUtc: Date;
  comment: string | null;
  projectId: string;
  steps: Step[];
  tags: string[];
  *
  * sample
  id: string;
  code: string;
  project: string;
  createdAtUtc: Date;
  *
  * entity
  id: string
  code: string
  createdAtUtc: Date
  comment?: string
  projectId: string
  recipeId?: string
* */
