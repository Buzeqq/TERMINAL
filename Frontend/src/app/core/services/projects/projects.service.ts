import { Injectable } from '@angular/core';
import { catchError, EMPTY, map, Observable, tap } from "rxjs";
import { HttpClient } from "@angular/common/http";
import { Project } from "../../models/projects/project";
import { ApiService } from "../api-service";
import { NotificationService } from "../notification/notification.service";

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

  getProjects(pageNumber: number, pageSize: number): Observable<Project[]> {
    return this.get<{ projects: Project[] }>(`projects?pageNumber=${pageNumber}&pageSize=${pageSize}`)
      .pipe(
        map(p => p.projects),
        catchError(this.handleError)
      );
  }

  getProject(id: string): Observable<Project> {
    return this.get<Project>(`projects/${id}`)
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
}
