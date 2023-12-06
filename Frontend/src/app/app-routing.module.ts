import { NgModule } from '@angular/core';
import { RouterModule, Routes } from '@angular/router';
import { DashboardComponent } from "./pages/dashboard/dashboard.component";
import { NotFoundComponent } from "./pages/not-found/not-found.component";
import { ResultsListComponent } from "./pages/search/results-list/results-list.component";
import { ItemViewsComponent } from './core/components/item-views/item-views.component';
import { AddSampleComponent } from "./pages/add-sample/add-sample.component";
import { ItemDetailsComponent } from "./core/components/item-details/item-details/item-details.component";
import { SettingsComponent } from "./pages/settings/settings.component";
import { LoginComponent } from "./pages/login/login.component";
import { AddRecipeComponent } from "./pages/add-recipe/add-recipe.component";
import {LoginPageGuard} from "./core/guards/login/login-page.guard";
import {PagesGuard} from "./core/guards/pages/pages.guard";
import {settingsGuard} from "./core/guards/settings/settings.guard";

const routes: Routes = [
  {path: '', redirectTo: 'login', pathMatch: 'full'},
  {path: 'login', component: LoginComponent, canActivate: [LoginPageGuard]},
  {path: 'home', component: DashboardComponent, canActivate: [PagesGuard]},
  {path: 'search', component: ResultsListComponent, canActivate: [PagesGuard]},
  {path: 'search/:q', component: ResultsListComponent, canActivate: [PagesGuard]},
  {path: 'samples', component: ItemViewsComponent, canActivate: [PagesGuard], data: {type: 'Sample'}},
  {path: 'samples/:id', component: ItemDetailsComponent, canActivate: [PagesGuard], data: {type: 'Sample'}},
  {path: 'projects', component: ItemViewsComponent, canActivate: [PagesGuard], data: {type: 'Project'}},
  {path: 'projects/:id', component: ItemDetailsComponent, canActivate: [PagesGuard], data: {type: 'Project'}},
  {path: 'add-sample', component: AddSampleComponent, canActivate: [PagesGuard]},
  {path: 'add-recipe', component: AddRecipeComponent, canActivate: [PagesGuard]},
  {path: 'recipes', component: ItemViewsComponent, canActivate: [PagesGuard], data: {type: 'Recipe'}},
  // {path: 'recipes/:id', component: ItemDetailsComponent, canActivate: [PagesGuard], data: {type: 'Recipe'}},
  {path: 'settings', component: SettingsComponent, canActivate: [PagesGuard, settingsGuard]},

  // must be the last
  {path: '**', component: NotFoundComponent},
];

@NgModule({
  imports: [RouterModule.forRoot(routes)],
  exports: [RouterModule]
})
export class AppRoutingModule {
}
