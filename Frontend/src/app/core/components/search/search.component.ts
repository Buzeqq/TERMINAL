import {Component, OnInit, Output} from '@angular/core';
import {
  BehaviorSubject,
  combineLatestWith,
  debounceTime,
  distinctUntilChanged, firstValueFrom,
  map, Observable,
  ReplaySubject
} from "rxjs";
import { MatCheckboxChange } from "@angular/material/checkbox";
import { MatDialog } from "@angular/material/dialog";
import {ActivatedRoute, Router} from "@angular/router";
import { AddProjectDialogComponent } from "../dialogs/add-project/add-project-dialog.component";
import {AuthService} from "../../services/auth/auth.service";
import {NotificationService} from "../../services/notification/notification.service";
import {PingService} from "../../services/ping/ping.service";

@Component({
  selector: 'app-search',
  templateUrl: './search.component.html',
  styleUrls: ['./search.component.scss'],
})
export class SearchComponent implements OnInit {
  protected readonly searchPhrase$ = new BehaviorSubject<string>("");
  protected readonly filters$ = new ReplaySubject();
  protected filtersState: Record<string, boolean> = {};
  private readonly filtersState$ = new ReplaySubject<Record<string, boolean>>();
  @Output('searchRequest')
  public searchRequest$?: Observable<{ filterState: any; searchPhrase: any }>;

  isOnline$ = this.pingService.isOnline$;
  moderatorPermissions = this.authService.isAdminOrMod();

  constructor(
    private readonly dialog: MatDialog,
    protected readonly router: Router,
    private readonly route: ActivatedRoute,
    private readonly authService: AuthService,
    private readonly notificationService: NotificationService,
    private readonly pingService: PingService
  )
  {
    this.route.queryParamMap.subscribe(
      params => {
        this.filtersState = {
          'samples': params.get('s') ? params.get('s') == 'true' : true,
          'projects': params.get('p') ? params.get('p') == 'true' : true,
          'recipes': params.get('r') ? params.get('r') == 'true' : true
        }
        this.filtersState$.next(this.filtersState);
        this.filters$.next(this.filtersState);
        this.searchPhrase$.next(params.get('q') ?? '');
      }
    );
  }

  ngOnInit(): void {
    this.newSearchRequest();
  }

  newSearchRequest() {
    this.searchRequest$ = this.filtersState$.pipe(
      combineLatestWith(this.searchPhrase$.pipe(
        // filter(phrase => phrase.length > 2),
        debounceTime(500),
        distinctUntilChanged()
      )),
      map(([filterState, searchPhrase,]) => ({searchPhrase, filterState}))
    );
  }

  searchClicked() {
    if (this.router.url.startsWith('/search')) {
      this.newSearchRequest();
    } else {
      this.router.navigate(['/search'], {
        queryParams: {
          q: this.searchPhrase$.value,
          s: this.filtersState['samples'],
          p: this.filtersState['projects'],
          r: this.filtersState['recipes']
        }
      })
    }
  }

  public updateFilterState(key: string, event: MatCheckboxChange) {
    this.filtersState[key] = event.checked;
    this.filtersState$.next(this.filtersState);
  }

  async openAddProjectDialog() {
    const online = await firstValueFrom(this.isOnline$);
    if (!online)
      this.notificationService.notifyConnectionError();
    else if (!this.moderatorPermissions)
      this.notificationService.notifyNoPermission();
    else
      this.dialog.open(AddProjectDialogComponent);
  }
}
