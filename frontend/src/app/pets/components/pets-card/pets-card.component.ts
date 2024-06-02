import { Component, EventEmitter, Input, Output } from '@angular/core';
import { PetsService } from '../../services/pets.service';
import { Pet } from '../../models/pet';

@Component({
  selector: 'pets-card',
  templateUrl: './pets-card.component.html',
})
export class PetsCardComponent {
  constructor(private petsService: PetsService) {}

  petTypes = ['Chien', 'Chat']; // Order is important. Must match backend/src/MonChenil.Domain/Pets/PetType.cs
  petIcons = ['🐶', '🐱'];

  @Input() declare pet: Pet;
  @Output() petDeleted = new EventEmitter();

  deletePet(pet: Pet) {
    this.petsService
      .deletePet(pet.id.value)
      .subscribe(() => this.petDeleted.emit(pet));
  }
}
