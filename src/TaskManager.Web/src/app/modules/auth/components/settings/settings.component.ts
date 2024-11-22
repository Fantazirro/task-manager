import { Component } from '@angular/core';
import { FormBuilder, FormControl, FormGroup, Validators } from '@angular/forms';
import { UserService } from 'src/app/shared/services/user.service';
import { AuthService } from '../../services/auth.service';
import { UpdateUser } from 'src/app/shared/models/user/user.model';

@Component({
    selector: 'app-settings',
    templateUrl: './settings.component.html',
    styleUrls: ['./settings.component.css']
})
export class SettingsComponent {
    settingsForm: FormGroup;
    isEmailTaken: boolean = false;
    settingsSaved: boolean = false;

    constructor(private formBuilder: FormBuilder, private userService: UserService, private auth: AuthService) {
        this.settingsForm = this.formBuilder.group({
            username: ['', [Validators.required, Validators.maxLength(30)]],
            email: ['', [Validators.required, Validators.email, Validators.maxLength(30)]],
            password: ['', [Validators.required, Validators.minLength(5), Validators.maxLength(15)]],
            confirmPassword: ['', [Validators.required]]
        });
    }

    get username() { return this.settingsForm.get('username') as FormControl; }
    get email() { return this.settingsForm.get('email') as FormControl; }
    get password() { return this.settingsForm.get('password') as FormControl; }
    get confirmPassword() { return this.settingsForm.get('confirmPassword') as FormControl; }

    onSubmit() {
        if (!this.isSettingsValid()) return;

        let userData = this.settingsForm.getRawValue() as UpdateUser;
        this.setEmptyToUndefined(userData);
        this.userService.updateUser(userData).subscribe({
            next: () => {
                this.settingsSaved = true;
            }
        });
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

    isSettingsValid() {
        return (this.username.value.length == 0 || this.username.valid) &&
            (this.email.value.length == 0 || this.email.valid) &&
            (this.password.value.length == 0 || this.password.valid && this.confirmPassword.valid)
    }

    isAllEmpty() {
        return this.username.value.length + this.email.value.length + this.password.value.length == 0;
    }

    setEmptyToUndefined(userData: UpdateUser) {
        if (userData.userName == '') userData.userName = undefined;
        if (userData.email == '') userData.email = undefined;
        if (userData.password == '') userData.password = undefined;
    }
}