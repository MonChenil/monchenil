import { Component, Input } from '@angular/core';
import { FormControl } from '@angular/forms';
import { BehaviorSubject, Observable, of, switchMap } from 'rxjs';
import { ReservationsService } from '../../services/reservations.service';

@Component({
  selector: 'select-end-date',
  templateUrl: './select-end-date.component.html',
})
export class SelectEndDateComponent {
  constructor(private reservationsServices: ReservationsService) {}

  @Input() declare endDayControl: FormControl;
  @Input() declare endDayTimeControl: FormControl;

  refresh$ = new BehaviorSubject(null);
  arrivalTimes$ = this.refresh$.pipe(
    switchMap(() =>
      this.reservationsServices.getArrivalTimes(
        new Date(new Date().getTime() + 2 * 60 * 60 * 1000).toISOString(),
      ),
    ),
  );

  refresh() {
    this.refresh$.next(null);
  }
}
