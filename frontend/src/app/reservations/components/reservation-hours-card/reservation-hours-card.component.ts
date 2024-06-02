import { Component, EventEmitter, Input, Output } from '@angular/core';
import { FormControl } from '@angular/forms';

@Component({
  selector: 'reservation-hours-card',
  templateUrl: './reservation-hours-card.component.html',
})
export class ReservationHoursCardComponent {
  @Input() declare hourControl: FormControl;
  @Input() declare arrivalTime: string | null;
  @Input() declare selected: boolean;

  toggleArrivalTime() {
    if (this.hourControl.value === this.arrivalTime) {
      this.hourControl.setValue('');
    } else {
      this.hourControl.setValue(this.arrivalTime);
    }
  }

  isSelected(): boolean {
    return true;
  }
}
