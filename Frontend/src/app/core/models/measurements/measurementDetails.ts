export interface MeasurementDetails
{
  measurementId: string;
  code: string;
  recipeId: string | null;
  createdAtUct: Date;
  comment: string | null;
  projectId: string;
  stepsId: string[];
}
