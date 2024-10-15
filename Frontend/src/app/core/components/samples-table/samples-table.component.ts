import {
  AfterViewInit,
  Component,
  EventEmitter,
  inject,
  Input,
  Output,
  ViewChild,
} from '@angular/core';
import {
  MatCell,
  MatCellDef,
  MatColumnDef,
  MatHeaderCell,
  MatHeaderCellDef,
  MatHeaderRow,
  MatHeaderRowDef,
  MatNoDataRow,
  MatRow,
  MatRowDef,
  MatTable,
} from '@angular/material/table';
import { TimeAgoPipe } from '../../pipes/time-ago.pipe';
import { MatRipple } from '@angular/material/core';
import { Sample } from '../../samples/sample.model';
import { MatTooltip } from '@angular/material/tooltip';
import { AsyncPipe, DatePipe } from '@angular/common';
import { MatSort, MatSortHeader, Sort } from '@angular/material/sort';
import { MatPaginator } from '@angular/material/paginator';
import { MatFormField, MatLabel } from '@angular/material/form-field';
import { MatInput } from '@angular/material/input';
import { map, merge, Observable, of, startWith, switchMap, tap } from 'rxjs';
import { SamplesService } from '../../samples/samples.service';
import { FormControl, ReactiveFormsModule } from '@angular/forms';

@Component({
  selector: 'app-samples-table',
  standalone: true,
  imports: [
    MatTable,
    MatHeaderCell,
    MatCell,
    MatColumnDef,
    MatHeaderCellDef,
    MatCellDef,
    TimeAgoPipe,
    MatHeaderRow,
    MatHeaderRowDef,
    MatRow,
    MatRipple,
    MatRowDef,
    MatTooltip,
    DatePipe,
    MatSort,
    MatPaginator,
    MatFormField,
    MatInput,
    MatLabel,
    MatSortHeader,
    MatNoDataRow,
    AsyncPipe,
    ReactiveFormsModule,
  ],
  templateUrl: './samples-table.component.html',
  styleUrl: './samples-table.component.scss',
})
export class SamplesTableComponent implements AfterViewInit {
  readonly displayedColumns = ['code', 'project', 'created'];
  @Input() filterable = false;
  @Input() pageable = true;
  @Output() sampleSelected = new EventEmitter<Sample>();
  isLoadingResults = true;
  resultsLength = 0;
  data: Sample[] = [];
  @ViewChild(MatPaginator) paginator?: MatPaginator;
  @ViewChild(MatSort) sort!: MatSort;
  observable?: Observable<Sample[] | Sort>;
  filter = new FormControl('');
  private readonly samplesService = inject(SamplesService);

  ngAfterViewInit() {
    const sortChange$ = this.sort.sortChange.pipe(
      tap(() => {
        if (this.pageable) {
          this.paginator!.pageIndex = 0;
        }
      })
    );

    const page = this.paginator?.page.asObservable() ?? of();
    const userChanges$ = merge(
      this.sort.sortChange,
      page,
      this.filter.valueChanges
    ).pipe(
      startWith({}),
      switchMap(() => {
        this.isLoadingResults = true;
        return this.samplesService.getSamples(
          this.paginator?.pageIndex ?? 0,
          this.paginator?.pageSize ?? 10,
          this.sort.active,
          this.sort.direction,
          this.filter.value ? this.filter.value : undefined
        );
      }),
      map(response => {
        this.isLoadingResults = false;
        this.resultsLength = response.totalCount;

        return response.samples;
      }),
      tap(samples => (this.data = samples))
    );

    this.observable = merge(sortChange$, userChanges$);
  }
}
