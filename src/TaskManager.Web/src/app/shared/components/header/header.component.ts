import { Component, OnDestroy } from '@angular/core';
import { Router } from '@angular/router';
import { BsDropdownConfig } from 'ngx-bootstrap/dropdown';
import { Subscription } from 'rxjs';
import { AuthService } from 'src/app/modules/auth/services/auth.service';
import { UserService } from '../../services/user.service';

@Component({
  selector: 'app-header',
  templateUrl: './header.component.html',
  styleUrls: ['./header.component.css'],
  providers: [ { provide: BsDropdownConfig, useValue: { isAnimated: true, autoClose: true }} ]
})
export class HeaderComponent implements OnDestroy {
    private authEvent: Subscription | null = null;

    isAuthenticated : boolean = true;
    username?: string;

    constructor(private auth: AuthService, private userServise: UserService, private router: Router) {
        this.isAuthenticated = auth.isAuthenticated();
        this.authEvent = auth.onAuthenticated.subscribe(isAuth => this.isAuthenticated = isAuth);
        this.username = this.userServise.getUser()?.userName;
    }

    ngOnDestroy(): void {
        this.authEvent?.unsubscribe();
    }

    signOut() : void {
        this.auth.signOut();
        this.router.navigate(['']);
    }
}