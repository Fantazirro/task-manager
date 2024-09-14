export interface Task {
    id: string,
    header: string,
    description: string,
    status: TaskStatus,
    deadline?: Date,
    completedDate?: Date
}

export enum TaskStatus {
    InProgress,
    Completed,
    InHistory
}

export interface AddTaskRequest {
    header: string,
    description: string,
    deadline?: Date
}

export interface UpdateTaskRequest {
    id: string,
    header: string,
    description: string,
    status: TaskStatus,
    deadline?: Date
}