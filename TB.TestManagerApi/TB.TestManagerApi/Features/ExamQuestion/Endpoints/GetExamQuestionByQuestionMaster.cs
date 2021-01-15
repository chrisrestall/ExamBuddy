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

    [Route(Routes.ExamQuestionMasterUri)]
    public class GetExamQuestionByQuestionMaster : BaseAsyncEndpoint
    {
        private readonly IExamQuestionQueryService _examService;
        private readonly ILogger<GetExamQuestionByQuestionMaster> _logger;

        public GetExamQuestionByQuestionMaster(ILogger<GetExamQuestionByQuestionMaster> logger, IExamQuestionQueryService examService)
        {
            _logger = logger;
            _examService = examService;
        }

        [HttpGet("{examQuestionMasterId}/{activeOnly}")]
        [ProducesResponseType(typeof(ExamQuestionsDto), StatusCodes.Status200OK)]
        [ProducesErrorResponseType(typeof(NotFoundResult))]
        [SwaggerOperation(
            Summary = "Retrieve ExamQuestion",
            Description = "Retrieves an ExamQuestion by Question Master ID",
            OperationId = nameof(GetExamQuestionByQuestionMaster),
            Tags = new[] { nameof(GetExamQuestionByQuestionMaster) }
        )]
        public async Task<ActionResult<ExamMasterDto>> HandleAsync(Guid examQuestionMasterId, bool activeOnly = true, CancellationToken cancellationToken = default)
        {
            try
            {
                return Ok(await _examService.GetExamQuestionByQuestionMasterIdAsync(examQuestionMasterId, activeOnly));
            }
            catch (Exception ex)
            {
                _logger.LogError(ex.ToString());
                return NotFound();
            }
        }
    }
}

