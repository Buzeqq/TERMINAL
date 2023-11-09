import { AfterViewInit, Component, ViewChild } from '@angular/core';
import { BehaviorSubject, combineLatestWith, filter, switchMap } from "rxjs";
import { SearchItem, SearchService } from "../../../core/services/search/search.service";
import { SearchComponent } from "../../../core/components/search/search.component";

@Component({
  selector: 'app-results-list',
  templateUrl: './results-list.component.html',
  styleUrls: ['./results-list.component.scss']
})
export class ResultsListComponent implements AfterViewInit {
  selectedItemId?: string;
  selectedItemType: 'Measurement' | 'Project' | 'Recipe' | 'None' = 'Measurement';
  @ViewChild(SearchComponent)
  search?: SearchComponent;
  private readonly pageSize = 20;
  private readonly page = new BehaviorSubject<number>(0);
  private readonly searchResult = new BehaviorSubject<SearchItem[]>([]);

  constructor(
    private readonly searchService: SearchService,
  ) {
  }

  public get searchResult$() {
    return this.searchResult.asObservable();
  }

  ngAfterViewInit(): void {
    if (this.search) {
      setTimeout(() => {
        this.search!.searchRequest$.pipe(
          switchMap(({
                       searchPhrase,
                       filterState
                     }) => this.searchService.searchIn(filterState, searchPhrase, 0, this.pageSize)),
        ).subscribe(r => {
          this.page.next(0);
          this.searchResult.next(r);
        });

        this.page.pipe(
          combineLatestWith(this.search!.searchRequest$),
          filter(([page, _]) => page !== 0),
          switchMap(([page, {searchPhrase, filterState}]) =>
            this.searchService.searchIn(filterState, searchPhrase, page, this.pageSize)
          )
        ).subscribe(r => {
          if (r.length > 0) {
            this.searchResult.next([...this.searchResult.value, ...r]);
          }
        })
      });
    }
  }

  selectItem(row: SearchItem) {
    this.selectedItemId = row.item.id;
    this.selectedItemType = row.type;
  }

  onScroll() {
    this.page.next(this.page.value + 1);
  }
}

