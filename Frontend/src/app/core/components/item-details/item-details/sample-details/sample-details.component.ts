import { Component, Input } from '@angular/core';
import { MatSnackBar } from '@angular/material/snack-bar';
import { EMPTY, Observable, catchError, tap } from 'rxjs';
import { SampleDetails } from 'src/app/core/models/samples/sampleDetails';
import { SamplesService } from 'src/app/core/services/samples/samples.service';
import { ItemDetailsComponent } from "../item-details.component";
import { ActivatedRoute } from '@angular/router';
import { Project } from "../../../../models/projects/project";
import { ProjectsService } from "../../../../services/projects/projects.service";
import { ExportService } from 'src/app/core/services/export/export.service';

@Component({
  selector: 'app-sample-details',
  templateUrl: './sample-details.component.html',
  styleUrls: ['./sample-details.component.scss']
})
export class SampleDetailsComponent extends ItemDetailsComponent {
  private _sampleId?: string;
  sampleDetails$?: Observable<SampleDetails>;
  projectDetails$?: Observable<Project>;

  constructor(
    private readonly samplesService: SamplesService,
    private readonly snackBar: MatSnackBar,
    private readonly projectService: ProjectsService,
    private readonly exportService: ExportService,
    protected override readonly route: ActivatedRoute
  ) {
    super(route);
  }

  @Input()
  get sampleId(): string | undefined {
    return this._sampleId;
  }

  set sampleId(id: string | undefined) {
    this._sampleId = id || this.route.snapshot.paramMap.get('id') || undefined;
    this.sampleDetails$ = this.samplesService.getSampleDetails(this._sampleId!)
      .pipe(
        catchError((err, _) => {
          console.log(err);
          this.snackBar.open('Failed to load sample', 'Close', {
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

  isDetailView(): boolean{
    return this.route.snapshot.paramMap.get('id') !== null;
  }

  hasRecipe(detail: SampleDetails): boolean{
    return detail.recipeId !== null;
  }

  export(details: SampleDetails) {
    this.projectDetails$?.subscribe(project => {
      this.exportService.exportSamples(details, project);
    });
  }

}
