﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Sirena.Books.Api.CustomAuth;
using Sirena.Books.Api.Models;
using Sirena.Books.Domain.Interfaces;

namespace Sirena.Books.Api.Controllers
{
    [Route("api/v1/manage")]
    [ApiController]
    [Authorize(Policy = AuthConstants.ADMIN)]
    public class ManageController : ApiControllerBase
    {
        private readonly IBooksService _service;

        public ManageController(IBooksService service, ILogger<ManageController> logger)
            : base(logger)
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
                    return await GetBadRequestResult(ModelState, "api/v1/manage/add");

                await _service.AddAsync(model.ToEntity(), cancellationToken);
                return Ok();
            }
            catch (Exception ex)
            {
                return await GetServerErrorResult(ex, "api/v1/manage/add");
            }
        }
    }
}