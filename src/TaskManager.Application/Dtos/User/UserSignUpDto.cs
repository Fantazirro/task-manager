﻿namespace TaskManager.Application.Dtos.User
{
    public class UserSignUpDto
    {
        public string UserName { get; set; } = null!;
        public string Email { get; set; } = null!;
        public string Password { get; set; } = null!;
    }
}