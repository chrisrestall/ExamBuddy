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
    [Route(Routes.ExamMasterUri)]
    public class CreateExamMaster : BaseAsyncEndpoint<CreateExamMasterDto, Guid>
    {

        private readonly IExamMasterCommandService _examService;
        private readonly ILogger<CreateExamTypeMeta> _logger;

        public CreateExamMaster(ILogger<CreateExamTypeMeta> logger, IExamMasterCommandService examService)
        {
            _logger = logger;
            _examService = examService;
        }

        [HttpPost]
        [ProducesResponseType(typeof(Guid), StatusCodes.Status200OK)]
        [SwaggerResponse((int)HttpStatusCode.OK)]
        [SwaggerResponse((int)HttpStatusCode.BadRequest)]
        [SwaggerResponse((int)HttpStatusCode.InternalServerError)]
        [SwaggerOperation(
            Summary = "Creates an ExamMaster",
            Description = "Creates an ExamMaster in the database using Dapper",
            OperationId = nameof(CreateExamMaster),
            Tags = new[] { nameof(CreateExamMaster) }
        )]
        public override async Task<ActionResult<Guid>> HandleAsync([FromBody] CreateExamMasterDto createExamMasterDto, CancellationToken cancellationToken = default)
        {
            try
            {
                if (string.IsNullOrEmpty(createExamMasterDto.Name) || string.IsNullOrEmpty(createExamMasterDto.UserId) || createExamMasterDto.TestTypeId == default)
                {
                    _logger.LogWarning("createExamMasterDto is Missing a name, userId or examType relationship");
                    return BadRequest();
                }

               
                return Ok(await _examService.CreateExamMasterAsync(createExamMasterDto));
            }
            catch (Exception ex)
            {
                _logger.LogError(ex.ToString());
                return StatusCode(500, ex.Message);
            }
        }
    }
}
