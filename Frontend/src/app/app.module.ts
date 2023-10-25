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
import { FormsModule } from "@angular/forms";
import { BrowserAnimationsModule } from "@angular/platform-browser/animations";
import { MatListModule } from "@angular/material/list";
import { MatTableModule } from "@angular/material/table";
import { CdkTableModule } from "@angular/cdk/table";
import { MatSidenavModule } from "@angular/material/sidenav";
import { QuickActionsComponent } from './dashboard/quick-actions/quick-actions.component';
import { MatCheckboxModule } from "@angular/material/checkbox";
import { MatProgressSpinnerModule } from "@angular/material/progress-spinner";
import { MeasurementDetailsComponent } from './dashboard/measurement-details/measurement-details.component';
import { MatMenuModule } from '@angular/material/menu';
import { MatTooltipModule } from '@angular/material/tooltip';
import { MatCardModule } from '@angular/material/card';
import { MatProgressBarModule } from '@angular/material/progress-bar';

@NgModule({
  declarations: [
    AppComponent,
    DashboardComponent,
    QuickActionsComponent,
    MeasurementDetailsComponent,
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
    MatProgressBarModule
  ],
  providers: [],
  bootstrap: [AppComponent]
})
export class AppModule { }
