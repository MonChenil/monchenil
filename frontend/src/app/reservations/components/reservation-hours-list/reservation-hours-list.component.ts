import { Component, Input } from '@angular/core';

@Component({
  selector: 'reservation-hours-list',
  templateUrl: './reservation-hours-list.component.html',
})
export class ReservationHoursListComponent {
  @Input() declare arrivalTimes: string[] | null;
}
