import { Component } from '@angular/core';
import { map, Observable } from "rxjs";
import { BreakpointObserver } from "@angular/cdk/layout";
import { PingService } from "./core/services/ping/ping.service";
import { AddProjectDialogComponent } from "./core/components/dialogs/add-project/add-project-dialog.component";
import { MatDialog } from "@angular/material/dialog";

@Component({
  selector: 'app-root',
  templateUrl: './app.component.html',
  styleUrls: ['./app.component.scss']
})
export class AppComponent {
  isExpanded: boolean = false;

  menuOpened$: Observable<boolean> = this.breakpointObserver.observe('(max-width: 768px)')
    .pipe(map(result => !result.matches));

  constructor(
    private readonly breakpointObserver: BreakpointObserver,
    private readonly dialog: MatDialog
  ) {  }

  openAddProjectDialog() {
    this.dialog.open(AddProjectDialogComponent);
  }
}
