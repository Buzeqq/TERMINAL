import { Component } from '@angular/core';
import {MatCard, MatCardTitle} from "@angular/material/card";
import {BasePageComponent} from "../../core/components/base-page/base-page.component";

@Component({
  selector: 'app-dashboard',
  standalone: true,
  imports: [
    MatCard,
    MatCardTitle,
    BasePageComponent
  ],
  templateUrl: './dashboard.component.html',
  styleUrl: './dashboard.component.scss'
})
export class DashboardComponent {

}
