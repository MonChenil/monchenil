import { Component, EventEmitter, Input, Output } from '@angular/core';
import { Pet } from '../../../../pets/models/pet';

@Component({
  selector: 'pets-list',
  templateUrl: './pets-list.component.html',
})
export class PetsListComponent {
  @Input() declare pets: Pet[] | null;
  @Output() petDeleted = new EventEmitter();
}
