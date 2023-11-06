import { FormControl } from "@angular/forms";

export const whitespaceValidator = (control: FormControl) => {
  if (!control.value) return null;
  return control.value.trim().length ? null : { 'whitespace': true };
};
