import { Component } from '@angular/core';
import { BasePageComponent } from "../../core/components/base-page/base-page.component";
import { BasePageHeaderComponent } from "../../core/components/base-page/base-page-header/base-page-header.component";
import {
  BasePageContentComponent
} from "../../core/components/base-page/base-page-content/base-page-content.component";
import { BasePageFooterComponent } from "../../core/components/base-page/base-page-footer/base-page-footer.component";

@Component({
  selector: 'app-samples',
  standalone: true,
  imports: [
    BasePageComponent,
    BasePageComponent,
    BasePageHeaderComponent,
    BasePageContentComponent,
    BasePageFooterComponent
  ],
  templateUrl: './samples.component.html',
  styleUrl: './samples.component.scss'
})
export class SamplesComponent {

}
