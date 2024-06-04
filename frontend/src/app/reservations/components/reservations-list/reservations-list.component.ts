import { Component, EventEmitter, Input, Output } from '@angular/core';
import { Reservation } from '../../models/reservation';

@Component({
  selector: 'reservations-list',
  templateUrl: './reservations-list.component.html'
})
export class ReservationsListComponent {
  @Input() declare reservations: Reservation[] | null;
  @Output() reservationDeleted = new EventEmitter();

  ngOnChanges() {
    this.reservations = this.sortReservationsByDate(this.reservations);
  }

  sortReservationsByDate(reservations: Reservation[] | null): Reservation[] | null {
    if (reservations) {
      return reservations.sort((a, b) => {
          return new Date(a.startDate).getTime() - new Date(b.startDate).getTime();
      });
    }
    return null;
  }
}
