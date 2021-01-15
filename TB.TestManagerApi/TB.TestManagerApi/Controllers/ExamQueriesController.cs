//using Microsoft.AspNetCore.Mvc;
//using Microsoft.Extensions.Logging;
//using Swashbuckle.AspNetCore.Annotations;
//using System;
//using System.Collections.Generic;
//using System.Linq;
//using System.Net;
//using System.Threading.Tasks;
//using TB.TestManagerApi.Domain;
//using TB.TestManagerApi.Services;

//namespace TB.TestManagerApi.Controllers
//{
//    [Route("api/[controller]")]
//    [ApiController]
//    public class ExamQueriesController : ControllerBase
//    {

//        private readonly IExamService _examService;
//        private readonly ILogger<ExamQueriesController> _logger;

//        public ExamQueriesController(ILogger<ExamQueriesController> logger, IExamService examService)
//        {
//            _logger = logger;
//            _examService = examService;
//        }
//        [HttpGet("GetExamTypeMetaById/{testTypeMetaId}")]
//        [SwaggerOperation("GetExamTypeMetaById")]
//        [SwaggerResponse((int)HttpStatusCode.OK)]
//        [SwaggerResponse((int)HttpStatusCode.NotFound)]
//        public async Task<IActionResult> GetExamTypeMetaByIdAsync(Guid examTypeMetaId, bool withSections = true, bool activeOnly = true)
//        {

//            try
//            {
//                return Ok(await _examService.GetExamTypeMetaByIdAsync(examTypeMetaId, withSections, activeOnly));
//            }
//            catch (Exception ex)
//            {
//                _logger.LogError(ex.ToString());
//                return NotFound();
//            }
//        }

//        [HttpGet("GetExamTypeMetas/{page}/{pageSize}")]
//        [SwaggerOperation("GetExamTypeMetas")]
//        [SwaggerResponse((int)HttpStatusCode.OK)]
//        [SwaggerResponse((int)HttpStatusCode.NotFound)]
//        public async Task<IActionResult> GetExamTypeMetasAsync(int page, int pageSize, bool withSections = true, bool activeOnly = true)
//        {
//            try
//            {
//                return Ok(await _examService.GetExamTypeMetasAsync(page, pageSize, withSections, activeOnly));
//            }
//            catch (Exception ex)
//            {
//                _logger.LogError(ex.ToString());
//                return NotFound();
//            }
//        }
//        [HttpGet("GetExamMastersByUserId/{userId}")]
//        [SwaggerOperation("GetExamMasterByUserIdAsync")]
//        [SwaggerResponse((int)HttpStatusCode.OK)]
//        [SwaggerResponse((int)HttpStatusCode.NotFound)]
//        public async Task<IActionResult> GetExamMasterByUserIdAsync(string userId, bool activeOnly = true)
//        {
//            try
//            {
//                return Ok(await _examService.GetExamMastersByUserIdAsync(userId, activeOnly));
//            }
//            catch (Exception ex)
//            {
//                _logger.LogError(ex.ToString());
//                return NotFound();
//            }

//        }
//        [HttpGet("GetExamMasterById/{examMasterId}")]
//        [SwaggerOperation("GetExamMasterById")]
//        [SwaggerResponse((int)HttpStatusCode.OK)]
//        [SwaggerResponse((int)HttpStatusCode.NotFound)]
//        public async Task<IActionResult> GetExamMasterByIdAsync(Guid examMasterId, bool activeOnly = true)
//        {
//            try
//            {
//                return Ok(await _examService.GetExamMasterByIdAsync(examMasterId, activeOnly));
//            }
//            catch (Exception ex)
//            {
//                _logger.LogError(ex.ToString());
//                return NotFound();
//            }
//        }

//        [HttpGet("GetExamMastersByExamTypeId/{examTypeId}")]
//        [SwaggerOperation("GetExamMastersByExamTypeId")]
//        [SwaggerResponse((int)HttpStatusCode.OK)]
//        [SwaggerResponse((int)HttpStatusCode.NotFound)]
//        public async Task<IActionResult> GetExamMastersByExamTypeId(Guid examTypeId, bool activeOnly = true)
//        {
//            try
//            {
//                return Ok(await _examService.GetExamMastersByExamTypeIdAsync(examTypeId, activeOnly));
//            }
//            catch (Exception ex)
//            {
//                _logger.LogError(ex.ToString());
//                return NotFound();
//            }
//            //}
//            [HttpGet("GetExamMasters")]
//            [SwaggerOperation("GetExamMasters")]
//            [SwaggerResponse((int)HttpStatusCode.OK)]
//            [SwaggerResponse((int)HttpStatusCode.NotFound)]
//            public async Task<IActionResult> GetExamMasters(bool activeOnly = true)
//            {
//                try
//                {
//                    return Ok(await _examService.GetExamMastersAsync(activeOnly));
//                }
//                catch (Exception ex)
//                {
//                    _logger.LogError(ex.ToString());
//                    return NotFound();
//                }
//            }

//            [HttpGet("GetExamQuestionByQuestionMasterId/{examQuestionMasterId}")]
//            [SwaggerOperation("GetExamQuestionByQuestionMasterId")]
//            [SwaggerResponse((int)HttpStatusCode.OK)]
//            [SwaggerResponse((int)HttpStatusCode.NotFound)]
//            public async Task<IActionResult> GetExamQuestionByQuestionMasterId(Guid examQuestionMasterId, bool activeOnly = true)
//            {
//                try
//                {
//                    return Ok(await _examService.GetExamQuestionByQuestionMasterIdAsync(examQuestionMasterId, activeOnly));
//                }
//                catch (Exception ex)
//                {
//                    _logger.LogError(ex.ToString());
//                    return NotFound();
//                }
//            }

//            [HttpGet("GetExamQuestionById/{examQuestionId}")]
//            [SwaggerOperation("GetExamQuestionById")]
//            [SwaggerResponse((int)HttpStatusCode.OK)]
//            [SwaggerResponse((int)HttpStatusCode.NotFound)]
//            public async Task<IActionResult> GetExamQuestionById(Guid examQuestionId, bool activeOnly = true)
//            {
//                try
//                {
//                    return Ok(await _examService.GetExamQuestionByIdAsync(examQuestionId, activeOnly));
//                }
//                catch (Exception ex)
//                {
//                    _logger.LogError(ex.ToString());
//                    return NotFound();
//                }
//            }

//            [HttpGet("GetExamQuestionsByExamMasterId/{examMasterId}")]
//            [SwaggerOperation("GetExamQuestionsByExamMasterId")]
//            [SwaggerResponse((int)HttpStatusCode.OK)]
//            [SwaggerResponse((int)HttpStatusCode.NotFound)]
//            public async Task<IActionResult> GetExamQuestionsByExamMasterId(Guid examMasterId, bool activeOnly = true)
//            {
//                try
//                {
//                    return Ok(await _examService.GetExamQuestionsByExamMasterIdAsync(examMasterId, activeOnly));
//                }
//                catch (Exception ex)
//                {
//                    _logger.LogError(ex.ToString());
//                    return NotFound();
//                }
//            }
//        }
//    }
