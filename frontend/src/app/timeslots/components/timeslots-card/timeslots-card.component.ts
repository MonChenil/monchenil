import { Component, Input } from '@angular/core';
import { TimeSlot } from '../../models/timeslot';

@Component({
  selector: 'timeslots-card',
  templateUrl: './timeslots-card.component.html',
  styles: ``,
})
export class TimeslotsCardComponent {
  @Input() declare timeSlot: TimeSlot;
}
