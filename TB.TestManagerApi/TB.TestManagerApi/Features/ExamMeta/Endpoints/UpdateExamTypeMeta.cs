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
    [Route(Routes.ExamMetaTypeUri)]
    public class UpdateExamTypeMeta : BaseAsyncEndpoint<UpdateExamTypeMetaDto, Guid>
    {
        private readonly IExamTypeMetaCommandService _examService;
        private readonly ILogger<UpdateExamTypeMeta> _logger;

        public UpdateExamTypeMeta(ILogger<UpdateExamTypeMeta> logger, IExamTypeMetaCommandService examService)
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
           Summary = "Updates an ExamTypeMeta",
           Description = "Updates an ExamTypeMeta in the database using Dapper",
           OperationId = nameof(UpdateExamTypeMeta),
           Tags = new[] { nameof(UpdateExamTypeMeta) }
       )]
        public override async Task<ActionResult<Guid>> HandleAsync([FromBody] UpdateExamTypeMetaDto updateExamTypeMeta, CancellationToken cancellationToken = default)
        {

            try
            {
                foreach (UpdateExamTypeSectionDto updateExamTypeSection in updateExamTypeMeta.TestTypeSections)
                {
                    //TODO: CENTRALIZE VALIDATION ROUTINES?
                    if (string.IsNullOrEmpty(updateExamTypeSection.Description) || string.IsNullOrEmpty(updateExamTypeSection.Name) || updateExamTypeSection.TestTypeMetaId == default || updateExamTypeSection.Id == default)
                    {
                        _logger.LogWarning("updateExamTypeSection is Missing a name, examType relationship, id, or description");
                        return BadRequest();
                    }
                }
                return Ok(await _examService.UpdateExamTypeMetaAsync(updateExamTypeMeta));
            }
            catch (Exception ex)
            {
                _logger.LogError(ex.ToString());
                return StatusCode(500, ex.Message);
            }
        }
    }
}
