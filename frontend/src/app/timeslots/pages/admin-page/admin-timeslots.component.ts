import { Component } from '@angular/core';
import { TimeSlotsService } from '../../services/timeslots.service';

@Component({
  selector: 'app-admin-timeslots',
  templateUrl: './admin-timeslots.component.html',
})
export class AdminTimeSlotsComponent {
  constructor(private timeSlotsService: TimeSlotsService) {}

  timeSlots$ = this.timeSlotsService.getAll();

  refresh() {
    this.timeSlots$ = this.timeSlotsService.getAll();
  }
}
