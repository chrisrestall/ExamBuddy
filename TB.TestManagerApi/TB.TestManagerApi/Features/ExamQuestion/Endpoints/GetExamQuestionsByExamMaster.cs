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

    [Route(Routes.ExamQuestionExamMasterUri)]
    public class GetExamQuestionsByExamMaster : BaseAsyncEndpoint
    {
        private readonly IExamQuestionQueryService _examService;
        private readonly ILogger<GetExamQuestionsByExamMaster> _logger;

        public GetExamQuestionsByExamMaster(ILogger<GetExamQuestionsByExamMaster> logger, IExamQuestionQueryService examService)
        {
            _logger = logger;
            _examService = examService;
        }

        [HttpGet("{examMasterId}/{activeOnly}")]
        [ProducesResponseType(typeof(List<ExamMasterDto>), StatusCodes.Status200OK)]
        [ProducesErrorResponseType(typeof(NotFoundResult))]
        [SwaggerOperation(
            Summary = "Retrieves ExamQuestions",
            Description = "Retrieves a list of ExamQuestions by ExamMaster ID",
            OperationId = nameof(GetExamQuestionsByExamMaster),
            Tags = new[] { nameof(GetExamQuestionsByExamMaster) }
        )]
        public async Task<ActionResult<List<ExamMasterDto>>> HandleAsync(Guid examMasterId, bool activeOnly = true, CancellationToken cancellationToken = default)
        {
            try
            {
                return Ok(await _examService.GetExamQuestionsByExamMasterIdAsync(examMasterId, activeOnly));
            }
            catch (Exception ex)
            {
                _logger.LogError(ex.ToString());
                return NotFound();
            }
        }
    }
}

