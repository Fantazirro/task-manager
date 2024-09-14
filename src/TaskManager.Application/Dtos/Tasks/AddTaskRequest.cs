namespace TaskManager.Application.Dtos.Tasks
{
    public class AddTaskRequest
    {
        public string Header { get; set; } = null!;
        public string Description { get; set; } = null!;
        public DateTimeOffset? Deadline { get; set; }
    }
}