import { Routes } from '@angular/router';
import { DashboardComponent } from "../pages/dashboard/dashboard.component";
import { AccountDetailsComponent } from "../pages/account-details/account-details.component";
import { SamplesComponent } from "../pages/samples/samples.component";
import { ProjectsComponent } from "../pages/projects/projects.component";
import { RecipesComponent } from "../pages/recipes/recipes.component";
import { LoginComponent } from "../pages/login/login.component";

export const routes: Routes = [
  { path: '', component: DashboardComponent },
  { path: 'dashboard', component: DashboardComponent },
  { path: 'account', component: AccountDetailsComponent },
  { path: 'samples', component: SamplesComponent },
  { path: 'projects', component: ProjectsComponent },
  { path: 'recipes', component: RecipesComponent },
  { path: 'login', component: LoginComponent },
];
