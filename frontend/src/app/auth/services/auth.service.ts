import { Injectable } from '@angular/core';
import { FormBuilder, FormGroup, Validators } from '@angular/forms';
import { passwordMatchValidator } from '../validators/password-match.validator';

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
    { validators: passwordMatchValidator },
  );

  public loginForm: FormGroup = this._formBuilder.group(
    {
      email: ['', [Validators.required]],
      password: ['', [Validators.required]],
    },
    { validators: passwordMatchValidator },
  );

  constructor(private _formBuilder: FormBuilder) {}
}
