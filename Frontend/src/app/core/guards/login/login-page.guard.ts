import {Injectable} from "@angular/core";
import {ActivatedRouteSnapshot, CanActivate, Router, RouterStateSnapshot} from "@angular/router";
import {AuthService} from "../../services/auth/auth.service";
import {PingService} from "../../services/ping/ping.service";
import {firstValueFrom} from "rxjs";

@Injectable({
  providedIn: 'root',
})
export class LoginPageGuard implements CanActivate {

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
        this.authService.logout();
        this.router.navigate(['/home']);
        return false;
      } else {
        return true;
      }
    } catch (error) {
      console.log('Error checking online status:', error);
      return false;
    }
  }
}
