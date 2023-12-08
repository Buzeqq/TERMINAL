import { Component } from '@angular/core';
import { FormBuilder, FormControl, Validators } from '@angular/forms';
import { whitespaceValidator } from '../../validators/whitespaceValidator';
import { FormMatcher } from '../../matchers/formMatcher';
import { RegistrationService } from 'src/app/core/services/registration/registration.service';
import { Clipboard } from '@angular/cdk/clipboard';
import { Router } from '@angular/router';
import { environment } from 'src/environments/environment';

@Component({
  selector: 'app-add-user',
  templateUrl: './add-user.component.html',
  styleUrls: ['./add-user.component.scss']
})
export class AddUserComponent {
  fb = new FormBuilder();
  userEmail = new FormControl('', [Validators.required, Validators.email]);
  userRole = new FormControl('', [ Validators.required ]);

  link?: string;

  form = this.fb.group({
    userEmail: this.userEmail,
    userRole: this.userRole
  });

  userRoles = ['Administrator', 'Moderator', 'User'];

  constructor(
    private readonly registrationService: RegistrationService,
    private readonly clipboard: Clipboard
    ) {
  }

  inviteUser() {
    var email = this.userEmail.value!;
    var role = this.userRole.value!;
    if (role == 'User') role = 'Registered';

    this.registrationService.createInvitation(email, role).subscribe(
      r => {
        // when development include port
        if (!environment.production)
          this.link = `${window.location.protocol}//${window.location.hostname}:${window.location.port}/register/${r.invitationLink}`;
        else
          this.link = `${window.location.protocol}//${window.location.hostname}/register/${r.invitationLink}`;
      }
    );
  }

  copyLink() {
    this.clipboard.copy(this.link!);
  }
}
