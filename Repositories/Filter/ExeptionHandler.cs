using CSharpFunctionalExtensions;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using MyNoteApi.Repositories.Services;

namespace MyNoteApi.Repositories.Filter;

public class ExeptionHandler : IAsyncExceptionFilter
{
    private readonly ILogger<ExeptionHandler> _logger;

    public ExeptionHandler(ILogger<ExeptionHandler> logger) => _logger = logger;

    public Task OnExceptionAsync(ExceptionContext context)
    {
        _logger.LogError(context.Exception, "This Error Ocurr On : " + context.HttpContext.Request.Path);
        context.ExceptionHandled = true;
        context.Result = new JsonResult(Result.Failure("An Internal Error Ocurr , Please Contact Developer !").ToResult())
        {
            StatusCode = 500
        };
        return Task.CompletedTask;
    }
}
