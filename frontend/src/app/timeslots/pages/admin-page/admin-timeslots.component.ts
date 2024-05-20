import { Component } from '@angular/core';
import { FormBuilder, Validators } from '@angular/forms';
import { TimeSlotsService } from '../../services/timeslots.service';

@Component({
  selector: 'app-admin-timeslots',
  templateUrl: './admin-timeslots.component.html',
})
export class AdminTimeSlotsComponent {
  constructor(
    private formBuilder: FormBuilder,
    private timeSlotsService: TimeSlotsService,
  ) {}

  form = this.formBuilder.group({
    startDate: ['', Validators.required],
    endDate: ['', Validators.required],
  });

  onSubmit() {
    console.log(this.form.value);
    this.timeSlotsService.create(this.form.value).subscribe({
      next: () => {
        this.form.reset();
      },
      error: (error) => {
        this.form.setErrors({ invalid: true });
      },
    });
  }

  public getErrorMessage(fieldName: string): string | null {
    const field = fieldName ? this.form.get(fieldName) : this.form;

    if (!field || !field.errors || !field.touched || !field.dirty) {
      return null;
    }

    if (field.errors['required']) {
      return 'Ce champ est obligatoire';
    }

    if (field.errors['invalid']) {
      return 'Une erreur est survenue';
    }

    return null;
  }
}
