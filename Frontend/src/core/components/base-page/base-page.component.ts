import {Component, Input} from '@angular/core';
import {MatCard, MatCardHeader} from "@angular/material/card";

@Component({
  selector: 'app-base-page',
  standalone: true,
    imports: [
        MatCard,
        MatCardHeader
    ],
  templateUrl: './base-page.component.html',
  styleUrl: './base-page.component.scss'
})
export class BasePageComponent {
  @Input({ required: true }) title = '';
}
