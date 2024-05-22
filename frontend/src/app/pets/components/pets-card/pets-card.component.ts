import { Component, EventEmitter, Input, Output } from '@angular/core';
import { Pet } from '../../models/pet';
import { PetsService } from '../../services/pets.service';

@Component({
  selector: 'pets-card',
  templateUrl: './pets-card.component.html',
})
export class PetsCardComponent {
  constructor(private petsService: PetsService) {}

  petTypes = ['Chat', 'Chien']; // Order is important. Must match backend/MonChenil/Entities/Pet.cs
  petIcons = ['🐱', '🐶'];

  @Input() declare pet: Pet;
  @Output() petDeleted = new EventEmitter();

  deletePet(pet: Pet) {
    this.petsService.delete(pet.id).subscribe(() => this.petDeleted.emit(pet));
  }
}
