import { Component } from '@angular/core';
import { AuthService } from '../../auth/services/auth.service';

@Component({
  selector: 'app-navbar',
  templateUrl: './navbar.component.html',
  styles: ``,
})
export class NavbarComponent {
  constructor(private authService: AuthService) {}

  isAuthenticated$ = this.authService.isAuthenticated();

  closeDropdown() {
    if (document.activeElement instanceof HTMLElement) {
      document.activeElement.blur();
    }
  }
}
