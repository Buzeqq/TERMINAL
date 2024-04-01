import { Component, signal } from '@angular/core';
import { MatListItem, MatNavList } from "@angular/material/list";
import { MatIcon } from "@angular/material/icon";
import { MatButton, MatFabButton, MatIconButton } from "@angular/material/button";
import { MatRipple } from "@angular/material/core";
import { NavigationRailButtonComponent } from "../navigation-rail-button/navigation-rail-button.component";
import {Router} from "@angular/router";

type ActiveButton = 'dashboard' | 'samples' | 'recipe' | 'projects' | 'account';
@Component({
  selector: 'app-navigation-rail',
  standalone: true,
  imports: [
    MatNavList,
    MatListItem,
    MatIcon,
    MatFabButton,
    MatIconButton,
    MatRipple,
    MatButton,
    NavigationRailButtonComponent,
    NavigationRailButtonComponent
  ],
  templateUrl: './navigation-rail.component.html',
  styleUrl: './navigation-rail.component.scss'
})
export class NavigationRailComponent {
  protected activeButton = signal<ActiveButton | undefined>(undefined);

  constructor(protected readonly router: Router) {}

  async setActive(button: ActiveButton, route: string) {
    this.activeButton.set(button);
    await this.router.navigate([route]);
  }
}
