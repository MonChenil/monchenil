import { Component, Input } from '@angular/core';
import { PetsService } from '../../../pets/services/pets.service';
import { Pet } from '../../../pets/models/pet';

@Component({
  selector: 'reservation-pets-item',
  templateUrl: './reservation-pets-item.component.html',
  styles: ``,
})
export class ReservationPetsItemComponent {
  constructor(private petsService: PetsService) {}

  petTypes = ['Chien', 'Chat']; // Order is important. Must match backend/src/MonChenil.Domain/Pets/PetType.cs
  petIcons = ['🐶', '🐱'];
  selectedPets: Pet[] = [];

  @Input() declare pet: Pet;

  addPet(pet: Pet) {
    const index = this.selectedPets.indexOf(pet);
    if (index === -1) {
      this.selectedPets.push(pet);
    } else {
      this.selectedPets.splice(index, 1);
    }
  }

  isSelected(pet: any): boolean {
    return this.selectedPets.includes(pet);
  }
}
