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

namespace TB.TestManagerApi.Features.ExamMaster.Endpoints
{
    [Route(Routes.ExamMasterUri)]
    public class DeactivateExamMaster : BaseAsyncEndpoint<DeactivateExamMasterDto, Guid>
    {

        private readonly IExamMasterCommandService _examService;
        private readonly ILogger<DeactivateExamMaster> _logger;

        public DeactivateExamMaster(ILogger<DeactivateExamMaster> logger, IExamMasterCommandService examService)
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
            Summary = "Deactivates an ExamMaster",
            Description = "Deactivates an ExamMaster in the database using Dapper",
            OperationId = nameof(DeactivateExamMaster),
            Tags = new[] { nameof(DeactivateExamMaster) }
        )]
        public override async Task<ActionResult<Guid>> HandleAsync([FromBody] DeactivateExamMasterDto deactivateExamMasterDto, CancellationToken cancellationToken = default)
        {
            try
            {
                if (deactivateExamMasterDto.Id == default)
                {
                    _logger.LogWarning("deactivateExamMasterDto using default Id");
                    return BadRequest();
                }

                return Ok(await _examService.DeactivateExamTypeMetaAsync(deactivateExamMasterDto));
            }
            catch (Exception ex)
            {
                _logger.LogError(ex.ToString());
                return StatusCode(500, ex.Message);
            }
        }
    }
}
