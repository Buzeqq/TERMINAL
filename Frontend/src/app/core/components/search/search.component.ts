import { Component, Input, Output } from '@angular/core';
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
import { MatDialog } from "@angular/material/dialog";
import { AddProjectDialogComponent } from "../dialogs/add-project-dialog.component";

@Component({
  selector: 'app-search',
  templateUrl: './search.component.html',
  styleUrls: ['./search.component.scss'],
})
export class SearchComponent {
  protected readonly searchPhrase$ = new BehaviorSubject<string>("");
  protected readonly filters$ = new ReplaySubject();
  protected filtersState: Record<string, boolean> = {};
  private readonly filtersState$ = new ReplaySubject<Record<string, boolean>>();

  constructor(private readonly dialog: MatDialog) {
  }

  @Output('searchRequest')
  public readonly searchRequest$ = this.filtersState$.pipe(
    combineLatestWith(this.searchPhrase$.pipe(
      filter(phrase => phrase.length > 2),
      debounceTime(500),
      distinctUntilChanged()
    )),
    map(([filterState, searchPhrase, ]) => ({searchPhrase, filterState}))
  );

  @Input({
    required: true
  })
  set filters(value: Record<string, boolean>) {
    this.filtersState = value;
    this.filtersState$.next(this.filtersState);
    this.filters$.next(value);
  }

  public updateFilterState(key: string, event: MatCheckboxChange) {
    this.filtersState[key] = event.checked;
    this.filtersState$.next(this.filtersState);
  }

  openAddProjectDialog() {
    this.dialog.open(AddProjectDialogComponent);
  }
}
