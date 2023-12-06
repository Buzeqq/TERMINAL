import { Injectable } from '@angular/core';
import { map, Observable, of, zip } from "rxjs";
import { Sample } from "../../models/samples/sample";
import { HttpClient, HttpParams } from "@angular/common/http";
import { ApiService } from "../api-service";
import { Project } from "../../models/projects/project";
import { Tag } from "../../models/tags/tag";
import {SamplesService} from "../samples/samples.service";
import {ProjectsService} from "../projects/projects.service";
import { Recipe } from "../../models/recipes/recipe";
import {PingService} from "../ping/ping.service";
import {IndexedDbService} from "../indexed-db/indexed-db.service";

@Injectable({
  providedIn: 'root'
})
export class SearchService extends ApiService {

  online = false;
  constructor(
    http: HttpClient,
    private readonly sampleService: SamplesService,
    private readonly projectService: ProjectsService,
    private readonly pingService: PingService,
    private readonly idbService: IndexedDbService,
  ) {
    super(http);
    this.pingService.isOnline$.subscribe(r => this.online = r);
  }

  public searchSamples(searchPhrase: string, pageNumber: number, pageSize: number): Observable<Sample[]> {
    if (this.online) return this.get<{ samples: Sample[] }>('samples/search', new HttpParams({
      fromObject: {
        searchPhrase,
        pageNumber,
        pageSize
      }
    }))
      .pipe(
        map(s => s.samples),
        map(samples => samples.map(s => ({
          ...s,
          createdAtUtc: new Date(s.createdAtUtc)
        })))
      );
    else return this.idbService.searchSamples(searchPhrase, pageNumber, pageSize)
      .pipe(
        map(samples => samples.map(s => ({
          ...s,
          createdAtUtc: new Date(s.createdAtUtc)
        })))
      );
  }

  public searchProjects(searchPhrase: string, pageNumber: number, pageSize: number): Observable<Project[]> {
    if (this.online) return this.get<{ projects: Project[] }>('projects/search', new HttpParams({
      fromObject: {
        searchPhrase,
        pageNumber,
        pageSize
      }
    }))
      .pipe(
        map(p => p.projects)
      );
    else return this.idbService.searchProjects(searchPhrase, pageNumber, pageSize);
  }

  searchRecipe(searchPhrase: string, pageNumber: number, pageSize: number): Observable<Recipe[]> {
    if (this.online) return this.get<{ recipes: Recipe[] }>('recipes/search', new HttpParams({
      fromObject: {
        searchPhrase,
        pageNumber,
        pageSize
      }
    }))
      .pipe(
        map(s => s.recipes)
      );
    else return this.idbService.searchRecipes(searchPhrase, pageNumber, pageSize);
  }

  public searchTags(searchPhrase: string, pageNumber: number, pageSize: number): Observable<Tag[]> {
    return this.get<{ tags: Tag[] }>('tags/search', new HttpParams({
      fromObject: {
        searchPhrase,
        pageNumber,
        pageSize
      }
    }))
      .pipe(
        map(t => t.tags)
      );
  }

  public searchIn(filterState: Record<'samples' | 'recipes' | 'projects', boolean>, searchPhrase: string, pageNumber: number, pageSize: number): Observable<SearchItem[]> {
    const apiCalls = [];
    if (searchPhrase == '') {
      if (filterState['samples']) apiCalls.push(this.sampleService.getSamples(pageNumber, pageSize).pipe(map(samples => samples.map(s => ({
        type: 'Sample',
        item: s
      } as SearchItem)))));
      if (filterState['projects']) apiCalls.push(this.projectService.getProjects(pageNumber, pageSize).pipe(map(p => p.map(p => ({
        type: 'Project',
        item: p
      } as SearchItem)))));
    } else {
      if (filterState['samples']) apiCalls.push(this.searchSamples(searchPhrase, pageNumber, pageSize).pipe(map(samples => samples.map(s => ({
        type: 'Sample',
        item: s
      } as SearchItem)))));
      if (filterState['recipes']) apiCalls.push(this.searchRecipe(searchPhrase, pageNumber, pageSize).pipe(map(r => r.map(r => ({
        type: 'Recipe',
        item: r
      } as SearchItem)))));
    }

    return apiCalls.length > 0 ? zip(apiCalls).pipe(map(r => r.flat())) : of([]);
  }
}

export interface SearchItem {
  type: 'Sample' | 'Project' | 'Recipe';
  item: Sample | Project | Recipe;
}
