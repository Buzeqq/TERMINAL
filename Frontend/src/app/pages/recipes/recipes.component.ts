import { Component } from '@angular/core';
import {BasePageComponent} from "../../core/components/base-page/base-page.component";

@Component({
  selector: 'app-recipes',
  standalone: true,
  imports: [
    BasePageComponent
  ],
  templateUrl: './recipes.component.html',
  styleUrl: './recipes.component.scss'
})
export class RecipesComponent {

}
