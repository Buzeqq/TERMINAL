import {ActivatedRouteSnapshot, CanActivate, RouterStateSnapshot} from '@angular/router';
import {Injectable} from "@angular/core";
import {PingService} from "../../services/ping/ping.service";
import {NotificationService} from "../../services/notification/notification.service";
import {firstValueFrom} from "rxjs";

@Injectable({
  providedIn: 'root',
})
export class OnlineFeaturesGuard implements CanActivate {

  constructor(
    private pingService: PingService,
    private notificationService: NotificationService
  ) {}

  async canActivate(
    route: ActivatedRouteSnapshot,
    state: RouterStateSnapshot
  ): Promise<boolean> {
    const online = await firstValueFrom(this.pingService.isOnline$);
    if (!online)
      this.notificationService.notifyConnectionError();

    return online;
  }
}
