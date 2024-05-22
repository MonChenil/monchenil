import { NgModule } from '@angular/core';
import { BrowserModule } from '@angular/platform-browser';

import { AppRoutingModule } from './app-routing.module';
import { AppComponent } from './app.component';
import { AuthModule } from './auth/auth.module';
import { AuthService } from './auth/services/auth.service';
import { DashboardComponent } from './components/dashboard/dashboard.component';
import { NavbarComponent } from './components/navbar/navbar.component';
import { TimeSlotsModule } from './timeslots/timeslots.module';
import { PetsModule } from './pets/pets.module';

@NgModule({
  declarations: [AppComponent, DashboardComponent, NavbarComponent],
  imports: [BrowserModule, AppRoutingModule, AuthModule, TimeSlotsModule, PetsModule],
  providers: [AuthService],
  bootstrap: [AppComponent],
})
export class AppModule {}