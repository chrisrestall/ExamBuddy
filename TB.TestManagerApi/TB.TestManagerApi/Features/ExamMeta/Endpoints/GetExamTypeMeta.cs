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

namespace TB.TestManagerApi.Features.ExamMeta.Endpoints
{
    [Route(Routes.ExamMetaTypeUri)]
    public class GetExamTypeMeta : BaseAsyncEndpoint
    {

        private readonly IExamTypeMetaQueryService _examService;
        private readonly ILogger<GetExamTypeMeta> _logger;

        public GetExamTypeMeta(ILogger<GetExamTypeMeta> logger, IExamTypeMetaQueryService examService)
        {
            _logger = logger;
            _examService = examService;
        }

        [HttpGet("{id}/{withSections}/{activeOnly}")]
        [ProducesResponseType(typeof(ExamTypeMetaDto), StatusCodes.Status200OK)]
        [ProducesErrorResponseType(typeof(NotFoundResult))]
        [SwaggerOperation(
            Summary = "Retrieve an ExamTypeMeta",
            Description = "Retrieves an ExamTypeMeta given a valid ID",
            OperationId = nameof(GetExamTypeMeta),
            Tags = new[] { nameof(GetExamTypeMeta) }
        )]
        public async Task<ActionResult<ExamTypeMetaDto>> HandleAsync(Guid id, bool withSections, bool activeOnly, CancellationToken cancellationToken = default)
        {
            try
            {
                return Ok(await _examService.GetExamTypeMetaByIdAsync(id, withSections, activeOnly));
            }
            catch (Exception ex)
            {
                _logger.LogError(ex.ToString());
                return NotFound();
            }
        }
    }
}
