export interface User {
    id: string,
    userName: string
    email: string
}

export interface UpdateUser {
    userName?: string,
    email?: string,
    password?: string
}