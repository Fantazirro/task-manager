import { Injectable } from '@angular/core';
import { AuthService } from 'src/app/shared/services/auth.service';

@Injectable({
    providedIn: 'root'
})
export class SendCodeService {
    buttonTimer?: any;
    secondsRemaining: number = 0;

    constructor(private auth: AuthService) { }

    sendCode(email: string) {
        this.secondsRemaining = 30;
        this.buttonTimer = setInterval(() => this.passSecond(), 1000);

        this.auth.sendCode(email).subscribe();
    }

    canSendAgain() {
        return this.secondsRemaining <= 0;
    }

    private passSecond() {
        this.secondsRemaining--;

        if (this.secondsRemaining == 0)
            clearInterval(this.buttonTimer);
    }
}
