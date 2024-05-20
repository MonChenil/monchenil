import { Component } from '@angular/core';
import { FormArray, FormBuilder, Validators } from '@angular/forms';
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

  incompatibleTypesList = ['Chien', 'Chat'];

  form = this.formBuilder.group({
    name: ['', Validators.required, Validators.minLength(3)],
    type: ['', Validators.required],
    incompatibleTypes: this.formBuilder.array(
      this.incompatibleTypesList.map(() => this.formBuilder.control(false)),
    ),
  });

  get incompatibleTypes(): FormArray {
    return this.form.get('incompatibleTypes') as FormArray;
  }

  onSubmit() {
    if (this.form.value.incompatibleTypes) {
      const selectedIncompatibleTypes = this.form.value.incompatibleTypes
        .map((checked, index) =>
          checked ? this.incompatibleTypesList[index] : null,
        )
        .filter((value) => value !== null);

      console.log(selectedIncompatibleTypes);
    }
  }

  public getErrorMessage(fieldName: string): string | null {
    const field = fieldName ?
      this.form.get(fieldName)
      : this.form;

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
