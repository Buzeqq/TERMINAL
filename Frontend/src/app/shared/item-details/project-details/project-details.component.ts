import {Component, Input} from '@angular/core';
import {ItemDetailsComponent} from "../item-details.component";
import {MeasurementsService} from "../../../core/services/measurements/measurements.service";
import {ProjectsService} from "../../../core/services/projects/projects.service";
import {map, Observable, tap} from "rxjs";
import {Project} from "../../../core/models/projects/project";

@Component({
  selector: 'app-project-details',
  templateUrl: './project-details.component.html',
  styleUrls: ['./project-details.component.scss']
})
export class ProjectDetailsComponent extends ItemDetailsComponent {
  constructor(
    private readonly measurementService: MeasurementsService,
    private readonly projectService: ProjectsService
  ) { super(); }

  set projectId(id: string | undefined) {
    this._projectId = id;
    let projectName: string;
    this.projectDetails$ = this.projectService.getProject(id!)
      .pipe(tap(r => projectName = r.name));
    this.numberOfMeasurements$ = this.measurementService.getMeasurements(0, 10)
      .pipe(
        tap(_ => super.loaded()),
        map(measurements => measurements.filter(
          m => m.project == projectName
          ).length
        )
      );
  }

  @Input()
  get projectId(): string | undefined {
    return this._projectId;
  }

  private _projectId?: string;
  numberOfMeasurements$: Observable<number> | undefined;
  projectDetails$?: Observable<Project>;
}
