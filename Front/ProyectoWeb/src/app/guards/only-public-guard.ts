import { CanActivateFn, Router, RedirectCommand } from '@angular/router';
import { inject } from '@angular/core';
import { AuthService } from '../services/auth-service';

export const onlyPublicGuard: CanActivateFn = (route, state) => {
  const authService = inject(AuthService)
  const router = inject(Router);
  if (authService.token())return new RedirectCommand(router.parseUrl("/admin"));
  return true;
};
