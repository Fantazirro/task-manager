using TaskManager.Domain.Common;
using TaskManager.Domain.Enums;

namespace TaskManager.Domain.Entities
{
    public class UserTask : BaseEntity
    {
        public Guid UserId { get; set; }
        public string Header { get; set; } = null!;
        public string Description { get; set; } = null!;
        public UserTaskStatus Status { get; set; }
        public DateTimeOffset? Deadline { get; set; }
        public DateTimeOffset CompletedDate { get; set; }

        public UserTask()
        {
            Status = UserTaskStatus.InProgress;
        }
    }
}