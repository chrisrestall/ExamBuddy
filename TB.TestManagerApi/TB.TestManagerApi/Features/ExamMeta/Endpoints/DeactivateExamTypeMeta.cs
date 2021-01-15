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
    [Route(Routes.ExamMetaTypeUri)]
    public class DeactivateExamTypeMeta : BaseAsyncEndpoint<DeactivateExamTypeMetaDto, Guid>
    {

        private readonly IExamTypeMetaCommandService _examService;
        private readonly ILogger<DeactivateExamTypeMeta> _logger;

        public DeactivateExamTypeMeta(ILogger<DeactivateExamTypeMeta> logger, IExamTypeMetaCommandService examService)
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
            Summary = "Deactivates an ExamTypeMeta",
            Description = "Deactivates an ExamTypeMeta in the database using Dapper",
            OperationId = nameof(DeactivateExamTypeMeta),
            Tags = new[] { nameof(DeactivateExamTypeMeta) }
        )]
        public override async Task<ActionResult<Guid>> HandleAsync([FromBody] DeactivateExamTypeMetaDto deactivateExamTypeMetaDto, CancellationToken cancellationToken = default)
        {
            try
            {
                if (deactivateExamTypeMetaDto.Id == default)
                {
                    _logger.LogWarning("deactivateExamTypeMetaDto using default Id");
                    return BadRequest();
                }
               
                return Ok(await _examService.DeactivateExamTypeMetaAsync(deactivateExamTypeMetaDto));
            }
            catch (Exception ex)
            {
                _logger.LogError(ex.ToString());
                return StatusCode(500, ex.Message);
            }
        }
    }
}
