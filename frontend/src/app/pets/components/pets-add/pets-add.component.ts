import { Component, EventEmitter, Output } from '@angular/core';
import { FormBuilder, Validators } from '@angular/forms';
import { PetsService } from '../../services/pets.service';
import { HttpErrorResponse } from '@angular/common/http';

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

  incompatibleTypesList = ['Chien', 'Chat'];

  form = this.formBuilder.group({
    name: ['', Validators.required, Validators.minLength(3)],
    type: ['', Validators.required],
    incompatibleTypes: this.formBuilder.array(
      this.incompatibleTypesList.map(() => this.formBuilder.control(false)),
    ),
  });

  onSubmit() {
    let selectedIncompatibleTypes;
    if (this.form.value.incompatibleTypes) {
      selectedIncompatibleTypes = this.form.value.incompatibleTypes
        .map((checked, index) =>
          checked ? this.incompatibleTypesList[index] : null,
        )
        .filter((value) => value !== null);
    }

    const petToAdd = {
      name: this.form.value.name,
      type: this.form.value.type,
      incompatibleTypes: selectedIncompatibleTypes,
    };

    this.petsService.create(petToAdd).subscribe({
      next: (response) => {
        this.petAdded.emit(response);
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

    if (field.errors['minlength']) {
      return `Le mot de passe doit avoir au moins ${field.errors['minlength'].requiredLength} caractères`;
    }

    if (field.errors['httpError']) {
      return field.errors['httpError'];
    }

    return null;
  }
}
