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

  constructor(http: HttpClient)
  {super(http);}

  getTags(pageNumber: number, pageSize: number, desc = true, all = false): Observable<Tag[]> {
    const url = all ? 'tags/all' : 'tags'
    return this.get<{ tags: Tag[] }>(url, new HttpParams({
      fromObject: {
        pageNumber,
        pageSize,
        desc
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

  getTagsAmount(): Observable<number> {
    return this.get<number>('tags/amount');
  }
}
