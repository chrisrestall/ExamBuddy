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

namespace TB.TestManagerApi.Features.ExamMeta.Endpoints
{
    [Route(Routes.ExamMetaSectionUri)]
    public class CreateExamTypeSection : BaseAsyncEndpoint<CreateExamTypeSectionDto, Guid>
    {
        private readonly IExamTypeMetaCommandService _examService;
        private readonly ILogger<CreateExamTypeSection> _logger;

        public CreateExamTypeSection(ILogger<CreateExamTypeSection> logger, IExamTypeMetaCommandService examService)
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
           Summary = "Creates an ExamTypeSection",
           Description = "Creates an ExamTypeSection in the database using Dapper",
           OperationId = nameof(CreateExamTypeSection),
           Tags = new[] { nameof(CreateExamTypeSection) }
       )]       
        public override async Task<ActionResult<Guid>> HandleAsync([FromBody] CreateExamTypeSectionDto createExamTypeSection, CancellationToken cancellationToken = default)
        {
            try
            {
                //TODO: CENTRALIZE VALIDATION ROUTINES?
                if (string.IsNullOrEmpty(createExamTypeSection.Description) || string.IsNullOrEmpty(createExamTypeSection.Name) || createExamTypeSection.TestTypeMetaId == default)
                {
                    _logger.LogWarning("createExamTypeSection is Missing a name, examType relationship or description");
                    return BadRequest();
                }

                return Ok(await _examService.CreateExamTypeSectionAsync(createExamTypeSection));
                // return Created(new Uri($"/{Routes.ExamMetaUri}/{guid}", UriKind.Relative), Entity);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex.ToString());
                return StatusCode(500, ex.Message);
            }
        
        }
    }
}
