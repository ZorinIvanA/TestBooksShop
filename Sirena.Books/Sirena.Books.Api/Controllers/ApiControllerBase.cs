using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.ModelBinding;
using Microsoft.Extensions.Logging;

namespace Sirena.Books.Api.Controllers
{
    public abstract class ApiControllerBase : ControllerBase
    {
        protected ILogger _logger;

        protected ApiControllerBase(ILogger logger)
        {
            _logger = logger ?? throw new ArgumentNullException(nameof(logger));
        }

        protected async Task<IActionResult> GetBadRequestResult(
            ModelStateDictionary errors, string route)
        {
            return Ok(new ProblemDetails
            {
                Type = "Client Error",
                Detail = errors.ToString(),
                Instance = route,
                Title = "Ошибка в заполнении полей",
                Status = StatusCodes.Status400BadRequest
            });
        }

        protected async Task<IActionResult> GetServerErrorResult(
            Exception error, string route)
        {
            _logger.LogError(error.Message.ToString(), null);

            return Ok(new ProblemDetails
            {
                Type = "Server Error",
                Detail = "Неизвестная ошибка, обратитесь в службу поддержки",
                Instance = route,
                Title = "Ошибка",
                Status = StatusCodes.Status500InternalServerError
            });
        }
    }
}