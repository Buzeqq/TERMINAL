import { Recipe } from "../recipes/recipe";
import { Tag } from "../tags/tag";
import {StepDetails} from "../steps/stepDetails";

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
