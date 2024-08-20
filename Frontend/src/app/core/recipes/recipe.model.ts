import { Entity } from '../common.model';

export interface Recipe extends Entity {
  id: string;
  name: string;
}
