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
    [Route(Routes.ExamQuestionExamQuestionUri)]
    public class DeactivateQuestionMaster : BaseAsyncEndpoint<DeactivateQuestionMasterDto, Guid>
    {

        private readonly IExamQuestionCommandService _examService;
        private readonly ILogger<DeactivateQuestionMaster> _logger;

        public DeactivateQuestionMaster(ILogger<DeactivateQuestionMaster> logger, IExamQuestionCommandService examService)
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
            Summary = "Deactivates an ExamQuestionMaster",
            Description = "Deactivates an ExamQuestionMaster in the database using Dapper",
            OperationId = nameof(DeactivateQuestionMaster),
            Tags = new[] { nameof(DeactivateQuestionMaster) }
        )]
        public override async Task<ActionResult<Guid>> HandleAsync([FromBody] DeactivateQuestionMasterDto deactivateQuestionMasterDto, CancellationToken cancellationToken = default)
        {
            try
            {
                if (deactivateQuestionMasterDto.Id == default)
                {
                    _logger.LogWarning("deactivateQuestionMasterDto using default Id");
                    return BadRequest();
                }

                return Ok(await _examService.DeactivateQuestionMasterAsync(deactivateQuestionMasterDto));
            }
            catch (Exception ex)
            {
                _logger.LogError(ex.ToString());
                return StatusCode(500, ex.Message);
            }
        }
    }
}
