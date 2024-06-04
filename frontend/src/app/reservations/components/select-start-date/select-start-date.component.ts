import { Component, Input, OnInit, SimpleChanges } from '@angular/core';
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
  selector: 'select-start-date',
  templateUrl: './select-start-date.component.html',
})
export class SelectStartDateComponent implements OnInit {
  constructor(private reservationsService: ReservationsService) {}

  @Input() declare startDayControl: FormControl<string>;
  @Input() declare startDayTimeControl: FormControl<string>;
  @Input() declare petsControl: FormControl;
  @Input() declare startDayError: string | null;
  @Input() declare startDayTimeError: string | null;
  @Input() declare minDate: Date;

  arrivalTimes$: Observable<string[]> = new Observable();

  ngOnInit() {
    this.arrivalTimes$ = this.startDayControl.valueChanges.pipe(
      startWith(this.startDayControl.value),
      debounceTime(200),
      switchMap(() => this.getArrivalTimes()),
      catchError((error) => {
        console.error(error);
        return of([]);
      }),
    );
  }

  ngOnChanges(changes: SimpleChanges) {
    this.startDayControl.markAsDirty();
    this.startDayControl.markAsTouched();
  }

  getArrivalTimes() {
    let startDate = new Date(this.startDayControl.value);
    startDate.setHours(0, 0, 0, 0);

    let dayOfMinDate = new Date(this.minDate);
    dayOfMinDate.setHours(0, 0, 0, 0);

    if (startDate < dayOfMinDate) {
      this.startDayControl.setErrors({ invalidDate: true });
      return of([]);
    }

    if (startDate.getTime() == dayOfMinDate.getTime()) {
      startDate = new Date(this.minDate);
    }

    let endDate = new Date(startDate);
    endDate.setDate(endDate.getDate() + 1);
    endDate.setHours(0, 0, 0, 0);

    return this.reservationsService.getArrivalTimes(startDate, endDate, this.petsControl.value);
  }

  getErrorMessage(): string | null {
    const field = this.startDayControl;
    return (field.touched || field.dirty) &&
      field.errors &&
      field.errors['invalidDate']
      ? `Cette date doit être postérieure ou égale au ${this.minDate.toLocaleDateString()}`
      : null;
  }
}
