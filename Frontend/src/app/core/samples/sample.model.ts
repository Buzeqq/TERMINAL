import { Recipe } from '../recipes/recipe.model';
import { StepDetails } from '../steps/step.model';
import { Tag } from '../tags/tag.model';
import { Entity } from '../common.model';

export interface Sample extends Entity {
  id: string;
  code: string;
  project: string;
  createdAtUtc: Date;
}

export interface SampleDetails extends Entity {
  id: string;
  code: string;
  recipe: Recipe | null;
  createdAtUtc: Date;
  comment: string | null;
  projectId: string;
  steps: StepDetails[];
  tags: Tag[];
}
