import { Component } from '@angular/core';
import { FormBuilder, FormControl, FormGroup, Validators } from '@angular/forms';
import { AuthService } from '../../../../shared/services/auth.service';
import { SignInData } from '../../../../shared/models/auth/sign-in.model';
import { Router } from '@angular/router';

@Component({
  selector: 'app-sign-in',
  templateUrl: './sign-in.component.html',
  styleUrls: ['./sign-in.component.css']
})
export class SignInComponent {
    signInForm: FormGroup;
    errorMessage: string | null = null;

    constructor(private auth: AuthService, private formBuilder: FormBuilder, private router: Router) {
        this.signInForm = this.formBuilder.group({
            email: ['', [Validators.required, Validators.email, Validators.maxLength(30)]],
            password: ['', [Validators.required, Validators.minLength(5), Validators.maxLength(15)]]
        });
    }

    onSubmit() : void {
        if (!this.signInForm.valid) return;

        let signInData: SignInData = this.signInForm.getRawValue();
        this.auth.signIn(signInData).subscribe({
            next: () =>{
                this.errorMessage = null;
                this.router.navigate(['tasks']);
            },
            error: () => {
                this.errorMessage = "Неверно введён адрес электронной почты или пароль";
            }
        });
    }

    get email() { return this.signInForm.get('email') as FormControl; }
    get password() { return this.signInForm.get('password') as FormControl; }
}