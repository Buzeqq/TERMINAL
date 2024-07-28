import { Routes } from '@angular/router';
import { DashboardComponent } from "./pages/dashboard/dashboard.component";
import { AccountDetailsComponent } from "./pages/account-details/account-details.component";
import { SamplesComponent } from "./pages/samples/samples.component";
import { ProjectsComponent } from "./pages/projects/projects.component";
import { RecipesComponent } from "./pages/recipes/recipes.component";
import { LoginComponent } from "./pages/login/login.component";
import { loggedInGuard } from "./core/guards/logged-in.guard";

export const routes: Routes = [
  { path: '', component: DashboardComponent, canActivate: [loggedInGuard] },
  { path: 'dashboard', component: DashboardComponent, canActivate: [loggedInGuard] },
  { path: 'account', component: AccountDetailsComponent, canActivate: [loggedInGuard] },
  { path: 'samples', component: SamplesComponent, canActivate: [loggedInGuard] },
  { path: 'projects', component: ProjectsComponent, canActivate: [loggedInGuard] },
  { path: 'recipes', component: RecipesComponent, canActivate: [loggedInGuard] },
  { path: 'login', component: LoginComponent, canActivate: [loggedInGuard] },
];
