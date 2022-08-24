using Camp.Common.Exceptions;
using Camp.Common.Wrappers;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using System.Net;
using WebException = Camp.Common.Exceptions.WebException;

namespace Camp.WebApi.Filters
{
    public class ExceptionFilter : ExceptionFilterAttribute, IExceptionFilter
    {
        private readonly ILogger<ExceptionFilter> _logger;

        public ExceptionFilter(ILogger<ExceptionFilter> logger)
        {
            _logger = logger;
        }

        public override void OnException(ExceptionContext context)
        {
            var response = GetResponse(context.Exception);

            context.HttpContext.Response.StatusCode = response.StatusCode;
            context.Result = new JsonResult(response);
            Log(response);

            base.OnException(context);
        }

        private Response GetResponse(Exception ex)
        {
            if (ex is WebException wx)
            {
                return ex switch
                {
                    NotFoundException => CreateClientErrorResponse(wx.Message, wx.MessageCode, HttpStatusCode.NotFound),
                    ValidateException => CreateClientErrorResponse(wx.Message, wx.MessageCode, HttpStatusCode.BadRequest),
                    UnauthorizedException => CreateClientErrorResponse(wx.Message, wx.MessageCode, HttpStatusCode.Unauthorized),
                    ForbiddenException => CreateClientErrorResponse(wx.Message, wx.MessageCode, HttpStatusCode.Forbidden)
                };
            }

            return CreateServerErrorResponse(ex.Message, ex.StackTrace, HttpStatusCode.InternalServerError);
        }

        private ClientErrorResponse CreateClientErrorResponse(string message, 
                                                              string messageCode, 
                                                              HttpStatusCode code) =>
             new ClientErrorResponse
             {
                 StatusCode = (int)code,
                 Message = message,
                 MessageCode = messageCode
             };

        private ServerErrorResponse CreateServerErrorResponse(string message, string stackTrace, HttpStatusCode code) =>
             new ServerErrorResponse
             {
                 StatusCode = (int)code,
                 Message = message,
                 StackTrace = stackTrace
             };

        private void Log(Response response)
        {
            if (response.StatusCode == 500)
                _logger.LogError($"{response.StatusCode}, {response.Message}");
            else
                _logger.LogWarning($"{response.StatusCode}, {response.Message}");
        }
    }
}
