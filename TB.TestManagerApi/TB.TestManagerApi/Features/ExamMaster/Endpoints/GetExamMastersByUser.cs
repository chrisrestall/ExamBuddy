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

    [Route(Routes.ExamMasterUserUri)]
    public class GetExamMasterByUser : BaseAsyncEndpoint
    {
        private readonly IExamMasterQueryService _examService;
        private readonly ILogger<GetExamMasterByUser> _logger;

        public GetExamMasterByUser(ILogger<GetExamMasterByUser> logger, IExamMasterQueryService examService)
        {
            _logger = logger;
            _examService = examService;
        }

        [HttpGet("{userId}/{activeOnly}")]
        [ProducesResponseType(typeof(List<ExamMasterDto>), StatusCodes.Status200OK)]
        [ProducesErrorResponseType(typeof(NotFoundResult))]
        [SwaggerOperation(
            Summary = "Retrieves ExamMasters",
            Description = "Retrieves a list of ExamMasters by UserID",
            OperationId = nameof(GetExamMasterByUser),
            Tags = new[] { nameof(GetExamMasterByUser) }
        )]
        public async Task<ActionResult<List<ExamMasterDto>>> HandleAsync(string userId, bool activeOnly = true, CancellationToken cancellationToken = default)
        {
            try
            {
                return Ok(await _examService.GetExamMastersByUserIdAsync(userId, activeOnly));
            }
            catch (Exception ex)
            {
                _logger.LogError(ex.ToString());
                return NotFound();
            }
        }
    }
}

   