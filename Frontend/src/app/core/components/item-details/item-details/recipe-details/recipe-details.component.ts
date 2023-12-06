import { Component, Input } from '@angular/core';
import { ItemDetailsComponent } from "../item-details.component";
import { catchError, EMPTY, Observable, tap } from "rxjs";
import { ActivatedRoute } from '@angular/router';
import { RecipeDetails } from "../../../../models/recipes/recipeDetails";
import { RecipesService } from "../../../../services/recipes/recipes.service";
import {NotificationService} from "../../../../services/notification/notification.service";

@Component({
  selector: 'app-recipe-details',
  templateUrl: './recipe-details.component.html',
  styleUrls: ['./recipe-details.component.scss']
})
export class RecipeDetailsComponent extends ItemDetailsComponent {
  private _recipeId?: string;
  numberOfSteps?: number;
  recipeDetails$: Observable<RecipeDetails> = new Observable<RecipeDetails>();

  constructor(
    private readonly recipesService: RecipesService,
    protected override readonly route: ActivatedRoute,
    private readonly notificationService: NotificationService
  ) { super(route); }

  @Input()
  get recipeId(): string | undefined {
    return this._recipeId;
  }

  set recipeId(id: string | undefined) {
    this._recipeId = id || this.route.snapshot.paramMap.get('id') || undefined;
    if (!this._recipeId) return;

    this.recipeDetails$ = this.recipesService.getRecipe(this._recipeId!)
      .pipe(
        catchError((err, _) => {
          console.log(err);
          this.notificationService.notifyError('Failed to load recipe');
          return EMPTY;
        }),
        tap(r => {
          this.numberOfSteps = r.steps.length;
          this.loading = 'determinate';
        })
      );
  }
}
