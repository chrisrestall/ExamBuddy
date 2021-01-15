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
using System.Collections.Generic;

namespace TB.TestManagerApi.Features.ExamMeta.Endpoints
{
    [Route(Routes.ExamMetaSectionsUri)]
    public class CreateExamTypeSections : BaseAsyncEndpoint<List<CreateExamTypeSectionDto>, Guid>
    {
        private readonly IExamTypeMetaCommandService _examService;
        private readonly ILogger<CreateExamTypeSections> _logger;

        public CreateExamTypeSections(ILogger<CreateExamTypeSections> logger, IExamTypeMetaCommandService examService)
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
           Summary = "Creates ExamTypeSections",
           Description = "Creates ExamTypeSections in the database using Dapper",
           OperationId = nameof(CreateExamTypeSections),
           Tags = new[] { nameof(CreateExamTypeSections) }
       )]
        public override async Task<ActionResult<Guid>> HandleAsync([FromBody] List<CreateExamTypeSectionDto> createExamTypeSections, CancellationToken cancellationToken = default)
        {
            try
            {
                foreach (CreateExamTypeSectionDto createExamTypeSection in createExamTypeSections)
                {
                    _logger.LogWarning("createExamTypeSection is Missing a name, examType relationship or description");
                    return BadRequest();
                }
                return Ok(await _examService.CreateExamTypeSectionsAsync(createExamTypeSections));
            }
            catch (Exception ex)
            {
                _logger.LogError(ex.ToString());
                return StatusCode(500, ex.Message);
            }

        }
    }
}
