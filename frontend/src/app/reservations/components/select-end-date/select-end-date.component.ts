import { Component, Input, OnInit } from '@angular/core';
import { FormControl } from '@angular/forms';
import {
  Observable,
  catchError,
  debounceTime,
  of,
  startWith,
  switchMap,
} from 'rxjs';
import { ReservationsService } from '../../services/reservations.service';

@Component({
  selector: 'select-end-date',
  templateUrl: './select-end-date.component.html',
})
export class SelectEndDateComponent implements OnInit {
  constructor(private reservationsService: ReservationsService) {}

  @Input() declare endDayControl: FormControl;
  @Input() declare endDayTimeControl: FormControl;
  @Input() declare minDate: Date;

  arrivalTimes$: Observable<string[]> = new Observable();

  ngOnInit() {
    this.arrivalTimes$ = this.endDayControl.valueChanges.pipe(
      startWith(this.endDayControl.value),
      debounceTime(200),
      switchMap(() => this.getArrivalTimes()),
      catchError((error) => {
        console.error(error);
        return of([]);
      }),
    );
  }

  getArrivalTimes() {
    let startDate = new Date(this.endDayControl.value);
    startDate.setHours(0, 0, 0, 0);

    let dayOfMinDate = new Date(this.minDate);
    dayOfMinDate.setHours(0, 0, 0, 0);

    if (startDate < dayOfMinDate) {
      this.endDayControl.setErrors({ invalidDate: true });
      return of([]);
    }

    if (startDate.getTime() == dayOfMinDate.getTime()) {
      startDate = new Date(this.minDate);
    }

    let endDate = new Date(startDate);
    endDate.setDate(endDate.getDate() + 1);
    endDate.setHours(0, 0, 0, 0);

    return this.reservationsService.getArrivalTimes(startDate, endDate);
  }

  getErrorMessage(): string | null {
    const field = this.endDayControl;
    return (field.touched || field.dirty) &&
      field.errors &&
      field.errors['invalidDate']
      ? `Cette date doit être postérieure ou égale au ${this.minDate.toLocaleDateString()}`
      : null;
  }
}
