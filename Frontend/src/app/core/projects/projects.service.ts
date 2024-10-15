import { inject, Injectable } from '@angular/core';
import { catchError, Observable } from 'rxjs';
import { HttpClient, HttpErrorResponse } from '@angular/common/http';
import { environment } from '../../../environments/environment';
import { Project } from './projects.model';
import { FailedToLoadProjectsError } from '../errors/errors.model';
import { PaginatedResponse } from '../common.model';
import { SortDirection } from '@angular/material/sort';

type ProjectsResponse = PaginatedResponse<Project>;

@Injectable({
  providedIn: 'root',
})
export class ProjectsService {
  private readonly http = inject(HttpClient);
  private readonly baseUrl = environment.apiUrl + '/projects';

  getProjects(
    pageNumber: number,
    pageSize: number,
    sortDirection?: SortDirection,
    searchPhrase?: string
  ): Observable<ProjectsResponse> {
    const params: Record<string, number | string | boolean> = {
      pageNumber,
      pageSize,
      orderDirection: sortDirection === 'desc' ? 1 : 0,
    };

    if (searchPhrase) {
      params['searchPhrase'] = searchPhrase;
    }

    return this.http
      .get<ProjectsResponse>(this.baseUrl + '/', {
        params: {
          ...params,
        },
      })
      .pipe(
        catchError((error: HttpErrorResponse) => {
          throw new FailedToLoadProjectsError(error.error);
        })
      );
  }
}
