import { AfterViewInit, Component, EventEmitter, Output } from '@angular/core';
import { BehaviorSubject, tap } from 'rxjs';
import { Project } from "../../../models/projects/project";
import { SelectedItem } from "../../../models/items/selected-item";
import { ProjectsService } from "../../../services/projects/projects.service";
@Component({
  selector: 'app-project-views',
  templateUrl: './project-views.component.html',
  styleUrls: ['./project-views.component.scss']
})
export class ProjectViewsComponent implements AfterViewInit {
  displayedColumns: string[] = ['name'];
  private readonly pageSize = 10;
  private page = 0;
  private readonly projectsSubject = new BehaviorSubject<Project[]>([]);
  projects$= this.projectsSubject.asObservable();
  selectedItem?: SelectedItem;
  @Output() selectedItemChangeEvent = new EventEmitter<SelectedItem>();

  constructor(
    private readonly projectService: ProjectsService,
  ) { }

  ngAfterViewInit(): void {
    this.projectService.getProjects(this.page, this.pageSize)
      .pipe(tap(r => this.selectProject(r[0])))
      .subscribe(r => this.projectsSubject.next(r))
  }

  selectProject(p: Project) {
    this.selectedItem = {type: 'Project', id: p.id};
    this.selectedItemChangeEvent.emit(this.selectedItem);
  }

  onScroll(event: any) {
    // check whether scroll reached bottom
    if (event.target.offsetHeight + event.target.scrollTop >= event.target.scrollHeight) {
      this.page += 1;
      this.projectService.getProjects(this.page, this.pageSize)
        .subscribe(r =>
          this.projectsSubject.next(this.projectsSubject.value.concat(r))
        )
      console.log(`loaded another ${this.pageSize} results, page: ${this.page}`);
    }
  }
}
