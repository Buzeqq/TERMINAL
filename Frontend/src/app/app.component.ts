import {Component, OnInit} from '@angular/core';
import { PingService } from "./core/services/ping/ping.service";
import {BreakpointObserver, Breakpoints} from "@angular/cdk/layout";

@Component({
  selector: 'app-root',
  templateUrl: './app.component.html',
  styleUrls: ['./app.component.scss']
})
export class AppComponent implements OnInit {
  isExpanded: boolean = false;
  user: string = 'John Doe';
  sideNavMode: 'side' | 'over' = 'side';
  constructor(private readonly pingService: PingService, private breakpointObserver: BreakpointObserver) {
  }

  isOnline$ = this.pingService.isOnline$;

  ngOnInit(): void {
    this.breakpointObserver.observe([
      Breakpoints.HandsetPortrait,
      Breakpoints.HandsetLandscape,
    ]).subscribe(result => {
      if (result.matches) {
        this.sideNavMode = 'over'; // switch to over mode for mobile screens
      } else {
        this.sideNavMode = 'side'; // switch to side mode for tablet/desktop screens
      }
    });
  }
}
