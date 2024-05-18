import { Component } from '@angular/core';
import { AuthService } from '../../auth/services/auth.service';

@Component({
  selector: 'app-dashboard',
  templateUrl: './dashboard.component.html',
  providers: [AuthService],
})
export class DashboardComponent {
  constructor(private authService: AuthService) {}

  isAuthenticated$ = this.authService.isAuthenticated();
}
