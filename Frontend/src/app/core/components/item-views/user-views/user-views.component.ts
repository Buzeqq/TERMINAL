import {Component, EventEmitter, Output, ViewChild} from '@angular/core';
import {MatTableDataSource} from "@angular/material/table";
import {Observable, tap} from "rxjs";
import {MatSort, Sort} from "@angular/material/sort";
import {SelectedItem} from "../../../models/items/selected-item";
import {PageEvent} from "@angular/material/paginator";
import {UserService} from "../../../services/users/user.service";
import {User} from "../../../models/users/user";

@Component({
  selector: 'app-user-views',
  templateUrl: './user-views.component.html',
  styleUrls: ['./user-views.component.scss', '../item-views.component.scss']
})
export class UserViewsComponent {
  displayedColumns: string[] = ['Email', 'Role'];
  queryPageSize = 10;
  private queryPageIndex = 0;

  private orderBy = "Role";
  private orderDir = "desc";
  dataSource = new MatTableDataSource<User>();
  length$?: Observable<number>;
  @ViewChild(MatSort) sort?: MatSort;

  selectedItem?: SelectedItem;
  @Output() selectedItemChangeEvent = new EventEmitter<SelectedItem>();

  constructor(
    private readonly userService: UserService,
  ) { }

  ngAfterViewInit(): void {
    this.loadData()
    this.length$ = this.userService.getUsersAmount();
    this.dataSource.sort = this.sort!;
  }

  private loadData() {
    this.userService.getUsers(this.queryPageIndex, this.queryPageSize, this.orderBy, this.orderDir == 'desc')
      .pipe(tap(r => {
        if (!this.selectedItem) this.selectUser(r[0]);
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
    if ($event.active != this.orderBy || this.orderDir != $event.direction) {
      this.orderBy = $event.active
      this.orderDir = $event.direction
      this.loadData();
    }
  }

  selectUser(t: User) {
    this.selectedItem = {type: 'User', id: t.id};
    this.selectedItemChangeEvent.emit(this.selectedItem);
  }
}
