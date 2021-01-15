using Ardalis.ApiEndpoints;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Swashbuckle.AspNetCore.Annotations;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using TB.TestManagerApi.Domain;
using TB.TestManagerApi.Services;

namespace TB.TestManagerApi.Features.ExamMaster.Endpoints
{

    [Route(Routes.ExamMasterUri)]
    public class GetExamMasters : BaseAsyncEndpoint
    {
        private readonly IExamMasterQueryService _examService;
        private readonly ILogger<GetExamMasters> _logger;

        public GetExamMasters(ILogger<GetExamMasters> logger, IExamMasterQueryService examService)
        {
            _logger = logger;
            _examService = examService;
        }

        [HttpGet("{activeOnly}")]
        [ProducesResponseType(typeof(List<ExamMasterDto>), StatusCodes.Status200OK)]
        [ProducesErrorResponseType(typeof(NotFoundResult))]
        [SwaggerOperation(
            Summary = "Retrieves ExamMasters",
            Description = "Retrieves a list of ExamMasters ",
            OperationId = nameof(GetExamMasters),
            Tags = new[] { nameof(GetExamMasters) }
        )]
        public async Task<ActionResult<List<ExamMasterDto>>> HandleAsync(bool activeOnly = true, CancellationToken cancellationToken = default)
        {
            try
            {
                return Ok(await _examService.GetExamMastersAsync(activeOnly));
            }
            catch (Exception ex)
            {
                _logger.LogError(ex.ToString());
                return NotFound();
            }
        }
    }
}

