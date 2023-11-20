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
import { MatChipsModule } from '@angular/material/chips';
import { SearchComponent } from "./components/search/search.component";
import { MatCardModule } from "@angular/material/card";
import { MatDividerModule } from '@angular/material/divider';
import { AppRoutingModule } from "../app-routing.module";
import { MatProgressBarModule } from "@angular/material/progress-bar";
import { MatCheckboxModule } from "@angular/material/checkbox";
import { ItemDetailsComponent } from "./components/item-details/item-details/item-details.component";
import {
  MeasurementDetailsComponent
} from "./components/item-details/item-details/measurement-details/measurement-details.component";
import {
  ProjectDetailsComponent
} from "./components/item-details/item-details/project-details/project-details.component";
import { MatTableModule } from "@angular/material/table";
import {SvgComponent} from "./components/svg/svg.component";


@NgModule({
  declarations: [
    TimeAgoPipe,
    CastPipe,
    AddProjectDialogComponent,
    ItemDetailsComponent,
    MeasurementDetailsComponent,
    ProjectDetailsComponent,
    SearchComponent,
    SvgComponent
  ],
  exports: [
    TimeAgoPipe,
    SearchComponent,
    ItemDetailsComponent,
    SvgComponent
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
    MatCheckboxModule,
    MatTableModule
  ]
})
export class CoreModule { }
