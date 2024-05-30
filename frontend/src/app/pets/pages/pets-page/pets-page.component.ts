import { Component } from '@angular/core';
import { BehaviorSubject, switchMap } from 'rxjs';
import { PetsService } from '../../services/pets.service';

@Component({
  selector: 'app-pets-page',
  templateUrl: './pets-page.component.html',
})
export class PetsPageComponent {
  constructor(private petsService: PetsService) {}

  refresh$ = new BehaviorSubject(null);
  pets$ = this.refresh$.pipe(
    switchMap(() => this.petsService.getCurrentUserPets()),
  );

  refresh() {
    this.refresh$.next(null);
  }
}
