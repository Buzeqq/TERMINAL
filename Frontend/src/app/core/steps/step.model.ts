import { ParameterValue } from "../parameters/parameter.model";

export interface StepDetails {
  id: string
  parameters: ParameterValue[]
  comment: string
}
