export interface Parameter {
  id: string;
  $type: "decimal" | "integer" | "text";
  name: string;
  order: number;
  defaultValue: number | null;
}

export interface NumericParameter extends Parameter {
  step: number;
  unit: string;
}

export interface TextParameter extends Parameter {
  allowedValues: string[];
}

export function isText(parameter: Parameter): parameter is TextParameter {
  return parameter.$type === 'text';
}

export function isNumeric(parameter: Parameter): parameter is NumericParameter {
  return parameter.$type === 'integer' || parameter.$type === 'decimal';
}
