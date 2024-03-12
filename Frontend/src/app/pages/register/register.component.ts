import { Component, OnInit } from '@angular/core';
import { AbstractControl, FormBuilder, FormControl, FormGroupDirective, NgForm, ValidationErrors, ValidatorFn, Validators } from '@angular/forms';
import { ErrorStateMatcher } from '@angular/material/core';
import { ActivatedRoute, Router } from '@angular/router';
import { EMPTY, catchError, firstValueFrom } from 'rxjs';
import { InvitationDetails } from 'src/app/core/models/users/invitations/invitationDetails';
import { NotificationService } from 'src/app/core/services/notification/notification.service';
import { RegistrationService } from 'src/app/core/services/registration/registration.service';

export class PasswordsStateMatcher implements ErrorStateMatcher {
  isErrorState(control: FormControl | null, form: FormGroupDirective | NgForm | null): boolean {
    const invalidCtrl = !!(control?.invalid && control?.parent?.dirty);
    const invalidParent = !!(control?.parent?.invalid && control?.parent?.dirty);

    return invalidCtrl || invalidParent;
  }
}

@Component({
  selector: 'app-register',
  templateUrl: './register.component.html',
  styleUrls: ['./register.component.scss']
})
export class RegisterComponent implements OnInit {
  constructor(
    private readonly registrationService: RegistrationService,
    private readonly route: ActivatedRoute,
    private readonly router: Router,
    private readonly notificationService: NotificationService
  ){
    this.token = this.route.snapshot.paramMap.get('token');
  }

  matcher = new PasswordsStateMatcher();
  invitationDetails?: InvitationDetails;
  fb = new FormBuilder();
  firstPassword = new FormControl('', [Validators.required, Validators.minLength(8)]);
  secondPassword = new FormControl('', [Validators.required, Validators.minLength(8)]);
  showPassword = false;
  token: string | null = null;

  checkPasswords: ValidatorFn = (group: AbstractControl):  ValidationErrors | null => {
    let pass = group.get('firstPassword')?.value;
    let confirmPass = group.get('secondPassword')?.value;
    return pass === confirmPass ? null : { notSame: true }
  }

  form = this.fb.group({
    firstPassword: this.firstPassword,
    secondPassword: this.secondPassword
  }, {validators: this.checkPasswords});

  ngOnInit() {
    this.registrationService.getInvitation(this.token ?? '').pipe(
      catchError(() => {
        console.error('Invitation not found!');
        this.router.navigate(['/']);
        return EMPTY;
      })
    ).subscribe(r => this.invitationDetails = r);
  }

  onSubmit() {
    if (this.form.valid && this.invitationDetails) {
      this.registrationService.confirmInvitation(
        this.token!,
        this.form.get('firstPassword')!.value!
      ).subscribe(r => {
        this.router.navigate(['/login']);
        this.notificationService.notifySuccess("Registration successful. You can now log in.");
      });
    }
  }




}
