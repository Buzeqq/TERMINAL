import { Injectable } from '@angular/core';
import { catchError, map, Observable } from "rxjs";
import { HttpClient } from "@angular/common/http";
import { Project } from "../../models/projects/project";
import { ApiService } from "../api-service";

@Injectable({
  providedIn: 'root'
})
export class ProjectsService extends ApiService {

  constructor(
    http: HttpClient,
  ) { super(http); }

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
}
