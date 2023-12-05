import { Injectable } from '@angular/core';
import {db} from "../../../db/terminal-db";
import {Tag} from "../../../models/tags/tag";
import {TagEntity} from "../../../db/tables/tag-entity";
import {TagDetails} from "../../../models/tags/tag-details";

@Injectable({
  providedIn: 'root'
})
export class IdbTagsService {

  constructor() { }

  async getTags(pageIndex: number, pageSize: number, desc = true, all = false) {
    let query = db.tags.orderBy('name');
    if (desc) query = query.reverse();
    const entities = await query
      .filter(e => e.isActive == 1 || e.isActive == (all ? 0 : 1))
      .offset(pageIndex)
      .limit(pageSize)
      .toArray();
    return this.entitiesToTags(entities);
  }

  async getTagById(id: string) {
    return this.entityToTag( await db.tags.where('id').equals(id).first() );
  }

  async getTagsAmount() {
    return db.tags.count();
  }

  async addTags(tags: Tag[]) {
    await db.tags.bulkPut(this.tagsToEntities(tags));
  }

  private entitiesToTags(entities: TagEntity[]): Tag[] {
    return entities.map(e => ({id: e.id, name: e.name}));
  }

  private entityToTag(e?: TagEntity): TagDetails {
    return e ?
      {id: e.id, name: e.name, isActive: !!e.isActive} :
      {id: 'not_found', name: 'not_found', isActive: false};
  }

  private tagsToEntities(tags: Tag[]): TagEntity[] {
    return tags.map(t => ({...t, isActive: 1}))
  }
}
