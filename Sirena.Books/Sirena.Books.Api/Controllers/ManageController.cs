using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Sirena.Books.Api.Models;
using Sirena.Books.Domain.Interfaces;

namespace Sirena.Books.Api.Controllers
{
    [Route("api/v1/manage")]
    [ApiController]
    public class ManageController : ControllerBase
    {
        private readonly IBooksService _service;

        public ManageController(IBooksService service)
        {
            _service = service ?? throw new ArgumentNullException(nameof(service));
        }

        [HttpPost("add")]
        [Consumes("application/json")]
        public async Task<IActionResult> Post([FromBody]BookModel model, CancellationToken cancellationToken)
        {
            try
            {
                if (!ModelState.IsValid)
                    return BadRequest(ModelState);

                await _service.AddAsync(model.ToEntity(), cancellationToken);
                return Ok();
            }
            catch (Exception ex)
            {
                return BadRequest("123");
            }
        }
    }
}