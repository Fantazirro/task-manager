import { Component, Input } from '@angular/core';
import { SignUpData } from 'src/app/modules/auth/models/sign-up.model';
import { SendCodeService } from '../../../services/send-code.service';
import { Router } from '@angular/router';
import { AuthService } from '../../../services/auth.service';

@Component({
    selector: 'app-send-code',
    templateUrl: './send-code.component.html',
    styleUrls: ['./send-code.component.css']
})
export class SendCodeComponent {
    invalidCode: boolean = false;

    @Input() signUpData?: SignUpData;

    constructor(private auth: AuthService, private codeServise: SendCodeService, private router: Router) { }

    get secondsRemaining() {
        return this.codeServise.secondsRemaining;
    }

    checkCode(code: string) {
        this.invalidCode = false;
        if (code.length == 4) {
            this.auth.signUp(this.signUpData!, code).subscribe({
                next: () => {
                    this.router.navigate(['tasks']);
                },
                error: () => {
                    this.invalidCode = true;
                }
            });
        }
    }

    sendCode() {
        this.codeServise.sendCode(this.signUpData?.email!);
    }

    canSendAgain() {
        return this.codeServise.canSendAgain();
    }
}