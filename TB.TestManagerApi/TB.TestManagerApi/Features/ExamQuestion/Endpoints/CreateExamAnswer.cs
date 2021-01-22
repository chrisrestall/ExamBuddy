
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
    public class CreateExamAnswer : BaseAsyncEndpoint<CreateExamAnswerDto, Guid>
    {

        private readonly IExamQuestionCommandService _examService;
        private readonly ILogger<CreateExamAnswer> _logger;

        public CreateExamAnswer(ILogger<CreateExamAnswer> logger, IExamQuestionCommandService examService)
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
            Summary = "Creates an ExamAnswer",
            Description = "Creates an ExamAnswer in the database using Dapper",
            OperationId = nameof(CreateExamAnswer),
            Tags = new[] { nameof(CreateExamAnswer) }
        )]
        public override async Task<ActionResult<Guid>> HandleAsync([FromBody] CreateExamAnswerDto createExamAnswerDto, CancellationToken cancellationToken = default)
        {
            try
            {
                if (createExamAnswerDto.ExamMasterId == default || createExamAnswerDto.ExamQuestionMasterId == default)
                {
                    _logger.LogWarning("createExamAnswerDto is Missing a questionMasterId or ExamMasterId");
                    return BadRequest();
                }

                foreach (CreateExamAnswerMasterDto examAnswer in createExamAnswerDto.ExamAnswers)
                {
                    if (string.IsNullOrEmpty(examAnswer.Answer))
                    {
                        _logger.LogWarning("CreateExamAnswerMasterDto is Missing an answer");
                        return BadRequest();
                    }
                }
                return Ok(await _examService.CreateExamAnswerAsync(createExamAnswerDto));
            }
            catch (Exception ex)
            {
                _logger.LogError(ex.ToString());
                return StatusCode(500, ex.Message);
            }
        }
    }
}
