import {Injectable} from "@angular/core";
import {ActivatedRouteSnapshot, CanActivate, Router, RouterStateSnapshot} from "@angular/router";
import {AuthService} from "../../services/auth/auth.service";
import {PingService} from "../../services/ping/ping.service";
import {firstValueFrom} from "rxjs";

@Injectable({
  providedIn: 'root',
})
export class PagesGuard implements CanActivate {
  constructor(
    private authService: AuthService,
    private pingService: PingService,
    private router: Router,
  ) {}

  async canActivate (
    route: ActivatedRouteSnapshot,
    state: RouterStateSnapshot
  ): Promise<boolean> {
    try {
      const online = await firstValueFrom(this.pingService.isOnline$);

      if (!online || this.authService.isLoggedIn()) {
        return true;
      } else {
        this.authService.logout();
        this.router.navigate(['/login']);
        return false;
      }
    } catch (error) {
      console.log('Error checking online status:', error);
      // Handle error, e.g., redirect to an error page
      return false;
    }
  }
}
