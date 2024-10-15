import { Component, inject } from '@angular/core';
import { BasePageComponent } from '../../core/components/base-page/base-page.component';
import { BasePageContentComponent } from '../../core/components/base-page/base-page-content/base-page-content.component';
import { BasePageFooterComponent } from '../../core/components/base-page/base-page-footer/base-page-footer.component';
import { BasePageHeaderComponent } from '../../core/components/base-page/base-page-header/base-page-header.component';
import {
  DataListComponent,
  DataListStateChangedEvent,
  PaginationOptions,
} from '../../core/components/data-list-component/data-list.component';
import {
  BehaviorSubject,
  catchError,
  EMPTY,
  map,
  Observable,
  switchMap,
} from 'rxjs';
import { RecipesService } from '../../core/recipes/recipes.service';
import { NotificationService } from '../../core/services/notification.service';
import { Recipe } from '../../core/recipes/recipe.model';
import { FailedToLoadProjectsError } from '../../core/errors/errors.model';

@Component({
  selector: 'app-recipes',
  standalone: true,
  imports: [
    BasePageComponent,
    BasePageContentComponent,
    BasePageFooterComponent,
    BasePageHeaderComponent,
    DataListComponent,
  ],
  templateUrl: './recipes.component.html',
  styleUrl: './recipes.component.scss',
})
export class RecipesComponent {
  readonly dataListFiltersState =
    new BehaviorSubject<DataListStateChangedEvent>({
      paginationOptions: { pageNumber: 0, pageSize: 10, sortDirection: '' },
    });
  private readonly recipesService = inject(RecipesService);
  private readonly notificationService = inject(NotificationService);
  recipesData$: Observable<{
    totalCount: number;
    data: Recipe[];
    paginationOptions: PaginationOptions;
  }> = this.dataListFiltersState.pipe(
    switchMap(state =>
      this.recipesService
        .getRecipes(
          state.paginationOptions.pageNumber,
          state.paginationOptions.pageSize,
          state.searchPhrase,
          state.paginationOptions.sortDirection
        )
        .pipe(
          map(r => ({
            recipes: r.recipes,
            totalCount: r.totalCount,
            state,
          }))
        )
    ),
    map(r => ({
      data: r.recipes,
      totalCount: r.totalCount,
      paginationOptions: {
        pageNumber: r.state.paginationOptions.pageNumber,
        pageSize: r.state.paginationOptions.pageSize,
        sortDirection: r.state.paginationOptions.sortDirection,
      },
    })),
    catchError((error: FailedToLoadProjectsError) => {
      this.notificationService.notifyError(error.message);
      return EMPTY;
    })
  );
}
