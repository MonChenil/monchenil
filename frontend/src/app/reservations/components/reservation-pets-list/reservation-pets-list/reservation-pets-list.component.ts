import { Component, Input } from '@angular/core';
import { Pet } from '../../../../pets/models/pet';

@Component({
  selector: 'reservation-pets-list',
  templateUrl: './reservation-pets-list.component.html',
  styles: ``,
})
export class ReservationPetsListComponent {
  @Input() declare pets: Pet[] | null;
}
