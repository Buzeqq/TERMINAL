import {ParameterValue} from "../../models/parameters/parameter-value";

export interface StepEntity {
  id: string,
  parameters: ParameterValue[]
  comment: string,
}
