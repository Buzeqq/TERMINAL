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
  combineLatestWith,
  EMPTY,
  map,
  Observable,
  switchMap,
} from 'rxjs';
import { AsyncPipe } from '@angular/common';
import { MatProgressSpinner } from '@angular/material/progress-spinner';
import { MatPaginator } from '@angular/material/paginator';
import { Project } from '../../core/projects/projects.model';
import { MatDivider } from '@angular/material/divider';
import { MatRipple } from '@angular/material/core';
import {
  DataListComponentComponent,
  DataListStateChangedEvent,
  PaginationOptions,
} from '../../core/components/data-list-component/data-list-component.component';
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
    DataListComponentComponent,
  ],
  templateUrl: './projects.component.html',
  styleUrl: './projects.component.scss',
})
export class ProjectsComponent {
  private readonly projectsService = inject(ProjectsService);
  private readonly notificationService = inject(NotificationService);

  private paginationOptions = new BehaviorSubject<PaginationOptions>({
    pageNumber: 0,
    pageSize: 10,
    desc: true,
  });
  private searchPhrase = new BehaviorSubject<string | undefined>(undefined);

  projectsData$: Observable<{
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
        .getProjects(
          paginationOptions.pageNumber,
          paginationOptions.pageSize,
          searchPhrase,
          paginationOptions.desc,
        )
        .pipe(
          map((data) => ({
            paginationOptions: paginationOptions,
            data: data.projects,
            totalCount: data.totalCount,
          })),
          catchError((error: FailedToLoadProjectsError) => {
            this.notificationService.notifyError(error.message);
            return EMPTY;
          }),
        ),
    ),
  );

  onPageChange(event: DataListStateChangedEvent) {
    this.paginationOptions.next(event.paginationOptions);
    this.searchPhrase.next(event.searchPhrase);
  }
}
