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
        /// <summary>
        /// Возврат ошибки валидации по RFC8707
        /// </summary>
        /// <param name="errors"></param>
        /// <param name="route"></param>
        /// <returns></returns>
        protected async Task<IActionResult> GetBadRequestResult(
            ModelStateDictionary errors, string route)
        {
            return Ok(new ProblemDetails
            {
                Type = "ClientError",
                Detail = errors.ToString(),
                Instance = route,
                Title = "Ошибка в заполнении полей",
                Status = StatusCodes.Status400BadRequest
            });
        }

        /// <summary>
        /// Возврат серверной ошибки по RFC8707
        /// </summary>
        /// <param name="errors"></param>
        /// <param name="route"></param>
        /// <returns></returns>
        protected async Task<IActionResult> GetServerErrorResult(
            Exception error, string route)
        {
            _logger.LogError("Ошибка сервера", error);

            //Возвращаем 200 чтобы фронт не раскрашивался красным
            return Ok(new ProblemDetails
            {
                Type = "ServerError",
                Detail = "Неизвестная ошибка, обратитесь в службу поддержки",
                Instance = route,
                Title = "Ошибка",
                Status = StatusCodes.Status500InternalServerError
            });
        }
    }
}