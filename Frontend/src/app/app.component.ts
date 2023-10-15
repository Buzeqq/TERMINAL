import { Component } from '@angular/core';
import { PingService } from "./core/services/ping/ping.service";

@Component({
  selector: 'app-root',
  templateUrl: './app.component.html',
  styleUrls: ['./app.component.scss']
})
export class AppComponent {
  constructor(private readonly pingService: PingService) {
  }

  isOnline$ = this.pingService.isOnline$;
}
