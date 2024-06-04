import { Component, Input, OnInit, SimpleChanges } from '@angular/core';
import { FormControl } from '@angular/forms';
import {
  Observable,
  catchError,
  combineLatest,
  debounceTime,
  of,
  startWith,
  switchMap,
  tap,
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
  @Input() declare petsControl: FormControl;
  @Input() declare endDayError: string | null;
  @Input() declare endDayTimeError: string | null;
  @Input() declare minDate: Date;
  @Input() declare maxDate: Date;

  arrivalTimes$: Observable<string[]> = new Observable();

  ngOnInit() {
    this.arrivalTimes$ = combineLatest([
      this.endDayControl.valueChanges.pipe(
        startWith(this.endDayControl.value),
        debounceTime(200),
      ),
      this.petsControl.valueChanges.pipe(startWith(this.petsControl.value)),
    ]).pipe(
      tap(() => this.endDayTimeControl.setValue('')),
      switchMap(() => this.getDepartureTimes()),
      catchError((error) => {
        console.error(error);
        return of([]);
      }),
    );
  }

  ngOnChanges(changes: SimpleChanges) {
    this.endDayControl.markAsDirty();
    this.endDayControl.markAsTouched();
  }

  getDepartureTimes() {
    let startDate = new Date(this.endDayControl.value);
    startDate.setHours(0, 0, 0, 0);

    let dayOfMinDate = new Date(this.minDate);
    dayOfMinDate.setHours(0, 0, 0, 0);

    let dayOfMaxDate = new Date(this.maxDate);
    dayOfMaxDate.setHours(0, 0, 0, 0);

    if (startDate < dayOfMinDate) {
      this.endDayControl.setErrors({ invalidDate: true });
      return of([]);
    }

    if (startDate > dayOfMaxDate) {
      this.endDayControl.setErrors({ invalidDate: true });
      return of([]);
    }

    if (startDate.getTime() == dayOfMinDate.getTime()) {
      startDate = new Date(this.minDate);
    }

    let endDate = new Date(startDate);
    endDate.setDate(endDate.getDate() + 1);
    endDate.setHours(0, 0, 0, 0);

    return this.reservationsService.getDepartureTimes(
      startDate,
      endDate,
      this.petsControl.value,
    );
  }

  getErrorMessage(): string | null {
    const field = this.endDayControl;
    return (field.touched || field.dirty) &&
      field.errors &&
      field.errors['invalidDate']
      ? `Cette date doit être comprise entre le ${this.minDate.toLocaleDateString()} et le ${this.maxDate.toLocaleDateString()}`
      : null;
  }
}
