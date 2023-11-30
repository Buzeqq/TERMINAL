import { NgModule } from '@angular/core';
import { RouterModule, Routes } from '@angular/router';
import { DashboardComponent } from "./pages/dashboard/dashboard.component";
import { NotFoundComponent } from "./pages/not-found/not-found.component";
import { ResultsListComponent } from "./pages/search/results-list/results-list.component";
import { ItemViewsComponent } from './core/components/item-views/item-views.component';
import { AddSampleComponent } from "./pages/add-sample/add-sample.component";
import { ItemDetailsComponent } from "./core/components/item-details/item-details/item-details.component";
import {SettingsComponent} from "./pages/settings/settings.component";
import {LoginComponent} from "./pages/login/login.component";

const routes: Routes = [
  {path: '', redirectTo: 'login', pathMatch: 'full'},
  {path: 'login', component: LoginComponent},
  {path: 'home', component: DashboardComponent},
  {path: 'search', component: ResultsListComponent},
  {path: 'search/:q', component: ResultsListComponent},
  {path: 'samples', component: ItemViewsComponent, data: {type: 'Sample'}},
  {path: 'samples/:id', component: ItemDetailsComponent, data: {type: 'Sample'}},
  {path: 'projects', component: ItemViewsComponent, data: {type: 'Project'}},
  {path: 'projects/:id', component: ItemDetailsComponent, data: {type: 'Project'}},
  {path: 'add-sample', component: AddSampleComponent},
  {path: 'recipes', component: ItemViewsComponent, data: {type: 'Recipe'}},
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
