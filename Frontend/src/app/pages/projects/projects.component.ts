import { Component, inject } from '@angular/core';
import { BasePageComponent } from '../../core/components/base-page/base-page.component';
import { BasePageHeaderComponent } from '../../core/components/base-page/base-page-header/base-page-header.component';
import { BasePageContentComponent } from '../../core/components/base-page/base-page-content/base-page-content.component';
import { BasePageFooterComponent } from '../../core/components/base-page/base-page-footer/base-page-footer.component';
import {
  MatAutocomplete,
  MatAutocompleteTrigger,
  MatOption,
} from '@angular/material/autocomplete';
import { MatFormField, MatLabel } from '@angular/material/form-field';
import { MatInput } from '@angular/material/input';
import { ReactiveFormsModule } from '@angular/forms';
import { MatList, MatListItem } from '@angular/material/list';
import { ProjectsService } from '../../core/projects/projects.service';
import {
  BehaviorSubject,
  catchError,
  EMPTY,
  map,
  Observable,
  switchMap,
} from 'rxjs';
import { AsyncPipe, NgTemplateOutlet } from '@angular/common';
import { MatProgressSpinner } from '@angular/material/progress-spinner';
import { MatPaginator } from '@angular/material/paginator';
import { Project } from '../../core/projects/projects.model';
import { MatDivider } from '@angular/material/divider';
import { MatRipple } from '@angular/material/core';
import {
  DataListComponent,
  DataListStateChangedEvent,
  PaginationOptions,
} from '../../core/components/data-list-component/data-list.component';
import { FailedToLoadProjectsError } from '../../core/errors/errors.model';
import { NotificationService } from '../../core/services/notification.service';

@Component({
  selector: 'app-projects',
  standalone: true,
  imports: [
    BasePageComponent,
    BasePageHeaderComponent,
    BasePageContentComponent,
    BasePageFooterComponent,
    MatAutocomplete,
    MatOption,
    MatFormField,
    MatInput,
    MatAutocompleteTrigger,
    ReactiveFormsModule,
    MatList,
    AsyncPipe,
    MatProgressSpinner,
    MatListItem,
    MatPaginator,
    MatDivider,
    MatRipple,
    MatLabel,
    DataListComponent,
    NgTemplateOutlet,
  ],
  templateUrl: './projects.component.html',
  styleUrl: './projects.component.scss',
})
export class ProjectsComponent {
  readonly dataListFiltersState =
    new BehaviorSubject<DataListStateChangedEvent>({
      paginationOptions: { pageNumber: 0, pageSize: 10, sortDirection: '' },
    });
  private readonly projectsService = inject(ProjectsService);
  private readonly notificationService = inject(NotificationService);
  projectsData$: Observable<{
    totalCount: number;
    data: readonly Project[];
    paginationOptions: PaginationOptions;
  }> = this.dataListFiltersState.pipe(
    switchMap(state =>
      this.projectsService
        .getProjects(
          state.paginationOptions.pageNumber,
          state.paginationOptions.pageSize,
          state.paginationOptions.sortDirection,
          state.searchPhrase
        )
        .pipe(
          map(r => ({
            projects: r.data,
            totalCount: r.totalCount,
            state,
          }))
        )
    ),
    map(r => ({
      data: r.projects,
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
