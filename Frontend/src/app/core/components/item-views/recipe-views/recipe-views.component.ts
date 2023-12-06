import {AfterViewInit, Component, EventEmitter, Output, ViewChild} from '@angular/core';
import {Observable, tap} from "rxjs";
import { SelectedItem } from "../../../models/items/selected-item";
import { Recipe } from "../../../models/recipes/recipe";
import { RecipesService } from "../../../services/recipes/recipes.service";
import {MatTableDataSource} from "@angular/material/table";
import {MatSort, Sort} from "@angular/material/sort";
import {PageEvent} from "@angular/material/paginator";
import {ActivatedRoute} from "@angular/router";

@Component({
  selector: 'app-recipe-views',
  templateUrl: './recipe-views.component.html',
  styleUrls: ['./recipe-views.component.scss', '../item-views.component.scss']
})
export class RecipeViewsComponent implements AfterViewInit {
  displayedColumns: string[] = ['Name'];
  queryPageSize = 10;
  private queryPageIndex = 0;

  private orderDir = "desc";
  dataSource = new MatTableDataSource<Recipe>();
  length$?: Observable<number>;
  @ViewChild(MatSort) sort?: MatSort;

  selectedItem?: SelectedItem;
  @Output() selectedItemChangeEvent = new EventEmitter<SelectedItem>();

  constructor(
    private readonly recipesService: RecipesService,
    private readonly route: ActivatedRoute
  ) { }

  ngAfterViewInit(): void {
    this.loadData()
    this.route.queryParamMap.subscribe(params => {
      const id = params.get('recipeId');
      if (id) this.recipesService.getRecipe(id)
        .subscribe(r => this.selectRecipe(r));
    })
    this.length$ = this.recipesService.getRecipesAmount();
    this.dataSource.sort = this.sort!;
  }

  private loadData() {
    this.recipesService.getRecipes(this.queryPageIndex, this.queryPageSize, this.orderDir == "desc")
      .pipe(tap(r => {
        if (!this.selectedItem) this.selectRecipe(r[0], true);
      }))
      .subscribe(r => this.dataSource.data = r)
  }

  pageSelected(event: PageEvent) {
    if (this.queryPageIndex != event.pageIndex || this.queryPageSize != event.pageSize) {
      this.queryPageIndex = event.pageIndex
      this.queryPageSize = event.pageSize
      this.loadData();
    }
  }

  sortColumnChanged($event: Sort) {
    if (this.orderDir != $event.direction) {
      this.orderDir = $event.direction
      this.loadData();
    }
  }

  selectRecipe(recipe: Recipe, init = false) {
    this.selectedItem = { type: 'Recipe', id: recipe.id, config: {init: init} };
    this.selectedItemChangeEvent.emit(this.selectedItem);
  }
}
