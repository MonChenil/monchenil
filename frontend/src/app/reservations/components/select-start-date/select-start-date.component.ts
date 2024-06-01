import { Component, Input } from '@angular/core';
import { ReservationsService } from '../../services/reservations.service';
import { BehaviorSubject, Observable, startWith, switchMap } from 'rxjs';
import { FormControl } from '@angular/forms';

@Component({
  selector: 'select-start-date',
  templateUrl: './select-start-date.component.html',
})
export class SelectStartDateComponent {
  constructor(private reservationsService: ReservationsService) {
  }

  @Input() startDayControl: FormControl = new FormControl();
  @Input() startDayTimeControl: FormControl = new FormControl();
  
  // arrivalTimes$: Observable<string[]> = this.startDayControl.valueChanges.pipe(
  //   switchMap(() => this.getArrivalTimes()),
  // );

  // pip it with startDayTimeControl.valueChanges
  arrivalTimes$: Observable<string[]> = this.startDayControl.valueChanges.pipe(
    startWith(this.startDayControl.value),
    switchMap(() => this.getArrivalTimes()),
  );

  getArrivalTimes() {
    return this.reservationsService.getArrivalTimes(
      new Date(this.startDayControl.value).toISOString(),
      new Date(new Date(this.startDayControl.value).setHours(23, 59, 59)).toISOString(),
    );
  }
}
