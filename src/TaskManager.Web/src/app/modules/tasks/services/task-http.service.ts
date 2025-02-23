import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { baseUrl } from 'src/app/shared/constants/environment';
import { AddTaskRequest, Task, UpdateTaskRequest } from '../models/task.model';

@Injectable({
    providedIn: 'root'
})
export class TaskHttpService {
    private url = baseUrl + '/tasks';

    constructor(private http: HttpClient) { }

    getTask(id: string) {
        return this.http.get<Task>(`${this.url}/${id}`);
    }

    getAllTasks() {
        return this.http.get<Task[]>(this.url);
    }

    getHistory() {
        return this.http.get<Task[]>(`${this.url}/history`);
    }

    addTask(data: AddTaskRequest) {
        return this.http.post<Task>(this.url, data);
    }

    updateTask(data: UpdateTaskRequest) {
        return this.http.put<Task>(this.url, data);
    }

    deleteTask(id: string) {
        return this.http.delete(`${this.url}/${id}`);
    }
}
