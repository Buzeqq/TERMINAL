import { Component, Input } from '@angular/core';
import { Observable, tap } from 'rxjs';
import { MeasurementDetails } from 'src/app/core/models/measurements/measurementDetails';
import { MeasurementsService } from 'src/app/core/services/measurements/measurements.service';
import {ProjectsService} from "../../../core/services/projects/projects.service";
import {Project} from "../../../core/models/projects/project";
import {ItemDetailsComponent} from "../item-details.component";

@Component({
  selector: 'app-measurement-details',
  templateUrl: './measurement-details.component.html',
  styleUrls: ['./measurement-details.component.scss']
})
export class MeasurementDetailsComponent extends ItemDetailsComponent {
  constructor(
    private readonly measurementService: MeasurementsService,
    private readonly projectService: ProjectsService
  ) { super(); }

  set measurementId(id: string | undefined) {
    this._measurementId = id;
    this.measurementDetails$ = this.measurementService.getMeasurementDetails(id!)
      .pipe(
        tap(_ => this.loaded()),
        tap(m => this.projectDetails$ = this.projectService.getProject(m.projectId))
      );
  }

  @Input()
  get measurementId(): string | undefined {
    return this._measurementId;
  }

  private _measurementId?: string;
  measurementDetails$?: Observable<MeasurementDetails>;
  projectDetails$?: Observable<Project>;
}
