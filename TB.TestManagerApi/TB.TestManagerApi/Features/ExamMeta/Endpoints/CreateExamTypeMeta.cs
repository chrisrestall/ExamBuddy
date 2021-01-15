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
    public class CreateExamTypeMeta : BaseAsyncEndpoint<CreateExamTypeMetaDto, Guid>
    {

        private readonly IExamTypeMetaCommandService _examService;
        private readonly ILogger<CreateExamTypeMeta> _logger;

        public CreateExamTypeMeta(ILogger<CreateExamTypeMeta> logger, IExamTypeMetaCommandService examService)
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
            Summary = "Creates an ExamTypeMeta",
            Description = "Creates an ExamTypeMeta in the database using Dapper",
            OperationId = nameof(CreateExamTypeMeta),
            Tags = new[] { nameof(CreateExamTypeMeta) }
        )]
        public override async Task<ActionResult<Guid>> HandleAsync([FromBody] CreateExamTypeMetaDto createExamTypeMetaDto, CancellationToken cancellationToken = default)
        {
            try
            {
                if (string.IsNullOrEmpty(createExamTypeMetaDto.Description) || string.IsNullOrEmpty(createExamTypeMetaDto.Name))
                {
                    _logger.LogWarning("createExamTypeMetaDto is Missing a name or description");
                    return BadRequest();
                }

                foreach (CreateExamTypeSectionDto examTypeSection in createExamTypeMetaDto.TestTypeSections)
                {
                    if (string.IsNullOrEmpty(examTypeSection.Description) || string.IsNullOrEmpty(examTypeSection.Name) || examTypeSection.TestTypeMetaId == default)
                    {
                        _logger.LogWarning("createExamTypeMetaDto is Missing a name, examType relationship or description");
                        return BadRequest();
                    }
                }
                return Ok(await _examService.CreateExamTypeMetaAsync(createExamTypeMetaDto));
            }
            catch (Exception ex)
            {
                _logger.LogError(ex.ToString());
                return StatusCode(500, ex.Message);
            }
        }
    }
}
