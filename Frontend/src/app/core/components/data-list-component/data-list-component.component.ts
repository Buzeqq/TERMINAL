import {
  Component,
  EventEmitter,
  Input,
  OnInit,
  Output,
  TemplateRef,
} from '@angular/core';
import { FormControl, FormGroup, ReactiveFormsModule } from '@angular/forms';
import { MatPaginator, PageEvent } from '@angular/material/paginator';
import {
  BehaviorSubject,
  combineLatestWith,
  debounceTime,
  distinctUntilChanged,
  filter,
  map,
  Observable,
  startWith,
  tap,
} from 'rxjs';
import { MatFormField, MatLabel } from '@angular/material/form-field';
import { MatInput } from '@angular/material/input';
import { AsyncPipe, NgTemplateOutlet } from '@angular/common';
import { MatList, MatListItem } from '@angular/material/list';
import { MatDivider } from '@angular/material/divider';
import { MatRipple } from '@angular/material/core';
import { MatProgressSpinner } from '@angular/material/progress-spinner';
import { Entity } from '../../common.model';
import { HintComponent } from '../hint/hint.component';

export type DataListStateChangedEvent = {
  paginationOptions: PaginationOptions;
  searchPhrase: string;
};

export interface PaginationOptions {
  pageNumber: number;
  pageSize: number;
  desc: boolean;
}

export type DataListDataSourceInput<T extends Entity> = Observable<{
  data: readonly T[];
  paginationOptions: PaginationOptions;
  totalCount: number;
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
  templateUrl: './data-list-component.component.html',
  styleUrl: './data-list-component.component.scss',
})
export class DataListComponentComponent<TData extends Entity>
  implements OnInit
{
  @Input({ required: true, alias: 'data' })
  dataSource$?: DataListDataSourceInput<TData>;
  @Input({ required: true }) rowTemplate!: TemplateRef<any>;
  @Output() filterChanged = new EventEmitter<DataListStateChangedEvent>();

  readonly formGroup = new FormGroup({
    searchPhrase: new FormControl(''),
  });
  searchPhrase$ = this.formGroup.controls.searchPhrase.valueChanges.pipe(
    startWith(''),
    filter((v) => v !== null),
    distinctUntilChanged(),
    debounceTime(300),
  );
  private paginationOptions = new BehaviorSubject<PaginationOptions>({
    pageNumber: 0,
    pageSize: 10,
    desc: true,
  });

  ngOnInit() {
    this.dataSource$ = this.dataSource$?.pipe(
      combineLatestWith(
        this.paginationOptions.pipe(
          combineLatestWith(this.searchPhrase$),
          tap(([paginationOptions, searchPhrase]) =>
            this.filterChanged.emit({ paginationOptions, searchPhrase }),
          ),
        ),
      ),
      map(([data, _]) => data),
    );
  }

  onPageChange(event: PageEvent) {
    this.paginationOptions.next({
      ...this.paginationOptions.value,
      pageNumber: event.pageIndex,
      pageSize: event.pageSize,
    });
  }
}
