import { NgModule } from '@angular/core';
import { BrowserModule } from '@angular/platform-browser';

import { AppRoutingModule } from './app-routing.module';
import { AppComponent } from './app.component';
import { AuthModule } from './auth/auth.module';
import { AuthService } from './auth/services/auth.service';
import { DashboardComponent } from './components/dashboard/dashboard.component';
import { NavbarComponent } from './components/navbar/navbar.component';
import { PetsModule } from './pets/pets.module';
import { ReservationsModule } from './reservations/reservations.module';

@NgModule({
  declarations: [AppComponent, DashboardComponent, NavbarComponent],
  imports: [
    BrowserModule,
    AppRoutingModule,
    AuthModule,
    PetsModule,
    ReservationsModule,
  ],
  providers: [AuthService],
  bootstrap: [AppComponent],
})
export class AppModule {}
