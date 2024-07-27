import {Component, Input} from '@angular/core';
import { MatButton } from "@angular/material/button";
import { MatIcon} from "@angular/material/icon";

@Component({
  selector: 'app-navigation-rail-button',
  standalone: true,
    imports: [
        MatButton,
        MatIcon
    ],
  templateUrl: './navigation-rail-button.component.html',
  styleUrl: './navigation-rail-button.component.scss'
})
export class NavigationRailButtonComponent {
  @Input({ required: true }) iconName: string = '';
  @Input({ required: true }) label: string = '';

  isActive = false;
}
