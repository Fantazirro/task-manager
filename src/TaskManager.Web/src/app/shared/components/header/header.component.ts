import { Component, OnDestroy } from '@angular/core';
import { Router } from '@angular/router';
import { BsDropdownConfig } from 'ngx-bootstrap/dropdown';
import { Subscription } from 'rxjs';
import { AuthService } from 'src/app/shared/services/auth.service';

@Component({
  selector: 'app-header',
  templateUrl: './header.component.html',
  styleUrls: ['./header.component.css'],
  providers: [ { provide: BsDropdownConfig, useValue: { isAnimated: true, autoClose: true }} ]
})
export class HeaderComponent implements OnDestroy {
    private authEvent: Subscription | null = null;

    isAuthenticated : boolean = true;

    constructor(private auth: AuthService, private router: Router) {
        this.isAuthenticated = auth.isAuthenticated();
        this.authEvent = auth.onAuthenticated.subscribe(isAuth => this.isAuthenticated = isAuth);
    }

    ngOnDestroy(): void {
        this.authEvent?.unsubscribe();
    }

    signOut() : void {
        this.auth.signOut();
        this.router.navigate(['']);
    }
}