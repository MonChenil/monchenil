import { HttpErrorResponse } from '@angular/common/http';
import { Component, EventEmitter, Output } from '@angular/core';
import { FormBuilder, Validators } from '@angular/forms';
import { TimeSlotsService } from '../../services/timeslots.service';

@Component({
  selector: 'timeslots-add',
  templateUrl: './timeslots-add.component.html',
})
export class TimeSlotsAddComponent {
  constructor(
    private formBuilder: FormBuilder,
    private timeSlotsService: TimeSlotsService,
  ) {}

  @Output() timeSlotAdded = new EventEmitter();

  form = this.formBuilder.group({
    startDate: ['', Validators.required],
    endDate: ['', Validators.required],
  });

  onSubmit() {
    this.timeSlotsService.create(this.form.value).subscribe({
      next: (response) => {
        this.timeSlotAdded.emit(response);
        this.form.reset();
      },
      error: (error: HttpErrorResponse) => {
        this.form.setErrors({ httpError: error.error });
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

    if (field.errors['httpError']) {
      return field.errors['httpError'];
    }

    return null;
  }
}
