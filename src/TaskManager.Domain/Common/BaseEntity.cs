namespace TaskManager.Domain.Common
{
    public abstract class BaseEntity : IAuditable
    {
        public Guid Id { get; set; }
        public string? CreatedBy { get; set; } = null!;
        public DateTimeOffset CreatedOnUtc { get; set; }
        public string? LastModifiedBy { get; set; } = null!;
        public DateTimeOffset LastModifiedOnUtc { get; set; }
    }
}