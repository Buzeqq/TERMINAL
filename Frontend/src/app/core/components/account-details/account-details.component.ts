import { Component, inject } from '@angular/core';
import {
  MatCard,
  MatCardActions,
  MatCardHeader,
  MatCardTitle,
} from '@angular/material/card';
import { BasePageComponent } from '../base-page/base-page.component';
import { Store } from '@ngrx/store';
import { selectIdentity } from '../../identity/state/identity.selectors';
import { MatTabGroup } from '@angular/material/tabs';
import { MatButton } from '@angular/material/button';
import { IdentityActions } from '../../identity/state/identity.actions';
import { BasePageFooterComponent } from '../base-page/base-page-footer/base-page-footer.component';
import { BasePageContentComponent } from '../base-page/base-page-content/base-page-content.component';

@Component({
  selector: 'app-account-details',
  standalone: true,
  imports: [
    MatCard,
    MatCardTitle,
    MatCardHeader,
    BasePageComponent,
    BasePageContentComponent,
    BasePageFooterComponent,
    MatTabGroup,
    MatCardActions,
    MatButton,
  ],
  templateUrl: './account-details.component.html',
  styleUrl: './account-details.component.scss',
})
export class AccountDetailsComponent {
  private readonly store = inject(Store);
  userInfo = this.store.selectSignal(selectIdentity);

  logout() {
    this.store.dispatch(IdentityActions.tryToLogOut());
  }
}
