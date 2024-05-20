import { NgModule } from '@angular/core';
import { BrowserModule } from '@angular/platform-browser';

import { AppRoutingModule } from './app-routing.module';
import { AppComponent } from './app.component';
import { AuthModule } from './auth/auth.module';
import { DashboardComponent } from './components/dashboard/dashboard.component';
import { TimeSlotsModule } from './timeslots/timeslots.module';
import { PetsModule } from './pets/pets.module';

@NgModule({
  declarations: [AppComponent, DashboardComponent],
  imports: [BrowserModule, AppRoutingModule, AuthModule, TimeSlotsModule, PetsModule],
  providers: [],
  bootstrap: [AppComponent],
})
export class AppModule {}
