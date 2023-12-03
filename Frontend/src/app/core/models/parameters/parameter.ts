export interface Parameter {
  id: string;
  name: string;
  $type: "decimal" | "integer" | "text";
  order: number;
  defaultValue: number;
  parentId: string | null;
}

export interface NumericParameter extends Parameter {
  step: number;
  unit: string;
}

export interface TextParameter extends Parameter {
  allowedValues: string[];
}
