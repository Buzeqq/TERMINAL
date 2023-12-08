import { Injectable } from '@angular/core';
import { SamplesService } from "../samples/samples.service";
import { RecipesService } from "../recipes/recipes.service";
import { EMPTY, map, Observable } from "rxjs";
import {Tag} from "../../models/tags/tag";
import {StepDetails} from "../../models/steps/stepDetails";
import {Recipe} from "../../models/recipes/recipe";

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
          type: 'Sample',
          basedOn: {
            id: d.id,
            name: d.code
          },
          comment: d.comment,
          steps: d.steps,
          projectId: d.projectId,
          tags: d.tags
          } as ReplicationData))
        );
    }

    if (query.type == 'Recipe') {
      return this.recipesService.getRecipe(query.id)
        .pipe(
          map(d => ({
            type: 'Recipe',
            basedOn: {
              id: d.id,
              name: d.name
            },
            comment: '',
            steps: d.steps
          }))
        );
    }
    if (query.type == 'EditSample') {
      return this.samplesService.getSampleDetails(query.id)
        .pipe(
          map(d => ({
            type: 'Sample',
            basedOn: {
              id: d.id,
              name: d.code
            },
            comment: d.comment,
            steps: d.steps,
            projectId: d.projectId,
            tags: d.tags,
            recipe: d.recipe
          } as ReplicationData))
        );
    }

    return EMPTY;
  }
}

export interface ReplicateQuery {
  type: 'Sample' | 'Recipe' | 'EditSample';
  id: string;
}

export interface ReplicationData {
  type: 'Sample' | 'Recipe';
  basedOn: {
    id: string;
    name: string;
  };
  comment: string;
  steps: StepDetails[];
  projectId?: string;
  tags?: Tag[];
  recipe?: Recipe | null;
}
