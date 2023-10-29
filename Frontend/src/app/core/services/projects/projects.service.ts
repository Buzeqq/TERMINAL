import { Injectable } from '@angular/core';
import {catchError, Observable} from "rxjs";
import {HttpClient} from "@angular/common/http";
import {Project} from "../../models/projects/project";
import {ApiService} from "../api-service";
import { map } from 'rxjs/operators';

@Injectable({
  providedIn: 'root'
})
export class ProjectsService extends ApiService {

  constructor(
    http: HttpClient,
  ) { super(http); }

  getAllProjects(): Observable<Project[]> {
    return this.get<any>("projects")
      .pipe(
        map(response => response["projects"] as Project[]),
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
