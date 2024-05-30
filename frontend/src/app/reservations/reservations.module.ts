import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import { SelectPetsComponent } from './components/select-pets/select-pets.component';
import { PetsModule } from '../pets/pets.module';
import { ReservationsPageComponent } from './pages/reservations-page.component';

@NgModule({
  declarations: [
    ReservationsPageComponent,
    SelectPetsComponent,
  ],
  imports: [CommonModule, PetsModule],
})
export class ReservationsModule {}
