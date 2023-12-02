import {Component, OnInit, ViewChild} from '@angular/core';
import { map, Observable } from "rxjs";
import { BreakpointObserver } from "@angular/cdk/layout";
import { AddProjectDialogComponent } from "./core/components/dialogs/add-project/add-project-dialog.component";
import { MatDialog } from "@angular/material/dialog";
import { Router } from "@angular/router";
import {AuthService} from "./core/services/auth/auth.service";
import {NotificationService} from "./core/services/notification/notification.service";
import { MatSidenav } from '@angular/material/sidenav';

@Component({
  selector: 'app-root',
  templateUrl: './app.component.html',
  styleUrls: ['./app.component.scss']
})
export class AppComponent implements OnInit {
  @ViewChild('sidenav') sidenav!: MatSidenav;
  isUserLoggedOut$ = this.authService.isLoggedOut();
  
  isMobile$: Observable<boolean> = this.breakpointObserver.observe('(max-width: 768px)').pipe(map(result => result.matches));
  isMobile: boolean = false;

  // honestly, I don't know why this works, but it does and I'm not going to question it
  isExpanded$: Observable<boolean> = this.isMobile$.pipe(map(result => result || !this.isMobile));
  isExpanded: boolean = false // toggleable

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

    this.isMobile$.subscribe(result => {
      this.isMobile = result;
      this.isExpanded = result;
    });
  }

  private isUserLoggedIn(){
    var loggedIn = false;
    this.isUserLoggedOut$.subscribe(loggedOut => {
      if (!loggedOut) {
        loggedIn = true;
      }
    });
    return loggedIn;
  }

  toggleWhenMobile() {
    if (this.isUserLoggedIn() && this.isMobile) {
      this.isExpanded = true;
      this.sidenav.toggle();
    }
  }

  expandWhenDesktop() {
    if (this.isUserLoggedIn() && !this.isMobile) {
      this.isExpanded = !this.isExpanded;
    }
  }

  closeWhenClicked(){
    if (this.isUserLoggedIn() && this.isMobile) {
      this.sidenav.close();
    }
  }
}
