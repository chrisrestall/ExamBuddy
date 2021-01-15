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
    public class GetExamMasterById : BaseAsyncEndpoint
    {
        private readonly IExamMasterQueryService _examService;
        private readonly ILogger<GetExamMasterById> _logger;

        public GetExamMasterById(ILogger<GetExamMasterById> logger, IExamMasterQueryService examService)
        {
            _logger = logger;
            _examService = examService;
        }

        [HttpGet("{id}/{activeOnly}")]
        [ProducesResponseType(typeof(List<ExamMasterDto>), StatusCodes.Status200OK)]
        [ProducesErrorResponseType(typeof(NotFoundResult))]
        [SwaggerOperation(
            Summary = "Retrieves ExamMasters",
            Description = "Retrieves a list of ExamMasters by UserID",
            OperationId = nameof(GetExamMasterById),
            Tags = new[] { nameof(GetExamMasterById) }
        )]
        public async Task<ActionResult<ExamTypeMetaDto>> HandleAsync(Guid id, bool activeOnly = true, CancellationToken cancellationToken = default)
        {
            try
            {
                return Ok(await _examService.GetExamMasterByIdAsync(id, activeOnly));
            }
            catch (Exception ex)
            {
                _logger.LogError(ex.ToString());
                return NotFound();
            }
        }
    }
}

