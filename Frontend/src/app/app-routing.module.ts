import { NgModule } from '@angular/core';
import { RouterModule, Routes } from '@angular/router';
import { DashboardComponent } from "./pages/dashboard/dashboard.component";
import { NotFoundComponent } from "./pages/not-found/not-found.component";
import { ResultsListComponent } from "./pages/search/results-list/results-list.component";
import { ItemViewsComponent } from './core/components/item-views/item-views.component';
import { AddMeasurementComponent } from "./pages/add-measurement/add-measurement.component";
import { ItemDetailsComponent } from "./core/components/item-details/item-details/item-details.component";
import {SettingsComponent} from "./pages/settings/settings.component";

const routes: Routes = [
  {path: '', redirectTo: 'home', pathMatch: 'full'},
  {path: 'home', component: DashboardComponent},
  {path: 'search', component: ResultsListComponent},
  {path: 'search/:searchPhrase', component: ResultsListComponent},
  {path: 'measurements', component: ItemViewsComponent, data: {type: 'Measurement'}},
  {path: 'measurements/:id', component: ItemDetailsComponent, data: {type: 'Measurement'}},
  {path: 'projects', component: ItemViewsComponent, data: {type: 'Project'}},
  {path: 'projects/:id', component: ItemDetailsComponent, data: {type: 'Project'}},
  {path: 'add-measurement', component: AddMeasurementComponent},
  {path: 'settings', component: SettingsComponent},

  // must be the last
  {path: '**', component: NotFoundComponent},
];

@NgModule({
  imports: [RouterModule.forRoot(routes)],
  exports: [RouterModule]
})
export class AppRoutingModule {
}
