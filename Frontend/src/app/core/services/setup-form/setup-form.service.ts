import { Injectable } from '@angular/core';
import { FormArray } from "@angular/forms";
import { ParameterFormControl } from "../../../pages/add-sample/types/addSampleTypes";
import { Subscription } from "rxjs";
import { Parameter, TextParameter } from "../../models/parameters/parameter";

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
}
