import { ParameterValue } from '../parameters/parameter.model';
import { Entity } from '../common.model';

export interface StepDetails extends Entity {
  id: string;
  parameters: ParameterValue[];
  comment: string;
}
