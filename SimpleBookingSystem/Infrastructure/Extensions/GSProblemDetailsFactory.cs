using Microsoft.AspNetCore.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Infrastructure;
using Microsoft.AspNetCore.Mvc.ModelBinding;
using Microsoft.Extensions.Options;
using System.Diagnostics;
using System.Net;
using System.Text.Json;

namespace SimpleBookingSystem.Infrastructure.Extensions
{
    /// <summary>
    /// Create a custom ProblemDetailsFactory based on Microsoft's DefaultProblemDeatilsFactory
    /// Make sure to cover the handling of the ApiException
    /// https://github.com/aspnet/AspNetCore/blob/2e4274cb67c049055e321c18cc9e64562da52dcf/src/Mvc/Mvc.Core/src/Infrastructure/DefaultProblemDetailsFactory.cs
    /// </summary>
    public class GSProblemDetailsFactory : ProblemDetailsFactory
    {
        private readonly ApiBehaviorOptions _options;
        public GSProblemDetailsFactory(IOptions<ApiBehaviorOptions> options)
        {
            _options = options?.Value ?? throw new ArgumentNullException(nameof(options));
        }

        public override ProblemDetails CreateProblemDetails(
            HttpContext httpContext,
            int? statusCode = null,
            string title = null,
            string type = null,
            string detail = null,
            string instance = null)
        {
            statusCode ??= 500;

            var context = httpContext.Features.Get<IExceptionHandlerFeature>();

            ProblemDetails problemDetails = new ProblemDetails
            {
                Status = statusCode,
                Title = title,
                Type = type,
                Detail = detail,
                Instance = instance,
            };

            ApplyProblemDetailsDefaults(httpContext, problemDetails, statusCode.Value);

            if (context?.Error != null)
            {
                if (context.Error is ApiException apiException)
                {
                    statusCode = (int)apiException.StatusCode;
                    //The result serializer doesn't use the status from the ProblemDetails object so we should set it manually.
                    httpContext.Response.StatusCode = statusCode.Value;
                    problemDetails.Status = statusCode;
                    if (!string.IsNullOrEmpty(apiException.ErrorCode))
                    {
                        problemDetails.Extensions.Add("errorcode", apiException.ErrorCode);
                    }
                    if (!string.IsNullOrEmpty(apiException.ErrorCode))
                    {
                        problemDetails.Extensions.Add("errormessage", apiException.ErrorCode);
                    }
                }
            }
            return problemDetails;
        }

        public override ValidationProblemDetails CreateValidationProblemDetails(
            HttpContext httpContext,
            ModelStateDictionary modelStateDictionary,
            int? statusCode = null,
            string title = null,
            string type = null,
            string detail = null,
            string instance = null)
        {
            if (modelStateDictionary == null)
            {
                throw new ArgumentNullException(nameof(modelStateDictionary));
            }

            statusCode ??= 400;

            var problemDetails = new ValidationProblemDetails(modelStateDictionary)
            {
                Status = statusCode,
                Type = type,
                Detail = detail,
                Instance = instance,
            };

            if (title != null)
            {
                // For validation problem details, don't overwrite the default title with null.
                problemDetails.Title = title;
            }

            ApplyProblemDetailsDefaults(httpContext, problemDetails, statusCode.Value);

            return problemDetails;
        }

        private void ApplyProblemDetailsDefaults(HttpContext httpContext, ProblemDetails problemDetails, int statusCode)
        {
            problemDetails.Status = problemDetails.Status ?? statusCode;

            if (_options.ClientErrorMapping.TryGetValue(statusCode, out var clientErrorData))
            {
                problemDetails.Title ??= clientErrorData.Title;
                problemDetails.Type ??= clientErrorData.Link;
            }

            //Add traceIdentifier as extension for all responses
            var traceId = Activity.Current?.Id ?? httpContext?.TraceIdentifier;
            if (traceId != null)
            {
                problemDetails.Extensions["traceId"] = traceId;
            }

        }
    }
}