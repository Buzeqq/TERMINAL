import { FormArray, FormControl, FormGroup, NG_VALUE_ACCESSOR } from "@angular/forms";
import { Parameter } from "../../../core/models/parameters/parameter";
import { Project } from "../../../core/models/projects/project";
import { Recipe } from "../../../core/models/recipes/recipe";
import { Tag } from "../../../core/models/tags/tag";
import { forwardRef } from "@angular/core";

export type StepFormArray = FormGroup<{comment: CommentFormControl, parameters: FormArray<ParameterFormControl>}>;
export type DateFormControl = FormControl<Date>;
export type ProjectFormControl = ComplexTypeFormControl<Project | null, string | null>;
export type RecipeFormControl = ComplexTypeFormControl<Recipe | null, string | null>;
export type TagsFormControl = ComplexTypeFormControl<Tag[], string[]>;
export type CommentFormControl = FormControl<string | null>;
export type SampleForm = FormGroup<{
  date: DateFormControl,
  project: ProjectFormControl,
  tags: TagsFormControl,
  recipe: RecipeFormControl,
  comment: CommentFormControl,
  steps: FormArray<StepFormArray>
}>
export interface AddSampleFormData {
  projects: Project[];
  recipes: Recipe[];
  tags: Tag[];
}

export type ParameterFormControl = ComplexTypeFormControl<Parameter>;
export class ComplexTypeFormControl<TComplexType, TFormState = number | string | null> extends FormControl {
  constructor(public item: TComplexType,
              formState?: TFormState | null, validatorOrOpts?: any, asyncValidator?: any) {
    super(formState, validatorOrOpts, asyncValidator);
  }

  public setItem(item: TComplexType, formState: TFormState) {
    this.item = item;
    this.setValue(formState);
  }
}

export const COMPLEX_TYPE_FORM_CONTROL_VALUE_ACCESSOR = {
  provide: NG_VALUE_ACCESSOR,
  useExisting: forwardRef(() => ComplexTypeFormControl),
  multi: true
}
