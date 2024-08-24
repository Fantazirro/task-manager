using TaskManager.Domain.Enums;

namespace TaskManager.Application.Dtos.UserTask
{
    public class UpdateUserTaskDto
    {
        public Guid Id { get; set; }
        public string Header { get; set; } = null!;
        public string Description { get; set; } = null!;
        public UserTaskStatus Status { get; set; }
        public DateTimeOffset? Deadline { get; set; }
    }
}