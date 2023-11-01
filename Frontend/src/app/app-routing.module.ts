import { NgModule } from '@angular/core';
import { RouterModule, Routes } from '@angular/router';
import {DashboardComponent} from "./dashboard/dashboard.component";
import {NotFoundComponent} from "./not-found/not-found.component";
import {ResultsListComponent} from "./search/results-list/results-list.component";
import { ItemDetailsComponent } from './shared/item-details/item-details.component';
import { ItemViewsComponent } from './shared/item-views/item-views.component';

const routes: Routes = [
  { path: '', redirectTo: 'home', pathMatch: 'full' },
  { path: 'home', component: DashboardComponent },
  { path: 'search', component: ResultsListComponent },
  { path: 'search/:searchPhrase', component: ResultsListComponent },
  { path: 'measurements', component: ItemViewsComponent, data: {type: 'Measurement'}},
  { path: 'measurements/:id', component: ItemDetailsComponent, data: {type: 'Measurement'}},
  { path: 'projects', component: ItemViewsComponent, data: {type: 'Project'}},
  { path: 'projects/:id', component: ItemDetailsComponent, data: {type: 'Project'}},

  // must be the lasts
  { path: '**', component: NotFoundComponent },
];

@NgModule({
  imports: [RouterModule.forRoot(routes)],
  exports: [RouterModule]
})
export class AppRoutingModule { }
