import { formatDate } from '@angular/common';
import { Component } from '@angular/core';
import { FormBuilder, FormGroup } from '@angular/forms';

@Component({
  selector: 'app-reservations-page',
  templateUrl: './reservations-page.component.html',
})
export class ReservationsPageComponent {
  minStartDate: Date;
  minEndDate: Date;

  reservationForm: FormGroup;

  constructor(private formBuilder: FormBuilder) {
    this.minStartDate = new Date();
    this.minEndDate = new Date();
    this.minEndDate.setDate(this.minStartDate.getDate() + 1);

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

  onSubmit() {
    console.log(this.reservationForm.value);
  }
}
