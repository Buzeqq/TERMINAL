export interface AddMeasurement {
  projectId: string;
  recipeId: string | null;
  steps: Step[];
  tagIds: string[];
  comment: string | null | undefined;
}

export interface Step {
  parameters: ParameterValue[];
  comment: string | null | undefined;
}

export interface ParameterValue {
  $type: 'decimal' | 'integer' | 'text';
  id: string;
  value: string | number;
}
