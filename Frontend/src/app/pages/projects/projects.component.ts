import { Component, inject } from '@angular/core';
import { BasePageComponent } from "../../core/components/base-page/base-page.component";
import { BasePageHeaderComponent } from "../../core/components/base-page/base-page-header/base-page-header.component";
import {
  BasePageContentComponent
} from "../../core/components/base-page/base-page-content/base-page-content.component";
import { BasePageFooterComponent } from "../../core/components/base-page/base-page-footer/base-page-footer.component";
import { MatAutocomplete, MatAutocompleteTrigger, MatOption } from "@angular/material/autocomplete";
import { MatFormField } from "@angular/material/form-field";
import { MatInput } from "@angular/material/input";
import { ReactiveFormsModule } from "@angular/forms";
import { MatList, MatListItem } from "@angular/material/list";
import { ProjectsService } from "../../core/projects/projects.service";
import { BehaviorSubject, map, Observable, switchMap, tap } from "rxjs";
import { AsyncPipe } from "@angular/common";
import { MatProgressSpinner } from "@angular/material/progress-spinner";
import { MatPaginator, PageEvent } from "@angular/material/paginator";
import { Project } from "../../core/projects/projects.model";
import { MatDivider } from "@angular/material/divider";
import { MatRipple } from "@angular/material/core";

export interface PaginationOptions {
  pageNumber: number;
  pageSize: number;
  desc: boolean;
}

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
    MatRipple
  ],
  templateUrl: './projects.component.html',
  styleUrl: './projects.component.scss'
})
export class ProjectsComponent {
  private readonly projectsService = inject(ProjectsService);
  private paginationOptions = new BehaviorSubject<PaginationOptions>({
    pageNumber: 0,
    pageSize: 10,
    desc: true
  });

  projectsData$: Observable<{
    totalCount: number;
    projects: Project[];
    paginationOptions: PaginationOptions;
  }> = this.paginationOptions.asObservable().pipe(
    switchMap(paginationOptions =>
      this.projectsService.getProjects(paginationOptions.pageNumber, paginationOptions.pageSize, paginationOptions.desc)
        .pipe(
          map(data => ({...data, paginationOptions: paginationOptions })),
          tap(console.log)
        ))
  );

  onPageChange(event: PageEvent) {
    this.paginationOptions.next({
      ...this.paginationOptions.value,
      pageNumber: event.pageIndex,
      pageSize: event.pageSize,
    });
  }
}
