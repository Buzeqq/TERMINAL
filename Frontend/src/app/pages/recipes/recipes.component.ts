import { Component, inject } from '@angular/core';
import { BasePageComponent } from '../../core/components/base-page/base-page.component';
import { BasePageContentComponent } from '../../core/components/base-page/base-page-content/base-page-content.component';
import { BasePageFooterComponent } from '../../core/components/base-page/base-page-footer/base-page-footer.component';
import { BasePageHeaderComponent } from '../../core/components/base-page/base-page-header/base-page-header.component';
import {
  DataListComponentComponent,
  DataListStateChangedEvent,
  PaginationOptions,
} from '../../core/components/data-list-component/data-list-component.component';
import {
  BehaviorSubject,
  catchError,
  combineLatestWith,
  EMPTY,
  map,
  Observable,
  switchMap,
} from 'rxjs';
import { Project } from '../../core/projects/projects.model';
import { RecipesService } from '../../core/recipes/recipes.service';
import { FailedToLoadRecipesError } from '../../core/errors/errors.model';
import { NotificationService } from '../../core/services/notification.service';

@Component({
  selector: 'app-recipes',
  standalone: true,
  imports: [
    BasePageComponent,
    BasePageContentComponent,
    BasePageFooterComponent,
    BasePageHeaderComponent,
    DataListComponentComponent,
  ],
  templateUrl: './recipes.component.html',
  styleUrl: './recipes.component.scss',
})
export class RecipesComponent {
  private readonly projectsService = inject(RecipesService);
  private readonly notificationService = inject(NotificationService);

  private paginationOptions = new BehaviorSubject<PaginationOptions>({
    pageNumber: 0,
    pageSize: 10,
    desc: true,
  });
  private searchPhrase = new BehaviorSubject<string | undefined>(undefined);

  recipesData$: Observable<{
    totalCount: number;
    data: Project[];
    paginationOptions: PaginationOptions;
  }> = this.paginationOptions.asObservable().pipe(
    combineLatestWith(this.searchPhrase.asObservable()),
    map(([paginationOptions, searchPhrase]) => ({
      paginationOptions,
      searchPhrase,
    })),
    switchMap(({ paginationOptions, searchPhrase }) =>
      this.projectsService
        .getRecipes(
          paginationOptions.pageNumber,
          paginationOptions.pageSize,
          searchPhrase,
          paginationOptions.desc,
        )
        .pipe(
          map((data) => ({
            paginationOptions: paginationOptions,
            data: data.recipes,
            totalCount: data.totalCount,
          })),
        ),
    ),
    catchError((error: FailedToLoadRecipesError) => {
      this.notificationService.notifyError(error.message);
      return EMPTY;
    }),
  );

  onPageChange(event: DataListStateChangedEvent) {
    this.paginationOptions.next(event.paginationOptions);
    this.searchPhrase.next(event.searchPhrase);
  }
}
