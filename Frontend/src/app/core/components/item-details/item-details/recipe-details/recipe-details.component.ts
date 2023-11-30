import { Component, Input } from '@angular/core';
import { ItemDetailsComponent } from "../item-details.component";
import { catchError, EMPTY, map, Observable, tap } from "rxjs";
import { ActivatedRoute } from '@angular/router';
import { MatSnackBar } from "@angular/material/snack-bar";
import { RecipeDetails } from "../../../../models/recipes/recipeDetails";
import { RecipesService } from "../../../../services/recipes/recipes.service";

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
    private readonly snackBar: MatSnackBar,
  ) { super(route); }

  @Input()
  get recipeId(): string | undefined {
    return this._recipeId;
  }

  set recipeId(id: string | undefined) {
    this._recipeId = id || this.route.snapshot.paramMap.get('id') || undefined;
    if (!id) return;

    this.recipeDetails$ = this.recipesService.getRecipe(this._recipeId!)
      .pipe(
        catchError((err, _) => {
          console.log(err);
          this.snackBar.open('Failed to load project', 'Close', {
            duration: 3000
          });
          return EMPTY;
        }),
        tap(r => {
          this.numberOfSteps = r.steps.length;
          this.loading = 'determinate';
        })
      );
  }
}
