import {StepDetails} from "../steps/stepDetails";

export interface RecipeDetails {
  id: string;
  name: string;
  steps: StepDetails[];
}
