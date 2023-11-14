import { Injectable } from '@angular/core';
import { ApiService } from "../api-service";
import { HttpClient, HttpParams } from "@angular/common/http";
import { map, Observable } from "rxjs";

@Injectable({
  providedIn: 'root'
})
export class TagsService extends ApiService {

  constructor(http: HttpClient) {
    super(http);
  }

  getTags(pageNumber: number, pageSize: number): Observable<string[]> {
    return this.get<{ tags: string[] }>('tags', new HttpParams({
      fromObject: {
        pageNumber,
        pageSize
      }
    })).pipe(
      map(t => t.tags)
    );
  }
}
