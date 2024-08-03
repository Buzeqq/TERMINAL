import {Component, Input} from '@angular/core';
import { MatCard, MatCardContent, MatCardFooter, MatCardHeader } from "@angular/material/card";
import { TitleCasePipe } from "@angular/common";

@Component({
  selector: 'app-base-page',
  standalone: true,
  imports: [
    MatCard,
    MatCardHeader,
    MatCardContent,
    MatCardFooter,
    TitleCasePipe
  ],
  templateUrl: './base-page.component.html',
  styleUrl: './base-page.component.scss'
})
export class BasePageComponent {
  @Input({ required: true }) title = '';
}
