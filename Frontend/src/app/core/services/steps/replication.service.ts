import { Injectable } from '@angular/core';
import { SamplesService } from "../samples/samples.service";
import { RecipesService } from "../recipes/recipes.service";
import { EMPTY, map, Observable } from "rxjs";
import {RecipeDetails} from "../../models/recipes/recipeDetails";

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
          basedOn: d.code,
          comment: d.comment,
          recipe: d.recipe
          } as ReplicationData))
        );
    }

    if (query.type == 'Recipe') {
      return this.recipesService.getRecipe(query.id)
        .pipe(
          map(d => ({
            basedOn: d.name,
            comment: '',
            recipe: d
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
  basedOn: string; // display name
  comment: string;
  recipe: RecipeDetails;
}
