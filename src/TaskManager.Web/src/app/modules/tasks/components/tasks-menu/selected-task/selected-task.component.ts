import { Component, TemplateRef } from '@angular/core';
import { AddTaskRequest, TaskStatus, UpdateTaskRequest } from '../../../models/task.model';
import { TaskService } from '../../../services/task.service';
import { FormBuilder, FormGroup, Validators } from '@angular/forms';
import { BsModalRef, BsModalService } from 'ngx-bootstrap/modal';

@Component({
    selector: 'app-selected-task',
    templateUrl: './selected-task.component.html',
    styleUrls: ['./selected-task.component.css']
})
export class SelectedTaskComponent {
    taskForm: FormGroup;
    deleteModalRef?: BsModalRef;

    constructor(
        private taskService: TaskService,
        private formBuilder: FormBuilder,
        private modalService: BsModalService) {
        this.taskForm = this.formBuilder.group({
            header: ['', [Validators.required, Validators.maxLength(50)]],
            description: ['', [Validators.maxLength(2000)]],
            deadline: [null]
        });
    }

    get currentTask() {
        return this.taskService.currentTask?.task;
    }

    get isEditMode() {
        return this.taskService.isEditMode;
    }

    get isNewTask() {
        return this.taskService.isNewTask;
    }

    editModeOn() {
        this.taskService.editModeOn();
        this.taskForm.setValue({
            header: this.currentTask!.header,
            description: this.currentTask!.description,
            deadline: null
        });
    }

    editModeOff() {
        this.taskService.editModeOff();
        this.taskForm.setValue({
            header: '',
            description: '',
            deadline: null
        });
    }

    saveChanges() {
        if (!this.taskForm.valid) return;

        if (this.taskService.isNewTask) this.saveTask();
        else this.updateTask();

        this.editModeOff();
    }

    addTask() {
        this.taskService.addTask();
    }

    saveTask() {
        let newTask: AddTaskRequest = this.taskForm.getRawValue();
        this.taskService.saveTask(newTask);
    }

    showDeleteModal(modal: TemplateRef<void>) {
        this.deleteModalRef = this.modalService.show(modal);
    }

    deleteTask() {
        this.deleteModalRef?.hide();
        this.taskService.deleteTask();
    }

    updateTask() {
        let updatedTask: UpdateTaskRequest = {
            id: this.currentTask!.id,
            header: this.currentTask!.header,
            description: this.currentTask!.description,
            status: this.currentTask!.status,
            deadline: this.currentTask!.deadline
        };

        if (this.taskService.isEditMode) {
            updatedTask.header = this.taskForm.get('header')?.value;
            updatedTask.description = this.taskForm.get('description')?.value;
            updatedTask.deadline = this.taskForm.get('deadline')?.value;
        }

        this.taskService.updateTask(updatedTask);
    }

    completeTask() {
        this.currentTask!.status = TaskStatus.Completed;
        this.updateTask();
    }

    uncompleteTask() {
        this.currentTask!.status = TaskStatus.InProgress;
        this.updateTask();
    }

    taskToHistory() {
        this.taskService.taskToHistory();
    }
}