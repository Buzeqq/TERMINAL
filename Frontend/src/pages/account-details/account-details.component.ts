import { Component, inject } from '@angular/core';
import { MatCard, MatCardHeader, MatCardTitle } from "@angular/material/card";
import { BasePageComponent } from "../../core/components/base-page/base-page.component";
import { Store } from "@ngrx/store";
import { selectIdentity } from "../../core/state/identity/identity.selectors";

@Component({
  selector: 'app-account-details',
  standalone: true,
    imports: [
        MatCard,
        MatCardTitle,
        MatCardHeader,
        BasePageComponent
    ],
  templateUrl: './account-details.component.html',
  styleUrl: './account-details.component.scss'
})
export class AccountDetailsComponent {
  private readonly store = inject(Store);
  userInfo = this.store.selectSignal(selectIdentity);
}
