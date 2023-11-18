import {AfterViewInit, Component, EventEmitter, Output} from '@angular/core';
import {BehaviorSubject, tap} from "rxjs";
import {SelectedItem} from "../../../models/items/selected-item";
import {Tag} from "../../../models/tags/tag";
import {TagsService} from "../../../services/tags/tags.service";

@Component({
  selector: 'app-tag-views',
  templateUrl: './tag-views.component.html',
  styleUrls: ['./tag-views.component.scss']
})
export class TagViewsComponent implements AfterViewInit {
  displayedColumns: string[] = ['name'];
  private readonly pageSize = 10;
  private page = 0;
  private readonly tagsSubject = new BehaviorSubject<Tag[]>([]);
  tags$= this.tagsSubject.asObservable();
  selectedItem?: SelectedItem;
  @Output() selectedItemChangeEvent = new EventEmitter<SelectedItem>();

  constructor(
    private readonly tagService: TagsService,
  ) { }

  ngAfterViewInit(): void {
    this.tagService.getTags(this.page, this.pageSize)
      .pipe(tap(r => this.selectTag(r[0])))
      .subscribe(r => this.tagsSubject.next(r))
  }

  selectTag(t: Tag) {
    const name = t as unknown as string; // this is needed bcs tag is actually a string
    this.selectedItem = {type: 'Tag', id: name};
    this.selectedItemChangeEvent.emit(this.selectedItem);
  }

  onScroll(event: any) {
    // check whether scroll reached bottom
    if (event.target.offsetHeight + event.target.scrollTop >= event.target.scrollHeight) {
      this.page += 1;
      this.tagService.getTags(this.page, this.pageSize)
        .subscribe(r =>
          this.tagsSubject.next(this.tagsSubject.value.concat(r))
        )
      console.log(`loaded another ${this.pageSize} results, page: ${this.page}`);
    }
  }
}
