import {Component, EventEmitter, Output} from '@angular/core';
import {Observable} from "rxjs";
import {PingService} from "../../services/ping/ping.service";
import {AuthService} from "../../services/auth/auth.service";
import {Router} from "@angular/router";
import {NotificationService} from "../../services/notification/notification.service";

@Component({
  selector: 'app-statusbar',
  templateUrl: './statusbar.component.html',
  styleUrls: ['./statusbar.component.scss']
})
export class StatusbarComponent {
  isOnline$: Observable<boolean> = this.pingService.isOnline$;
  isLoggedOut$ = this.authService.isLoggedOut();

  constructor(
    private readonly pingService: PingService,
    protected readonly authService: AuthService,
    private readonly router: Router,
    private readonly notificationService: NotificationService
  ) {  }

  logOutButtonClicked() {
    this.authService.logout();
    this.router.navigate([''])
      .then(_ => this.notificationService.notifySuccess('Logged out successfully.'))
  }

  @Output() toggleMobileMenu = new EventEmitter<void>();

  toggleMobileMenuClicked() {
    this.toggleMobileMenu.emit();
  }
}
