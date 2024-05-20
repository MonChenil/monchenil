import { Component } from '@angular/core';
import { PetsService } from '../../services/pets.service';
import { BehaviorSubject, switchMap } from 'rxjs';

@Component({
  selector: 'app-pets-page',
  templateUrl: './pets-page.component.html',
})
export class PetsPageComponent {
  constructor(private petsService: PetsService) {}

  refresh$ = new BehaviorSubject(null);
  pets$ = this.refresh$.pipe(
    switchMap(() => this.petsService.getAll())
  );

  refresh() {
    this.refresh$.next(null);
  }
}
