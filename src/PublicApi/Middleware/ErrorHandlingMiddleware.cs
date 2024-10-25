using MediatR;
using ProtocolReception.Api.Middleware;

namespace ProtocolReception.PublicApi.Middleware
{
    public class ErrorHandlingMiddleware
    {
        private readonly RequestDelegate _next;
        private readonly IMediator _mediator;

        public ErrorHandlingMiddleware(RequestDelegate next, IMediator mediator)
        {
            _next = next;
            _mediator = mediator;
        }

        public async Task InvokeAsync(HttpContext context)
        {
            try
            {
                await _next(context);
            }
            catch (Exception ex)
            {
                await _mediator.Send(new LogErrorCommand { ErrorMessage = ex.Message, TimeStamp = DateTime.Now });
                context.Response.StatusCode = 500;
                await context.Response.WriteAsync("An unexpected error occurred. Please try again later.");
            }
        }
    }
}
