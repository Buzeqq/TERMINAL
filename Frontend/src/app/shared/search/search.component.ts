import { AfterViewInit, Component, EventEmitter, Input, Output } from '@angular/core';
import {
  BehaviorSubject,
  combineLatestWith,
  debounceTime,
  distinctUntilChanged,
  filter,
  map,
  ReplaySubject
} from "rxjs";
import { MatCheckboxChange } from "@angular/material/checkbox";
import { FiltersState } from "../../core/types/types";

@Component({
  selector: 'app-search',
  templateUrl: './search.component.html',
  styleUrls: ['./search.component.scss'],
})
export class SearchComponent implements AfterViewInit {
  @Input() defaultSearchPhrase: string = '';
  protected readonly searchPhrase$ = new BehaviorSubject<string>(this.defaultSearchPhrase);
  protected readonly filters$ = new ReplaySubject();
  protected filtersState: FiltersState = { measurements: true, projects: true, recipes: true };
  private readonly filtersState$ = new ReplaySubject<FiltersState>();

  @Output('searchRequest')
  public readonly searchRequest$ = this.filtersState$.pipe(
    combineLatestWith(this.searchPhrase$.pipe(
      filter(phrase => phrase.length > 2),
      debounceTime(500),
      distinctUntilChanged()
    )),
    map(([filterState, searchPhrase, ]) => ({searchPhrase, filterState}))
  );

  @Output()
  searchButtonClick = new EventEmitter<{ searchPhrase: string, filtersState: FiltersState }>();

  @Input({
    required: true
  })
  set filters(value: FiltersState) {
    this.filtersState = value;
    this.filtersState$.next(this.filtersState);
    this.filters$.next(value);
  }

  public updateFilterState(key: string, event: MatCheckboxChange) {
    this.filtersState[key] = event.checked;
    this.filtersState$.next(this.filtersState);
  }

  ngAfterViewInit(): void {
    setTimeout(() => {
      this.searchPhrase$.next(this.defaultSearchPhrase);
      this.filtersState$.next(this.filtersState);
    });
  }
}
