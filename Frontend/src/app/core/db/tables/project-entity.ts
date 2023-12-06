import {Project} from "../../models/projects/project";

export interface ProjectEntity extends Project {
  isActive: 0 | 1
}
