using Ardalis.ApiEndpoints;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Swashbuckle.AspNetCore.Annotations;
using System;
using System.Net;
using System.Threading;
using System.Threading.Tasks;
using TB.TestManagerApi.Domain;
using TB.TestManagerApi.Services;

namespace TB.TestManagerApi.Features.ExamQuestion.Endpoints
{
    [Route(Routes.ExamQuestionExamAnswerUri)]
    public class DeactivateAnswerMaster : BaseAsyncEndpoint<DeactivateAnswerMasterDto, Guid>
    {

        private readonly IExamQuestionCommandService _examService;
        private readonly ILogger<DeactivateAnswerMaster> _logger;

        public DeactivateAnswerMaster(ILogger<DeactivateAnswerMaster> logger, IExamQuestionCommandService examService)
        {
            _logger = logger;
            _examService = examService;
        }

        [HttpPatch]
        [ProducesResponseType(typeof(Guid), StatusCodes.Status200OK)]
        [SwaggerResponse((int)HttpStatusCode.OK)]
        [SwaggerResponse((int)HttpStatusCode.BadRequest)]
        [SwaggerResponse((int)HttpStatusCode.InternalServerError)]
        [SwaggerOperation(
            Summary = "Deactivates an ExamAnswerMaster",
            Description = "Deactivates an ExamAnswerMaster in the database using Dapper",
            OperationId = nameof(DeactivateAnswerMaster),
            Tags = new[] { nameof(DeactivateAnswerMaster) }
        )]
        public override async Task<ActionResult<Guid>> HandleAsync([FromBody] DeactivateAnswerMasterDto deactivateAnswerMasterDto, CancellationToken cancellationToken = default)
        {
            try
            {
                if (deactivateAnswerMasterDto.Id == default)
                {
                    _logger.LogWarning("deactivateQuestionMasterDto using default Id");
                    return BadRequest();
                }

                return Ok(await _examService.DeactivateAnswerMasterAsync(deactivateAnswerMasterDto));
            }
            catch (Exception ex)
            {
                _logger.LogError(ex.ToString());
                return StatusCode(500, ex.Message);
            }
        }
    }
}
