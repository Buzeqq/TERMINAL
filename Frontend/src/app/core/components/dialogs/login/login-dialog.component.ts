import {Component, OnDestroy} from '@angular/core';
import {FormBuilder, FormControl, FormGroup, Validators} from "@angular/forms";
import {AuthService} from "../../../services/auth/auth.service";
import {Subscription} from "rxjs";

@Component({
  selector: 'app-login',
  templateUrl: './login-dialog.component.html',
  styleUrls: ['./login-dialog.component.scss']
})
export class LoginDialogComponent implements OnDestroy {
  form: FormGroup;
  usernameFormControl = new FormControl('',[Validators.required])
  passwordFormControl = new FormControl('',[Validators.required])
  subscription: Subscription | undefined;

  constructor (
    private fb: FormBuilder,
    private authService: AuthService
  ) {
    this.form = this.fb.group([this.usernameFormControl, this.passwordFormControl]);
  }

  login() {
    let username = this.usernameFormControl.value;
    let password = this.passwordFormControl.value;
    if (username && password) {
      this.subscription = this.authService.login(username, password)
        .subscribe(() => {
          // TODO AD: check for success
        });
    }
  }

  ngOnDestroy(): void {
    this.subscription?.unsubscribe();
  }
}
