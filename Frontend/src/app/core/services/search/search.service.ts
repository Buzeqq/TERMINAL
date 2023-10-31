import { Injectable } from '@angular/core';
import { map, Observable, zip } from "rxjs";
import { Measurement } from "../../models/measurements/measurement";
import { HttpClient, HttpParams } from "@angular/common/http";
import { ApiService } from "../api-service";
import { Project } from "../../models/projects/project";

@Injectable({
  providedIn: 'root'
})
export class SearchService extends ApiService {

  constructor(http: HttpClient) { super(http); }

  public searchMeasurements(searchPhrase: string, pageNumber: number, pageSize: number): Observable<Measurement[]> {
    // const params = new HttpParams().appendAll({
    //   'phrase': searchPhrase,
    //   'pageNumber': pageNumber,
    //   'pageSize': pageSize
    // });
    return this.get<{measurements: Measurement[]}>('measurements/search')
      .pipe(
        map(m => m.measurements),
        map(m => m.map(m => ({
          ...m,
          createdAtUtc: new Date(m.createdAtUtc)
        })))
      );
  }

  public searchProjects(searchPhrase: string, pageNumber: number, pageSize: number): Observable<Project[]> {
    // const params = new HttpParams().appendAll({
    //   'searchPhrase': searchPhrase,
    //   'pageNumber': pageNumber,
    //   'pageSize': pageSize
    // });
    return this.get<{projects: Project[]}>('projects/search')
      .pipe(
        map(p => p.projects)
      );
  }

  public searchIn(filterState: Record<'measurements' | 'recipes' | 'projects', boolean>, searchPhrase: string, pageNumber: number, pageSize: number): Observable<SearchItem[]> {
    const apiCalls = [];
    if (filterState['measurements']) apiCalls.push(this.searchMeasurements(searchPhrase, pageNumber, pageSize).pipe(map(m => m.map(m => ({
      type: 'Measurement',
      item: m
    } as SearchItem)))));
    // if (filterState['recipes']) apiCalls.push(this.searchRecipes(searchPhrase));
    if (filterState['projects']) apiCalls.push(this.searchProjects(searchPhrase, pageNumber, pageSize).pipe(map(p => p.map(p => ({
      type: 'Project',
      item: p
    } as SearchItem)))));

    return zip(apiCalls).pipe(map(r => r.flat()));
  }
}

export interface SearchItem {
  type: 'Measurement' | 'Project' | 'Recipe';
  item: Measurement | Project; // | Recipe
}
