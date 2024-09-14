import { Component, OnInit, ViewChild } from '@angular/core';
import { Task, TaskStatus } from '../../models/task.model';
import { TasksListItem } from '../../models/tasks-list-item.model';
import { SelectedTaskComponent } from './selected-task/selected-task.component';
import { TaskService } from '../../services/task.service';

@Component({
    selector: 'app-tasks-menu',
    templateUrl: './tasks-menu.component.html',
    styleUrls: ['./tasks-menu.component.css']
})
export class TasksMenuComponent implements OnInit {
    tasks: Task[] = [];
    currentTask?: TasksListItem = undefined;

    @ViewChild(SelectedTaskComponent) selectedTaskCmp?: SelectedTaskComponent;

    constructor(private taskService: TaskService) { }

    ngOnInit(): void {
        this.taskService.getAllTasks().subscribe({
            next: (response) => {
                this.tasks = response;
            }
        });
    }

    selectTask(index: number) {
        if (!this.selectedTaskCmp?.isEditMode) {
            this.currentTask = {
                index: index,
                task: this.tasks[index]
            };
        }
    }

    newTask() {
        this.selectedTaskCmp?.addTask();
    }

    addTask(task: Task) {
        this.tasks.unshift(task);
        this.currentTask = {
            index: 0,
            task: task
        };
    }

    deleteTask(index: number) {
        this.tasks.splice(index, 1);
    }

    updateTask(task: Task) {
        this.currentTask!.task = task;
        this.tasks[this.currentTask!.index] = task;
    }
}