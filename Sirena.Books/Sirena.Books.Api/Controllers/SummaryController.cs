﻿using System;
using System.Linq;
using System.Security.Claims;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Sirena.Books.Api.CustomAuth;
using Sirena.Books.Api.Models;
using Sirena.Books.Domain.Interfaces;

namespace Sirena.Books.Api.Controllers
{
    [Route("api/v1/summary")]
    [ApiController]
    [Authorize(Policy = AuthConstants.ADMIN)]
    public class SummaryController : ApiControllerBase
    {
        private readonly ISummaryService _summaryService;

        public SummaryController(ISummaryService service,
            ILogger<SummaryController> logger) :
            base(logger)
        {
            _summaryService = service ?? throw new ArgumentNullException(nameof(service));
        }

        [HttpGet("by-time")]
        [Produces("application/json")]
        public async Task<IActionResult> GetByTime(
            [FromQuery]DateTime? start, [FromQuery]DateTime? end,
            CancellationToken cancellationToken)
        {
            try
            {
                return Ok((await _summaryService.GetSoldByTimesAsync(start, end, cancellationToken))
                    .Select(StatByTimeResultModel.FromEnity));
            }
            catch (Exception ex)
            {
                return await GetServerErrorResult(ex, "api/v1/summary/by-time");
            }
        }

        [HttpGet("by-types")]
        [Produces("application/json")]
        public async Task<IActionResult> GetByTypes(
            [FromQuery]DateTime? start, [FromQuery]DateTime? end,
            CancellationToken cancellationToken)
        {
            try
            {
                return Ok((await _summaryService.GetSoldByTypesAsync(start, end, cancellationToken))
                    .Select(StatByTypesResultModel.FromEnity));
            }
            catch (Exception ex)
            {
                return BadRequest("123");
            }
        }
    }
}