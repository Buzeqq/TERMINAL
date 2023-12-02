import { Injectable } from '@angular/core';
import { catchError, EMPTY, map, Observable, tap } from "rxjs";
import {HttpClient, HttpParams} from "@angular/common/http";
import { Project } from "../../models/projects/project";
import { ApiService } from "../api-service";
import { NotificationService } from "../notification/notification.service";
import {ProjectDetails} from "../../models/projects/project-details";

@Injectable({
  providedIn: 'root'
})
export class ProjectsService extends ApiService {

  constructor(
    http: HttpClient,
    private readonly notificationService: NotificationService
  ) {
    super(http);
  }

  getProjects(pageNumber: number, pageSize: number, desc = true): Observable<Project[]> {
    return this.get<{ projects: Project[] }>('projects', new HttpParams({
      fromObject: {
        pageNumber,
        pageSize,
        desc
      }
    }))
      .pipe(
        map(p => p.projects),
        catchError(this.handleError)
      );
  }

  getProject(id: string): Observable<ProjectDetails> {
    return this.get<ProjectDetails>(`projects/${id}`)
      .pipe(
        catchError(this.handleError)
      );
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
    return this.get<number>('projects/amount');
  }
}
