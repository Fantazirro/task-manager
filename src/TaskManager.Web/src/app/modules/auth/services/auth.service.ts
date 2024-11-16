import { EventEmitter, Injectable } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { baseUrl } from 'src/app/shared/constants/environment';
import { tap } from 'rxjs';
import { UserService } from 'src/app/shared/services/user.service';
import { SignUpData } from '../models/sign-up.model';
import { JwtResponse } from '../models/jwt-response.model';
import { SignInData } from '../models/sign-in.model';
import { NewPassword } from '../models/new-password.model';

@Injectable({
    providedIn: 'root'
})
export class AuthService {
    private url = baseUrl + '/auth';
    private tokenKey: string = 'token';

    onAuthenticated = new EventEmitter<boolean>();

    constructor(private http: HttpClient, private userServise: UserService) { }

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

    sendCode(email: string) {
        return this.http.post(`${this.url}/send-code?email=${email}`, null);
    }

    checkEmail(email: string) {
        return this.http.get(`${this.url}/check-email?email=${email}`);
    }

    resetPassword(email: string) {
        return this.http.post(`${this.url}/reset-password?email=${email}`, null);
    }

    confirmReset(newPassword: NewPassword) {
        return this.http.post(`${this.url}/confirm-reset`, newPassword);
    }

    isAuthenticated(): boolean {
        return this.getToken() != null;
    }

    getToken(): string | null {
        return localStorage.getItem(this.tokenKey);
    }

    private authenticate(token: string) {
        localStorage.setItem(this.tokenKey, token);
        this.onAuthenticated.emit(true);
        this.userServise.getUserFromServer();
    }
}