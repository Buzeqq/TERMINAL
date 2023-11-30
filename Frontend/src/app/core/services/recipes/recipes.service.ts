import { Injectable } from '@angular/core';
import { ApiService } from "../api-service";
import { HttpClient, HttpParams } from "@angular/common/http";
import { map, Observable } from "rxjs";
import { Step } from "../../models/steps/step";
import { RecipeDetails } from "../../models/recipes/recipeDetails";
import { Recipe } from "../../models/recipes/recipe";

@Injectable({
  providedIn: 'root'
})
export class RecipesService extends ApiService {
  constructor(http: HttpClient) {
    super(http);
  }

  public getRecipes(pageNumber: number, pageSize: number): Observable<Recipe[]> {
    return this.get<{ recipes: Recipe[] }>('recipes', new HttpParams({ fromObject: {
      pageNumber,
      pageSize
    }})).pipe(
      map(r => r.recipes)
    );
  }

  public getRecipe(id: string): Observable<RecipeDetails> {
    return this.get<RecipeDetails>(`recipes/${id}/details`);
  }

  public getRecipeSteps(id: string) : Observable<Step[]> {
    return this.get<RecipeDetails>(`recipes/${id}/details`)
      .pipe(
        map(r => r.steps)
      );
  }
}
