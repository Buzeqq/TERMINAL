import { Component } from '@angular/core';
import { CommonModule } from '@angular/common';
import { RouterOutlet } from '@angular/router';
import { NavigationRailComponent } from '../core/components/navigation-rail/navigation-rail.component';
import {
  NavigationRailContainerComponent
} from "../core/components/navigation-rail-container/navigation-rail-container.component";
import {
  NavigationRailContentComponent
} from "../core/components/navigation-rail-content/navigation-rail-content.component";

@Component({
  selector: 'app-root',
  standalone: true,
  imports: [
    CommonModule,
    RouterOutlet,
    NavigationRailComponent,
    NavigationRailContainerComponent,
    NavigationRailContentComponent
  ],
  templateUrl: './app.component.html',
  styleUrls: ['./app.component.scss']
})
export class AppComponent {
  title = 'terminal';
}
