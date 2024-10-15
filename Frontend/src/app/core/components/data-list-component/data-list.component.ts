import {
  Component,
  EventEmitter,
  Input,
  OnInit,
  Output,
  signal,
  TemplateRef,
} from '@angular/core';
import { FormControl, FormGroup, ReactiveFormsModule } from '@angular/forms';
import { MatPaginator, PageEvent } from '@angular/material/paginator';
import { BehaviorSubject, catchError, Observable, tap } from 'rxjs';
import { MatFormField, MatLabel } from '@angular/material/form-field';
import { MatInput } from '@angular/material/input';
import { AsyncPipe, NgTemplateOutlet } from '@angular/common';
import { MatList, MatListItem } from '@angular/material/list';
import { MatDivider } from '@angular/material/divider';
import { MatRipple } from '@angular/material/core';
import { MatProgressSpinner } from '@angular/material/progress-spinner';
import { Entity } from '../../common.model';
import { HintComponent } from '../hint/hint.component';
import { SortDirection } from '@angular/material/sort';

export type DataListStateChangedEvent = {
  paginationOptions: PaginationOptions;
  searchPhrase?: string;
};

export interface PaginationOptions {
  pageNumber: number;
  pageSize: number;
  sortDirection: SortDirection;
}

export type DataListDataSourceInput<T extends Entity> = Observable<{
  data: readonly T[];
  paginationOptions: PaginationOptions;
  totalCount: number;
  searchPhrase?: string;
}>;

@Component({
  selector: 'app-data-list-component',
  standalone: true,
  imports: [
    MatFormField,
    MatInput,
    ReactiveFormsModule,
    AsyncPipe,
    MatList,
    MatDivider,
    MatListItem,
    MatRipple,
    MatPaginator,
    MatProgressSpinner,
    MatLabel,
    NgTemplateOutlet,
    HintComponent,
  ],
  templateUrl: './data-list.component.html',
  styleUrl: './data-list.component.scss',
})
export class DataListComponent<TData extends Entity> implements OnInit {
  @Input({ required: true, alias: 'data' })
  dataSource$!: DataListDataSourceInput<TData>;
  @Input({ required: true }) rowTemplate!: TemplateRef<{ row: TData }>;

  @Output() filterChanged = new EventEmitter<DataListStateChangedEvent>();

  readonly formGroup = new FormGroup({
    searchPhrase: new FormControl(''),
  });

  isLoading = signal(true);

  private readonly filterState = new BehaviorSubject<DataListStateChangedEvent>(
    {
      paginationOptions: {
        pageNumber: 0,
        pageSize: 10,
        sortDirection: 'asc',
      },
    }
  );

  ngOnInit() {
    this.filterChanged.emit({
      paginationOptions: this.filterState.value.paginationOptions,
    });

    this.dataSource$ = this.dataSource$.pipe(
      tap(() => this.isLoading.set(false)),
      catchError(err => {
        this.isLoading.set(false);
        throw err;
      })
    );
  }

  onPageChange(event: PageEvent) {
    this.filterChanged.emit({
      paginationOptions: {
        pageNumber: event.pageIndex,
        pageSize: event.pageSize,
        sortDirection: 'asc',
      },
      searchPhrase: this.filterState.value?.searchPhrase,
    });
  }

  onFilterChange(phrase: string) {
    this.filterChanged.emit({
      paginationOptions: this.filterState.value.paginationOptions,
      searchPhrase: phrase,
    });
  }
}
