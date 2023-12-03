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
import { loginPageGuard, pagesGuard, settingsPageGuard } from "./core/guards/auth.guard";
import { AddRecipeComponent } from "./pages/add-recipe/add-recipe.component";

const routes: Routes = [
  {path: '', redirectTo: 'login', pathMatch: 'full'},
  {path: 'login', component: LoginComponent, canActivate: [loginPageGuard]},
  {path: 'home', component: DashboardComponent, canActivate: [pagesGuard]},
  {path: 'search', component: ResultsListComponent, canActivate: [pagesGuard]},
  {path: 'search/:q', component: ResultsListComponent, canActivate: [pagesGuard]},
  {path: 'samples', component: ItemViewsComponent, canActivate: [pagesGuard], data: {type: 'Sample'}},
  {path: 'samples/:id', component: ItemDetailsComponent, canActivate: [pagesGuard], data: {type: 'Sample'}},
  {path: 'projects', component: ItemViewsComponent, canActivate: [pagesGuard], data: {type: 'Project'}},
  {path: 'projects/:id', component: ItemDetailsComponent, canActivate: [pagesGuard], data: {type: 'Project'}},
  {path: 'add-sample', component: AddSampleComponent, canActivate: [pagesGuard]},
  {path: 'add-recipe', component: AddRecipeComponent, canActivate: [pagesGuard]},
  {path: 'recipes', component: ItemViewsComponent, canActivate: [pagesGuard], data: {type: 'Recipe'}},
  {path: 'settings', component: SettingsComponent, canActivate: [pagesGuard, settingsPageGuard]},

  // must be the last
  {path: '**', component: NotFoundComponent},
];

@NgModule({
  imports: [RouterModule.forRoot(routes)],
  exports: [RouterModule]
})
export class AppRoutingModule {
}
