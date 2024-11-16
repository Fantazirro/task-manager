import { Injectable } from '@angular/core';
import { UpdateUser, User } from '../models/user/user.model';
import { HttpClient } from '@angular/common/http';
import { baseUrl } from '../constants/environment';

@Injectable({
    providedIn: 'root'
})
export class UserService {
    private url = baseUrl + '/users';
    private userKey: string = 'user';

    constructor(private http: HttpClient) { }

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
        this.http.put(this.url, updatedUser);
    }
}