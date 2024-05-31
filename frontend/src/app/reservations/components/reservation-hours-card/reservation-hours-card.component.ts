import { Component, Input } from '@angular/core';

@Component({
  selector: 'reservation-hours-card',
  templateUrl: './reservation-hours-card.component.html',
})
export class ReservationHoursCardComponent {
  @Input() declare arrivalTime: string | null;

  getFormattedTime(dateTimeString: string | null | undefined): string {
    if (!dateTimeString) {
      return '';
    }
    const timePart = dateTimeString.split('T')[1];
    return timePart ? timePart.substring(0, 5) : '';
  }

  selectHour() {
    console.log('Hour selected')
  }

  isSelected(): boolean {
    return true;
  }
}
