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
    public class UpdateExamTypeSections : BaseAsyncEndpoint<List<UpdateExamTypeSectionDto>, Guid>
    {
        private readonly IExamTypeMetaCommandService _examService;
        private readonly ILogger<UpdateExamTypeSections> _logger;

        public UpdateExamTypeSections(ILogger<UpdateExamTypeSections> logger, IExamTypeMetaCommandService examService)
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
           Summary = "Updates ExamTypeSections",
           Description = "Updates ExamTypeSections in the database using Dapper",
           OperationId = nameof(UpdateExamTypeSections),
           Tags = new[] { nameof(UpdateExamTypeSections) }
       )]
        public override async Task<ActionResult<Guid>> HandleAsync([FromBody] List<UpdateExamTypeSectionDto> examTypeSections, CancellationToken cancellationToken = default)
        {
            try
            {
                foreach (UpdateExamTypeSectionDto updateExamTypeSection in examTypeSections)
                {
                    if (string.IsNullOrEmpty(updateExamTypeSection.Description) || string.IsNullOrEmpty(updateExamTypeSection.Name) || updateExamTypeSection.TestTypeMetaId == default || updateExamTypeSection.Id == default)
                    {
                        _logger.LogWarning("updateExamTypeSection is Missing a name, examType relationship, id, or description");
                        return BadRequest();
                    }
                }
                return Ok(await _examService.UpdateExamTypeSectionsAsync(examTypeSections));
            }
            catch (Exception ex)
            {
                _logger.LogError(ex.ToString());
                return StatusCode(500, ex.Message);
            }
        }
    }
}
