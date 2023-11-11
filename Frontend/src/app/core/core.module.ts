import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import { TimeAgoPipe } from './pipes/time-ago.pipe';
import { MatButtonModule } from "@angular/material/button";
import { MatIconModule } from "@angular/material/icon";
import { MatProgressSpinnerModule } from "@angular/material/progress-spinner";
import { CastPipe } from './pipes/cast.pipe';
import { AddProjectDialogComponent } from "./components/dialogs/add-project/add-project-dialog.component";
import { MatDialogModule } from "@angular/material/dialog";
import { MatInputModule } from "@angular/material/input";
import { FormsModule, ReactiveFormsModule } from "@angular/forms";
import { LoginDialogComponent } from "./components/dialogs/login/login-dialog.component";


@NgModule({
  declarations: [
    TimeAgoPipe,
    CastPipe,
    AddProjectDialogComponent,
    LoginDialogComponent
  ],
    exports: [
        TimeAgoPipe,
    ],
  imports: [
    CommonModule,
    MatButtonModule,
    MatIconModule,
    MatProgressSpinnerModule,
    MatDialogModule,
    MatInputModule,
    FormsModule,
    ReactiveFormsModule
  ]
})
export class CoreModule { }
