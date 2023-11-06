import { AfterViewInit, Component, ViewChild } from '@angular/core';
import { BehaviorSubject, combineLatestWith, filter, switchMap } from "rxjs";
import { SearchItem, SearchService } from "../../core/services/search/search.service";
import { SearchComponent } from "../../shared/search/search.component";
import { ActivatedRoute } from "@angular/router";
import { FiltersState } from "../../core/types/types";

@Component({
  selector: 'app-results-list',
  templateUrl: './results-list.component.html',
  styleUrls: ['./results-list.component.scss']
})
export class ResultsListComponent implements AfterViewInit {
  private readonly pageSize = 20;
  private readonly page = new BehaviorSubject<number>(0);
  private readonly searchResult = new BehaviorSubject<SearchItem[]>([]);
  public get searchResult$() {
    return this.searchResult.asObservable();
  }

  selectedItemId?: string;
  selectedItemType: 'Measurement' | 'Project' | 'Recipe' | 'None' = 'Measurement';

  constructor(
    private readonly searchService: SearchService,
    private readonly route: ActivatedRoute,
  ) {  }

  ngAfterViewInit(): void {
    if (this.search) {
      const params = this.parseQueryParams();

      this.search.filters = params.filtersState;
      this.search.defaultSearchPhrase = params.searchPhrase;

      setTimeout(() => {
        this.search!.searchRequest$.pipe(
          switchMap(({ searchPhrase, filterState }) => this.searchService.searchIn(filterState, searchPhrase, 0, this.pageSize)),
        ).subscribe(r => {
          this.page.next(0);
          this.searchResult.next(r);
        });

        this.page.pipe(
          combineLatestWith(this.search!.searchRequest$),
          filter(([page, _]) => page !== 0),
          switchMap(([page, { searchPhrase, filterState}]) =>
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

  @ViewChild(SearchComponent)
  search?: SearchComponent;

  onScroll() {
    this.page.next(this.page.value + 1);
  }

  private parseQueryParams(): { searchPhrase: string, filtersState: FiltersState } {
    const params = this.route.snapshot.queryParams;
    const searchPhrase = params['searchPhrase'];
    const measurements = params['measurements'];
    const projects = params['projects'];
    const recipes = params['recipes'];

    return { searchPhrase, filtersState: { measurements, projects, recipes }}
  }
}

