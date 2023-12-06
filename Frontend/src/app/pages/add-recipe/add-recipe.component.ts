import { Component, OnDestroy, OnInit } from '@angular/core';
import { FormArray, FormControl, FormGroup, Validators } from "@angular/forms";
import {
  StepFormArray
} from "../add-sample/types/addSampleTypes";
import { map, Subscription } from "rxjs";
import { Parameter } from "../../core/models/parameters/parameter";
import { ParametersService } from "../../core/services/parameters/parameters.service";
import { SetupFormService } from "../../core/services/setup-form/setup-form.service";
import { environment } from "../../../environments/environment";
import { RecipesService } from "../../core/services/recipes/recipes.service";
import { AddRecipe } from "../../core/models/recipes/addRecipe";
import { Router } from "@angular/router";
import { NotificationService } from "../../core/services/notification/notification.service";

@Component({
  selector: 'app-add-recipe',
  templateUrl: './add-recipe.component.html',
  styleUrls: ['./add-recipe.component.scss']
})
export class AddRecipeComponent implements OnInit, OnDestroy {
  public parameters: Parameter[] = [];
  private readonly subscriptions: Subscription[] = [];

  public recipeForm = new FormGroup({
    name: new FormControl('', [Validators.required]),
    steps: new FormArray<StepFormArray>([])
  });

  constructor(
    private readonly parameterService: ParametersService,
    private readonly setupFormService: SetupFormService,
    private readonly recipeService: RecipesService,
    private readonly router: Router,
    private readonly notificationService: NotificationService) {
  }

  ngOnInit(): void {
    this.parameterService.getParameters()
      .pipe(
        map(parameters => parameters.sort((p1, p2) => p1.order - p2.order)),
      ).subscribe(parameters => {
      this.parameters = parameters;

      const firstStep = this.setupFormService.getFirstStep(parameters);
      this.subscriptions.push(...this.setupFormService.setParents(firstStep.controls.parameters, parameters));

      this.recipeForm.controls.steps.push(firstStep);
    });
  };

  protected readonly environment = environment;

  ngOnDestroy(): void {
    this.subscriptions.forEach(s => s.unsubscribe());
  }

  addRecipe() {
    const name = this.recipeForm.controls.name.value;
    if (!name) return;

    const addRecipe = {
      name,
      steps: this.setupFormService.getStepsDto(this.recipeForm.controls.steps)
    } as AddRecipe;

    this.recipeService.addRecipe(addRecipe).subscribe(_ => {
      this.router.navigate(['/recipes'])
        .then(_ => this.notificationService.notifySuccess('Recipe added!'));
    });
  }
}
