import { Component } from '@angular/core';
import {BasePageComponent} from "../../core/components/base-page/base-page.component";

@Component({
  selector: 'app-samples',
  standalone: true,
  imports: [
    BasePageComponent
  ],
  templateUrl: './samples.component.html',
  styleUrl: './samples.component.scss'
})
export class SamplesComponent {

}
