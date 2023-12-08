import { Injectable } from '@angular/core';
import { FormArray, FormControl, FormGroup, Validators } from "@angular/forms";
import {
  CommentFormControl, ComplexTypeFormControl,
  ParameterFormControl,
  StepFormArray
} from "../../../pages/add-sample/types/addSampleTypes";
import { Subscription } from "rxjs";
import { NumericParameter, Parameter, TextParameter } from "../../models/parameters/parameter";
import { ParameterValue, Step } from "../../models/samples/addSample";

@Injectable({
  providedIn: 'root'
})
export class SetupFormService {

  constructor() { }

  public setParents(parameterControls: FormArray<ParameterFormControl>, parameters: Parameter[]): Subscription[] {
    const subscriptions: Subscription[] = [];
    for (const parameterControl of parameterControls.controls) {
      if (!parameterControl.item.parentId) continue;

      const parent = parameters
        .find(p => p.id === parameterControl.item.parentId);
      if (!parent) continue;

      const parentControl = parameterControls.controls
        .find(c => c.item === parent);
      if (!parentControl) continue;

      parameterControl.parentControl = parentControl;
      subscriptions.push(parentControl.valueChanges.subscribe(value => {
        if (value === this.getDefaultValue(parentControl.item)) {
          parameterControl.setValue(null);
          parameterControl.disable();
        } else {
          parameterControl.enable();
        }
      }));
    }

    return subscriptions;
  }

  getDefaultValue(item: Parameter): string {
    if (item.$type === 'text') {
      return (item as TextParameter).allowedValues[item.defaultValue];
    }

    return item.defaultValue.toString();
  }

  getFirstStep(parameters: Parameter[]): StepFormArray {
    const firstStep: StepFormArray = new FormGroup<{
      comment: CommentFormControl;
      parameters: FormArray<ParameterFormControl>
    }>({
      comment: new FormControl<string | null>(''),
      parameters: new FormArray<ParameterFormControl>([])
    });

    for (const parameter of parameters) {
      switch (parameter.$type) {
        case "decimal":
        case "integer": {
          const numericParameter = parameter as NumericParameter;
          firstStep.controls.parameters.push(new ComplexTypeFormControl<Parameter>(
            numericParameter,
            numericParameter.defaultValue,
            [Validators.required]));
          break;
        }
        case "text": {
          const textParameter = parameter as TextParameter;
          const defaultValue = textParameter.defaultValue ?
            textParameter.allowedValues[textParameter.defaultValue] : null;
          firstStep.controls.parameters.push(new ComplexTypeFormControl<Parameter>(textParameter, defaultValue,
            [Validators.required]));
          break;
        }
      }
    }

    return firstStep;
  }

  getStepsDto(stepsControls: FormArray<StepFormArray>): Step[] {
    const steps: Step[] = [];
    for (const stepControls of stepsControls.controls) {
      steps.push({
        parameters: stepControls.controls.parameters.controls
          .filter(c => c.value !== null)
          .map(c => ({
            $type: c.item.$type,
            id: c.item.id,
            value: c.value
          } as ParameterValue)),
        comment: stepControls.controls.comment.value ?? ''
      });
    }

    return steps;
  }
}
