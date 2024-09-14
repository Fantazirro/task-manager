using TaskManager.Domain.Enums;

namespace TaskManager.Application.Dtos.Tasks
{
    public class UpdateTaskRequest
    {
        public Guid Id { get; set; }
        public string Header { get; set; } = null!;
        public string Description { get; set; } = null!;
        public TaskEntityStatus Status { get; set; }
        public DateTimeOffset? Deadline { get; set; }
    }
}