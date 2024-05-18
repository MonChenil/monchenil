import { Component } from '@angular/core';
import { AuthService } from '../../services/auth.service';

@Component({
  selector: 'app-login',
  templateUrl: './login.component.html',
})
export class LoginPageComponent {
  constructor(public authService: AuthService) {}

  public resetForm(): void {
    this.authService.loginForm.reset();
  }

  public onSubmit(): void {
    console.log(this.authService.loginForm.value);
  }

  public getErrorMessage(fieldName: string): string | null {
    const field = this.authService.loginForm.get(fieldName);

    if (!field || !field.errors || !field.touched || !field.dirty) {
      return null;
    }

    if (field.errors['required']) {
      return fieldName == 'email'
        ? `L'email est obligatoire`
        : `Le mot de passe est obligatoire`;
    }

    return null;
  }
}
