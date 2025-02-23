import { Component, OnInit } from '@angular/core';
import { Task, TaskStatus } from '../../models/task.model';
import { TaskService } from '../../services/task.service';

@Component({
    selector: 'app-tasks-menu',
    templateUrl: './tasks-menu.component.html',
    styleUrls: ['./tasks-menu.component.css']
})
export class TasksMenuComponent implements OnInit {
    constructor(private taskService: TaskService) {

    }

    get tasks() {
        return this.taskService.tasks;
    }

    ngOnInit(): void {
        this.taskService.getTasks();
    }

    selectTask(index: number) {
        this.taskService.selectTask(index);
    }

    addTask() {
        this.taskService.addTask();
    }

    isCompleted(task: Task) {
        return task.status == TaskStatus.Completed;
    }

    changeTaskStatus(task: Task) {
        if (this.taskService.isEditMode) return;

        if (task.status == TaskStatus.InProgress) {
            this.taskService.changeTaskStatus(task, TaskStatus.Completed);
            task.status = TaskStatus.Completed;
        }
        else {
            this.taskService.changeTaskStatus(task, TaskStatus.InProgress);
            task.status =TaskStatus.InProgress;
        }
    }
}