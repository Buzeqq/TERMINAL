import { Injectable } from '@angular/core';
import { SamplesService } from "../samples/samples.service";
import { RecipesService } from "../recipes/recipes.service";
import { EMPTY, map, Observable } from "rxjs";
import { Step } from "../../models/steps/step";

@Injectable({
  providedIn: 'root'
})
export class ReplicationService {
  constructor(
    private readonly samplesService: SamplesService,
    private readonly recipesService: RecipesService) {
  }

  public getReplicationData(query: ReplicateQuery): Observable<ReplicationData> {
    if (query.type == 'Sample') {
      return this.samplesService.getSampleDetails(query.id)
        .pipe(
          map(d => ({
          basedOn: {
            name: d.comment,
            id: d.id
          },
          comment: d.comment,
          steps: d.steps,
          type: query.type
          } as ReplicationData))
        );
    }

    if (query.type == 'Recipe') {
      return this.recipesService.getRecipe(query.id)
        .pipe(
          map(d => ({
            basedOn: {
              name: d.name,
              id: d.id,
            },
            comment: '',
            steps: d.steps,
            type: query.type
          }))
        );
    }

    return EMPTY;
  }
}

export interface ReplicateQuery {
  type: 'Sample' | 'Recipe';
  id: string;
}

export interface ReplicationData {
  basedOn: {
    name: string;
    id: string;
  };
  type: 'Sample' | 'Recipe';
  comment: string;
  steps: Step[];
}
