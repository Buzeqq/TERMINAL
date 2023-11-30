import { Injectable } from '@angular/core';
import { map, Observable, of, zip } from "rxjs";
import { Sample } from "../../models/samples/sample";
import { HttpClient, HttpParams } from "@angular/common/http";
import { ApiService } from "../api-service";
import { Project } from "../../models/projects/project";
import { Tag } from "../../models/tags/tag";
import {SamplesService} from "../samples/samples.service";
import {ProjectsService} from "../projects/projects.service";

@Injectable({
  providedIn: 'root'
})
export class SearchService extends ApiService {
  constructor(
    http: HttpClient,
    private readonly sampleService: SamplesService,
    private readonly projectService: ProjectsService
  ) { super(http); }

  public searchSamples(searchPhrase: string, pageNumber: number, pageSize: number): Observable<Sample[]> {
    return this.get<{ samples: Sample[] }>('samples/search', new HttpParams({
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
  }

  public searchProjects(searchPhrase: string, pageNumber: number, pageSize: number): Observable<Project[]> {
    return this.get<{ projects: Project[] }>('projects/search', new HttpParams({
      fromObject: {
        searchPhrase,
        pageNumber,
        pageSize
      }
    }))
      .pipe(
        map(p => p.projects)
      );
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

  // TODO recipes
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
      if (filterState['projects']) apiCalls.push(this.searchProjects(searchPhrase, pageNumber, pageSize).pipe(map(p => p.map(p => ({
        type: 'Project',
        item: p
      } as SearchItem)))));
    }

    return apiCalls.length > 0 ? zip(apiCalls).pipe(map(r => r.flat())) : of([]);
  }
}

export interface SearchItem {
  type: 'Sample' | 'Project' | 'Recipe';
  item: Sample | Project; // | Recipe
}
