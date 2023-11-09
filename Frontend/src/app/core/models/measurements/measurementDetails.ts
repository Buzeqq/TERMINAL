export interface MeasurementDetails {
  measurementId: string;
  code: string;
  recipeId: string | null;
  createdAtUtc: Date;
  comment: string | null;
  projectId: string;
  tags: string[];
  stepIds: string[];
}
