import { Component, Input } from '@angular/core';
import { ItemDetailsComponent } from "../item-details.component";
import { catchError, EMPTY, map, Observable, tap } from "rxjs";
import { ActivatedRoute } from '@angular/router';
import { MatSnackBar } from "@angular/material/snack-bar";
import { Project } from "../../../../models/projects/project";
import { MeasurementsService } from "../../../../services/measurements/measurements.service";
import { ProjectsService } from "../../../../services/projects/projects.service";

@Component({
  selector: 'app-project-details',
  templateUrl: './project-details.component.html',
  styleUrls: ['./project-details.component.scss']
})
export class ProjectDetailsComponent extends ItemDetailsComponent {
  private _projectId?: string;
  numberOfMeasurements?: number;
  projectDetails$: Observable<Project> = new Observable<Project>();

  constructor(
    private readonly projectService: ProjectsService,
    protected override readonly route: ActivatedRoute,
    private readonly snackBar: MatSnackBar,
  ) { super(route); }

  @Input()
  get projectId(): string | undefined {
    return this._projectId;
  }

  set projectId(id: string | undefined) {
    this._projectId = id || this.route.snapshot.paramMap.get('id') || undefined;
    if (!id) return;

    console.log(this._projectId);
    this.projectDetails$ = this.projectService.getProject(this._projectId!)
      .pipe(
        catchError((err, _) => {
          console.log(err);
          this.snackBar.open('Failed to load project', 'Close', {
            duration: 3000
          });
          return EMPTY;
        }),
        tap(r => {
          this.numberOfMeasurements = r.measurementsIds.length;
          this.loading = 'determinate';
        })
      );
  }
}
