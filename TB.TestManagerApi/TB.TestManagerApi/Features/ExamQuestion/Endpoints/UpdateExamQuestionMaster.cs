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
    [Route(Routes.ExamQuestionExamQuestionUri)]
    public class UpdateExamQuestionMaster : BaseAsyncEndpoint<UpdateExamQuestionMasterDto, Guid>
    {

        private readonly IExamQuestionCommandService _examService;
        private readonly ILogger<UpdateExamQuestionMaster> _logger;

        public UpdateExamQuestionMaster(ILogger<UpdateExamQuestionMaster> logger, IExamQuestionCommandService examService)
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
            Summary = "Updates an ExamQuestion",
            Description = "Updates an ExamQuestion in the database using Dapper",
            OperationId = nameof(UpdateExamQuestionMaster),
            Tags = new[] { nameof(UpdateExamQuestionMaster) }
        )]
        public override async Task<ActionResult<Guid>> HandleAsync([FromBody] UpdateExamQuestionMasterDto updateExamQuestionMasterDto, CancellationToken cancellationToken = default)
        {
            try
            {
                if (string.IsNullOrEmpty(updateExamQuestionMasterDto.Question) || updateExamQuestionMasterDto.ExamTypeSectionId == default || updateExamQuestionMasterDto.Id == default)
                {
                    _logger.LogWarning("updateExamQuestionMasterDto is Missing a question, ExamTypeSectionId or id");
                    return BadRequest();
                }

            
                return Ok(await _examService.UpdateExamQuestionMasterAsync(updateExamQuestionMasterDto));
            }
            catch (Exception ex)
            {
                _logger.LogError(ex.ToString());
                return StatusCode(500, ex.Message);
            }
        }
    }
}
