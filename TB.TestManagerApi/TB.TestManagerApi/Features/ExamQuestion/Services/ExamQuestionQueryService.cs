using AutoMapper;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TB.TestManagerApi.Domain;

namespace TB.TestManagerApi.Services
{
    public class ExamQuestionQueryService : IExamQuestionQueryService
    {
        private readonly IExamTypeMetaManager _examTypeMetaManagerService;
        private readonly IExamStructureManager _examStructureManagerService;
        private readonly ILogger<ExamQuestionQueryService> _logger;
        private readonly IMapper _mapper;

        public ExamQuestionQueryService(ILogger<ExamQuestionQueryService> logger, IExamTypeMetaManager examTypeMetaManagerService, IExamStructureManager examStructureManagerService, IMapper mapper)
        {
            _logger = logger;
            _examTypeMetaManagerService = examTypeMetaManagerService;
            _examStructureManagerService = examStructureManagerService;
            _mapper = mapper;
        }

        public async Task<ExamQuestionsDto> GetExamQuestionByIdAsync(Guid examQuestionId, bool activeOnly)
        {
            try
            {
                var examQuestion = await _examStructureManagerService.FetchExamQuestionById(examQuestionId, activeOnly);
                return await ConstructExamQuestions(new List<ExamQuestion> { examQuestion }).ConfigureAwait(false);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex.ToString());
                throw;
            }
        }

        public async Task<ExamQuestionsDto> GetExamQuestionByQuestionMasterIdAsync(Guid examQuestionMasterId, bool activeOnly)
        {
            try
            {
                var examQuestions = await _examStructureManagerService.FetchExamQuestionAnswersByQuestionMasterId(examQuestionMasterId, activeOnly).ConfigureAwait(false);
                return await ConstructExamQuestions(examQuestions).ConfigureAwait(false);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex.ToString());
                throw;
            }
        }

        public async Task<ExamQuestionsDto> GetExamQuestionsByExamMasterIdAsync(Guid examMasterId, bool activeOnly)
        {
            try
            {
                var examQuestions = await _examStructureManagerService.FetchExamQuestionsByExamMasterId(examMasterId, activeOnly);
                return await ConstructExamQuestions(examQuestions).ConfigureAwait(false);

            }
            catch (Exception ex)
            {
                _logger.LogError(ex.ToString());
                throw;
            }
        }

        #region Helper Methods
        private async Task<ExamQuestionsDto> ConstructExamQuestions(IEnumerable<ExamQuestion> examQuestions)
        {
            ExamQuestionsDto examQuestionsDto = new ExamQuestionsDto();
            ExamQuestion currentExamQuestion;
            try
            {
                if (examQuestions == null || !examQuestions.Any()) return null;
                List<ExamQuestionDto> eqs = _mapper.Map<List<ExamQuestionDto>>(examQuestions);
                foreach (ExamQuestionDto eq in eqs)
                {
                    var examAnswerXref = new ExamAnswerXrefDto()
                    {
                        Answer = eq.TestQuestionAnswer.ExamAnswer.Answer,
                        Id = eq.TestQuestionAnswer.ExamAnswer.Id,
                        isCorrect = eq.TestQuestionAnswer.IsCorrect,
                        XrefId = eq.TestQuestionAnswer.Id
                    };

                    var examQuestionAnswerXref = examQuestionsDto.ExamQuestionCollection.SingleOrDefault(p => p.ExamQuestion.Id == eq.TestQuestionAnswer.ExamQuestion.Id);

                    if (examQuestionAnswerXref != null)
                        examQuestionAnswerXref.ExamQuestionAnswers.Add(examAnswerXref);
                    else
                        examQuestionsDto.ExamQuestionCollection.Add(new ExamQuestionAnswersXrefDto() { ExamQuestion = eq.TestQuestionAnswer.ExamQuestion, ExamQuestionAnswers = new List<ExamAnswerXrefDto> { examAnswerXref } });
                }

                var examMaster = await _examStructureManagerService.FetchExamMasterById(eqs.First().TestMasterId, false).ConfigureAwait(false);
                examQuestionsDto.ExamMaster = _mapper.Map<ExamMasterDto>(examMaster);
                var examTypeMeta = await _examTypeMetaManagerService.FetchExamTypeMetaById(examMaster.TestTypeId, true, false).ConfigureAwait(false);
                examQuestionsDto.ExamMaster.ExamType = _mapper.Map<ExamTypeMetaDto>(examTypeMeta);

                //TODO: GroupBy TO DECREASE ITERATIONS?
                foreach (ExamQuestionAnswersXrefDto xref in examQuestionsDto.ExamQuestionCollection)
                {
                    currentExamQuestion = examQuestions.First(x => x.ExamQuestionMaster.Id == xref.ExamQuestion.Id);
                    xref.ExamQuestion.ExamTypeSection = examQuestionsDto.ExamMaster.ExamType.TestTypeSections.SingleOrDefault(p => p.Id == currentExamQuestion.ExamQuestionMaster.TestTypeSectionId);
                }

                return examQuestionsDto;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex.ToString());
                throw;
            }
        }
        #endregion
    }
}
