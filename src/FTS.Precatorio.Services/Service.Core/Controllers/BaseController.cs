using System;
using FTS.Precatorio.Application.Core;
using Microsoft.AspNetCore.Mvc;

namespace Service.Core.Controllers
{
    public class BaseController : Controller
    {
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
            return BadRequest(new ReturnContentJson<T>(false, instance, ""));
        }

        protected bool isValid()
        {
            return true;
        }
    }
}