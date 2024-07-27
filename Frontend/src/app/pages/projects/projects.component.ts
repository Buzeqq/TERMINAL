import { Component } from '@angular/core';
import {BasePageComponent} from "../../core/components/base-page/base-page.component";

@Component({
  selector: 'app-projects',
  standalone: true,
  imports: [
    BasePageComponent
  ],
  templateUrl: './projects.component.html',
  styleUrl: './projects.component.scss'
})
export class ProjectsComponent {

}
