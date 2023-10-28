import { Component, Input } from '@angular/core';
import { MatSnackBar } from '@angular/material/snack-bar';
import { EMPTY, Observable, catchError, tap } from 'rxjs';
import { MeasurementDetails } from 'src/app/core/models/measurements/measurementDetails';
import { MeasurementsService } from 'src/app/core/services/measurements/measurements.service';
import { ProjectsService } from "../../../core/services/projects/projects.service";
import { Project } from "../../../core/models/projects/project";
import { ItemDetailsComponent } from "../item-details.component";
import { ActivatedRoute } from '@angular/router';

@Component({
  selector: 'app-measurement-details',
  templateUrl: './measurement-details.component.html',
  styleUrls: ['./measurement-details.component.scss']
})
export class MeasurementDetailsComponent extends ItemDetailsComponent {
  constructor(
    private readonly measurementService: MeasurementsService,
    private readonly snackBar: MatSnackBar,
    private readonly projectService: ProjectsService,
    protected override readonly route: ActivatedRoute
  ) {
    super(route);

  }

  @Input()
  get measurementId(): string | undefined {
    return this._measurementId;
  }

  set measurementId(id: string | undefined) {
    this._measurementId = id || this.route.snapshot.paramMap.get('id') || undefined;
    this.measurementDetails$ = this.measurementService.getMeasurementDetails(id!)
      .pipe(
        catchError((err, caught) => {
          console.log(err);
          this.snackBar.open('Failed to load measurement', 'Close', {
            duration: 3000
          });
          return EMPTY;
        }),
        tap(m => {
          this.loading = 'determinate';
          this.projectDetails$ = this.projectService.getProject(m.projectId)
        })
      );
  }

  private _measurementId?: string;
  measurementDetails$?: Observable<MeasurementDetails>;
  projectDetails$?: Observable<Project>;
}
