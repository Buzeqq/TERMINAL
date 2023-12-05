import { Injectable } from '@angular/core';
import {db} from "../../../db/terminal-db";
import {ProjectEntity} from "../../../db/tables/project-entity";
import {Project} from "../../../models/projects/project";
import {ProjectDetails} from "../../../models/projects/project-details";
import {NotFoundError} from "rxjs";
import {SampleEntity} from "../../../db/tables/sample-entity";

@Injectable({
  providedIn: 'root'
})
export class IdbProjectsService {

  constructor( ) { }

  async getProjects(pageIndex: number, pageSize: number, desc = true, all = false) {
    let query = db.projects.orderBy('name');
    if (desc) query = query.reverse();
    const entities = await query
      .filter(e => e.isActive == 1 || e.isActive == (all ? 0 : 1))
      .offset(pageIndex)
      .limit(pageSize)
      .toArray();
    return this.entitiesToProjects(entities);
  }

  async getProject(id: string) : Promise<ProjectDetails> {
    const project = await db.projects.where('id').equals(id).first();
    if (project == undefined) throw NotFoundError;
    const samples = await this.getSamplesByProject(project.id);
    return this.entityToProjectDetails(project, samples);
  }

  async getProjectsAmount() {
    return db.projects.count();
  }

  async addProjects(projects: Project[]) {
    await db.projects.bulkPut(this.projectsToEntities(projects));
  }

  private getSamplesByProject(id: string) {
    return db.samples.where('projectId').equals(id).toArray();
  }

  private entitiesToProjects(entities: ProjectEntity[]): Project[] {
    return entities.map(e => ({id: e.id, name: e.name}));
  }

  private entityToProjectDetails(e: ProjectEntity, samples: SampleEntity[]): ProjectDetails {
    return {id: e.id, name: e.name, isActive: !!e.isActive, samplesIds: samples.map(s => s.id)};
  }

  private projectsToEntities(projects: Project[]): ProjectEntity[] {
    return projects.map(p => ({...p, isActive: 1}))
  }
}
