import { EventEmitter, Injectable } from '@angular/core';
import { SignInData } from '../models/auth/sign-in.model';
import { HttpClient } from '@angular/common/http';
import { SignUpData } from '../models/auth/sign-up.model';
import { baseUrl } from 'src/app/shared/constants/environment';
import { JwtResponse } from '../models/auth/jwt-response.model';
import { tap } from 'rxjs';
import { User } from '../models/auth/user.model';

@Injectable({
    providedIn: 'root'
})
export class AuthService {
    private url = baseUrl + '/auth';
    private tokenKey: string = 'token';
    private userKey: string = 'user';

    onAuthenticated = new EventEmitter<boolean>();

    constructor(private http: HttpClient) { }

    signUp(data: SignUpData, code: string) {
        return this.http.post<JwtResponse>(`${this.url}/sign-up?code=${code}`, data).pipe(
            tap((response) => {
                this.authenticate(response.token);
            })
        );
    }

    signIn(data: SignInData) {
        return this.http.post<JwtResponse>(`${this.url}/sign-in`, data).pipe(
            tap((response) => {
                this.authenticate(response.token);
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

    getUser(): User | null {
        const userValue = localStorage.getItem(this.userKey);
        if (userValue == null) return null;
        return JSON.parse(userValue);
    }

    sendCode(email: string) {
        return this.http.post(`${this.url}/send-code?email=${email}`, null);
    }

    checkEmail(email: string) {
        return this.http.get(`${this.url}/check-email?email=${email}`);
    }

    private authenticate(token: string) {
        localStorage.setItem(this.tokenKey, token);
        this.onAuthenticated.emit(true);
        this.http.get(`${baseUrl}/users`).subscribe({
            next: (response) => {
                localStorage.setItem(this.userKey, JSON.stringify(response));
            }
        });
    }
}