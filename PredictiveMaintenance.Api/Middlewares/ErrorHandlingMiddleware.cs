using Microsoft.AspNetCore.Http;
using PredictiveMaintenance.Application.DTOs.Common;
using System.Net;
using System.Text.Json;

namespace PredictiveMaintenance.Api.Middlewares
{
    public class ErrorHandlingMiddleware
    {
        private readonly RequestDelegate _next;

        // El constructor recibe el "siguiente paso" en la cadena de middlewares
        public ErrorHandlingMiddleware(RequestDelegate next)
        {
            _next = next;
        }

        // Este método se ejecuta para cada request que entra a la API
        public async Task Invoke(HttpContext context)
        {
            try
            {
                // Si no hay error, continúa con el siguiente middleware o controlador
                await _next(context);
            }
            catch (ArgumentException ex)
            {
                await HandleExceptionAsync(context, HttpStatusCode.BadRequest, "ERROR_DE_VALIDACION", ex.Message);
            }
            catch (KeyNotFoundException ex)
            {
                await HandleExceptionAsync(context, HttpStatusCode.NotFound, "NO_ENCONTRADO", ex.Message);
            }
            catch (UnauthorizedAccessException ex)
            {
                await HandleExceptionAsync(context, HttpStatusCode.Unauthorized, "NO_AUTORIZADO", ex.Message);
            }
            catch (Exception ex)
            {
                await HandleExceptionAsync(context, HttpStatusCode.InternalServerError, "ERROR_DE_SERVIDOR", ex.Message);
            }
        }

        private static async Task HandleExceptionAsync(HttpContext context, HttpStatusCode statusCode, string errorCode, string message)
        {
            context.Response.ContentType = "application/json";
            context.Response.StatusCode = (int)statusCode;

            var errorResponse = new ErrorResponseDto
            {
                ErrorCode = errorCode,
                Message = message,
                Details = null 
            };

            var json = JsonSerializer.Serialize(errorResponse);
            await context.Response.WriteAsync(json);
        }
    }
}