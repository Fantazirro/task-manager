import { EventEmitter, Injectable, Output } from '@angular/core';
import { TaskHttpService } from './task-http.service';
import { AddTaskRequest, Task, TaskStatus, UpdateTaskRequest } from '../models/task.model';
import { TasksListItem } from '../models/tasks-list-item.model';

@Injectable({
    providedIn: 'root'
})
export class TaskService {
    tasks: Task[] = [];
    currentTask?: TasksListItem = undefined;

    isEditMode: boolean = false;
    isNewTask: boolean = false;

    @Output() onEditMode = new EventEmitter<boolean>();

    constructor(private taskHttpService: TaskHttpService) { }

    selectTask(index: number) {
        if (!this.isEditMode) {
            this.currentTask = {
                index: index,
                task: this.tasks[index]
            };
        }
    }

    getTasks() {
        this.taskHttpService.getAllTasks().subscribe({
            next: (response) => {
                this.tasks = response;
            }
        });
    }

    addTask() {
        this.currentTask = undefined;
        this.isNewTask = true;
        this.isEditMode = true;
    }

    saveTask(task: AddTaskRequest) {
        this.taskHttpService.addTask(task).subscribe({
            next: (response) => {
                this.tasks.unshift(response);
                this.currentTask = {
                    index: 0,
                    task: response
                };
            }
        });
    }

    updateTask(task: UpdateTaskRequest) {
        this.taskHttpService.updateTask(task).subscribe({
            next: (response) => {
                this.currentTask!.task = response;
                this.tasks[this.currentTask!.index] = response;
            }
        });
    }

    deleteTask() {
        this.taskHttpService.deleteTask(this.currentTask!.task.id).subscribe({
            next: () => {
                this.tasks.splice(this.currentTask!.index, 1);
                this.currentTask = undefined;
            }
        });
    }

    changeTaskStatus(task: Task, status: TaskStatus) {
        let updatedTask: UpdateTaskRequest = {
            id: task.id,
            header: task.header,
            description: task.description,
            status: status,
            deadline: task.deadline
        };
        this.taskHttpService.updateTask(updatedTask).subscribe();
    }

    taskToHistory() {
        this.currentTask!.task.status = TaskStatus.InHistory;
        this.taskHttpService.updateTask(this.currentTask!.task).subscribe({
            next: () => {
                this.currentTask = undefined;
            }
        });
    }

    editModeOn() {
        this.isEditMode = true;
        this.onEditMode.emit(true);
    }

    editModeOff() {
        this.isEditMode = false;
        this.isNewTask = false;
        this.onEditMode.emit(false);
    }
}