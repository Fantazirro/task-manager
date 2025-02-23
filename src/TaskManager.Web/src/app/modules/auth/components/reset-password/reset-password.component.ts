import { Component } from '@angular/core';
import { FormBuilder, FormControl, FormGroup, Validators } from '@angular/forms';
import { AuthService } from '../../services/auth.service';
import { ActivatedRoute, Router } from '@angular/router';
import { NewPassword } from '../../models/new-password.model';

@Component({
    selector: 'app-reset-password',
    templateUrl: './reset-password.component.html',
    styleUrls: ['./reset-password.component.css']
})
export class ResetPasswordComponent {
    resetPasswordForm: FormGroup;
    newPasswordForm: FormGroup;
    token: string = '';

    resetSubmitted: boolean = false;

    constructor(private formBuilder: FormBuilder, private auth: AuthService, private router: Router, private route: ActivatedRoute) {
        this.token = route.snapshot.queryParams['token'] ?? '';

        this.resetPasswordForm = formBuilder.group({
            email: ['', [Validators.required, Validators.email, Validators.maxLength(30)]]
        });
        this.newPasswordForm = formBuilder.group({
            password: ['', [Validators.required, Validators.minLength(5), Validators.maxLength(15)]],
            confirmPassword: ['', [Validators.required]]
        });
    }

    get email() { return this.resetPasswordForm?.get('email') as FormControl; }
    get password() { return this.newPasswordForm.get('password') as FormControl; }
    get confirmPassword() { return this.newPasswordForm.get('confirmPassword') as FormControl; }

    onResetSubmit() {
        if (this.resetPasswordForm.valid) {
            this.auth.resetPassword(this.email.value).subscribe({
                next: () => {
                    this.resetSubmitted = true;
                }
            });
        }
    }

    onNewPasswordSubmit() {
        if (this.newPasswordForm.valid) {
            const newPassword: NewPassword = {
                token: this.token,
                password: this.password.value
            };

            this.auth.confirmReset(newPassword).subscribe({
                next: () => {
                    // TODO: НА СТРАНИЦЕ ПОКАЗАТЬ СООБЩЕНИЕ, ЧТО ПАРОЛЬ УСПЕШНО ИЗМЕНЁН
                    this.router.navigate(['sign-in']);
                }
            });
        }
    }

    matchPassword() {
        if (this.password.value !== this.confirmPassword.value) {
            this.confirmPassword.setErrors({ notmatched: true });
        }
    }
}