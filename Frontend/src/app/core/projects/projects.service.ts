import { inject, Injectable } from '@angular/core';
import { catchError, Observable } from 'rxjs';
import { HttpClient, HttpErrorResponse } from '@angular/common/http';
import { environment } from '../../../environments/environment';
import { Project } from './projects.model';
import { FailedToLoadProjectsError } from '../errors/errors.model';
import { PaginatedResponse } from '../common.model';

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
    searchPhrase?: string,
    desc?: boolean
  ): Observable<ProjectsResponse> {
    desc ??= true;
    const params: Record<string, number | string | boolean> = {
      pageNumber,
      pageSize,
      desc,
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
