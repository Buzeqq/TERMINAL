import {Component, Input} from '@angular/core';
import {ItemDetailsComponent} from "../item-details.component";
import {MeasurementsService} from "../../../core/services/measurements/measurements.service";
import {ProjectsService} from "../../../core/services/projects/projects.service";
import {catchError, EMPTY, map, Observable, tap} from "rxjs";
import {Project} from "../../../core/models/projects/project";
import { ActivatedRoute } from '@angular/router';
import {MatSnackBar} from "@angular/material/snack-bar";

@Component({
  selector: 'app-project-details',
  templateUrl: './project-details.component.html',
  styleUrls: ['./project-details.component.scss']
})
export class ProjectDetailsComponent extends ItemDetailsComponent {

  private _projectId?: string;
  numberOfMeasurements$: Observable<number> | undefined;
  projectDetails$?: Observable<Project>;

  constructor(
    private readonly measurementService: MeasurementsService,
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
    let projectName: string;
    console.log(this._projectId)
    this.projectDetails$ = this.projectService.getProject(this._projectId!)
      .pipe(
        catchError((err, caught) => {
          console.log(err);
          this.snackBar.open('Failed to load project', 'Close', {
            duration: 3000
          });
          return EMPTY;
        }),
        tap(r => {
          projectName = r.name;
          this.loading = 'determinate';
        })
      );
    this.numberOfMeasurements$ = this.measurementService.getMeasurements(0, 10)
      .pipe(
        map(measurements => measurements.filter(
          m => m.project == projectName
          ).length
        )
      );
  }
}
