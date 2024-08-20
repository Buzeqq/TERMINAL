import { Injectable } from '@angular/core';
import { catchError, Observable } from 'rxjs';
import { Recipe } from './recipe.model';
import { environment } from '../../../environments/environment';
import { HttpClient, HttpErrorResponse } from '@angular/common/http';
import { FailedToLoadRecipesError } from '../errors/errors.model';

type RecipesResponse = { recipes: Recipe[]; totalCount: number };

@Injectable({
  providedIn: 'root',
})
export class RecipesService {
  private readonly baseUrl = environment.apiUrl + '/recipes';

  constructor(private readonly http: HttpClient) {}

  getRecipes(
    pageNumber: number,
    pageSize: number,
    searchPhrase?: string,
    desc?: boolean,
  ): Observable<RecipesResponse> {
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
      .get<RecipesResponse>(this.baseUrl + '/', {
        params: {
          ...params,
        },
      })
      .pipe(
        catchError((error: HttpErrorResponse) => {
          throw new FailedToLoadRecipesError(error.error);
        }),
      );
  }
}
