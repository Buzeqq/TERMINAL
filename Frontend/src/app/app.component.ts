import { Component } from '@angular/core';
import { CommonModule } from '@angular/common';
import { RouterOutlet } from '@angular/router';
import { MatProgressSpinner } from "@angular/material/progress-spinner";
import { NavigationRailComponent } from "./core/components/navigation-rail/navigation-rail.component";
import {
  NavigationRailContainerComponent
} from "./core/components/navigation-rail/navigation-rail-container/navigation-rail-container.component";
import {
  NavigationRailContentComponent
} from "./core/components/navigation-rail/navigation-rail-content/navigation-rail-content.component";
import { LoginComponent } from "./pages/login/login.component";

@Component({
  selector: 'app-root',
  standalone: true,
  imports: [
    CommonModule,
    RouterOutlet,
    NavigationRailComponent,
    NavigationRailContainerComponent,
    NavigationRailContentComponent,
    LoginComponent,
    MatProgressSpinner
  ],
  templateUrl: './app.component.html',
  styleUrls: ['./app.component.scss']
})
export class AppComponent {
}
