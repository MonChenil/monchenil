using Microsoft.AspNetCore.Mvc.Filters;
using Microsoft.AspNetCore.Mvc;
using MonChenil.Domain.Reservations;

namespace MonChenil.Api.Filters;

public class ReservationStartDateExceptionFilter : IExceptionFilter
{
    public void OnException(ExceptionContext context)
    {
        if (context.Exception is ReservationStartDateException)
        {
            context.Result = new BadRequestObjectResult(context.Exception.Message);
            context.ExceptionHandled = true;
        }
    }
}