import {AfterViewInit, Component, EventEmitter, Input, Output, ViewChild} from '@angular/core';
import {Observable, tap} from "rxjs";
import {SelectedItem} from "../../../models/items/selected-item";
import {Tag} from "../../../models/tags/tag";
import {TagsService} from "../../../services/tags/tags.service";
import {MatTableDataSource} from "@angular/material/table";
import {MatSort, Sort} from "@angular/material/sort";
import {PageEvent} from "@angular/material/paginator";

@Component({
  selector: 'app-tag-views',
  templateUrl: './tag-views.component.html',
  styleUrls: ['./tag-views.component.scss', '../item-views.component.scss']
})
export class TagViewsComponent implements AfterViewInit {
  displayedColumns: string[] = ['Name'];
  queryPageSize = 10;
  private queryPageIndex = 0;

  private orderDir = "desc";
  dataSource = new MatTableDataSource<Tag>();
  length$?: Observable<number>;
  @ViewChild(MatSort) sort?: MatSort;

  selectedItem?: SelectedItem;
  @Output() selectedItemChangeEvent = new EventEmitter<SelectedItem>();

  @Input() all?: boolean;

  constructor(
    private readonly tagService: TagsService,
  ) { }

  ngAfterViewInit(): void {
    this.loadData()
    this.length$ = this.tagService.getTagsAmount();
    this.dataSource.sort = this.sort!;
  }

  private loadData() {
    this.tagService.getTags(this.queryPageIndex, this.queryPageSize, this.orderDir == "desc", this.all)
      .pipe(tap(r => {
        if (!this.selectedItem) this.selectTag(r[0]);
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

  selectTag(t: Tag) {
    this.selectedItem = {type: 'Tag', id: t.id};
    this.selectedItemChangeEvent.emit(this.selectedItem);
  }
}
