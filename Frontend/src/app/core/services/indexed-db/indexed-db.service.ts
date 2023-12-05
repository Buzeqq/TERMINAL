import {Injectable} from '@angular/core';
import {Project} from "../../models/projects/project";
import {IdbProjectsService} from "./projects/idb-projects.service";
import {from} from "rxjs";
import {IdbTagsService} from "./tags/idb-tags.service";
import {Tag} from "../../models/tags/tag";
import {Sample} from "../../models/samples/sample";
import {IdbSamplesService} from "./samples/idb-samples.service";
import {Recipe} from "../../models/recipes/recipe";
import {IdbRecipesService} from "./recipes/idb-recipes.service";
import {SampleDetails} from "../../models/samples/sampleDetails";

@Injectable({
  providedIn: 'root'
})
export class IndexedDbService {

  constructor(
    private readonly idbProjectService: IdbProjectsService,
    private readonly idbTagService: IdbTagsService,
    private readonly idbSampleService: IdbSamplesService,
    private readonly idbRecipeService: IdbRecipesService
  ) { }

  addProjects(projects: Project[]) {
    this.idbProjectService.addProjects(projects);
  }

  getProjects(pageIndex: number, pageSize: number, desc: boolean, all: boolean) {
    return from(this.idbProjectService.getProjects(pageIndex, pageSize, desc, all));
  }

  getProject(id: string) {
    return from(this.idbProjectService.getProject(id));
  }

  getProjectsAmount() {
    return from(this.idbProjectService.getProjectsAmount())
  }

  addTags(tags: Tag[]) {
    this.idbTagService.addTags(tags);
  }

  getTags(pageIndex: number, pageSize: number, desc: boolean, all: boolean) {
    return from(this.idbTagService.getTags(pageIndex, pageSize, desc, all));
  }

  getTagsAmount() {
    return from(this.idbTagService.getTagsAmount())
  }

  getTagById(id: string) {
    return from(this.idbTagService.getTagById(id))
  }

  addSamples(samples: Sample[]) {
    this.idbSampleService.addSamples(samples);
  }

  addSample(sample: SampleDetails) {
    this.idbSampleService.addSample(sample)
  }

  getSamples(pageIndex: number, pageSize: number, orderBy: string, desc: boolean) {
    return from(this.idbSampleService.getSamples(pageIndex, pageSize, orderBy, desc));
  }

  getRecentSamples(length: number) {
    return from(this.idbSampleService.getRecentSamples(length));
  }

  getSample(id: string) {
    return from(this.idbSampleService.getSample(id));
  }

  getSamplesAmount() {
    return from(this.idbSampleService.getSamplesAmount());
  }

  addRecipes(recipes: Recipe[]) {
    this.idbRecipeService.addRecipes(recipes);
  }
}
