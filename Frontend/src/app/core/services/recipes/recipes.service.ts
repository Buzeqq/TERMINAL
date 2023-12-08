import { Injectable } from '@angular/core';
import { ApiService } from "../api-service";
import { HttpClient, HttpParams } from "@angular/common/http";
import {catchError, EMPTY, map, Observable, tap} from "rxjs";
import { RecipeDetails } from "../../models/recipes/recipeDetails";
import { Recipe } from "../../models/recipes/recipe";
import { AddRecipe } from "../../models/recipes/addRecipe";
import {IndexedDbService} from "../indexed-db/indexed-db.service";
import {PingService} from "../ping/ping.service";
import {NotificationService} from "../notification/notification.service";

@Injectable({
  providedIn: 'root'
})
export class RecipesService extends ApiService {

  private online = false;

  constructor(
    http: HttpClient,
    private readonly idbService: IndexedDbService,
    private readonly pingService: PingService,
    private readonly notificationService: NotificationService,
  ) {
    super(http);
    this.pingService.isOnline$.subscribe(r => this.online = r);
  }

  public getRecipes(pageNumber: number, pageSize: number, desc = true): Observable<Recipe[]> {
    if (this.online) return this.get<{ recipes: Recipe[] }>('recipes', new HttpParams({
      fromObject: {
        pageNumber,
        pageSize,
        desc
    }})).pipe(
      map(r => r.recipes)
    );
    else return this.idbService.getRecipes(pageNumber, pageSize, desc);
  }

  public getRecipe(id: string): Observable<RecipeDetails> {
    if (this.online) return this.get<RecipeDetails>(`recipes/${id}/details`);
    else return this.idbService.getRecipe(id);
  }

  addRecipe(addRecipe: AddRecipe) {
    return this.post<never>(`recipes`, addRecipe);
  }

  getRecipesAmount(): Observable<number> {
    if (this.online) return this.get<number>('recipes/amount');
    else return this.idbService.getRecipesAmount();
  }

  deleteRecipe(id: string, name: string) {
    return this.delete(`recipes/${id}`)
      .pipe(
        tap(_ => this.notificationService.notifySuccess(`Deleted recipe ${name}. Reload page to see changes.`)),
        catchError(_ => {
          this.notificationService.notifyError(`Failed deleting recipe ${name}. Check your network connection.`)
          return EMPTY;
        })
      );
  }

  editRecipe(id: string, data: RecipeDetails) {
    return this.patch(`recipes/${id}`, data)
      .pipe(
        tap(_ => this.notificationService.notifyUpdated('Recipe')),
        catchError(_ => {
          this.notificationService.notifyUpdatingFailed('Recipe');
          return EMPTY;
        })
      )
  }
}
