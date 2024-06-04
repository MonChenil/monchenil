import { Component, EventEmitter, Input, Output } from '@angular/core';
import { Reservation } from '../../models/reservation';

@Component({
  selector: 'reservations-list',
  templateUrl: './reservations-list.component.html'
})
export class ReservationsListComponent {
  @Input() declare reservations: Reservation[] | null;
  @Output() reservationDeleted = new EventEmitter();
}
