import { Component, inject, OnInit } from '@angular/core';
import { TerminalIconComponent } from "../../core/components/terminal-icon/terminal-icon.component";
import { MatError, MatFormField, MatLabel, MatSuffix } from "@angular/material/form-field";
import { MatInput } from "@angular/material/input";
import { MatButton, MatIconButton } from "@angular/material/button";
import { MatIcon } from "@angular/material/icon";
import { MatCheckbox } from "@angular/material/checkbox";
import { FormControl, FormGroup, ReactiveFormsModule, Validators } from "@angular/forms";
import { AsyncPipe, JsonPipe } from "@angular/common";
import { MatProgressSpinner } from "@angular/material/progress-spinner";
import { LoginStore } from "./login.store";
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
    MatError,
    MatProgressSpinner,
    AsyncPipe
  ],
  templateUrl: './login.component.html',
  styleUrl: './login.component.scss'
})
export class LoginComponent implements OnInit {

  loginForm = new FormGroup({
    email: new FormControl('', [Validators.required, Validators.email]),
    password: new FormControl('', [Validators.required]),
    rememberMe: new FormControl(false, { nonNullable: true })
  });
  private readonly store = inject(LoginStore);
  readonly isLoading = this.store.selectSignal((state) => state.isLoading);

  ngOnInit() {
    this.store.tryToLoadUser();
  }

  onSubmit() {
    this.store.tryToLogIn(this.loginForm.value as LoginForm);
  }
}
