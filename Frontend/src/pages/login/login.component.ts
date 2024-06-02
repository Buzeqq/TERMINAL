import { Component, inject } from '@angular/core';
import { TerminalIconComponent } from "../../core/components/terminal-icon/terminal-icon.component";
import { MatError, MatFormField, MatLabel, MatSuffix } from "@angular/material/form-field";
import { MatInput } from "@angular/material/input";
import { MatButton, MatIconButton } from "@angular/material/button";
import { MatIcon } from "@angular/material/icon";
import { MatCheckbox } from "@angular/material/checkbox";
import { FormControl, FormGroup, ReactiveFormsModule, Validators } from "@angular/forms";
import { JsonPipe } from "@angular/common";
import { Store } from "@ngrx/store";
import { IdentityActions } from "../../core/state/identity/identityActions";
import { LoginForm } from "../../core/identity/identity.model";

@Component({
  selector: 'app-login',
  standalone: true,
  imports: [
    TerminalIconComponent,
    MatFormField,
    MatInput,
    MatLabel,
    MatButton,
    MatIconButton,
    MatIcon,
    MatCheckbox,
    ReactiveFormsModule,
    JsonPipe,
    MatSuffix,
    MatError
  ],
  templateUrl: './login.component.html',
  styleUrl: './login.component.scss'
})
export class LoginComponent {
  loginForm = new FormGroup({
    email: new FormControl('', [Validators.required, Validators.email]),
    password: new FormControl('', [Validators.required]),
    rememberMe: new FormControl(false, { nonNullable: true })
  });

  private readonly store = inject(Store);
  onSubmit() {
    this.store.dispatch(IdentityActions.tryToLogIn({ form: this.loginForm.value as LoginForm }));
  }
}
