import { inject, Injectable } from '@angular/core';
import { Observable } from "rxjs";
import { HttpClient } from "@angular/common/http";
import { environment } from "../../../environments/environment";
import { Project } from "./projects.model";

@Injectable({
  providedIn: 'root'
})
export class ProjectsService {
  private readonly http = inject(HttpClient);
  private readonly baseUrl = environment.apiUrl + '/projects';

  getProjects(pageNumber: number, pageSize: number, desc = true)
    : Observable<{ projects: Project[], totalAmount: number }> {
    return this.http.get<{ projects: Project[], totalAmount: number }>(this.baseUrl + '/', {
      params: {
        pageNumber,
        pageSize,
        desc
      }
    });
  }
}
