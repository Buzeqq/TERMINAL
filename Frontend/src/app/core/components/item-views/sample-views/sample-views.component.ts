import {AfterViewInit, Component, EventEmitter, Output} from '@angular/core';
import {Observable, tap} from 'rxjs';
import {Sample} from 'src/app/core/models/samples/sample';
import {SamplesService} from 'src/app/core/services/samples/samples.service';
import {SelectedItem} from "../../../models/items/selected-item";
import {MatSort, Sort} from "@angular/material/sort";
import {MatTableDataSource} from "@angular/material/table";
import {PageEvent} from "@angular/material/paginator";

@Component({
  selector: 'app-sample-views',
  templateUrl: './sample-views.component.html',
  styleUrls: ['./sample-views.component.scss', '../item-views.component.scss']
})
export class SampleViewsComponent implements AfterViewInit {
  displayedColumns: string[] = ['Code', 'Project.Name', 'CreatedAtUtc'];
  queryPageSize = 10;
  private queryPageIndex = 0;

  private orderBy = "CreatedAtUtc";
  private orderDir = 'desc';
  dataSource = new MatTableDataSource<Sample>();
  length$?: Observable<number>;

  selectedItem: SelectedItem | undefined;
  @Output() selectedItemChangeEvent = new EventEmitter<SelectedItem>();

  constructor(
    private readonly samplesService: SamplesService,
  ) {  }

  ngAfterViewInit(): void {
    this.loadData();
    this.length$ = this.samplesService.getSamplesAmount();
    this.dataSource.sortData = this.sortData();
  }

  private sortData() {
    return (items: Sample[], sort: MatSort): Sample[] => {
      if (!sort.active || sort.direction === '') {
        return items;
      }
      return items.sort((a: Sample, b: Sample) => {
        let comparatorResult: number;
        console.log(sort.active)
        switch (sort.active) {
          case 'Project.Name':
            comparatorResult = a.project.localeCompare(b.project);
            break;
          case 'Code':
            // filter letters out
            const codeA = +a.code.replace(/\D+/g, '');
            const codeB = +b.code.replace(/\D+/g, '');
            comparatorResult = (codeA < codeB) ? -1 : 1;
            break;
          default:
            const dateA = new Date(a.createdAtUtc).getTime();
            const dateB = new Date(b.createdAtUtc).getTime();
            comparatorResult = (dateA < dateB) ? -1 : 1;
            break;
        }
        return comparatorResult * (sort.direction == 'asc' ? 1 : -1);
      });
    };
  }

  pageSelected(event: PageEvent) {
    if (this.queryPageIndex != event.pageIndex || this.queryPageSize != event.pageSize) {
      this.queryPageIndex = event.pageIndex
      this.queryPageSize = event.pageSize
      this.loadData();
    }
  }

  sortColumnChanged($event: Sort) {
    if ($event.active != this.orderBy || this.orderDir != $event.direction) {
      this.orderBy = $event.active
      this.orderDir = $event.direction
      this.loadData();
    }
  }

  private loadData() {
    this.samplesService.getSamples(this.queryPageIndex, this.queryPageSize, this.orderBy, this.orderDir == 'desc')
      .pipe(tap(r => {
        if (!this.selectedItem) this.selectSample(r[0]);
      }))
      .subscribe(r => this.dataSource.data = r)
  }

  selectSample(m: Sample) {
    this.selectedItem = {type: 'Sample', id: m.id};
    this.selectedItemChangeEvent.emit({type: 'Sample', id: m.id});
  }
}
