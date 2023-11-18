import { Injectable } from '@angular/core';
import { ApiService } from "../api-service";
import { HttpClient, HttpParams } from "@angular/common/http";
import {catchError, map, Observable} from "rxjs";
import {Tag} from "../../models/tags/tag";
import {TagDetails} from "../../models/tags/tag-details";

@Injectable({
  providedIn: 'root'
})
export class TagsService extends ApiService {

  constructor(http: HttpClient) {
    super(http);
  }

  getTags(pageNumber: number, pageSize: number): Observable<Tag[]> {
    return this.get<{ tags: Tag[] }>('tags', new HttpParams({
      fromObject: {
        pageNumber,
        pageSize
      }
    })).pipe(
      map(t => t.tags)
    );
  }

  getTag(id: string): Observable<TagDetails> {
    return this.get<TagDetails>(`tags/${id}`)
      .pipe(
        catchError(this.handleError)
      );
  }
}
