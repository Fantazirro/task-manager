import { NgModule } from '@angular/core';
import { RouterModule, Routes } from '@angular/router';
import { MainComponent } from './modules/main/main.component';
import { SignUpComponent } from './modules/auth/components/sign-up/sign-up.component';
import { SignInComponent } from './modules/auth/components/sign-in/sign-in.component';
import { TasksMenuComponent } from './modules/tasks/components/tasks-menu/tasks-menu.component';
import { AuthorizedGuard, NotAuthorizedGuard } from './shared/guards/auth.guard';

const routes: Routes = [
    { path: '', component: MainComponent, canActivate: [NotAuthorizedGuard] },
    { path: 'sign-up', component: SignUpComponent, canActivate: [NotAuthorizedGuard] },
    { path: 'sign-in', component: SignInComponent, canActivate: [NotAuthorizedGuard] },
    { path: 'tasks', component: TasksMenuComponent, canActivate: [AuthorizedGuard] },
    { path: '**', redirectTo: '/', pathMatch: 'full' }
];

@NgModule({
    imports: [RouterModule.forRoot(routes)],
    exports: [RouterModule]
})
export class AppRoutingModule { }