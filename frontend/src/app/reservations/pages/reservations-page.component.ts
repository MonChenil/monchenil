import { Component } from '@angular/core';
import { FormBuilder } from '@angular/forms';

@Component({
  selector: 'app-reservations-page',
  templateUrl: './reservations-page.component.html',
})
export class ReservationsPageComponent {
  constructor(private formBuilder: FormBuilder) {}

  reservationForm = this.formBuilder.group({
    pets: [['']],
    startDate: [''],
    endDate: [''],
  });

  onSubmit() { 
    console.log(this.reservationForm.value);
  }
}
