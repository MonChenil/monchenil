import { NgModule } from '@angular/core';
import { RouterModule, Routes } from '@angular/router';
import { PetsPageComponent } from './pets/pages/pets-page/pets-page.component';
import { ReservationsPageComponent } from './reservations/pages/reservations-page.component';
import { isAuthenticatedGuard } from './auth/guards/is-authenticated.guard';

const routes: Routes = [
  {
    path: 'auth',
    loadChildren: () => import('./auth/auth.module').then((m) => m.AuthModule),
  },
  {
    path: 'pets',
    component: PetsPageComponent,
    canActivate: [isAuthenticatedGuard],
  },
  {
    path: 'reservations',
    component: ReservationsPageComponent,
    canActivate: [isAuthenticatedGuard],
  },
  { path: '', redirectTo: 'reservations', pathMatch: 'full' },
  { path: '**', redirectTo: 'reservations' },
];

@NgModule({
  imports: [RouterModule.forRoot(routes)],
  exports: [RouterModule],
})
export class AppRoutingModule {}
