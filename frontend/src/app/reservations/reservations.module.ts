import { NgModule } from '@angular/core';
import { RouterModule } from '@angular/router';
import { SharedModule } from '../shared/shared.module';
import { ReservationHoursCardComponent } from './components/reservation-hours-card/reservation-hours-card.component';
import { ReservationHoursListComponent } from './components/reservation-hours-list/reservation-hours-list.component';
import { ReservationPetsItemComponent } from './components/reservation-pets-item/reservation-pets-item.component';
import { ReservationPetsListComponent } from './components/reservation-pets-list/reservation-pets-list/reservation-pets-list.component';
import { ReservationsCardComponent } from './components/reservations-card/reservations-card.component';
import { ReservationsListComponent } from './components/reservations-list/reservations-list.component';
import { SelectEndDateComponent } from './components/select-end-date/select-end-date.component';
import { SelectPetsComponent } from './components/select-pets/select-pets.component';
import { SelectStartDateComponent } from './components/select-start-date/select-start-date.component';
import { ReservationsPageComponent } from './pages/reservations-page.component';
import { ReservationsService } from './services/reservations.service';

@NgModule({
  declarations: [
    ReservationsPageComponent,
    SelectPetsComponent,
    ReservationPetsListComponent,
    ReservationPetsItemComponent,
    ReservationHoursListComponent,
    ReservationHoursCardComponent,
    SelectStartDateComponent,
    SelectEndDateComponent,
    ReservationsListComponent,
    ReservationsCardComponent,
  ],
  imports: [RouterModule, SharedModule],
  providers: [ReservationsService],
})
export class ReservationsModule {}
