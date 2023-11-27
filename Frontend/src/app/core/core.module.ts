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
  SampleDetailsComponent
} from "./components/item-details/item-details/sample-details/sample-details.component";
import {
  ProjectDetailsComponent
} from "./components/item-details/item-details/project-details/project-details.component";
import { MatTableModule } from "@angular/material/table";
import { TagViewsComponent } from './components/item-views/tag-views/tag-views.component';
import { CdkFixedSizeVirtualScroll, CdkVirtualScrollViewport } from "@angular/cdk/scrolling";
import { MatRippleModule } from "@angular/material/core";
import { SvgComponent } from "./components/svg/svg.component";


@NgModule({
  declarations: [
    TimeAgoPipe,
    CastPipe,
    AddProjectDialogComponent,
    ItemDetailsComponent,
    SampleDetailsComponent,
    ProjectDetailsComponent,
    SearchComponent,
    TagViewsComponent,
    SearchComponent,
    SvgComponent
  ],
  exports: [
    TimeAgoPipe,
    SearchComponent,
    ItemDetailsComponent,
    SampleDetailsComponent,
    ProjectDetailsComponent,
    TagViewsComponent,
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
    MatTableModule,
    CdkFixedSizeVirtualScroll,
    CdkVirtualScrollViewport,
    MatRippleModule
  ]
})
export class CoreModule { }
