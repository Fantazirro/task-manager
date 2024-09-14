import { Component, EventEmitter, Input, Output } from '@angular/core';
import { AddTaskRequest, Task, TaskStatus, UpdateTaskRequest } from '../../../models/task.model';
import { TaskService } from '../../../services/task.service';
import { TasksListItem } from '../../../models/tasks-list-item.model';
import { FormBuilder, FormGroup, Validators } from '@angular/forms';

@Component({
    selector: 'app-selected-task',
    templateUrl: './selected-task.component.html',
    styleUrls: ['./selected-task.component.css']
})
export class SelectedTaskComponent {
    isEditMode: boolean = false;
    isNewTask: boolean = false;

    taskForm: FormGroup;

    @Input() currentTask?: TasksListItem = undefined;

    @Output() onAddTask = new EventEmitter<Task>();
    @Output() onDeleteTask = new EventEmitter<number>();
    @Output() onUpdateTask = new EventEmitter<Task>();

    constructor(private taskService: TaskService, private formBuilder: FormBuilder) {
        this.taskForm = this.formBuilder.group({
            header: ['', [Validators.maxLength(30)]],
            description: ['', [Validators.maxLength(500)]]
        });
    }

    editModeOn() {
        this.isEditMode = true;
        this.taskForm.setValue({
            header: this.currentTask?.task.header,
            description: this.currentTask?.task.description
        });
    }

    editModeOff() {
        this.isEditMode = false;
        this.isNewTask = false;
        this.taskForm.setValue({
            header: '',
            description: ''
        });
    }

    saveChanges() {
        if (this.isNewTask) this.saveTask();
        else this.updateTask();
    }

    addTask() {
        this.currentTask = undefined;
        this.isNewTask = true;
        this.editModeOn();
    }

    saveTask() {
        let newTask: AddTaskRequest = this.taskForm.getRawValue();
        this.taskService.addTask(newTask).subscribe({
            next: (response) => {
                this.editModeOff();
                this.onAddTask.emit(response);
            }
        });
    }

    deleteTask() {
        this.taskService.deleteTask(this.currentTask!.task.id).subscribe({
            next: () => {
                this.onDeleteTask.emit(this.currentTask?.index);
                this.currentTask = undefined;
            }
        });
    }

    updateTask() {
        let updatedTask: UpdateTaskRequest = {
            id: this.currentTask!.task.id,
            header: this.taskForm.get('header')?.value,
            description: this.taskForm.get('description')?.value,
            status: this.currentTask!.task.status,
            deadline: this.currentTask!.task.deadline
        };
        this.taskService.updateTask(updatedTask).subscribe({
            next: (response) => {
                this.editModeOff();
                this.onUpdateTask.emit(response);
            }
        });
    }

    completeTask() {
        this.currentTask!.task.status = TaskStatus.Completed;
        this.updateTask();
    }

    uncompleteTask() {
        this.currentTask!.task.status = TaskStatus.InProgress;
        this.updateTask();
    }

    taskToHistory() {
        this.currentTask!.task.status = TaskStatus.InHistory;
        this.taskService.updateTask(this.currentTask!.task).subscribe({
            next: () => {
                this.onDeleteTask.emit(this.currentTask?.index);
                this.currentTask = undefined;
            }
        });
    }
}