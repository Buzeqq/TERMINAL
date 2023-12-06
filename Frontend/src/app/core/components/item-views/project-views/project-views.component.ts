import {AfterViewInit, Component, EventEmitter, Input, Output, ViewChild} from '@angular/core';
import {Observable, tap} from 'rxjs';
import { Project } from "../../../models/projects/project";
import { SelectedItem } from "../../../models/items/selected-item";
import { ProjectsService } from "../../../services/projects/projects.service";
import {MatTableDataSource} from "@angular/material/table";
import {MatSort, Sort} from "@angular/material/sort";
import {PageEvent} from "@angular/material/paginator";

@Component({
  selector: 'app-project-views',
  templateUrl: './project-views.component.html',
  styleUrls: ['./project-views.component.scss', '../item-views.component.scss']
})
export class ProjectViewsComponent implements AfterViewInit {
  displayedColumns: string[] = ['Name'];
  queryPageSize = 10;
  private queryPageIndex = 0;

  private orderDir = "desc";
  dataSource = new MatTableDataSource<Project>();
  length$?: Observable<number>;
  @ViewChild(MatSort) sort?: MatSort;

  selectedItem?: SelectedItem;
  @Output() selectedItemChangeEvent = new EventEmitter<SelectedItem>();

  @Input() all?: boolean;

  constructor(
    private readonly projectService: ProjectsService,
  ) { }

  ngAfterViewInit(): void {
    this.loadData()
    this.length$ = this.projectService.getProjectsAmount();
    this.dataSource.sort = this.sort!;
  }

  private loadData() {
    this.projectService.getProjects(this.queryPageIndex, this.queryPageSize, this.orderDir == "desc", this.all)
      .pipe(tap(r => {
        if (!this.selectedItem) this.selectProject(r[0], true);
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

  selectProject(p: Project, init = false) {
    this.selectedItem = {type: 'Project', id: p.id, config: {init: init}};
    this.selectedItemChangeEvent.emit(this.selectedItem);
  }
}
