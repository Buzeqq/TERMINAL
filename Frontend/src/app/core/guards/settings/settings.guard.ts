import {ActivatedRouteSnapshot, CanActivate, RouterStateSnapshot} from "@angular/router";
import {Injectable} from "@angular/core";
import {AuthService} from "../../services/auth/auth.service";
import {NotificationService} from "../../services/notification/notification.service";
import {firstValueFrom} from "rxjs";
import {PingService} from "../../services/ping/ping.service";

@Injectable({
  providedIn: 'root',
})
export class SettingsGuard implements CanActivate {

  constructor(
    private pingService: PingService,
    private authService: AuthService,
    private notificationService: NotificationService
  ) {}

  async canActivate(
    route: ActivatedRouteSnapshot,
    state: RouterStateSnapshot
  ): Promise<boolean> {
    const online = await firstValueFrom(this.pingService.isOnline$);
    const permission = this.authService.isAdminOrMod()
    if (!online)
      this.notificationService.notifyConnectionError();
    else if (!permission)
      this.notificationService.notifyNoPermission();

    return online && permission;
  }
}
