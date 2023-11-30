import { AfterViewInit, Component, EventEmitter, Output } from '@angular/core';
import { BehaviorSubject, tap } from "rxjs";
import { SelectedItem } from "../../../models/items/selected-item";
import { Recipe } from "../../../models/recipes/recipe";
import { RecipesService } from "../../../services/recipes/recipes.service";

@Component({
  selector: 'app-recipe-views',
  templateUrl: './recipe-views.component.html',
  styleUrls: ['./recipe-views.component.scss']
})
export class RecipeViewsComponent implements AfterViewInit {
  displayedColumns: string[] = ['name'];
  private readonly pageSize = 10;
  private page = 0;
  private readonly recipeSubject = new BehaviorSubject<Recipe[]>([]);
  recipes$= this.recipeSubject.asObservable();
  selectedItem?: SelectedItem;
  @Output() selectedItemChangeEvent = new EventEmitter<SelectedItem>();

  constructor(
    private readonly recipesService: RecipesService,
  ) { }

  ngAfterViewInit(): void {
    this.recipesService.getRecipes(this.page, this.pageSize)
      .pipe(tap(r => this.selectRecipe(r[0])))
      .subscribe(r => this.recipeSubject.next(r))
  }

  selectRecipe(recipe: Recipe) {
    this.selectedItem = { type: 'Recipe', id: recipe.id };
    this.selectedItemChangeEvent.emit(this.selectedItem);
  }

  onScroll(event: any) {
    if (event.target.offsetHeight + event.target.scrollTop >= event.target.scrollHeight) {
      this.page += 1;
      this.recipesService.getRecipes(this.page, this.pageSize)
        .subscribe(r =>
          this.recipeSubject.next(this.recipeSubject.value.concat(r))
        )
    }
  }
}
