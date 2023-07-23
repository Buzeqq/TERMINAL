import { Component } from '@angular/core';
import {PingService} from "./services/ping/ping.service";

@Component({
  selector: 'app-root',
  templateUrl: './app.component.html',
  styleUrls: ['./app.component.scss']
})
export class AppComponent {
  constructor(private readonly pingService: PingService) { }
  title = 'terminal-client';
  isOnline$ = this.pingService.isOnline$;
}
