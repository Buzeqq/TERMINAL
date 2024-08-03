import { Component, computed, inject } from '@angular/core';
import { MatListItem, MatNavList } from "@angular/material/list";
import { MatIcon } from "@angular/material/icon";
import { MatButton, MatFabButton, MatIconButton } from "@angular/material/button";
import { MatRipple } from "@angular/material/core";
import { NavigationRailButtonComponent } from "./navigation-rail-button/navigation-rail-button.component";
import { Router, RouterLinkActive } from "@angular/router";
import { Store } from "@ngrx/store";
import { selectIdentity } from "../../identity/state/identity.selectors";

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
    NavigationRailButtonComponent,
    RouterLinkActive
  ],
  templateUrl: './navigation-rail.component.html',
  styleUrl: './navigation-rail.component.scss'
})
export class NavigationRailComponent {
  protected readonly router = inject(Router);
  private readonly store = inject(Store);
  private readonly userInfo = this.store.selectSignal(selectIdentity);
  isVisible = computed(() => this.userInfo().isAuthenticated);
}
