import { Component } from '@angular/core';
import { FormBuilder, FormControl, FormGroup, Validators } from '@angular/forms';
import { SignUpData } from '../../models/sign-up.model';
import { SendCodeService } from '../../services/send-code.service';
import { AuthService } from '../../services/auth.service';

@Component({
  selector: 'app-sign-up',
  templateUrl: './sign-up.component.html',
  styleUrls: ['./sign-up.component.css']
})
export class SignUpComponent {
    signUpForm: FormGroup;
    isEmailTaken: boolean = false;

    signUpData?: SignUpData;

    constructor(private auth: AuthService, private codeService: SendCodeService, private formBuilder: FormBuilder) {
        this.signUpForm = this.formBuilder.group({
            username: ['', [Validators.required, Validators.maxLength(30)]],
            email: ['', [Validators.required, Validators.email, Validators.maxLength(30)]],
            password: ['', [Validators.required, Validators.minLength(5), Validators.maxLength(15)]],
            confirmPassword: ['', [Validators.required]]
        });
    }

    get username() { return this.signUpForm.get('username') as FormControl; }
    get email() { return this.signUpForm.get('email') as FormControl; }
    get password() { return this.signUpForm.get('password') as FormControl; }
    get confirmPassword() { return this.signUpForm.get('confirmPassword') as FormControl; }

    onSubmit() {
        if (!this.signUpForm.valid) return;

        this.signUpData = this.signUpForm.getRawValue();
        this.codeService.sendCode(this.signUpData?.email!);
    }

    checkEmail() {
        if (!this.email.valid) return;
        this.auth.checkEmail(this.email.value).subscribe({
            next: (response) => {
                if (response) this.email.setErrors({ emailtaken: true });
            }
        });
    }

    matchPassword() {
        if (this.password.value !== this.confirmPassword.value) {
            this.confirmPassword.setErrors({ notmatched: true });
        }
    }

    isSendCodePhase() {
        return this.signUpData !== undefined;
    }
}