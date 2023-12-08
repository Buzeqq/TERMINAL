import {StepDetails} from "../steps/stepDetails";

export interface EditSample {
  recipeId: string | null,
  comment: string | null | undefined;
  projectId: string,
  steps: StepDetails[],
  tagIds: string[];
}
