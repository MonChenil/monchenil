import { Component, Input } from '@angular/core';
import { TimeSlot } from '../../models/timeslot';

@Component({
  selector: 'timeslots-list',
  templateUrl: './timeslots-list.component.html',
  styles: ``,
})
export class TimeslotsListComponent {
  @Input() declare timeSlots: TimeSlot[] | null;
}
