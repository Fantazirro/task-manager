namespace TaskManager.Application.Dtos.UserTask
{
    public class AddUserTaskDto
    {
        public string Header { get; set; } = null!;
        public string Description { get; set; } = null!;
        public DateTimeOffset? Deadline { get; set; }
    }
}