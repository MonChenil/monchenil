import { Component } from '@angular/core';
import { FormBuilder, Validators } from '@angular/forms';
import { ActivatedRoute, Router } from '@angular/router';
import { AuthService } from '../../services/auth.service';
import { passwordMatchValidator } from '../../validators/password-match.validator';

@Component({
  selector: 'auth-register',
  templateUrl: './register.component.html',
})
export class RegisterPageComponent {
  constructor(
    private formBuilder: FormBuilder,
    private authService: AuthService,
    private router: Router,
    private route: ActivatedRoute,
  ) {}

  redirectUrl = this.route.snapshot.queryParams['redirectUrl'] || '/';

  registerForm = this.formBuilder.group(
    {
      email: ['', [Validators.required, Validators.email]],
      password: [
        '',
        [
          Validators.required,
          Validators.minLength(6),
          Validators.pattern(/^(?=.*[a-z])(?=.*[A-Z])(?=.*\d)(?=.*\W).+$/),
        ],
      ],
      confirmPassword: ['', [Validators.required]],
    },
    {
      validators: passwordMatchValidator,
    },
  );

  public onSubmit(): void {
    if (this.registerForm.invalid) {
      return;
    }

    this.authService.register(this.registerForm.value).subscribe({
      next: () => {
        this.router.navigate(['/auth/login']);
      },
      error: (error) => {
        this.registerForm.setErrors({ invalidRegister: true });
      },
    });
  }

  public getErrorMessage(fieldName: string): string | null {
    const field = fieldName
      ? this.registerForm.get(fieldName)
      : this.registerForm;

    if (!field || !field.errors || !field.touched || !field.dirty) {
      return null;
    }

    if (field.errors['required']) {
      return 'Ce champ est obligatoire';
    }

    if (field.errors['email']) {
      return `L'email est incorrect`;
    }

    if (field.errors['minlength']) {
      return `Le mot de passe doit avoir au moins ${field.errors['minlength'].requiredLength} caractères`;
    }

    if (field.errors['pattern']) {
      return `Le mot de passe doit contenir une minuscule, une majuscule, un chiffre, et un caractère non-alphanumérique`;
    }

    if (field.errors['passwordMismatch']) {
      return `Les mots de passe ne correspondent pas`;
    }

    if (field.errors['invalidRegister']) {
      return "Une erreur est survenue lors de l'inscription";
    }

    return null;
  }
}
