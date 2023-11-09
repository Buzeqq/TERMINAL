import {Component, Input} from '@angular/core';
import {ItemDetailsComponent} from "../item-details.component";
import {MeasurementsService} from "../../../services/measurements/measurements.service";
import {ProjectsService} from "../../../services/projects/projects.service";
import {map, Observable, tap} from "rxjs";
import {Project} from "../../../models/projects/project";
import { ActivatedRoute } from '@angular/router';

@Component({
  selector: 'app-project-details',
  templateUrl: './project-details.component.html',
  styleUrls: ['./project-details.component.scss']
})
export class ProjectDetailsComponent extends ItemDetailsComponent {
  constructor(
    private readonly measurementService: MeasurementsService,
    private readonly projectService: ProjectsService,
    protected override readonly route: ActivatedRoute
  ) { super(route); }

  set projectId(id: string | undefined) {
    this._projectId = id || this.route.snapshot.paramMap.get('id') || undefined;
    let projectName: string;
    this.projectDetails$ = this.projectService.getProject(this._projectId!)
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
