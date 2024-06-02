import { formatDate } from '@angular/common';
import { Component, Input } from '@angular/core';
import { FormControl } from '@angular/forms';
import {
  Observable,
  catchError,
  debounceTime,
  of,
  startWith,
  switchMap,
  tap,
} from 'rxjs';
import { ReservationsService } from '../../services/reservations.service';

@Component({
  selector: 'select-start-date',
  templateUrl: './select-start-date.component.html',
})
export class SelectStartDateComponent {
  constructor(private reservationsService: ReservationsService) {}

  @Input() declare startDayControl: FormControl<string>;
  @Input() declare startDayTimeControl: FormControl<string>;

  minDate = new Date();
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
      tap((arrivalTimes) => console.log(arrivalTimes)),
    );
  }

  getArrivalTimes() {
    const startDate = new Date(this.startDayControl.value);
    startDate.setHours(0, 0, 0, 0);

    const endDate = new Date(startDate);
    endDate.setDate(endDate.getDate() + 1);
    endDate.setHours(0, 0, 0, 0);

    let formattedStartDate;
    let formattedEndDate;

    try {
      formattedStartDate = formatDate(startDate, 'yyyy-MM-dd', 'en');
      formattedEndDate = formatDate(endDate, 'yyyy-MM-dd', 'en');
    } catch (error) {
      console.error(error);
      return of([]);
    }

    return this.reservationsService.getArrivalTimes(
      formattedStartDate,
      formattedEndDate,
    );
  }
}
