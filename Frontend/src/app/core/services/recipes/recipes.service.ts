import { Injectable } from '@angular/core';
import { ApiService } from "../api-service";
import { HttpClient } from "@angular/common/http";
import { map, Observable } from "rxjs";
import { Step } from "../../models/steps/step";
import { RecipeDetails } from "../../models/recipes/recipeDetails";

@Injectable({
  providedIn: 'root'
})
export class RecipesService extends ApiService {
  constructor(http: HttpClient) {
    super(http);
  }

  public getRecipeSteps(id: string) : Observable<Step[]> {
    return this.get<RecipeDetails>(`api/recipe/${id}/details`)
      .pipe(
        map(r => r.steps)
      );
  }
}
