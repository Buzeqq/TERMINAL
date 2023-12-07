import { Injectable } from '@angular/core';
import {catchError, EMPTY, map, Observable, tap} from "rxjs";
import {HttpClient, HttpParams} from "@angular/common/http";
import { Project } from "../../models/projects/project";
import { ApiService } from "../api-service";
import { NotificationService } from "../notification/notification.service";
import {ProjectDetails} from "../../models/projects/project-details";
import {IndexedDbService} from "../indexed-db/indexed-db.service";
import {PingService} from "../ping/ping.service";

@Injectable({
  providedIn: 'root'
})
export class ProjectsService extends ApiService {

  private online = false;

  constructor(
    http: HttpClient,
    private readonly notificationService: NotificationService,
    private readonly idbService: IndexedDbService,
    private readonly pingService: PingService
  ) {
    super(http);
    this.pingService.isOnline$.subscribe(r => this.online = r);
  }

  getProjects(pageNumber: number, pageSize: number, desc = true, all = false): Observable<Project[]> {
    const endpoint = all ? 'projects/all' : 'projects';
    if (this.online) return this.get<{ projects: Project[] }>(endpoint, new HttpParams({
      fromObject: {
        pageNumber,
        pageSize,
        desc
      }
    }))
      .pipe(
        map(p => p.projects)
      );
    else
      return this.idbService.getProjects(pageNumber, pageSize, desc);
  }

  getProject(id: string): Observable<ProjectDetails> {
    if (this.online) return this.get<ProjectDetails>(`projects/${id}`);
    else return this.idbService.getProject(id);
  }

  addProject(name: string) {
    return this.post<never>(`projects`, {
      name
    })
      .pipe(
        tap(_ => this.notificationService.notifySuccess(`Created new project: ${name}`)),
        catchError((err, _) => {
          this.notificationService.notifyError(err);
          return EMPTY;
        })
      );
  }

  getProjectsAmount(): Observable<number> {
    if (this.online) return this.get<number>('projects/amount');
    else return this.idbService.getProjectsAmount();
  }

  deleteProject(id: string, name:string) {
    return this.delete(`projects/${id}`)
      .pipe(
        tap(_ => this.notificationService.notifySuccess(`Deleted project ${name}`)),
        catchError(_ => {
          this.notificationService.notifyError(`Failed deletion of project ${name}`);
          return EMPTY;
        })
      );
  }
}
