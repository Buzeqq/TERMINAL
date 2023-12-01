import {Component, OnInit} from '@angular/core';
import { map, Observable } from "rxjs";
import { BreakpointObserver } from "@angular/cdk/layout";
import { AddProjectDialogComponent } from "./core/components/dialogs/add-project/add-project-dialog.component";
import { MatDialog } from "@angular/material/dialog";
import { Router } from "@angular/router";
import {AuthService} from "./core/services/auth/auth.service";
import {NotificationService} from "./core/services/notification/notification.service";

@Component({
  selector: 'app-root',
  templateUrl: './app.component.html',
  styleUrls: ['./app.component.scss']
})
export class AppComponent implements OnInit {
  isExpanded: boolean = false;

  menuOpened$: Observable<boolean> = this.breakpointObserver.observe('(max-width: 768px)')
    .pipe(map(result => !result.matches));

  isLoggedOut$ = this.authService.isLoggedOut();

  constructor(
    private readonly breakpointObserver: BreakpointObserver,
    private readonly dialog: MatDialog,
    protected readonly router: Router,
    private readonly authService: AuthService,
    private readonly snackbar: NotificationService
  ) {  }

  openAddProjectDialog() {
    this.dialog.open(AddProjectDialogComponent);
  }

  ngOnInit(): void {
    this.authService.sessionWarningTimer$
      .subscribe(_ => this.snackbar.notifySessionExpiration(this.authService.alertBefore)
        .subscribe(_ => this.authService.renewSession()));
  }
}
