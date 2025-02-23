namespace TaskManager.Domain.Exceptions
{
    public class NotFoundException : Exception
    {
        public NotFoundException(Guid userId, Guid entityId, Type entityType)
            : base($"[User: {userId}] Entity {entityType} with Id = {entityId} not found") { }
    }
}