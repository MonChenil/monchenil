import { Component, Input } from '@angular/core';
import { Pet } from '../../../pets/models/pet';
import { FormControl } from '@angular/forms';

@Component({
  selector: 'reservation-pets-item',
  templateUrl: './reservation-pets-item.component.html',
  styles: ``,
})
export class ReservationPetsItemComponent {
  petTypes = ['Chien', 'Chat']; // Order is important. Must match backend/src/MonChenil.Domain/Pets/PetType.cs
  petIcons = ['🐶', '🐱'];

  @Input() declare petsControl: FormControl;
  @Input() declare pet: Pet;
  @Input() declare selected: boolean;

  togglePetSelection(petId: string) {
    const currentSelection = this.petsControl.value as string[];
    if (this.selected) {
      // Remove pet from selection
      this.petsControl.setValue(currentSelection.filter((id) => id !== petId));
    } else {
      // Add pet to selection
      this.petsControl.setValue([...currentSelection, petId]);
    }
  }
}
