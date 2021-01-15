using Ardalis.ApiEndpoints;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Swashbuckle.AspNetCore.Annotations;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Threading;
using System.Threading.Tasks;
using TB.TestManagerApi.Domain;
using TB.TestManagerApi.Services;

namespace TB.TestManagerApi.Features.ExamMeta.Endpoints
{
    [Route(Routes.ExamMetaSectionUri)]
    public class DeactivateExamTypeSection : BaseAsyncEndpoint<DeactivateExamTypeSectionDto, Guid>
    {

        private readonly IExamTypeMetaCommandService _examService;
        private readonly ILogger<DeactivateExamTypeSection> _logger;

        public DeactivateExamTypeSection(ILogger<DeactivateExamTypeSection> logger, IExamTypeMetaCommandService examService)
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
            Summary = "Deactivates an ExamTypeSection",
            Description = "Deactivates an ExamTypeSection in the database using Dapper",
            OperationId = nameof(DeactivateExamTypeSection),
            Tags = new[] { nameof(DeactivateExamTypeSection) }
        )]
        public override async Task<ActionResult<Guid>> HandleAsync([FromBody] DeactivateExamTypeSectionDto deactivateExamTypeSectionDto, CancellationToken cancellationToken = default)
        {
            try
            {
                if (deactivateExamTypeSectionDto.Id == default)
                {
                    _logger.LogWarning("deactivateExamTypeSectionDto using default Id");
                    return BadRequest();
                }

                return Ok(await _examService.DeactivateExamTypeSectionAsync(deactivateExamTypeSectionDto));
            }
            catch (Exception ex)
            {
                _logger.LogError(ex.ToString());
                return StatusCode(500, ex.Message);
            }
        }
    }
}
