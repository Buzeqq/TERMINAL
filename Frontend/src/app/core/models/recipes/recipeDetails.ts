import { Step } from "../steps/step";

export interface RecipeDetails {
  id: string;
  name: string;
  steps: Step[];
}
