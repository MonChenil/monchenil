import { Component, Input } from '@angular/core';
import { Pet } from '../../../../pets/models/pet';
import { FormControl } from '@angular/forms';

@Component({
  selector: 'reservation-pets-list',
  templateUrl: './reservation-pets-list.component.html',
})
export class ReservationPetsListComponent {
  @Input() declare petsControl: FormControl;
  @Input() declare pets: Pet[] | null;

  isSelected(petId: string): boolean {
    return this.petsControl.value.includes(petId);
  }
}
