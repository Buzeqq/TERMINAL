import { Injectable } from '@angular/core';
import { SamplesService } from "../samples/samples.service";
import { RecipesService } from "../recipes/recipes.service";
import { EMPTY, map, Observable } from "rxjs";
import { RecipeDetails} from "../../models/recipes/recipeDetails";
import { Step } from "../../models/steps/step";
import { SampleDetails } from "../../models/samples/sampleDetails";

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
          steps: d.steps
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

    return EMPTY;
  }
}

export interface ReplicateQuery {
  type: 'Sample' | 'Recipe';
  id: string;
}

export interface ReplicationData {
  type: 'Sample' | 'Recipe';
  basedOn: {
    id: string;
    name: string;
  };
  comment: string;
  steps: Step[];
}
