import { Step } from "../steps/step";

export interface MeasurementDetails {
  id: string;
  code: string;
  recipeId: string | null;
  createdAtUtc: Date;
  comment: string | null;
  projectId: string;
  steps: Step[];
  tags: string[];
}
