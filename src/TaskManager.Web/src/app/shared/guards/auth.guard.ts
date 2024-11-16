import { CanActivateFn, Router } from '@angular/router';
import { inject } from '@angular/core';
import { AuthService } from 'src/app/modules/auth/services/auth.service';

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