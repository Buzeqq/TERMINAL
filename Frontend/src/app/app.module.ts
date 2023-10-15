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

@NgModule({
  declarations: [
    AppComponent,
    DashboardComponent
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
        CoreModule
    ],
  providers: [],
  bootstrap: [AppComponent]
})
export class AppModule { }
