import { Component } from '@angular/core';
import { AuthService } from '../../../../shared/services/auth.service';
import { FormBuilder, FormControl, FormGroup, Validators } from '@angular/forms';
import { SignUpData } from '../../../../shared/models/auth/sign-up.model';
import { Router } from '@angular/router';

@Component({
  selector: 'app-sign-up',
  templateUrl: './sign-up.component.html',
  styleUrls: ['./sign-up.component.css']
})
export class SignUpComponent {
    signUpForm: FormGroup;

    constructor(private auth: AuthService, private formBuilder: FormBuilder, private router: Router) {
        this.signUpForm = this.formBuilder.group({
            username: ['', [Validators.required, Validators.maxLength(30)]],
            email: ['', [Validators.required, Validators.email, Validators.maxLength(30)]],
            password: ['', [Validators.required, Validators.minLength(5), Validators.maxLength(15)]]
        });
    }

    onSubmit() : void {
        if (!this.signUpForm.valid) return;

        let signUpData: SignUpData = this.signUpForm.getRawValue();
        this.auth.signUp(signUpData).subscribe({
            next: () => {
                this.router.navigate(['sign-in']);
            }
        });
    }

    get username() { return this.signUpForm.get('username') as FormControl; }
    get email() { return this.signUpForm.get('email') as FormControl; }
    get password() { return this.signUpForm.get('password') as FormControl; }
}