using System;
using System.Linq;
using FTS.Precatorio.Domain.Notifications;
using FTS.Precatorio.Dto;
using Microsoft.AspNetCore.Mvc;

namespace FTS.Precatorio.Api.Controllers
{
    public class BaseController : Controller
    {
        protected readonly IDomainNotification _notifications;

        public BaseController(IDomainNotification notifications)
        {
            _notifications = notifications;
        }

        protected new IActionResult Response(object result = null)
        {
            return Response<object>(result);
        }

        protected new IActionResult Response<T>(T result)
        {
            if (isValid())
            {
                return Ok(new ReturnContentJson<T>(true, result));
            }

            T instance = Activator.CreateInstance<T>();

            return BadRequest(new ReturnContentJson<T>(false, instance, _notifications.GetNotifications()));
        }

        protected bool isValid()
        {
            return (!_notifications.HasNotifications());
        }

        protected void NotificateErrorInvalidModel()
        {
            var erros = ModelState.Values.SelectMany(v => v.Errors);

            foreach (var erro in erros)
            {
                var erroMsg = erro.Exception == null ? erro.ErrorMessage : erro.Exception.Message;

                _notifications.AddNotification(erroMsg);
            }
        }
    }
}