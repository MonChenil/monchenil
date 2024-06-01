import { Component, Input } from '@angular/core';
import { FormControl } from '@angular/forms';

@Component({
  selector: 'reservation-hours-list',
  templateUrl: './reservation-hours-list.component.html',
})
export class ReservationHoursListComponent {
  @Input() declare arrivalTimes: string[] | null;
  @Input() declare hourControl: FormControl;

  isSelected(arrivalTime: string): boolean {
    return this.hourControl.value === this.getFormattedTime(arrivalTime);
  }

  getFormattedTime(dateTimeString: string | null | undefined): string {
    if (!dateTimeString) {
      return '';
    }
    const timePart = dateTimeString.split('T')[1];
    return timePart ? timePart.substring(0, 5) : '';
  }
}
