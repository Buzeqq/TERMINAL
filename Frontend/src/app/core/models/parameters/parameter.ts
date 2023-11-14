export interface TextParameter {
  id: string;
  name: string;
  allowedValues: string[];
  $type: 'text';
}

export interface NumericParameter {
  id: string;
  name: string;
  unit: string;
  $type: 'integer' | 'decimal';
  step: number;
}

export type Parameter = TextParameter | NumericParameter;

export function isText(parameter: Parameter): parameter is TextParameter {
  console.log()
  return parameter.$type === 'text';
}

export function isNumeric(parameter: Parameter): parameter is NumericParameter {
  return parameter.$type === 'integer' || parameter.$type === 'decimal';
}
