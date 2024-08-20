import { Component } from '@angular/core';
import { MatIcon } from '@angular/material/icon';

@Component({
  selector: 'app-hint',
  standalone: true,
  imports: [MatIcon],
  templateUrl: './hint.component.html',
  styleUrl: './hint.component.scss',
})
export class HintComponent {}
