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

namespace TB.TestManagerApi.Features.ExamMaster.Endpoints
{
    [Route(Routes.ExamMasterUri)]
    public class UpdateExamMaster : BaseAsyncEndpoint<UpdateExamMasterDto, Guid>
    {

        private readonly IExamMasterCommandService _examService;
        private readonly ILogger<CreateExamTypeMeta> _logger;

        public UpdateExamMaster(ILogger<CreateExamTypeMeta> logger, IExamMasterCommandService examService)
        {
            _logger = logger;
            _examService = examService;
        }

        [HttpPut]
        [ProducesResponseType(typeof(Guid), StatusCodes.Status200OK)]
        [SwaggerResponse((int)HttpStatusCode.OK)]
        [SwaggerResponse((int)HttpStatusCode.BadRequest)]
        [SwaggerResponse((int)HttpStatusCode.InternalServerError)]
        [SwaggerOperation(
            Summary = "Updates an ExamMaster",
            Description = "Updates an ExamMaster in the database using Dapper",
            OperationId = nameof(UpdateExamMaster),
            Tags = new[] { nameof(UpdateExamMaster) }
        )]
        public override async Task<ActionResult<Guid>> HandleAsync([FromBody] UpdateExamMasterDto updateExamMasterDto, CancellationToken cancellationToken = default)
        {
            try
            {
                if (string.IsNullOrEmpty(updateExamMasterDto.Name) || string.IsNullOrEmpty(updateExamMasterDto.UserId) || updateExamMasterDto.TestTypeId == default || updateExamMasterDto.Id == default)
                {
                    _logger.LogWarning("createExamMasterDto is Missing a name, userId, Id or examType relationship");
                    return BadRequest();
                }


                return Ok(await _examService.UpdateExamMasterAsync(updateExamMasterDto));
            }
            catch (Exception ex)
            {
                _logger.LogError(ex.ToString());
                return StatusCode(500, ex.Message);
            }
        }
    }
}
