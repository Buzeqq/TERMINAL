import {AfterViewInit, Component} from '@angular/core';
import {Measurement, MeasurementsService, RawMeasurement} from "../measurements.service";
import {StatusService} from "../status.service";
import {v4 as uuidv4} from 'uuid';

@Component({
  selector: 'app-measurements',
  templateUrl: './measurements.component.html',
  styleUrls: ['./measurements.component.scss']
})
export class MeasurementsComponent implements AfterViewInit {
  measurements$ = this.measurementService.measurements$;
  columns = ["id", "value", "created", "isEditing"];
  $isOnline = this.statusService.isOnline$;
  data: Measurement[] = [];
  constructor(private readonly measurementService: MeasurementsService,
              private readonly statusService: StatusService) {
  }
  ngAfterViewInit(): void {
    this.measurements$.subscribe(r => this.data = r);
  }
  logData() {
    console.log(this.data);
  }

  async addData() {
    const now = new Date();
    const measurement: RawMeasurement = {
      id: uuidv4(),
      value: Math.random(),
      createdOnUtc: now.toUTCString()
    };
    (await this.measurementService.addMeasurement(measurement)).subscribe();
  }

  async update(row: { id: string, value: number, createdOnUtc: Date, isEditing: boolean }) {
    (await this.measurementService.updateMeasurement(row)).subscribe();
  }
}
