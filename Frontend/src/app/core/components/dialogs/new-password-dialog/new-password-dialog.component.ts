import {Component, Inject} from '@angular/core';
import {AbstractControl, FormBuilder, FormControl, ValidationErrors, ValidatorFn, Validators} from "@angular/forms";
import {MAT_DIALOG_DATA} from "@angular/material/dialog";
import {PasswordsStateMatcher} from "../../../../pages/register/register.component";

@Component({
  selector: 'app-new-password-dialog',
  templateUrl: './new-password-dialog.component.html',
  styleUrls: ['./new-password-dialog.component.scss']
})
export class NewPasswordDialogComponent {
  userPasswordFormControl = new FormControl('', [
    Validators.required,
    Validators.minLength(8)
  ])
  secondPassword = new FormControl('', [Validators.required, Validators.minLength(8)]);
  matcher = new PasswordsStateMatcher();
  fb = new FormBuilder();

  checkPasswords: ValidatorFn = (group: AbstractControl):  ValidationErrors | null => {
    let pass = group.get('firstPassword')?.value;
    let confirmPass = group.get('secondPassword')?.value;
    return pass === confirmPass ? null : { notSame: true }
  }

  form = this.fb.group({
    firstPassword: this.userPasswordFormControl,
    secondPassword: this.secondPassword
  }, {validators: this.checkPasswords});

  constructor(
    @Inject(MAT_DIALOG_DATA) public data: { email: string },
  ) {}

}
