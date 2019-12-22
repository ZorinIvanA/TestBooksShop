using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Sirena.Books.Api.Models;
using Sirena.Books.Domain.Interfaces;

namespace Sirena.Books.Api.Controllers
{
    [Route("api/v1/books")]
    [ApiController]
    public class BooksController : ApiControllerBase
    {
        private readonly IBooksService _booksService;
        public BooksController(IBooksService service,
            ILogger<BooksController> logger) :
            base(logger)
        {
            _booksService = service ?? throw new ArgumentNullException(nameof(service));
        }

        [HttpPost]
        public async Task<IActionResult> Get([FromBody]FilterModel model, CancellationToken cancellationToken)
        {
            try
            {
                if (model == null || !ModelState.IsValid)
                    return await GetBadRequestResult(ModelState, "api/v1/books");

                return Ok((await _booksService.GetByParamsAsync(
                    model.Exists, model.Types, model.MinCost, model.MaxCost,
                    model.Author, model.Name, cancellationToken))
                    .Select(BookModel.FromEntity).ToArray());
            }
            catch (Exception ex)
            {
                return await GetServerErrorResult(ex, "api/v1/books");
            }
        }

        [HttpPost("buy/{id}")]
        public async Task<IActionResult> Post([FromRoute]int id, CancellationToken cancellationToken)
        {
            try
            {
                await _booksService.BuyAsync(id, cancellationToken);
                return Ok();
            }
            catch (Exception ex)
            {
                return await GetServerErrorResult(ex, "api/v1/books/buy");
            }
        }
    }

}