import { Injectable } from '@angular/core';
import {ProjectsService} from "../projects/projects.service";
import {RecipesService} from "../recipes/recipes.service";
import {SamplesService} from "../samples/samples.service";
import {PingService} from "../ping/ping.service";
import {NotificationService} from "../notification/notification.service";
import {IndexedDbService} from "../indexed-db/indexed-db.service";

@Injectable({
  providedIn: 'root'
})
export class SyncService {

  online = false;

  constructor(
    private readonly pingService: PingService,
    private readonly idbService: IndexedDbService,
    private readonly projectService: ProjectsService,
    private readonly recipeService: RecipesService,
    private readonly sampleService: SamplesService,
    private readonly notificationService: NotificationService,
  ) { }

  async synchronise() {
    this.pingService.isOnline$.subscribe(o => this.online = o);

    if (!this.online) {
      this.notificationService.notifyError("Failed synchronising data with server. Check your network connection.");
      return;
    }

    this.projectService.getProjects(0, 250)
      .subscribe(projects => this.idbService.addProjects(projects));

    this.recipeService.getRecipes(0, 250)
      .subscribe(recipes => this.idbService.addRecipes(recipes));

    this.sampleService.getSamples(0, 750)
      .subscribe(samples => this.idbService.addSamples(samples));

    this.notificationService.notifySuccess("Data synchronised with server successfully.");
  }

}
