import { isDevMode, NgModule } from '@angular/core';
import { BrowserModule } from '@angular/platform-browser';
import { HTTP_INTERCEPTORS, HttpClientModule } from "@angular/common/http";

import { AppComponent } from './app.component';
import { ServiceWorkerModule } from '@angular/service-worker';
import { MatToolbarModule } from "@angular/material/toolbar";
import { MatIconModule } from "@angular/material/icon";
import { MatButtonModule } from "@angular/material/button";
import { NgOptimizedImage } from "@angular/common";
import { RouterOutlet } from "@angular/router";
import { DashboardComponent } from './pages/dashboard/dashboard.component';
import { CoreModule } from "./core/core.module";
import { MatRippleModule } from "@angular/material/core";
import { MatInputModule } from "@angular/material/input";
import { FormsModule, ReactiveFormsModule } from "@angular/forms";
import { BrowserAnimationsModule } from "@angular/platform-browser/animations";
import { MatListModule } from "@angular/material/list";
import { MatTableModule } from "@angular/material/table";
import { CdkTableModule } from "@angular/cdk/table";
import { MatSidenavModule } from "@angular/material/sidenav";
import { MatCheckboxModule } from "@angular/material/checkbox";
import { MatProgressSpinnerModule } from "@angular/material/progress-spinner";
import { MatMenuModule } from '@angular/material/menu';
import { MatTooltipModule } from '@angular/material/tooltip';
import { MatCardModule } from '@angular/material/card';
import { MatProgressBarModule } from '@angular/material/progress-bar';
import { AppRoutingModule } from './app-routing.module';
import { NotFoundComponent } from './pages/not-found/not-found.component';
import { ResultsListComponent } from './pages/search/results-list/results-list.component';
import { MatGridListModule } from '@angular/material/grid-list';
import { MatSnackBarModule } from '@angular/material/snack-bar';
import { InfiniteScrollModule } from "ngx-infinite-scroll";
import { AddSampleComponent } from './pages/add-sample/add-sample.component';
import { SampleViewsComponent } from "./core/components/item-views/sample-views/sample-views.component";
import { ItemViewsComponent } from "./core/components/item-views/item-views.component";
import { MatAutocompleteModule } from "@angular/material/autocomplete";
import { MtxDatetimepickerModule } from '@ng-matero/extensions/datetimepicker';
import { MtxNativeDatetimeModule } from "@ng-matero/extensions/core";
import { MatChipsModule } from "@angular/material/chips";
import { MatTabsModule } from "@angular/material/tabs";
import { StatusbarComponent } from './core/components/statusbar/statusbar.component';
import { AuthInterceptor } from "./core/interceptors/auth/auth.interceptor";
import { MatDialogModule } from "@angular/material/dialog";
import { CdkFixedSizeVirtualScroll, CdkVirtualForOf, ScrollingModule } from "@angular/cdk/scrolling";
import { MatSelectModule } from "@angular/material/select";
import { ProjectViewsComponent } from "./core/components/item-views/project-views/project-views.component";
import { SettingsComponent } from './pages/settings/settings.component';
import { ProjectEditComponent } from './pages/settings/project-edit/project-edit.component';
import { MatSlideToggleModule } from "@angular/material/slide-toggle";
import { SampleEditComponent } from './pages/settings/sample-edit/sample-edit.component';
import { TagEditComponent } from './pages/settings/tag-edit/tag-edit.component';
import { LoginComponent } from "./pages/login/login.component";
import { MatSortModule } from "@angular/material/sort";
import { MatPaginatorModule } from "@angular/material/paginator";
import { UserEditComponent } from './pages/settings/user-edit/user-edit.component';
import { AddRecipeComponent } from './pages/add-recipe/add-recipe.component';
import { RecipeEditComponent } from './pages/settings/recipe-edit/recipe-edit.component';
import {SampleInterceptor} from "./core/interceptors/samples/sample.interceptor";
import {RecipeInterceptor} from "./core/interceptors/recipes/recipe.interceptor";
import { RegisterComponent } from './pages/register/register.component';
import { AddUserComponent } from './core/components/dialogs/add-user/add-user.component';
import { AddTagComponent } from './core/components/dialogs/add-tag/add-tag.component';

@NgModule({
  declarations: [
    AppComponent,
    DashboardComponent,
    NotFoundComponent,
    ResultsListComponent,
    AddSampleComponent,
    SampleViewsComponent,
    ProjectViewsComponent,
    ItemViewsComponent,
    StatusbarComponent,
    SettingsComponent,
    ProjectEditComponent,
    SampleEditComponent,
    TagEditComponent,
    StatusbarComponent,
    LoginComponent,
    UserEditComponent,
    AddRecipeComponent,
    RecipeEditComponent,
    RegisterComponent,
    AddUserComponent,
    AddTagComponent
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
    MatGridListModule,
    MatSnackBarModule,
    InfiniteScrollModule,
    MatDialogModule,
    MatAutocompleteModule,
    MtxDatetimepickerModule,
    MtxNativeDatetimeModule,
    MatChipsModule,
    MatTabsModule,
    MatSelectModule,
    MatDialogModule,
    CdkFixedSizeVirtualScroll,
    CdkVirtualForOf,
    ScrollingModule,
    MatSlideToggleModule,
    MatSortModule,
    MatPaginatorModule
  ],
  providers: [
    { provide: HTTP_INTERCEPTORS, useClass: AuthInterceptor, multi: true },
    { provide: HTTP_INTERCEPTORS, useClass: SampleInterceptor, multi: true },
    { provide: HTTP_INTERCEPTORS, useClass: RecipeInterceptor, multi: true }
  ],
  bootstrap: [AppComponent]
})
export class AppModule { }
