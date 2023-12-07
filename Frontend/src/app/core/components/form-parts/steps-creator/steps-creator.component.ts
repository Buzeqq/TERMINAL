import { Component, Input, OnDestroy, OnInit } from '@angular/core';
import { FormArray, FormControl, FormGroup } from "@angular/forms";
import {
  CommentFormControl,
  ComplexTypeFormControl,
  ParameterFormControl,
  StepFormArray
} from "../../../../pages/add-sample/types/addSampleTypes";
import { NumericParameter, Parameter, TextParameter } from "../../../models/parameters/parameter";
import { SetupFormService } from "../../../services/setup-form/setup-form.service";
import { Subscription } from "rxjs";

@Component({
  selector: 'app-steps-creator',
  templateUrl: './steps-creator.component.html',
  styleUrls: ['./steps-creator.component.scss']
})
export class StepsCreatorComponent implements OnInit, OnDestroy {
  @Input({ required: true })
  stepsControls: FormArray<StepFormArray> = new FormArray<StepFormArray>([]);
  selectedTabIndex: number = 0;

  @Input({ required: true })
  parameters: Parameter[] = [];

  private subscriptions: Subscription[] = [];

  constructor(readonly setupFormService: SetupFormService) {
  }

  getRootControls(parameters: FormArray<ParameterFormControl>): ParameterFormControl[] {
    return parameters.controls.filter(c => c.parentControl === null);
  }

  getControlChild(parameterControl: ComplexTypeFormControl<Parameter>, step: number): ParameterFormControl | undefined {
    return this.stepsControls.at(step).controls.parameters.controls
      .find(c => c.parentControl === parameterControl);
  }

  getUnit(parameter: Parameter): string | undefined {
    if (parameter.$type !== 'text') {
      return (parameter as NumericParameter).unit;
    }
    return undefined;
  }

  getStep(parameter: Parameter) {
    if (parameter.$type !== 'text') {
      return (parameter as NumericParameter).step;
    }

    return undefined;
  }

  getOptions(parameter: Parameter) {
    if (parameter.$type === 'text') {
      return (parameter as TextParameter).allowedValues;
    }

    return [];
  }

  removeTab(i: number) {
    this.stepsControls.removeAt(i);
  }
  addTab(selectedTabIndex: number) {
    const t = this.stepsControls.at(selectedTabIndex).controls.parameters as FormArray<ParameterFormControl>;
    const parameterControls = new FormArray<ParameterFormControl>([]);
    for (const p of t.controls) {
      let newControl = new ComplexTypeFormControl<Parameter>(
        p.item,
        p.value,
        p.validator
      );
      if (newControl.value == null) {
        newControl.disable();
      }
      parameterControls.push(newControl);
    }

    this.subscriptions.push(...this.setupFormService.setParents(parameterControls, this.parameters));

    this.stepsControls.insert(selectedTabIndex + 1, new FormGroup<{comment: CommentFormControl; parameters: FormArray<ParameterFormControl>}>({
      comment: new FormControl<string | null>(''),
      parameters: parameterControls
    }));
  }

  ngOnDestroy(): void {
    this.subscriptions.forEach(s => s.unsubscribe());
  }

  ngOnInit(): void {
    this.subscriptions.push(...this.setupFormService.setParents(this.stepsControls.controls.at(0)!.controls.parameters, this.parameters));
  }
}
