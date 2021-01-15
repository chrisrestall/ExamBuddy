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
    public class GetExamTypeMetas  : BaseAsyncEndpoint
    {
        private readonly IExamTypeMetaQueryService _examService;
        private readonly ILogger<GetExamTypeMetas> _logger;

        public GetExamTypeMetas(ILogger<GetExamTypeMetas> logger, IExamTypeMetaQueryService examService)
        {
            _logger = logger;
            _examService = examService;
        }

        [HttpGet("{page}/{pageSize}/{withSections}/{activeOnly}")]
        [ProducesResponseType(typeof(List<ExamTypeMetaDto>), StatusCodes.Status200OK)]
        [ProducesErrorResponseType(typeof(NotFoundResult))]
        [SwaggerOperation(
            Summary = "Retrieve ExamTypeMetas",
            Description = "Retrieves a list of ExamTypeMetas",
            OperationId = nameof(GetExamTypeMeta),
            Tags = new[] { nameof(GetExamTypeMeta) }
        )]
        public async Task<ActionResult<ExamTypeMetaDto>> HandleAsync(int page, int pageSize, bool withSections = true, bool activeOnly = true, CancellationToken cancellationToken = default)
        {
            try
            {
                return Ok(await _examService.GetExamTypeMetasAsync(page, pageSize, withSections, activeOnly));
            }
            catch (Exception ex)
            {
                _logger.LogError(ex.ToString());
                return NotFound();
            }
        }
    }
}

