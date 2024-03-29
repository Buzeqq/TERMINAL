import { Component, Input } from '@angular/core';
import { ItemDetailsComponent } from "../item-details.component";
import { catchError, EMPTY, Observable, tap } from "rxjs";
import { ActivatedRoute } from '@angular/router';
import { Project } from "../../../../models/projects/project";
import { ProjectsService } from "../../../../services/projects/projects.service";
import {NotificationService} from "../../../../services/notification/notification.service";

@Component({
  selector: 'app-project-details',
  templateUrl: './project-details.component.html',
  styleUrls: ['./project-details.component.scss']
})
export class ProjectDetailsComponent extends ItemDetailsComponent {
  private _projectId?: string;
  numberOfSamples?: number;
  projectDetails$: Observable<Project> = new Observable<Project>();

  constructor(
    private readonly projectService: ProjectsService,
    protected override readonly route: ActivatedRoute,
    private readonly notificationService: NotificationService
  ) { super(route); }

  @Input()
  get projectId(): string | undefined {
    return this._projectId;
  }

  set projectId(id: string | undefined) {
    this._projectId = id || this.route.snapshot.paramMap.get('id') || undefined;
    if (!this._projectId) return;

    this.projectDetails$ = this.projectService.getProject(this._projectId!)
      .pipe(
        catchError((err, _) => {
          console.log(err);
          this.notificationService.notifyError('Failed to load project');
          return EMPTY;
        }),
        tap(r => {
          this.numberOfSamples = r.samplesIds.length;
          this.loading = 'determinate';
        })
      );
  }
}
