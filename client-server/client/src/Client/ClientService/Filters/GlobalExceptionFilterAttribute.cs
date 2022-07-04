using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc.Filters;

namespace ClientService.Filters
{
    public class GlobalExceptionFilterAttribute : ExceptionFilterAttribute
    {
        public GlobalExceptionFilterAttribute()
        {
        }

        public override void OnException(ExceptionContext context)
        {
            base.OnException(context);
        }
    }
}
