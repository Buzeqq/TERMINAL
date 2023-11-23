import {Component} from '@angular/core';
import {FormBuilder, FormControl, FormGroup, Validators} from "@angular/forms";
import {AuthService} from "../../core/services/auth/auth.service";
import {NotificationService} from "../../core/services/notification/notification.service";
import {Router} from "@angular/router";

@Component({
  selector: 'app-login',
  templateUrl: './login.component.html',
  styleUrls: ['./login.component.scss']
})
export class LoginComponent {
  form: FormGroup;
  emailFormControl = new FormControl('',[Validators.required, Validators.email])
  passwordFormControl = new FormControl('',[Validators.required])

  constructor (
    private fb: FormBuilder,
    private readonly authService: AuthService,
    private readonly notificationService: NotificationService,
    private readonly router: Router
  ) {
    this.form = this.fb.group([this.emailFormControl, this.passwordFormControl]);
  }

  tryLogin() {
    let email = this.emailFormControl.value;
    let password = this.passwordFormControl.value;
    if (email && password) {
      this.authService.login(email, password)
        .subscribe((_) => {
          this.router.navigate(['/home'])
            .then(_ => this.notificationService.notifySuccess('Logged in successfully'));
        }, (_) =>
        {
          this.passwordFormControl.reset();
          this.notificationService.notifyError("Invalid credentials.");
        });
    }
  }
}
