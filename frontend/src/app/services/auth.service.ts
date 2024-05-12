import { Injectable } from '@angular/core';
import {
  AbstractControl,
  FormBuilder,
  FormGroup,
  Validators,
} from '@angular/forms';

@Injectable({
  providedIn: 'root',
})
export class AuthService {
  public registerForm: FormGroup = this._formBuilder.group(
    {
      email: ['', [Validators.required, Validators.email]],
      password: ['', [Validators.required, Validators.minLength(6)]],
      confirmPassword: ['', [Validators.required]],
    },
    { validators: this.passwordMatchValidator }
  );

  constructor(private _formBuilder: FormBuilder) {}

  public passwordMatchValidator(control: AbstractControl): void {
    return control.get('password')?.value ===
      control.get('confirmPassword')?.value
      ? control.get('confirmPassword')?.setErrors(null)
      : control.get('confirmPassword')?.setErrors({ passwordMismatch: true });
  }
}
