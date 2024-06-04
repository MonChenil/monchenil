import { inject } from '@angular/core';
import { CanActivateFn, Router } from '@angular/router';
import { of, switchMap } from 'rxjs';
import { AuthService } from '../services/auth.service';

export const isAuthenticatedGuard: CanActivateFn = (route, state) => {
  const router = inject(Router);
  const authService = inject(AuthService);

  return authService.isAuthenticated().pipe(
    switchMap((isAuthenticated) => {
      if (isAuthenticated) {
        return of(true);
      }

      router.navigate(['/auth/login'], {
        queryParams: { redirectUrl: state.url },
      });
      return of(false);
    }),
  );
};
