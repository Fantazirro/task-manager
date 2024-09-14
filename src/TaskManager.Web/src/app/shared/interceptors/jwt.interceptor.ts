import { Injectable } from '@angular/core';
import {
    HttpRequest,
    HttpHandler,
    HttpEvent,
    HttpInterceptor
} from '@angular/common/http';
import { catchError, map, Observable, throwError } from 'rxjs';
import { AuthService } from '../services/auth.service';
import { Router } from '@angular/router';

@Injectable()
export class JwtInterceptor implements HttpInterceptor {

    constructor(private authService: AuthService, private router: Router) { }

    intercept(request: HttpRequest<unknown>, next: HttpHandler): Observable<HttpEvent<unknown>> {
        let token = this.authService.getToken();

        if (token != null) {
            request = request.clone({
                headers: request.headers.set('Authorization', 'Bearer ' + token)
            });
        }

        return next.handle(request).pipe(
            map((response) => {
                return response;
            }),
            catchError((error) => {
                if (error.status == 401 && this.authService.isAuthenticated()) {
                    this.authService.signOut();
                    this.router.navigate(['']);
                }

                return throwError(() => error);
            })
        );
    }
}