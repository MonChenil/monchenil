import { Component, Input } from '@angular/core';
import { PetsService } from '../../../pets/services/pets.service';
import { FormControl } from '@angular/forms';

@Component({
  selector: 'select-pets',
  templateUrl: './select-pets.component.html',
})
export class SelectPetsComponent {
  constructor(private petsService: PetsService) {}

  @Input() declare petsControl: FormControl;

  pets$ = this.petsService.getCurrentUserPets();
}
