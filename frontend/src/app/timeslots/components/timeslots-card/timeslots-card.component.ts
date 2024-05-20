import { Component, EventEmitter, Input, Output } from '@angular/core';
import { TimeSlot } from '../../models/timeslot';
import { TimeSlotsService } from '../../services/timeslots.service';

@Component({
  selector: 'timeslots-card',
  templateUrl: './timeslots-card.component.html',
  styles: ``,
})
export class TimeslotsCardComponent {
  constructor(private timeSlotsService: TimeSlotsService) {}

  @Input() declare timeSlot: TimeSlot;
  @Output() timeSlotDeleted = new EventEmitter();

  deleteTimeSlot(timeSlot: TimeSlot) {
    this.timeSlotsService
      .delete(timeSlot.id)
      .subscribe(() => this.timeSlotDeleted.emit(timeSlot));
  }
}
