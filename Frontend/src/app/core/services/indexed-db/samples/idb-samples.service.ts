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

  async searchSamples(searchPhrase: string, pageIndex: number, pageSize: number) {
    const offset = pageIndex * pageSize;
    const entities = await db.samples
      .filter((sample) => {
        return (
          sample.code.toLowerCase().includes(searchPhrase.toLowerCase()) ||
          sample.projectName.toLowerCase().includes(searchPhrase.toLowerCase())
        );
      })
      .offset(offset)
      .limit(pageSize)
      .toArray();
    return this.entitiesToSamples(entities);
  }

  async getSamplesAmount() {
    return db.samples.count();
  }

  async addSamples(samples: Sample[]) {
    const projects = await Promise.all(samples.map(s => this.getProjectByName(s.project)));
    for (const newSamp of samples) {
      const index = samples.indexOf(newSamp);
      const sample = await db.samples.get(newSamp.id);
      if (sample) db.samples.put(this.smartMerge(sample, newSamp, projects[index]));
      else db.samples.put(this.sampleToEntity(newSamp, projects[index]))
    }
  }

  async addSample(sample: SampleDetails) {
    const entity = await this.sampleDetailsToEntity(sample);
    db.samples.put(entity);
  }

  private smartMerge(se: SampleEntity, sa: Sample, p: ProjectEntity): SampleEntity {
    /* overwrite first and second row with potential new values, 3rd remains the same */
    return {
      id: sa.id, code: sa.code, createdAtUtc: sa.createdAtUtc,
      projectId: p.id, projectName: p.name,
      comment: se.comment, recipe: se.recipe, steps: se.steps, tags: se.tags
    }
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
      id: e.id, code: e.code, createdAtUtc: e.createdAtUtc,
      projectId: e.projectId,
      comment: e.comment, recipe: e.recipe, steps: e.steps, tags: e.tags
    }
  }

  private async entitiesToSamples(entities: SampleEntity[], orderBy = '', desc = true): Promise<Sample[]> {
    const projects = await Promise.all(entities.map(e => this.getProjectById(e.projectId)))
    let samples = entities.map((e, index) => ({id: e.id, code: e.code, project: projects[index].name, createdAtUtc: e.createdAtUtc}))
    /* dexie provides weird results when sorting by code which is usually sth like AX10, so we use simple sorting */
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

  private sampleToEntity(s: Sample, p: ProjectEntity): SampleEntity {
    return {
      id: s.id, code: s.code, createdAtUtc: s.createdAtUtc,
      projectId: p.id, projectName: s.project,
      comment: null, recipe: null, steps: [], tags: []
    }
  }

  private fieldMapper(field: 'Code' | 'Project.Name' | 'CreatedAtUtc') {
    return {
      'Code': 'code',
      'Project.Name': 'projectName',
      'CreatedAtUtc': 'createdAtUtc'
    }[field]
  }
}
