import { Component } from '@angular/core';
import { PetsService } from '../../../pets/services/pets.service';

@Component({
  selector: 'select-pets',
  templateUrl: './select-pets.component.html',
})
export class SelectPetsComponent {
  constructor(private petsService: PetsService) {}

  pets$ = this.petsService.getCurrentUserPets();
}
