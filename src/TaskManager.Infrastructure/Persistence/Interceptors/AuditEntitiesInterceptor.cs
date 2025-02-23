using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Diagnostics;
using System.IdentityModel.Tokens.Jwt;
using TaskManager.Domain.Common;

namespace TaskManager.Infrastructure.Persistence.Interceptors
{
    internal class AuditEntitiesInterceptor(IHttpContextAccessor httpContextAccessor) : SaveChangesInterceptor
    {
        public override ValueTask<InterceptionResult<int>> SavingChangesAsync(
            DbContextEventData eventData, 
            InterceptionResult<int> result, 
            CancellationToken cancellationToken = default)
        {
            var modifiedBy = "%SYSTEM%";
            var httpContext = httpContextAccessor.HttpContext;
            if (httpContext is not null)
            {
                var userNameClaim = httpContext.User.FindFirst(JwtRegisteredClaimNames.Name);
                modifiedBy = userNameClaim is null ? null : userNameClaim.Value;
            }

            var dbContext = eventData.Context;
            if (dbContext is null)
            {
                return base.SavingChangesAsync(eventData, result, cancellationToken);
            }

            var entries = dbContext.ChangeTracker.Entries<IAuditable>();

            foreach(var entry in entries)
            {
                if (entry.State == EntityState.Added)
                {
                    entry.Property(a => a.CreatedBy).CurrentValue = modifiedBy;
                    entry.Property(a => a.CreatedOnUtc).CurrentValue = DateTimeOffset.UtcNow;
                }

                if (entry.State == EntityState.Modified)
                {
                    entry.Property(a => a.LastModifiedBy).CurrentValue = modifiedBy;
                    entry.Property(a => a.LastModifiedOnUtc).CurrentValue = DateTimeOffset.UtcNow;
                }
            }

            return base.SavingChangesAsync(eventData, result, cancellationToken);
        }
    }
}