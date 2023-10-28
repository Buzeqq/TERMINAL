import {Component, OnInit} from '@angular/core';
import {ProjectsService} from "../../core/services/projects/projects.service";
import {ActivatedRoute, ParamMap} from "@angular/router";
import {Observable, tap} from "rxjs";
import {MeasurementsService} from "../../core/services/measurements/measurements.service";
import {Measurement} from "../../core/models/measurements/recentMeasurement";
import {Project} from "../../core/models/projects/project";
import {Filter} from "../../core/filters/filter";

@Component({
  selector: 'app-results-list',
  templateUrl: './results-list.component.html',
  styleUrls: ['./results-list.component.scss']
})
export class ResultsListComponent implements OnInit {

  selectedItemId?: string;
  selectedItemType: 'Measurement' | 'Project' | 'Recipe' | 'None' = 'Measurement';
  projects$ = new Observable<Project[]>();
  measurements$ = new Observable<Measurement[]>();

  // TODO: make services application scoped?
  constructor(
    private readonly projectService: ProjectsService,
    private readonly measurementService: MeasurementsService,
    private route: ActivatedRoute,
    private readonly filter: Filter
  ) {  }

  selectItem(row: Measurement | Project) {
    this.selectedItemId = row.id;
    // TODO: Handle recipes
    this.selectedItemType = 'project' in row ? 'Measurement' : 'Project';
  }

  private loadData() {
    this.projects$ = this.projectService.getAllProjects();
    this.measurements$ = this.measurementService.getAllMeasurements();
  }

  private handleItemSelection(filterMeasurements: boolean, filterProjects: boolean) {
    if (filterMeasurements) {
      this.measurements$ = this.measurements$
        .pipe(tap(r => this.selectedItemId = r[0].id));
      this.selectedItemType = 'Measurement';
    } else if (filterProjects) {
      this.projects$ = this.projects$
        .pipe(tap(r => this.selectedItemId = r[0].id))
      this.selectedItemType = 'Project';
    } else {
      this.selectedItemId = '';
      this.selectedItemType = 'None';
    }
  }

  ngOnInit(): void {
    this.loadData();
    this.measurements$.pipe(tap(r => this.selectedItemId = r[0].id));

    this.route.queryParamMap.subscribe((params: ParamMap) => {
      this.loadData();
      let searchPhrase = params.get('q') ?? '';
      this.projects$ = this.filter.filterProjects(this.projects$, searchPhrase, params.get('projects') == 'true');
      this.measurements$ = this.filter.filterMeasurements(this.measurements$, searchPhrase, params.get('measurements') == 'true');

      // TODO: load and filter recipes

      this.handleItemSelection(
        params.get('measurements') == 'true',
        params.get('projects') == 'true'
      );
    })
  }

}
