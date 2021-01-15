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
    public class UpdateExamTypeSection : BaseAsyncEndpoint<UpdateExamTypeSectionDto, Guid>
    {
        private readonly IExamTypeMetaCommandService _examService;
        private readonly ILogger<UpdateExamTypeSection> _logger;

        public UpdateExamTypeSection(ILogger<UpdateExamTypeSection> logger, IExamTypeMetaCommandService examService)
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
           Summary = "Updates an ExamTypeSection",
           Description = "Updates an ExamTypeSection in the database using Dapper",
           OperationId = nameof(UpdateExamTypeSection),
           Tags = new[] { nameof(UpdateExamTypeSection) }
       )]
        public override async Task<ActionResult<Guid>> HandleAsync([FromBody] UpdateExamTypeSectionDto updateExamTypeSection, CancellationToken cancellationToken = default)
        {

            try
            {
                if (string.IsNullOrEmpty(updateExamTypeSection.Description) || string.IsNullOrEmpty(updateExamTypeSection.Name) || updateExamTypeSection.TestTypeMetaId == default || updateExamTypeSection.Id == default)
                {
                    _logger.LogWarning("updateExamTypeSection is Missing a name, examType relationship, id, or description");
                    return BadRequest();
                }

                return Ok(await _examService.UpdateExamTypeSectionAsync(updateExamTypeSection));
            }
            catch (Exception ex)
            {
                _logger.LogError(ex.ToString());
                return StatusCode(500, ex.Message);
            }
        }
    }
}
