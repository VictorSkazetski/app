using api.Error.Exceptions;
using api.Error.Model;
using Microsoft.AspNetCore.Http;
using Newtonsoft.Json;

namespace api.Error
{
    public class ExceptionMiddleware : IMiddleware
    {
        public async Task InvokeAsync(HttpContext context, RequestDelegate next)
        {
            
            try
            {
                await next(context);
            }
            catch(ConflictException exception)
            {
                await CreateResponse(context, exception);
            }
            catch (VerifyUserEmailException exception)
            {
                await CreateResponse(context, exception);
            }
            catch (UnauthorizedException exception)
            {
                await CreateResponse(context, exception);
            }
            catch (BadRequestException exception)
            {
                await CreateResponse(context, exception);
            }
        }

        private bool IsResponseStarte(HttpResponse response)
            => response.HasStarted;

        private CustomHttpErrorResponse CreateCustomHttpErrorResponse(CustomException exception)
        {
            return new CustomHttpErrorResponse
            {
                Message = exception.Message,
                Status = (int)exception.Status,
            };
        }

        private async Task CreateResponse(HttpContext context, CustomException exception)
        {
            var response = context.Response;
            response.ContentType = "application/json";
            var errorResult = this.CreateCustomHttpErrorResponse(exception);
            if (!this.IsResponseStarte(response))
            {
                response.StatusCode = errorResult.Status;
                await response.WriteAsync(JsonConvert.SerializeObject(errorResult));
            }
        }
    }
}
