using System.Collections.Generic;
using FluentValidation.Results;

namespace FTS.Precatorio.Domain.Interfaces
{
    public interface IDomainNotification
    {
        bool HasNotifications();
        List<string> GetNotifications();
        void NotifyError(ValidationResult validationResult);
        void AddNotification(string notification);
    }
}