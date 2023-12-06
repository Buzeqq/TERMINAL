import { Injectable } from '@angular/core';
import { ApiService } from "../api-service";
import { HttpClient, HttpParams } from "@angular/common/http";
import { map, Observable, tap } from "rxjs";
import { RecipeDetails } from "../../models/recipes/recipeDetails";
import { Recipe } from "../../models/recipes/recipe";
import { AddRecipe } from "../../models/recipes/addRecipe";

@Injectable({
  providedIn: 'root'
})
export class RecipesService extends ApiService {
  constructor(http: HttpClient) {
    super(http);
  }

  public getRecipes(pageNumber: number, pageSize: number, desc = true): Observable<Recipe[]> {
    return this.get<{ recipes: Recipe[] }>('recipes', new HttpParams({
      fromObject: {
        pageNumber,
        pageSize,
        desc
    }})).pipe(
      map(r => r.recipes)
    );
  }

  public getRecipe(id: string): Observable<RecipeDetails> {
    return this.get<RecipeDetails>(`recipes/${id}/details`);
  }

  addRecipe(addRecipe: AddRecipe) {
    return this.post<never>(`recipes`, addRecipe).pipe(tap(console.log));
  }

  getRecipesAmount(): Observable<number> {
    return this.get<number>('recipes/amount');
  }
}
