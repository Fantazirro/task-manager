import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import { TasksMenuComponent } from './components/tasks-menu/tasks-menu.component';
import { RouterModule } from '@angular/router';
import { SelectedTaskComponent } from './components/tasks-menu/selected-task/selected-task.component';
import { ReactiveFormsModule } from '@angular/forms';

@NgModule({
  declarations: [
    TasksMenuComponent,
    SelectedTaskComponent
  ],
  imports: [
    CommonModule,
    RouterModule,
    ReactiveFormsModule
  ]
})
export class TasksModule { }
