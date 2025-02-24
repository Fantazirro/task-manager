﻿using TaskManager.Domain.Enums;

namespace TaskManager.Application.Models.Tasks
{
    public class TaskResponse
    {
        public Guid Id { get; set; }
        public string Header { get; set; } = null!;
        public string Description { get; set; } = null!;
        public TaskEntityStatus Status { get; set; }
        public DateTimeOffset? Deadline { get; set; }
        public DateTimeOffset? CompletedDate { get; set; }
    }
}
