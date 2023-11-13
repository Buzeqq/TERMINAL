import {ParameterValue} from "../parameters/parameter-value";

export interface Step {
  parameters: ParameterValue[]
  comment: string
}
