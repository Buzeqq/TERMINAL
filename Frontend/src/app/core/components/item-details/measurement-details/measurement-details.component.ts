import { Component, Input } from '@angular/core';
import { MatSnackBar } from '@angular/material/snack-bar';
import { catchError, EMPTY, Observable, tap } from 'rxjs';
import { MeasurementDetails } from 'src/app/core/models/measurements/measurementDetails';
import { MeasurementsService } from 'src/app/core/services/measurements/measurements.service';
import { ProjectsService } from "../../../services/projects/projects.service";
import { Project } from "../../../models/projects/project";
import { ItemDetailsComponent } from "../item-details.component";
import { ActivatedRoute } from '@angular/router';

@Component({
  selector: 'app-measurement-details',
  templateUrl: './measurement-details.component.html',
  styleUrls: ['./measurement-details.component.scss']
})
export class MeasurementDetailsComponent extends ItemDetailsComponent {
  measurementDetails$: Observable<MeasurementDetails> = new Observable<MeasurementDetails>();
  projectDetails$?: Observable<Project>;

  constructor(
    private readonly measurementService: MeasurementsService,
    private readonly snackBar: MatSnackBar,
    private readonly projectService: ProjectsService,
    protected override readonly route: ActivatedRoute
  ) {
    super(route);
  }

  private _measurementId?: string;

  @Input()
  get measurementId(): string | undefined {
    return this._measurementId;
  }

  set measurementId(id: string | undefined) {
    this._measurementId = id || this.route.snapshot.paramMap.get('id') || undefined;
    this.measurementDetails$ = this.measurementService.getMeasurementDetails(this._measurementId!)
      .pipe(
        catchError((err, _) => {
          console.log(err);
          this.snackBar.open('Failed to load measurement', 'Close', {
            duration: 3000
          });
          return EMPTY;
        }),
        tap(m => {
          console.log(m);
          this.loading = 'determinate';
          this.projectDetails$ = this.projectService.getProject(m.projectId)
        })
      );
  }
}
