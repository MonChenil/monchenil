import { Component } from '@angular/core';
import { BehaviorSubject, switchMap } from 'rxjs';
import { TimeSlotsService } from '../../services/timeslots.service';

@Component({
  selector: 'app-admin-timeslots',
  templateUrl: './admin-timeslots.component.html',
})
export class AdminTimeSlotsComponent {
  constructor(private timeSlotsService: TimeSlotsService) {}

  refresh$ = new BehaviorSubject(null);
  timeSlots$ = this.refresh$.pipe(
    switchMap(() => this.timeSlotsService.getAll()),
  );

  refresh() {
    this.refresh$.next(null);
  }
}
