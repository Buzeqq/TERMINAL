import {SampleDetails} from "../../models/samples/sampleDetails";

export interface SampleEntity extends SampleDetails {
  projectName: string;
}
