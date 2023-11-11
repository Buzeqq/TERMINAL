import {Component} from '@angular/core';
import {Observable} from "rxjs";
import {PingService} from "../../core/services/ping/ping.service";
import {MatDialog, MatDialogConfig} from "@angular/material/dialog";
import {LoginDialogComponent} from "../../core/components/dialogs/login/login-dialog.component";
import {AuthService} from "../../core/services/auth/auth.service";

@Component({
  selector: 'app-statusbar',
  templateUrl: './statusbar.component.html',
  styleUrls: ['./statusbar.component.scss']
})
export class StatusbarComponent {

  dialogConfig: MatDialogConfig;
  isOnline$: Observable<boolean> = this.pingService.isOnline$;
  usernameLabel$ = this.authService.usernameLabel$;
  loginButtonLabel$ = this.authService.loginButtonLabel$;

  constructor(
    private readonly pingService: PingService,
    private readonly dialog: MatDialog,
    protected readonly authService: AuthService
  ) {
    this.dialogConfig = new MatDialogConfig();
    this.dialogConfig.width = '600px';
    this.dialogConfig.height = '400px';
  }

  openLoginDialog() {
    if (this.authService.isLoggedOut()) {
      this.dialog.open(LoginDialogComponent, this.dialogConfig);
    } else {
      this.authService.logout();
    }
  }
}
