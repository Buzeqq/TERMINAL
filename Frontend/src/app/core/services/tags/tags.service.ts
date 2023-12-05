import { Injectable } from '@angular/core';
import { ApiService } from "../api-service";
import { HttpClient, HttpParams } from "@angular/common/http";
import {catchError, map, Observable} from "rxjs";
import {Tag} from "../../models/tags/tag";
import {TagDetails} from "../../models/tags/tag-details";
import {IndexedDbService} from "../indexed-db/indexed-db.service";
import {PingService} from "../ping/ping.service";

@Injectable({
  providedIn: 'root'
})
export class TagsService extends ApiService {

  private online = false;

  constructor(
    http: HttpClient,
    private readonly idbService: IndexedDbService,
    private readonly pingService: PingService
  ) {
    super(http);
    this.pingService.isOnline$.subscribe(r => this.online = r);
  }

  getTags(pageNumber: number, pageSize: number, desc = true, all = false): Observable<Tag[]> {
    const url = all ? 'tags/all' : 'tags'
    if (this.online) return this.get<{ tags: Tag[] }>(url, new HttpParams({
      fromObject: {
        pageNumber,
        pageSize,
        desc
      }
    })).pipe(
      map(t => t.tags)
    );
    else return this.idbService.getTags(pageNumber, pageSize, desc, all);
  }

  getTag(id: string): Observable<TagDetails> {
    if (this.online) return this.get<TagDetails>(`tags/${id}`)
      .pipe(
        catchError(this.handleError)
      );
    else return this.idbService.getTagById(id);
  }

  getTagsAmount(): Observable<number> {
    if (this.online) return this.get<number>('tags/amount');
    else return this.idbService.getTagsAmount();
  }
}
