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
    [Route(Routes.ExamQuestionExamQuestionUri)]
    public class CreateExamQuestion : BaseAsyncEndpoint<CreateExamQuestionDto, Guid>
    {

        private readonly IExamQuestionCommandService _examService;
        private readonly ILogger<CreateExamQuestion> _logger;

        public CreateExamQuestion(ILogger<CreateExamQuestion> logger, IExamQuestionCommandService examService)
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
            Summary = "Creates an ExamQuestion",
            Description = "Creates an ExamQuestion in the database using Dapper",
            OperationId = nameof(CreateExamQuestion),
            Tags = new[] { nameof(CreateExamQuestion) }
        )]
        public override async Task<ActionResult<Guid>> HandleAsync([FromBody] CreateExamQuestionDto createExamQuestionDto, CancellationToken cancellationToken = default)
        {
            try
            {
                if (string.IsNullOrEmpty(createExamQuestionDto.ExamQuestion.Question) || createExamQuestionDto.ExamQuestion.ExamTypeSectionId == default || createExamQuestionDto.ExamMasterId == default )
                {
                    _logger.LogWarning("createExamQuestionDto is Missing a question, ExamTypeSectionId or ExamMasterId");
                    return BadRequest();
                }

                foreach (CreateExamAnswerMasterDto examAnswer in createExamQuestionDto.ExamAnswers)
                {
                    if (string.IsNullOrEmpty(examAnswer.Answer))
                    {
                        _logger.LogWarning("CreateExamAnswerMasterDto is Missing an answer");
                        return BadRequest();
                    }
                }
                return Ok(await _examService.CreateExamQuestionAsync(createExamQuestionDto));
            }
            catch (Exception ex)
            {
                _logger.LogError(ex.ToString());
                return StatusCode(500, ex.Message);
            }
        }
    }
}
