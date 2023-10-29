import { Component, EventEmitter, Output } from '@angular/core';
import { Observable, tap } from 'rxjs';
import { Project } from "../../../core/models/projects/project";
import { ProjectsService } from "../../../core/services/projects/projects.service";
import { ItemViewsComponent } from '../item-views.component';

@Component({
  selector: 'app-project-views',
  templateUrl: './project-views.component.html',
  styleUrls: ['./project-views.component.scss']
})
export class ProjectViewsComponent {
  displayedColumns: string[] = ['id','name'];
  selectedItemId: string | undefined;

  projects$: Observable<Project[]> = this.projectService.getAllProjects()
    .pipe(tap(r => {
      this.selectedItemIdChangeEvent.emit(r[0].id);
      this.selectedItemId = r[0].id;
    }));

  constructor(
    private readonly projectService: ProjectsService,
  ) { }

  @Output() selectedItemIdChangeEvent = new EventEmitter<string>();

  selectProject(row: Project) {
    this.selectedItemIdChangeEvent.emit(row.id);
    this.selectedItemId = row.id;
  }
}
