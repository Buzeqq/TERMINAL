import { Component } from '@angular/core';
import {MatCard, MatCardHeader, MatCardTitle} from "@angular/material/card";
import {BasePageComponent} from "../../core/components/base-page/base-page.component";

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

}
