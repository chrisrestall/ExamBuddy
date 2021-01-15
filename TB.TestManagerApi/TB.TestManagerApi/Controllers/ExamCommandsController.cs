using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Swashbuckle.AspNetCore.Annotations;
using System;
using System.Collections.Generic;
using System.Net;
using System.Threading.Tasks;
using TB.TestManagerApi.Domain;
using TB.TestManagerApi.Services;

//namespace TB.TestManagerApi.Controllers
//{    
//    [Route("api/[controller]")]
//    [ApiController]
//    public class ExamCommandsController : ControllerBase
//    {
//        private readonly IExamService _examService;
//        private readonly ILogger<ExamCommandsController> _logger;
//        public ExamCommandsController(ILogger<ExamCommandsController> logger, IExamService examService)
//        {
//            _logger = logger;
//            _examService = examService;
//        }

        //[HttpPost("CreateExamTypeSection")]
        //[SwaggerOperation("CreateExamTypeSection")]
        //[SwaggerResponse((int)HttpStatusCode.OK)]
        //[SwaggerResponse((int)HttpStatusCode.BadRequest)]
        //[SwaggerResponse((int)HttpStatusCode.InternalServerError)]
        //public async Task<IActionResult> CreateExamTypeSection([FromBody] ExamTypeSectionDto examTypeSection)
        //{
        //    try
        //    {
        //        if (!ValidateExamTypeSection(examTypeSection)) return BadRequest();
        //        SetDefaultExamTypeSectionValues(examTypeSection);

        //        return Ok(await _examService.CreateExamTypeSectionAsync(examTypeSection));
        //    }
        //    catch (Exception ex)
        //    {
        //        _logger.LogError(ex.ToString());
        //        return StatusCode(500, ex.Message);
        //    }
        //}
        //[HttpPut("UpdateExamTypeSection")]
        //[SwaggerOperation("UpdateExamTypeSection")]
        //[SwaggerResponse((int)HttpStatusCode.OK)]
        //[SwaggerResponse((int)HttpStatusCode.BadRequest)]
        //[SwaggerResponse((int)HttpStatusCode.InternalServerError)]
        //public async Task<IActionResult> UpdateExamTypeSection([FromBody] ExamTypeSectionDto examTypeSection)
        //{
        //    try
        //    {
        //        if (!ValidateExamTypeSection(examTypeSection)) return BadRequest();
        //        SetDefaultExamTypeSectionValues(examTypeSection);

        //        return Ok(await _examService.UpdateExamTypeSectionAsync(examTypeSection));
        //    }
        //    catch (Exception ex)
        //    {
        //        _logger.LogError(ex.ToString());
        //        return StatusCode(500, ex.Message);
        //    }
        //}
        //[HttpPost("CreateExamTypeSections")]
        //[SwaggerOperation("CreateExamTypeSections")]
        //[SwaggerResponse((int)HttpStatusCode.OK)]
        //[SwaggerResponse((int)HttpStatusCode.BadRequest)]
        //[SwaggerResponse((int)HttpStatusCode.InternalServerError)]
        //public async Task<IActionResult> CreateExamTypeSections([FromBody] List<ExamTypeSectionDto> examTypeSections)
        //{
        //    try 
        //    {
        //        foreach (ExamTypeSectionDto examTypeSection in examTypeSections)
        //        {
        //            if (!ValidateExamTypeSection(examTypeSection)) return BadRequest();
        //            SetDefaultExamTypeSectionValues(examTypeSection);
        //        }
        //        return Ok(await _examService.CreateExamTypeSectionsAsync(examTypeSections));
        //    }
        //    catch (Exception ex)
        //    {
        //        _logger.LogError(ex.ToString());
        //        return StatusCode(500, ex.Message);
        //    }
        //}
        //[HttpPut("UpdateExamTypeSections")]
        //[SwaggerOperation("UpdateExamTypeSections")]
        //[SwaggerResponse((int)HttpStatusCode.OK)]
        //[SwaggerResponse((int)HttpStatusCode.BadRequest)]
        //[SwaggerResponse((int)HttpStatusCode.InternalServerError)]
        //public async Task<IActionResult> UpdateExamTypeSections([FromBody] List<ExamTypeSectionDto> examTypeSections)
        //{
        //    try
        //    {
        //        foreach (ExamTypeSectionDto examTypeSection in examTypeSections)
        //        {
        //            if (!ValidateExamTypeSection(examTypeSection)) return BadRequest();
        //            SetDefaultExamTypeSectionValues(examTypeSection);
        //        }
        //        return Ok(await _examService.UpdateExamTypeSectionsAsync(examTypeSections));
        //    }
        //    catch (Exception ex)
        //    {
        //        _logger.LogError(ex.ToString());
        //        return StatusCode(500, ex.Message);
        //    }
        //}

        //[HttpPost("CreateExamTypeMeta")]
        //[SwaggerOperation("CreateExamTypeMeta")]
        //[SwaggerResponse((int)HttpStatusCode.OK)]
        //[SwaggerResponse((int)HttpStatusCode.BadRequest)]
        //[SwaggerResponse((int)HttpStatusCode.InternalServerError)]
        //public async Task<IActionResult> CreateExamTypeMeta([FromBody] ExamTypeMetaDto examTypeMeta)
        //{
        //    try
        //    {
        //        foreach (ExamTypeSectionDto examTypeSection in examTypeMeta.TestTypeSections)
        //        {
        //            if (!ValidateExamTypeSection(examTypeSection)) return BadRequest();
        //            SetDefaultExamTypeSectionValues(examTypeSection);
        //        }
        //        return Ok(await _examService.CreateExamTypeMetaAsync(examTypeMeta));
        //    }
        //    catch (Exception ex)
        //    {
        //        _logger.LogError(ex.ToString());
        //        return StatusCode(500, ex.Message);
        //    }           
        //}

//        [HttpPut("UpdateExamTypeMeta")]
//        [SwaggerOperation("UpdateExamTypeMeta")]
//        [SwaggerResponse((int)HttpStatusCode.OK)]
//        [SwaggerResponse((int)HttpStatusCode.BadRequest)]
//        [SwaggerResponse((int)HttpStatusCode.InternalServerError)]
//        public async Task<IActionResult> UpdateExamTypeMeta([FromBody] ExamTypeMetaDto examTypeMeta)
//        {
//            try
//            {
//                foreach (ExamTypeSectionDto examTypeSection in examTypeMeta.TestTypeSections)
//                {
//                    if (!ValidateExamTypeSection(examTypeSection,false)) return BadRequest();
//                    SetDefaultExamTypeSectionValues(examTypeSection,false);
//                }
//                return Ok(await _examService.UpdateExamTypeMetaAsync(examTypeMeta));
//            }
//            catch (Exception ex)
//            {
//                _logger.LogError(ex.ToString());
//                return StatusCode(500, ex.Message);
//            }
//        }


//        #region Helpers
//        private bool ValidateExamTypeSection(ExamTypeSectionDto examTypeSection, bool isCreate = true)
//        {
//            bool isValid = true;
//            if (string.IsNullOrEmpty(examTypeSection.Name))
//            {
//                _logger.LogInformation("Invalid examTypeSection name");
//                isValid = false;
//            }
//            if (examTypeSection.TestTypeMetaId == default)
//            {
//                _logger.LogInformation($"Invalid examTypeSection Exam type meta ID: {examTypeSection.TestTypeMetaId}");
//                isValid = false;
//            }
//            if (!isCreate)
//            {
//                if(examTypeSection.Id == default)
//                {
//                    _logger.LogInformation($"Invalid examTypeSection ID for update: {examTypeSection.Id}");
//                    isValid = false;
//                }
//            }
//            return isValid;
//        }

//        private void SetDefaultExamTypeSectionValues(ExamTypeSectionDto examTypeSection, bool isCreate = true)
//        {
//            if (isCreate)
//            {
//                if (examTypeSection.Id == default(Guid))
//                {
//                    _logger.LogInformation($"examTypeSection ID assignment for create");
//                    examTypeSection.Id = Guid.NewGuid();
//                }
//                examTypeSection.CreateDate = DateTime.Now;
//                examTypeSection.EditDate = examTypeSection.CreateDate;
//            }
//            else
//            {
//                examTypeSection.EditDate = DateTime.Now;
//            }            
//        }
//        #endregion
//    }
//}

