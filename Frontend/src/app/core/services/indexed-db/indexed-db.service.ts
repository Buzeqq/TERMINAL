import {Injectable} from '@angular/core';
import {Project} from "../../models/projects/project";
import {IdbProjectsService} from "./projects/idb-projects.service";
import {from} from "rxjs";
import {Sample} from "../../models/samples/sample";
import {IdbSamplesService} from "./samples/idb-samples.service";
import {Recipe} from "../../models/recipes/recipe";
import {IdbRecipesService} from "./recipes/idb-recipes.service";
import {SampleDetails} from "../../models/samples/sampleDetails";
import {RecipeDetails} from "../../models/recipes/recipeDetails";

@Injectable({
  providedIn: 'root'
})
export class IndexedDbService {

  constructor(
    private readonly idbProjectService: IdbProjectsService,
    private readonly idbSampleService: IdbSamplesService,
    private readonly idbRecipeService: IdbRecipesService
  ) { }

  getProjects(pageIndex: number, pageSize: number, desc: boolean) {
    return from(this.idbProjectService.getProjects(pageIndex, pageSize, desc));
  }

  getProject(id: string) {
    return from(this.idbProjectService.getProject(id));
  }

  getProjectsAmount() {
    return from(this.idbProjectService.getProjectsAmount())
  }

  addProjects(projects: Project[]) {
    this.idbProjectService.addProjects(projects);
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

  getRecipes(pageIndex: number, pageSize: number, desc: boolean) {
    return from(this.idbRecipeService.getRecipes(pageIndex, pageSize, desc));
  }

  getRecipe(id: string) {
    return from(this.idbRecipeService.getRecipe(id));
  }

  addRecipes(recipes: Recipe[]) {
    this.idbRecipeService.addRecipes(recipes);
  }

  addRecipe(recipe: RecipeDetails) {
    this.idbRecipeService.addRecipe(recipe);
  }

  getRecipesAmount() {
    return from(this.idbRecipeService.getRecipesAmount())
  }

  searchSamples(searchPhrase: string, pageIndex: number, pageSize: number) {
    return from(this.idbSampleService.searchSamples(searchPhrase, pageIndex, pageSize))
  }

  searchProjects(searchPhrase: string, pageIndex: number, pageSize: number) {
    return from(this.idbProjectService.searchProjects(searchPhrase, pageIndex, pageSize));
  }

  searchRecipes(searchPhrase: string, pageIndex: number, pageSize: number) {
    return from(this.idbRecipeService.searchRecipes(searchPhrase, pageIndex, pageSize));
  }
}
