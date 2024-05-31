import { Component, Input } from '@angular/core';
import { ReservationsService } from '../../services/reservations.service';
import { BehaviorSubject, switchMap } from 'rxjs';
import { FormControl } from '@angular/forms';

@Component({
  selector: 'select-start-date',
  templateUrl: './select-start-date.component.html',
})
export class SelectStartDateComponent {
  constructor(private reservationsServices: ReservationsService) {}

  @Input() declare control: FormControl;

  refresh$ = new BehaviorSubject(null);
  arrivalTimes$ = this.refresh$.pipe(
    switchMap(() =>
      this.reservationsServices.getArrivalTimes(new Date().toISOString()),
    ),
  );

  refresh() {
    this.refresh$.next(null);
  }
}
