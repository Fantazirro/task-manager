import { EventEmitter, Injectable } from '@angular/core';
import { SignInData } from '../models/auth/sign-in.model';
import { HttpClient } from '@angular/common/http';
import { SignUpData } from '../models/auth/sign-up.model';
import { baseUrl } from 'src/app/shared/constants/environment';
import { JwtResponse } from '../models/auth/jwt-response.model';
import { catchError, map } from 'rxjs';

@Injectable({
    providedIn: 'root'
})
export class AuthService {
    private url = baseUrl + '/auth';
    private tokenKey: string = 'token';

    onAuthenticated = new EventEmitter<boolean>();

    constructor(private http: HttpClient) { }

    signUp(data: SignUpData) {
        return this.http.post(`${this.url}/sign-up`, data);
    }

    signIn(data: SignInData) {
        return this.http.post<JwtResponse>(`${this.url}/sign-in`, data).pipe(
            map(response => {
                localStorage.setItem(this.tokenKey, response.token);
                this.onAuthenticated.emit(true);
            })
        );
    }

    signOut(): void {
        localStorage.removeItem(this.tokenKey);
        this.onAuthenticated.emit(false);
    }

    isAuthenticated(): boolean {
        return this.getToken() != null;
    }

    getToken(): string | null {
        return localStorage.getItem(this.tokenKey);
    }
}