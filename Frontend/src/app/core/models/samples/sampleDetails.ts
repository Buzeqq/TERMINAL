import { Step } from "../steps/step";
import { Recipe } from "../recipes/recipe";
import { Tag } from "../tags/tag";

export interface SampleDetails {
  id: string;
  code: string;
  recipe: Recipe | null;
  createdAtUtc: Date;
  comment: string | null;
  projectId: string;
  steps: Step[];
  tags: Tag[];
}
