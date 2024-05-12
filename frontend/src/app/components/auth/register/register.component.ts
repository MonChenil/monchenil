import { Component } from '@angular/core';
import { AuthService } from '../../../services/auth.service';

@Component({
  selector: 'app-register',
  templateUrl: './register.component.html',
  styleUrl: './register.component.css',
})
export class RegisterComponent {
  constructor(public authService: AuthService) {}

  public resetForm(): void {
    this.authService.registerForm.reset();
  }

  public onSubmit(): void {
    console.log(this.authService.registerForm.value);
  }

  public getErrorMessage(fieldName: string): string | null {
    const field = this.authService.registerForm.get(fieldName);

    if (!field || !field.errors || !field.touched || !field.dirty) {
      return null;
    }

    if (field.errors['required']) {
      return fieldName == 'email'
        ? `L'email est obligatoire`
        : `Le mot de passe est obligatoire`;
    }

    if (field.errors['email']) {
      return `L'email est incorrect`;
    }

    if (field.errors['minlength']) {
      return `Le mot de passe doit avoir au moins ${field.errors['minlength'].requiredLength} caractères`;
    }

    if (field.errors['passwordMismatch']) {
      return `Les mots de passe ne correspondent pas`;
    }

    return null;
  }
}
