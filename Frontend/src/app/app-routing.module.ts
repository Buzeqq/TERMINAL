import { NgModule } from '@angular/core';
import { RouterModule, Routes } from '@angular/router';
import {DashboardComponent} from "./dashboard/dashboard.component";
import {NotFoundComponent} from "./not-found/not-found.component";
import {ResultsListComponent} from "./search/results-list/results-list.component";

const routes: Routes = [
  { path: '', redirectTo: 'home', pathMatch: 'full' },
  { path: 'home', component: DashboardComponent },
  { path: 'search', component: ResultsListComponent },
  { path: 'search/:searchPhrase', component: ResultsListComponent },

  // must be the lasts
  { path: '**', component: NotFoundComponent },
];

@NgModule({
  imports: [RouterModule.forRoot(routes)],
  exports: [RouterModule]
})
export class AppRoutingModule { }
