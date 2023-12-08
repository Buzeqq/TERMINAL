import {ParameterValue} from "../parameters/parameter-value";

export interface StepDetails {
  id: string
  parameters: ParameterValue[]
  comment: string
}
