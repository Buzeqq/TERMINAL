import { Component } from '@angular/core';
import { ParametersService } from "../../core/services/parameters/parameters.service";

@Component({
  selector: 'app-add-measurement',
  templateUrl: './add-measurement.component.html',
  styleUrls: ['./add-measurement.component.scss']
})
export class AddMeasurementComponent {
  parameters$ = this.parameterService.getParameters();
  constructor(protected readonly parameterService: ParametersService) {
  }
}
