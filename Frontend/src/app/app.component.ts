import { Component } from '@angular/core';
import { map, Observable } from "rxjs";
import { BreakpointObserver } from "@angular/cdk/layout";
import { PingService } from "./core/services/ping/ping.service";

@Component({
  selector: 'app-root',
  templateUrl: './app.component.html',
  styleUrls: ['./app.component.scss']
})
export class AppComponent {
  isExpanded: boolean = false;
  user: string = 'John Doe';

  menuOpened$: Observable<boolean> = this.breakpointObserver.observe('(max-width: 768px)')
    .pipe(map(result => !result.matches));
  isOnline$: Observable<boolean> = this.pingService.isOnline$;

  constructor(private readonly breakpointObserver: BreakpointObserver, private readonly pingService: PingService) {
  }
}
