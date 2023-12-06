import {CanActivateFn} from "@angular/router";
import {inject} from "@angular/core";
import {AuthService} from "../../services/auth/auth.service";
import {NotificationService} from "../../services/notification/notification.service";

export const settingsGuard: CanActivateFn = (route, state) => {
  const canActivate = inject(AuthService).isAdminOrMod()
  if (!canActivate) {
    inject(NotificationService).notifyNoPermission("Access to Settings denied. Contact administration for assistance.");
  }
  return canActivate;
};
