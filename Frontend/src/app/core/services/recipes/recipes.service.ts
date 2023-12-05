import { Injectable } from '@angular/core';
import { ApiService } from "../api-service";
import { HttpClient, HttpParams } from "@angular/common/http";
import { map, Observable, tap } from "rxjs";
import { Step } from "../../models/steps/step";
import { RecipeDetails } from "../../models/recipes/recipeDetails";
import { Recipe } from "../../models/recipes/recipe";
import { AddRecipe } from "../../models/recipes/addRecipe";
import {IndexedDbService} from "../indexed-db/indexed-db.service";
import {PingService} from "../ping/ping.service";

@Injectable({
  providedIn: 'root'
})
export class RecipesService extends ApiService {

  private online = false;

  constructor(
    http: HttpClient,
    private readonly idbService: IndexedDbService,
    private readonly pingService: PingService
  ) {
    super(http);
    this.pingService.isOnline$.subscribe(r => this.online = r);
  }

  public getRecipes(pageNumber: number, pageSize: number): Observable<Recipe[]> {
    return this.get<{ recipes: Recipe[] }>('recipes', new HttpParams({ fromObject: {
      pageNumber,
      pageSize
    }})).pipe(
      map(r => r.recipes)
    );
    // else return this.idbService.getRecipes(pageNumber, pageSize);
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

  addRecipe(addRecipe: AddRecipe) {
    return this.post<never>(`recipes`, addRecipe).pipe(tap(console.log));
  }
}
