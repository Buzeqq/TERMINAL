import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import { TimeAgoPipe } from './pipes/time-ago.pipe';
import { MatButtonModule } from "@angular/material/button";
import { MatIconModule } from "@angular/material/icon";
import { MatProgressSpinnerModule } from "@angular/material/progress-spinner";
import { CastPipe } from './pipes/cast.pipe';
import { AddProjectDialogComponent } from "./components/dialogs/add-project-dialog.component";
import { MatDialogModule } from "@angular/material/dialog";
import { MatInputModule } from "@angular/material/input";
import { FormsModule, ReactiveFormsModule } from "@angular/forms";
import { MatChipsModule } from '@angular/material/chips';
import { ItemDetailsComponent } from "./components/item-details/item-details.component";
import { MeasurementDetailsComponent } from "./components/item-details/measurement-details/measurement-details.component";
import { ProjectDetailsComponent } from "./components/item-details/project-details/project-details.component";
import { SearchComponent } from "./components/search/search.component";
import { MatCardModule } from "@angular/material/card";
import { MatDividerModule } from '@angular/material/divider';
import { AppRoutingModule } from "../app-routing.module";
import { MatProgressBarModule } from "@angular/material/progress-bar";
import { MatCheckboxModule } from "@angular/material/checkbox";


@NgModule({
  declarations: [
    TimeAgoPipe,
    CastPipe,
    AddProjectDialogComponent,
    ItemDetailsComponent,
    MeasurementDetailsComponent,
    ProjectDetailsComponent,
    SearchComponent
  ],
    exports: [
      TimeAgoPipe,
      SearchComponent,
      ItemDetailsComponent
    ],
  imports: [
    CommonModule,
    MatButtonModule,
    MatIconModule,
    MatProgressSpinnerModule,
    MatDialogModule,
    MatInputModule,
    FormsModule,
    ReactiveFormsModule,
    MatChipsModule,
    MatCardModule,
    MatDividerModule,
    AppRoutingModule,
    MatProgressBarModule,
    MatCheckboxModule
  ]
})
export class CoreModule { }
