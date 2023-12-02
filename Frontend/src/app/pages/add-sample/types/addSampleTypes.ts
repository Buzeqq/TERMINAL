import { FormArray, FormControl, FormGroup } from "@angular/forms";
import { Parameter } from "../../../core/models/parameters/parameter";
import { Project } from "../../../core/models/projects/project";
import { Recipe } from "../../../core/models/recipes/recipe";
import { Tag } from "../../../core/models/tags/tag";

export type StepFormArray = FormGroup<{comment: CommentFormControl, parameters: FormArray<ParameterFormControl>}>;
export type DateFormControl = FormControl<Date>;
export type StringFormControl = FormControl<string | null>;
export type ProjectFormControl = StringFormControl;
export type RecipeFormControl = StringFormControl;
export type TagsFormControl = FormControl<string[]>;
export type CommentFormControl = StringFormControl;
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

export class ParameterFormControl<T = number | string | null> extends FormControl {
  constructor(public readonly parameter : Parameter,
              formState?: T | null, validatorOrOpts?: any, asyncValidator?: any) {
    super(formState, validatorOrOpts, asyncValidator);
  }
}
