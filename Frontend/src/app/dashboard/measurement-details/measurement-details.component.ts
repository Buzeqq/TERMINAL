import { Component, Input } from '@angular/core';
import { MeasurementDetails } from "../../core/models/measurements/measurementDetails";

@Component({
  selector: 'app-measurement-details',
  templateUrl: './measurement-details.component.html',
  styleUrls: ['./measurement-details.component.scss']
})
export class MeasurementDetailsComponent {
  @Input()
  get measurementId(): string | undefined {
    return this._measurementId;
  }

  set measurementId(id: string | undefined) {
    this.measurementDetails = measurementsDetails.find(m => m.measurementId === id);
  }

  private _measurementId?: string;

  measurementDetails?: MeasurementDetails;
}

const measurementsDetails: MeasurementDetails[] = [
  {
    measurementId: "1",
    code: "AX1",
    recipeId: null,
    createdAtUct: new Date(),
    comment: "comment for measurement",
    projectId: "1",
    stepsId: ["1", "2"]
  }
]
