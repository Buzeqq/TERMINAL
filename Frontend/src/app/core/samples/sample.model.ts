import { Recipe } from "../recipes/recipe.model";
import { StepDetails } from "../steps/step.model";
import { Tag } from "../tags/tag.model";

export interface Sample {
  id: string;
  code: string;
  project: string;
  createdAtUtc: Date;
}

export interface SampleDetails {
  id: string;
  code: string;
  recipe: Recipe | null;
  createdAtUtc: Date;
  comment: string | null;
  projectId: string;
  steps: StepDetails[];
  tags: Tag[];
}
