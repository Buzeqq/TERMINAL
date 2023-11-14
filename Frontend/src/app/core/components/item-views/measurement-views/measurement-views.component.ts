import { Component, Output, EventEmitter, AfterViewInit } from '@angular/core';
import { BehaviorSubject, tap } from 'rxjs';
import { Measurement } from 'src/app/core/models/measurements/measurement';
import { MeasurementsService } from 'src/app/core/services/measurements/measurements.service';
import { SelectedItem } from "../../../models/items/selected-item";

@Component({
  selector: 'app-measurement-views',
  templateUrl: './measurement-views.component.html',
  styleUrls: ['./measurement-views.component.scss']
})
export class MeasurementViewsComponent implements AfterViewInit {
  displayedColumns: string[] = ['code', 'project', 'created'];
  private readonly pageSize = 10;
  private page = 0;
  private readonly measurementsSubject = new BehaviorSubject<Measurement[]>([]);
  measurements$ = this.measurementsSubject.asObservable().pipe(tap(console.log));
  selectedItem: SelectedItem | undefined;
  @Output() selectedItemChangeEvent = new EventEmitter<SelectedItem>();

  constructor(
    private readonly measurementService: MeasurementsService,
  ) {  }

  ngAfterViewInit(): void {
    this.measurementService.getMeasurements(this.page, this.pageSize)
      .pipe(tap(r => this.selectMeasurement(r[0])))
      .subscribe(r => this.measurementsSubject.next(r))
  }

  onScroll(event: any) {
    // check whether scroll reached bottom
    if (event.target.offsetHeight + event.target.scrollTop >= event.target.scrollHeight) {
      this.page += 1;
      this.measurementService.getMeasurements(this.page, this.pageSize)
        .subscribe(r =>
          this.measurementsSubject.next(this.measurementsSubject.value.concat(r))
        )
      console.log(`loaded another ${this.pageSize} results, page: ${this.page}`);
    }
  }

  selectMeasurement(m: Measurement) {
    this.selectedItem = {type: 'Measurement', id: m.id};
    this.selectedItemChangeEvent.emit({type: 'Measurement', id: m.id});
  }
}
