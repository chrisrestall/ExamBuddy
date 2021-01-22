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

namespace TB.TestManagerApi.Features.ExamQuestion.Endpoints
{
    [Route(Routes.ExamQuestionExamAnswerUri)]
    public class UpdateExamAnswerMaster : BaseAsyncEndpoint<UpdateExamAnswerMasterDto, Guid>
    {

        private readonly IExamQuestionCommandService _examService;
        private readonly ILogger<UpdateExamAnswerMaster> _logger;

        public UpdateExamAnswerMaster(ILogger<UpdateExamAnswerMaster> logger, IExamQuestionCommandService examService)
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
            Summary = "Updates an ExamAnswer",
            Description = "Updates an ExamAnswer in the database using Dapper",
            OperationId = nameof(UpdateExamAnswerMaster),
            Tags = new[] { nameof(UpdateExamAnswerMaster) }
        )]
        public override async Task<ActionResult<Guid>> HandleAsync([FromBody] UpdateExamAnswerMasterDto updateExamAnswerMasterDto, CancellationToken cancellationToken = default)
        {
            try
            {
                if (string.IsNullOrEmpty(updateExamAnswerMasterDto.Answer ) || updateExamAnswerMasterDto.Id == default || updateExamAnswerMasterDto.ExamAnswerQuestionXrefCorrect.Id == default)
                {
                    _logger.LogWarning("updateExamAnswerMasterDto is Missing a answer or id");
                    return BadRequest();
                }


                return Ok(await _examService.UpdateExamAnswerMasterAsync(updateExamAnswerMasterDto));
            }
            catch (Exception ex)
            {
                _logger.LogError(ex.ToString());
                return StatusCode(500, ex.Message);
            }
        }
    }
}
