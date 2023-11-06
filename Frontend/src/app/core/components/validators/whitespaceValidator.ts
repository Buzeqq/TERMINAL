import { AbstractControl, ValidationErrors } from "@angular/forms";

export const whitespaceValidator = (control: AbstractControl<any, any>): ValidationErrors | null => {
  if (!control.value) return null;
  return control.value.trim().length ? null : { 'whitespace': true };
};
