import { Injectable } from '@angular/core';
import {
    HttpRequest,
    HttpHandler,
    HttpEvent,
    HttpInterceptor,
    HttpResponse,
    HttpErrorResponse
} from '@angular/common/http';
import { Observable, tap } from 'rxjs';

@Injectable()
export class DisableButtonsInterceptor implements HttpInterceptor {

    constructor() { }

    intercept(request: HttpRequest<unknown>, next: HttpHandler): Observable<HttpEvent<unknown>> {
        let buttons = document.getElementsByClassName('async-disable');

        for (let button of buttons) {
            button.classList.add('disabled');
        }

        return next.handle(request).pipe(
            tap({
                next: (response) => {
                    if (response instanceof HttpResponse) {
                        for (let button of buttons) {
                            button.classList.remove('disabled');
                        }
                    }
                },
                error: (error) => {
                    if (error instanceof HttpErrorResponse) {
                        for (let button of buttons) {
                            button.classList.remove('disabled');
                        }
                    }
                }
            }));
    }
}