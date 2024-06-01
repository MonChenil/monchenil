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
    startDay: [''],
    startDayTime: [''],
    endDay: [''],
    endDayTime: [''],
  });

  ngOnInit(): void {
    const tomorrow = new Date();
    tomorrow.setDate(tomorrow.getDate() + 1);
    this.reservationForm.patchValue({
      startDay: new Date().toISOString().split('T')[0],
    });
    this.reservationForm.patchValue({
      endDay: tomorrow.toISOString().split('T')[0],
    });
  }

  onSubmit() {
    console.log(this.reservationForm.value);
  }
}
