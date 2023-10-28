import { Component } from '@angular/core';
import { Observable, tap } from 'rxjs';
import { Project } from "../../../core/models/projects/project";
import { ProjectsService } from "../../../core/services/projects/projects.service";

@Component({
  selector: 'app-project-views',
  templateUrl: './project-views.component.html',
  styleUrls: ['./project-views.component.scss']
})
export class ProjectViewsComponent {
  displayedColumns: string[] = ['name'];
  selectedProjectId?: string;
  projects$: Observable<Project[]> = this.projectService.getAllProjects()
    .pipe(tap(r => this.selectedProjectId = r[0].id));

  constructor(
    private readonly projectService: ProjectsService
  ) { }

  selectProject(row: Project) {
    this.selectedProjectId = row.id;
  }
}
