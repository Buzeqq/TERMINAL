import { Component, Output, EventEmitter, AfterViewInit } from '@angular/core';
import { BehaviorSubject, tap } from 'rxjs';
import { Sample } from 'src/app/core/models/samples/sample';
import { SamplesService } from 'src/app/core/services/samples/samples.service';
import { SelectedItem } from "../../../models/items/selected-item";

@Component({
  selector: 'app-sample-views',
  templateUrl: './sample-views.component.html',
  styleUrls: ['./sample-views.component.scss']
})
export class SampleViewsComponent implements AfterViewInit {
  displayedColumns: string[] = ['code', 'project', 'created'];
  private readonly pageSize = 10;
  private page = 0;
  private readonly samplesSubject = new BehaviorSubject<Sample[]>([]);
  samples$ = this.samplesSubject.asObservable();
  selectedItem: SelectedItem | undefined;
  @Output() selectedItemChangeEvent = new EventEmitter<SelectedItem>();

  constructor(
    private readonly samplesService: SamplesService,
  ) {  }

  ngAfterViewInit(): void {
    this.samplesService.getSamples(this.page, this.pageSize)
      .pipe(tap(r => this.selectSample(r[0])))
      .subscribe(r => this.samplesSubject.next(r))
  }

  onScroll(event: any) {
    // check whether scroll reached bottom
    if (event.target.offsetHeight + event.target.scrollTop >= event.target.scrollHeight) {
      this.page += 1;
      this.samplesService.getSamples(this.page, this.pageSize)
        .subscribe(r =>
          this.samplesSubject.next(this.samplesSubject.value.concat(r))
        )
      console.log(`loaded another ${this.pageSize} results, page: ${this.page}`);
    }
  }

  selectSample(m: Sample) {
    this.selectedItem = {type: 'Sample', id: m.id};
    this.selectedItemChangeEvent.emit({type: 'Sample', id: m.id});
  }
}
