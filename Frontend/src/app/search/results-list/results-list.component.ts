import { AfterViewInit, Component, ViewChild } from '@angular/core';
import { ActivatedRoute } from "@angular/router";
import { BehaviorSubject, Observable, Subscription, switchMap, tap } from "rxjs";
import { SearchItem, SearchService } from "../../core/services/search/search.service";
import { SearchComponent } from "../../shared/search/search.component";
import { CollectionViewer, DataSource } from "@angular/cdk/collections";

@Component({
  selector: 'app-results-list',
  templateUrl: './results-list.component.html',
  styleUrls: ['./results-list.component.scss']
})
export class ResultsListComponent implements AfterViewInit {
  private page = 0;
  private readonly pageSize = 10;
  private readonly threshold = 0.8;
  searchItems$?: Observable<SearchItem[]>;

  selectedItemId?: string;
  selectedItemType: 'Measurement' | 'Project' | 'Recipe' | 'None' = 'Measurement';

  constructor(
    private readonly searchService: SearchService,
    private route: ActivatedRoute
  ) {  }

  ngAfterViewInit(): void {
    if (this.search) {
      setTimeout(() => {
        this.searchItems$ = this.search?.searchRequest$.pipe(
          switchMap(({ searchPhrase, filterState }) => this.searchService.searchIn(filterState, searchPhrase, this.page, this.pageSize)),
          tap(r => console.log(r))
        );
      });
    }
  }

  selectItem(row: SearchItem) {
    this.selectedItemId = row.item.id;
    this.selectedItemType = row.type;
  }

  @ViewChild(SearchComponent)
  search?: SearchComponent;

  onScroll() {

  }
}

