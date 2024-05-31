import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import { SelectPetsComponent } from './components/select-pets/select-pets.component';
import { ReservationsPageComponent } from './pages/reservations-page.component';
import { ReservationPetsListComponent } from './components/reservation-pets-list/reservation-pets-list/reservation-pets-list.component';
import { ReservationPetsItemComponent } from './components/reservation-pets-item/reservation-pets-item.component';

@NgModule({
  declarations: [
    ReservationsPageComponent,
    SelectPetsComponent,
    ReservationPetsListComponent,
    ReservationPetsItemComponent
  ],
  imports: [CommonModule],
})
export class ReservationsModule {}
