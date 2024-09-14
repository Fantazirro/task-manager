import { CanActivateFn, Router } from '@angular/router';
import { AuthService } from '../services/auth.service';
import { inject } from '@angular/core';

export const AuthorizedGuard: CanActivateFn = (route, state) => {
    const authService = inject(AuthService);
    return authService.isAuthenticated();
};

export const NotAuthorizedGuard: CanActivateFn = (route, state) => {
    const authService = inject(AuthService);
    const router = inject(Router);

    if (authService.isAuthenticated())
    {
        router.navigate(['tasks']);
        return false;
    }
    else return true;
};