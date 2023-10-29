import { NgModule, isDevMode } from '@angular/core';
import { BrowserModule } from '@angular/platform-browser';
import { HttpClientModule } from "@angular/common/http";

import { AppComponent } from './app.component';
import { ServiceWorkerModule } from '@angular/service-worker';
import { MatToolbarModule } from "@angular/material/toolbar";
import { MatIconModule } from "@angular/material/icon";
import { MatButtonModule } from "@angular/material/button";
import { NgOptimizedImage } from "@angular/common";
import { RouterOutlet } from "@angular/router";
import { DashboardComponent } from './dashboard/dashboard.component';
import { CoreModule } from "./core/core.module";
import { MatRippleModule } from "@angular/material/core";
import { MatInputModule } from "@angular/material/input";
import { FormsModule, ReactiveFormsModule } from "@angular/forms";
import { BrowserAnimationsModule } from "@angular/platform-browser/animations";
import { MatListModule } from "@angular/material/list";
import { MatTableModule } from "@angular/material/table";
import { CdkTableModule } from "@angular/cdk/table";
import { MatSidenavModule } from "@angular/material/sidenav";
import { QuickActionsComponent } from './shared/quick-actions/quick-actions.component';
import { MatCheckboxModule } from "@angular/material/checkbox";
import { MatProgressSpinnerModule } from "@angular/material/progress-spinner";
import { MeasurementDetailsComponent } from './shared/item-details/measurement-details/measurement-details.component';
import { MatMenuModule } from '@angular/material/menu';
import { MatTooltipModule } from '@angular/material/tooltip';
import { MatCardModule } from '@angular/material/card';
import { MatProgressBarModule } from '@angular/material/progress-bar';
import { AppRoutingModule } from './app-routing.module';
import { NotFoundComponent } from './not-found/not-found.component';
import { ResultsListComponent } from './search/results-list/results-list.component';
import { ItemDetailsComponent } from './shared/item-details/item-details.component';
import { ProjectDetailsComponent } from './shared/item-details/project-details/project-details.component';
import { MatChipsModule } from '@angular/material/chips';
import { MatGridListModule } from '@angular/material/grid-list';
import { MatSnackBarModule } from '@angular/material/snack-bar';
import { ItemViewsComponent } from './shared/item-views/item-views.component';
import { MeasurementViewsComponent } from './shared/item-views/measurement-views/measurement-views.component';
import { ProjectViewsComponent } from './shared/item-views/project-views/project-views.component';

@NgModule({
  declarations: [
    AppComponent,
    DashboardComponent,
    QuickActionsComponent,
    MeasurementDetailsComponent,
    NotFoundComponent,
    ResultsListComponent,
    ItemDetailsComponent,
    ProjectDetailsComponent,
    ItemViewsComponent,
    MeasurementViewsComponent,
    ProjectViewsComponent
  ],
  imports: [
    BrowserModule,
    HttpClientModule,
    ServiceWorkerModule.register('ngsw-worker.js', {
      enabled: !isDevMode(),
      // Register the ServiceWorker as soon as the application is stable
      // or after 30 seconds (whichever comes first).
      registrationStrategy: 'registerWhenStable:30000'
    }),
    AppRoutingModule,
    MatToolbarModule,
    MatIconModule,
    MatButtonModule,
    NgOptimizedImage,
    RouterOutlet,
    CoreModule,
    MatRippleModule,
    MatInputModule,
    FormsModule,
    BrowserAnimationsModule,
    MatListModule,
    MatTableModule,
    CdkTableModule,
    MatSidenavModule,
    MatCheckboxModule,
    MatProgressSpinnerModule,
    MatMenuModule,
    MatTooltipModule,
    MatCardModule,
    MatProgressBarModule,
    ReactiveFormsModule,
    MatChipsModule,
    MatGridListModule,
    MatSnackBarModule
  ],
  providers: [],
  bootstrap: [AppComponent]
})
export class AppModule { }
