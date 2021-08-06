using System;
using System.Collections.Generic;
using System.Linq;
using FluentValidation.Results;

namespace FTS.Precatorio.Domain.Notifications
{
    public class DomainNotification : IDomainNotification, IDisposable
    {
        private List<string> _notifications;

        public DomainNotification()
        {
            _notifications = new List<string>();
        }

        public List<string> GetNotifications()
        {
            return _notifications;
        }

        public void AddNotification(string notification)
        {
            _notifications.Add(notification);
        }

        public void NotifyError(ValidationResult validationResult)
        {
            foreach (var erro in validationResult.Errors)
            {
                Console.WriteLine(erro.ErrorMessage);
                AddNotification(erro.ErrorMessage);
            }
        }

        public bool HasNotifications()
        {
            return _notifications.Any();
        }

        public void Dispose()
        {
            _notifications = new List<string>();
        }
    }
}