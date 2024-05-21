import { HttpErrorResponse } from '@angular/common/http';
import { Component, EventEmitter, Output } from '@angular/core';
import { FormBuilder, Validators } from '@angular/forms';
import { PetsService } from '../../services/pets.service';

@Component({
  selector: 'pets-add',
  templateUrl: './pets-add.component.html',
})
export class PetsAddComponent {
  constructor(
    private formBuilder: FormBuilder,
    private petsService: PetsService,
  ) {}

  @Output() petAdded = new EventEmitter();

  petTypes = ['Chat', 'Chien']; // Order is important. Must match backend/MonChenil/Entities/Pet.cs

  form = this.formBuilder.group({
    name: ['', [Validators.required, Validators.minLength(3)]],
    type: ['', Validators.required],
    incompatibleTypes: this.formBuilder.array(
      this.petTypes.map(() => this.formBuilder.control(false)),
    ),
  });

  onSubmit() {
    const selectedIncompatibleTypes: number[] =
      this.form.value.incompatibleTypes
        ?.map((checked, index) => (checked ? index : -1))
        .filter((index) => index !== -1) || [];

    const petToAdd = {
      name: this.form.value.name,
      type: parseInt(this.form.value.type!),
      incompatibleTypes: selectedIncompatibleTypes,
    };

    this.petsService.create(petToAdd).subscribe({
      next: (response) => {
        this.petAdded.emit(response);
        this.form.reset({
          type: '',
        });
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

    if (field.errors['minlength']) {
      return `Ce champ doit contenir au moins ${field.errors['minlength'].requiredLength} caractères`;
    }

    if (field.errors['httpError']) {
      return field.errors['httpError'];
    }

    return null;
  }
}
