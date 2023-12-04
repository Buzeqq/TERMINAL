import { Injectable } from '@angular/core';
import { db } from "../../db/terminal-db";
import {Project} from "../../models/projects/project";
import {ProjectEntity} from "../../db/tables/project-entity";

@Injectable({
  providedIn: 'root'
})
export class DbService {

  constructor() { }

  /* we can assume for every project isActive=true */
  async addProjects(projects: Project[]) {
    const projectEntities: ProjectEntity[] = projects
      .map(p => ({...p, isActive: true}))
    await db.projects.bulkPut(projectEntities);
  }

  async getProjects(pageIndex: number, pageSize: number) {
    return db.projects.offset(pageIndex).limit(pageSize).toArray();
  }

}
