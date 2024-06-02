import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import { SelectPetsComponent } from './components/select-pets/select-pets.component';
import { ReservationsPageComponent } from './pages/reservations-page.component';
import { ReservationPetsListComponent } from './components/reservation-pets-list/reservation-pets-list/reservation-pets-list.component';
import { ReservationPetsItemComponent } from './components/reservation-pets-item/reservation-pets-item.component';
import { SelectStartDateComponent } from './components/select-start-date/select-start-date.component';
import { SelectEndDateComponent } from './components/select-end-date/select-end-date.component';
import { SharedModule } from '../shared/shared.module';
import { ReservationHoursListComponent } from './components/reservation-hours-list/reservation-hours-list.component';
import { ReservationHoursCardComponent } from './components/reservation-hours-card/reservation-hours-card.component';
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
    SelectEndDateComponent
  ],
  imports: [CommonModule, SharedModule],
  providers: [ReservationsService],
})
export class ReservationsModule {}
