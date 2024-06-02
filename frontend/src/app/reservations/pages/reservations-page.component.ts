import { formatDate } from '@angular/common';
import { Component } from '@angular/core';
import { FormBuilder } from '@angular/forms';

@Component({
  selector: 'app-reservations-page',
  templateUrl: './reservations-page.component.html',
})
export class ReservationsPageComponent {
  constructor(private formBuilder: FormBuilder) {}

  reservationForm = this.formBuilder.group({
    pets: [[]],
    startDay: [formatDate(new Date(), 'yyyy-MM-dd', 'en')],
    startDayTime: [''],
    endDay: [
      formatDate(
        new Date().setDate(new Date().getDate() + 1),
        'yyyy-MM-dd',
        'en',
      ),
    ],
    endDayTime: [''],
  });

  onSubmit() {
    console.log(this.reservationForm.value);
  }
}
