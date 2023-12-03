import {CanActivateFn, Router} from '@angular/router';
import {inject, Injectable} from "@angular/core";
import {AuthService} from "../services/auth/auth.service";
import {NotificationService} from "../services/notification/notification.service";

export const pagesGuard: CanActivateFn = (route, state) => {
  return inject(PermissionService).pages();
};

export const loginPageGuard: CanActivateFn = (route, state) => {
  return inject(PermissionService).loginPage();
};

export const settingsPageGuard: CanActivateFn = (route, state) => {
  const canActivate = inject(AuthService).isAdminOrMod()
  if (!canActivate) {
    inject(NotificationService).notifyNoPermission("Access to Settings denied. Contact administration for assistance.");
  }
  return canActivate;
};

@Injectable()
export class PermissionService {
  constructor(
    private readonly authService: AuthService,
    private readonly router: Router,
  ) {  }
  pages() {
    if (this.authService.isLoggedIn()) {
      return true;
    } else {
      this.authService.logout();
      this.router.navigate(['/login']);
      return false;
    }
  }
  loginPage() {
    if (this.authService.isLoggedIn()) {
      this.router.navigate(['/home']);
      return false;
    } else {
      return true;
    }
  }
}
