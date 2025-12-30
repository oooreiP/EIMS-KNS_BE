using EIMS.Domain.Entities;
using Microsoft.EntityFrameworkCore.Diagnostics;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Text.Json;
using EIMS.Application.Commons.Interfaces;
using Microsoft.AspNetCore.Http;

namespace EIMS.Infrastructure.Interceptors
{
    public class AuditableEntityInterceptor : SaveChangesInterceptor
    {
        private readonly ICurrentUserService _currentUserService;
        private readonly IHttpContextAccessor _httpContextAccessor;

        public AuditableEntityInterceptor(ICurrentUserService currentUserService, IHttpContextAccessor httpContextAccessor)
        {
            _currentUserService = currentUserService;
            _httpContextAccessor = httpContextAccessor;
        }

        public override InterceptionResult<int> SavingChanges(DbContextEventData eventData, InterceptionResult<int> result)
        {
            UpdateAuditFields(eventData.Context);
            return base.SavingChanges(eventData, result);
        }

        public override ValueTask<InterceptionResult<int>> SavingChangesAsync(DbContextEventData eventData, InterceptionResult<int> result, CancellationToken cancellationToken = default)
        {
            UpdateAuditFields(eventData.Context);
            return base.SavingChangesAsync(eventData, result, cancellationToken);
        }

        private void UpdateAuditFields(DbContext context)
        {
            if (context == null) return;
            var userId = _currentUserService.UserId;
            var traceId = _httpContextAccessor.HttpContext?.TraceIdentifier;
            var auditEntries = new List<AuditLog>();
            var entries = context.ChangeTracker.Entries()
            .Where(e => !(e.Entity is AuditLog) && !(e.Entity is SystemActivityLog) && !(e.Entity is RefreshToken)) // Bỏ qua bảng Log
            .ToList(); 
            foreach (var entry in entries)
            {
                if (entry.State == EntityState.Detached || entry.State == EntityState.Unchanged)
                    continue;

                var auditEntry = new AuditLog
                {
                    TableName = entry.Entity.GetType().Name,
                    UserId = userId,
                    Timestamp = DateTime.UtcNow,
                    Action = entry.State.ToString(), // "Added", "Modified", "Deleted"
                    TraceId = traceId ?? Guid.NewGuid().ToString()
                };

                var oldValues = new Dictionary<string, object>();
                var newValues = new Dictionary<string, object>();

                foreach (var property in entry.Properties)
                {
                    string propertyName = property.Metadata.Name;
                    if (property.IsTemporary) continue;

                    // Lấy Primary Key
                    if (property.Metadata.IsPrimaryKey())
                    {
                        auditEntry.RecordId = property.CurrentValue?.ToString();
                    }

                    switch (entry.State)
                    {
                        case EntityState.Added:
                            newValues[propertyName] = property.CurrentValue;
                            break;
                        case EntityState.Deleted:
                            oldValues[propertyName] = property.OriginalValue;
                            break;
                        case EntityState.Modified:
                            if (property.IsModified)
                            {
                                oldValues[propertyName] = property.OriginalValue;
                                newValues[propertyName] = property.CurrentValue;
                            }
                            break;
                    }
                }
                auditEntry.OldValues = oldValues.Count == 0 ? null : JsonSerializer.Serialize(oldValues);
                auditEntry.NewValues = newValues.Count == 0 ? null : JsonSerializer.Serialize(newValues);
                context.Set<AuditLog>().Add(auditEntry);
            }
        }
    }
}
