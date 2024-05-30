import { Component } from '@angular/core';
import { PetsService } from '../../../pets/services/pets.service';
import { BehaviorSubject } from 'rxjs';

@Component({
  selector: 'select-pets',
  templateUrl: './select-pets.component.html',
})
export class SelectPetsComponent {
  constructor(private petsService: PetsService) {}

  refresh$ = new BehaviorSubject(null);
  pets$ = this.petsService.getCurrentUserPets();
}
