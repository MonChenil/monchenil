import { formatDate } from '@angular/common';
import { HttpErrorResponse } from '@angular/common/http';
import { Component } from '@angular/core';
import { FormBuilder, FormGroup } from '@angular/forms';
import { BehaviorSubject, switchMap } from 'rxjs';
import { ReservationsService } from '../services/reservations.service';

@Component({
  selector: 'app-reservations-page',
  templateUrl: './reservations-page.component.html',
})
export class ReservationsPageComponent {
  minStartDate: Date;
  minEndDate: Date;
  reservationForm: FormGroup;

  constructor(
    private formBuilder: FormBuilder,
    private reservationsService: ReservationsService,
  ) {
    ({ startDay: this.minStartDate, endDay: this.minEndDate } =
      this.initDates());
    this.reservationForm = this.formBuilder.group({
      pets: [[]],
      startDay: [formatDate(this.minStartDate, 'yyyy-MM-dd', 'en')],
      startDayTime: [''],
      endDay: [formatDate(this.minEndDate, 'yyyy-MM-dd', 'en')],
      endDayTime: [''],
    });

    this.reservationForm.get('startDay')!.valueChanges.subscribe((value) => {
      this.minEndDate = new Date(value);
      this.minEndDate.setDate(this.minEndDate.getDate() + 1);
      this.reservationForm
        .get('endDay')!
        .setValue(formatDate(this.minEndDate, 'yyyy-MM-dd', 'en'));
    });
  }

  refresh$ = new BehaviorSubject(null);
  reservations$ = this.refresh$.pipe(
    switchMap(() => this.reservationsService.getReservations()),
  );

  refresh() {
    this.refresh$.next(null);

    // Trigger validation to update sub components
    this.reservationForm.get('startDay')?.updateValueAndValidity();
    this.reservationForm.get('endDay')?.updateValueAndValidity();
    this.reservationForm.get('pets')?.updateValueAndValidity();
  }

  onSubmit() {
    const reservationToAdd = this.prepareDataToSend();
    this.reservationsService.createReservation(reservationToAdd).subscribe({
      next: () => {
        this.onReset();
        this.refresh();
      },
      error: (error: HttpErrorResponse) => {
        this.reservationForm.setErrors({ httpError: error.error });
      },
    });
  }

  onReset() {
    ({ startDay: this.minStartDate, endDay: this.minEndDate } =
      this.initDates());
    this.reservationForm.reset({
      pets: [],
      startDay: formatDate(this.minStartDate, 'yyyy-MM-dd', 'en'),
      startDayTime: '',
      endDay: formatDate(this.minEndDate, 'yyyy-MM-dd', 'en'),
      endDayTime: '',
    });
  }

  initDates() {
    this.minStartDate = new Date();
    this.minEndDate = new Date();
    this.minEndDate.setDate(this.minStartDate.getDate() + 1);
    this.minEndDate.setHours(0, 0, 0, 0);
    return { startDay: this.minStartDate, endDay: this.minEndDate };
  }

  prepareDataToSend() {
    const reservation = {
      petIds: [] as { value: string }[],
      startDate: '',
      endDate: '',
    };
    reservation.startDate = this.getDateTime(
      this.reservationForm.get('startDay')!.value,
      this.reservationForm.get('startDayTime')!.value,
    );
    reservation.endDate = this.getDateTime(
      this.reservationForm.get('endDay')!.value,
      this.reservationForm.get('endDayTime')!.value,
    );
    for (const pet of this.reservationForm.get('pets')!.value) {
      reservation.petIds.push({ value: pet });
    }
    return reservation;
  }

  getDateTime(date: string, time: string): string {
    return date + 'T' + time + ':00';
  }
}
