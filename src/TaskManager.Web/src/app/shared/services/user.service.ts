import { Injectable } from '@angular/core';
import { UpdateUser, User } from '../models/user/user.model';
import { HttpClient } from '@angular/common/http';
import { baseUrl } from '../constants/environment';
import { AuthService } from 'src/app/modules/auth/services/auth.service';
import { tap } from 'rxjs';
import { JwtResponse } from 'src/app/modules/auth/models/jwt-response.model';


@Injectable({
    providedIn: 'root'
})
export class UserService {
    private url = baseUrl + '/users';
    private userKey: string = 'user';

    constructor(private http: HttpClient, private auth: AuthService) { }

    getUser(): User | null {
        const userValue = localStorage.getItem(this.userKey);
        if (userValue == null) return null;
        return JSON.parse(userValue);
    }

    getUserFromServer() {
        this.http.get(this.url).subscribe({
            next: (response) => {
                localStorage.setItem(this.userKey, JSON.stringify(response));
            }
        });
    }

    updateUser(updatedUser: UpdateUser) {
        return this.http.put<JwtResponse>(this.url, updatedUser).pipe(
            tap((response) => {
                this.auth.createOrUpdateToken(response.token);
                this.getUserFromServer();
            })
        );
    }
}