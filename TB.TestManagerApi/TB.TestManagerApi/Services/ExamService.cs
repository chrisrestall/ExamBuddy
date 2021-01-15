using AutoMapper;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TB.TestManagerApi.Domain;

//namespace TB.TestManagerApi.Services
//{
    //public class ExamService : IExamService
    //{
    //    private readonly IExamTypeMetaManager _examTypeMetaManagerService;
    //    private readonly IExamStructureManager _examStructureManagerService;
    //    private readonly ILogger<ExamService> _logger;
    //    private readonly IMapper _mapper;

    //    public ExamService(ILogger<ExamService> logger, IExamTypeMetaManager examTypeMetaManagerService, IExamStructureManager examStructureManagerService, IMapper mapper)
    //    {
    //        _logger = logger;
    //        _examTypeMetaManagerService = examTypeMetaManagerService;
    //        _examStructureManagerService = examStructureManagerService;
    //        _mapper = mapper;
    //    }
        //public async Task<ExamTypeMetaDto> GetExamTypeMetaByIdAsync(Guid testTypeMetaId, bool withSections, bool activeOnly)
        //{
        //    try
        //    {
        //        var examMeta = await _examTypeMetaManagerService.FetchExamTypeMetaById(testTypeMetaId, withSections, activeOnly);
        //        var examMetaDto = _mapper.Map<ExamTypeMetaDto>(examMeta);
        //        return examMetaDto;
        //    }
        //    catch (Exception ex)
        //    {
        //        _logger.LogError(ex.ToString());
        //        throw;
        //    }
        //}
        //public async Task<IEnumerable<ExamTypeMetaDto>> GetExamTypeMetasAsync(int page, int pageSize, bool withSections, bool activeOnly)
        //{
        //    try
        //    {
        //        var examMetas = await _examTypeMetaManagerService.FetchExamMetas(page, pageSize, withSections, activeOnly);
        //        var examMetasDto = _mapper.Map<List<ExamTypeMetaDto>>(examMetas);
        //        return examMetasDto;
        //    }
        //    catch (Exception ex)
        //    {
        //        _logger.LogError(ex.ToString());
        //        throw;
        //    }
        //}

        //public async Task<ExamMasterDto> GetExamMasterByIdAsync(Guid examMasterId, bool activeOnly)
        //{
        //    ExamMasterDto examMasterDto = null;
        //    try
        //    {
        //        var examMaster = await _examStructureManagerService.FetchExamMasterById(examMasterId, activeOnly).ConfigureAwait(false);
        //        if (examMaster != null)
        //        {
        //            var examTypeMeta = await _examTypeMetaManagerService.FetchExamTypeMetaById(examMaster.TestTypeId, true, true).ConfigureAwait(false);
        //            examMasterDto = _mapper.Map<ExamMasterDto>(examMaster);
        //            var examTypeMetaDto = _mapper.Map<ExamTypeMetaDto>(examTypeMeta);
        //            examMasterDto.ExamType = examTypeMetaDto;
        //        }
        //        return examMasterDto;
        //    }
        //    catch (Exception ex)
        //    {
        //        _logger.LogError(ex.ToString());
        //        throw;
        //    }
        //}
       
        //public async Task<IEnumerable<ExamMasterDto>> GetExamMastersByUserIdAsync(string userId, bool activeOnly)
        //{
        //    try
        //    {
        //        var examMasters = await _examStructureManagerService.FetchExamMasterByUserId(userId, activeOnly).ConfigureAwait(false);
        //        var examMastersDto = await AddExamTypes(examMasters).ConfigureAwait(false);
        //        return examMastersDto;
        //    }
        //    catch (Exception ex)
        //    {
        //        _logger.LogError(ex.ToString());
        //        throw;
        //    }
        //}
               
        //public async Task<IEnumerable<ExamMasterDto>> GetExamMastersByExamTypeIdAsync(Guid examTypeId, bool activeOnly)
        //{
        //    try
        //    {
        //        var examMasters = await _examStructureManagerService.FetchExamMastersByExamTypeId(examTypeId, activeOnly).ConfigureAwait(false);
        //        var examMastersDto = await AddExamTypes(examMasters).ConfigureAwait(false);
        //        return examMastersDto;
        //    }
        //    catch (Exception ex)
        //    {
        //        _logger.LogError(ex.ToString());
        //        throw;
        //    }
        //}
        //public async Task<IEnumerable<ExamMasterDto>> GetExamMastersAsync(bool activeOnly)
        //{
        //    try
        //    {
        //        var examMasters = await _examStructureManagerService.FetchExamMasters(activeOnly).ConfigureAwait(false);
        //        var examMastersDto = await AddExamTypes(examMasters).ConfigureAwait(false);
        //        return examMastersDto;
        //    }
        //    catch (Exception ex)
        //    {
        //        _logger.LogError(ex.ToString());
        //        throw;
        //    }
        //}

        //public async Task<ExamQuestionsDto> GetExamQuestionByIdAsync(Guid examQuestionId, bool activeOnly)
        //{
        //    try
        //    {
        //        var examQuestion = await _examStructureManagerService.FetchExamQuestionById(examQuestionId, activeOnly);
        //        return await ConstructExamQuestions(new List<ExamQuestion> { examQuestion }).ConfigureAwait(false);
        //    }
        //    catch (Exception ex)
        //    {
        //        _logger.LogError(ex.ToString());
        //        throw;
        //    }            
        //}

        //public async Task<ExamQuestionsDto> GetExamQuestionByQuestionMasterIdAsync(Guid examQuestionMasterId, bool activeOnly)
        //{
        //    try
        //    {
        //        var examQuestions = await _examStructureManagerService.FetchExamQuestionAnswersByQuestionMasterId(examQuestionMasterId, activeOnly).ConfigureAwait(false);
        //        return await ConstructExamQuestions(examQuestions).ConfigureAwait(false);               
        //    }
        //    catch (Exception ex)
        //    {
        //        _logger.LogError(ex.ToString());
        //        throw;
        //    }
        //}

        //public async Task<ExamQuestionsDto> GetExamQuestionsByExamMasterIdAsync(Guid examMasterId, bool activeOnly)
        //{
        //    try
        //    {
        //        var examQuestions = await _examStructureManagerService.FetchExamQuestionsByExamMasterId(examMasterId, activeOnly);
        //        return await ConstructExamQuestions(examQuestions).ConfigureAwait(false);
                  
        //    }
        //    catch (Exception ex)
        //    {
        //        _logger.LogError(ex.ToString());
        //        throw;
        //    }
        //}

        //public async Task<Guid> CreateExamTypeMetaAsync(CreateExamTypeMetaDto createExamTypeMetaDto)
        //{
        //    try
        //    {
        //        ExamTypeMeta etm = _mapper.Map<ExamTypeMeta>(createExamTypeMetaDto);

        //        return await _examTypeMetaManagerService.CreateExamTypeMeta(etm).ConfigureAwait(false);
        //    }
        //    catch (Exception ex)
        //    {
        //        _logger.LogError(ex.ToString());
        //        throw;
        //    }
        //}
    

        //public async Task<Guid> CreateExamTypeSectionAsync(CreateExamTypeSectionDto createExamTypeSectionDto)
        //{
        //    try
        //    {
        //        ExamTypeSection ets = _mapper.Map<ExamTypeSection>(createExamTypeSectionDto);
        //        return await _examTypeMetaManagerService.CreateExamTypeSection(ets).ConfigureAwait(false);
        //    }
        //    catch (Exception ex)
        //    {
        //        _logger.LogError(ex.ToString());
        //        throw;
        //    }
        //}
        //public async Task<Guid> UpdateExamTypeSectionAsync(ExamTypeSectionDto examTypeSectionDto)
        //{
        //    try
        //    {
        //        ExamTypeSection ets = _mapper.Map<ExamTypeSection>(examTypeSectionDto);
        //        return await _examTypeMetaManagerService.UpdateExamTypeSection(ets).ConfigureAwait(false);
        //    }
        //    catch (Exception ex)
        //    {
        //        _logger.LogError(ex.ToString());
        //        throw;
        //    }
        //}
        //public async Task<IEnumerable<Guid>> CreateExamTypeSectionsAsync(IEnumerable<CreateExamTypeSectionDto> createExamTypeSectionDto)
        //{
        //    try
        //    {
        //        List<ExamTypeSection> ets = _mapper.Map<List<ExamTypeSection>>(createExamTypeSectionDto);
        //        return await _examTypeMetaManagerService.CreateExamTypeSections(ets).ConfigureAwait(false);
        //    }
        //    catch (Exception ex)
        //    {
        //        _logger.LogError(ex.ToString());
        //        throw;
        //    }
        //}
        //public async Task<IEnumerable<Guid>> UpdateExamTypeSectionsAsync(List<ExamTypeSectionDto> examTypeSectionsDto)
        //{
        //    try
        //    {
        //        List<ExamTypeSection> ets = _mapper.Map<List<ExamTypeSection>>(examTypeSectionsDto);
        //        return await _examTypeMetaManagerService.UpdateExamTypeSections(ets).ConfigureAwait(false);
        //    }
        //    catch (Exception ex)
        //    {
        //        _logger.LogError(ex.ToString());
        //        throw;
        //    }
        //}

        //#region Helper Methods
        //private async Task<ExamQuestionsDto> ConstructExamQuestions(IEnumerable<ExamQuestion> examQuestions )
        //{
        //    ExamQuestionsDto examQuestionsDto = new ExamQuestionsDto();
        //    ExamQuestion currentExamQuestion;
        //    try
        //    {
        //        if (examQuestions == null || !examQuestions.Any()) return null;
        //        List<ExamQuestionDto> eqs = _mapper.Map<List<ExamQuestionDto>>(examQuestions);
        //        foreach (ExamQuestionDto eq in eqs)
        //        {
        //            var examAnswerXref = new ExamAnswerXrefDto()
        //            {
        //                Answer = eq.TestQuestionAnswer.ExamAnswer.Answer,
        //                Id = eq.TestQuestionAnswer.ExamAnswer.Id,
        //                isCorrect = eq.TestQuestionAnswer.IsCorrect,
        //                XrefId = eq.TestQuestionAnswer.Id
        //            };

        //            var examQuestionAnswerXref = examQuestionsDto.ExamQuestionCollection.SingleOrDefault(p => p.ExamQuestion.Id == eq.TestQuestionAnswer.ExamQuestion.Id);

        //            if (examQuestionAnswerXref != null)
        //                examQuestionAnswerXref.ExamQuestionAnswers.Add(examAnswerXref);
        //            else
        //                examQuestionsDto.ExamQuestionCollection.Add(new ExamQuestionAnswersXrefDto() { ExamQuestion = eq.TestQuestionAnswer.ExamQuestion, ExamQuestionAnswers = new List<ExamAnswerXrefDto> { examAnswerXref } });
        //        }

        //        var examMaster = await _examStructureManagerService.FetchExamMasterById(eqs.First().TestMasterId, false).ConfigureAwait(false);
        //        examQuestionsDto.ExamMaster = _mapper.Map<ExamMasterDto>(examMaster);
        //        var examTypeMeta = await _examTypeMetaManagerService.FetchExamTypeMetaById(examMaster.TestTypeId, true, false).ConfigureAwait(false);
        //        examQuestionsDto.ExamMaster.ExamType = _mapper.Map<ExamTypeMetaDto>(examTypeMeta);

        //        //TODO: GroupBy TO DECREASE ITERATIONS?
        //        foreach (ExamQuestionAnswersXrefDto xref in examQuestionsDto.ExamQuestionCollection)
        //        {
        //            currentExamQuestion = examQuestions.First(x => x.ExamQuestionMaster.Id == xref.ExamQuestion.Id);
        //            xref.ExamQuestion.ExamTypeSection = examQuestionsDto.ExamMaster.ExamType.TestTypeSections.SingleOrDefault(p => p.Id == currentExamQuestion.ExamQuestionMaster.TestTypeSectionId);
        //        }

        //        return examQuestionsDto;
        //    }
        //    catch (Exception ex)
        //    {
        //        _logger.LogError(ex.ToString());
        //        throw;
        //    }
        //}
        //private async Task<IEnumerable<ExamMasterDto>> AddExamTypes(IEnumerable<ExamMaster> examMasters)
        //{
        //    List<ExamMasterDto> examMastersDto = new List<ExamMasterDto>();
        //    ExamTypeMeta examTypeMeta = null;
        //    ExamMasterDto examMasterDto = null;
        //    try
        //    {
        //        examMastersDto = _mapper.Map<List<ExamMasterDto>>(examMasters);
        //        foreach (var examMaster in examMasters)
        //        {
        //            examTypeMeta = await _examTypeMetaManagerService.FetchExamTypeMetaById(examMaster.TestTypeId, true, false).ConfigureAwait(false);
        //            examMasterDto = examMastersDto.SingleOrDefault(p => p.Id == examMaster.Id);
        //            examMasterDto.ExamType = _mapper.Map<ExamTypeMetaDto>(examTypeMeta);
        //        }
        //        return examMastersDto;
        //    }
        //    catch (Exception ex)
        //    {
        //        _logger.LogError(ex.ToString());
        //        throw;
        //    }
        //}
        //#endregion
   // }
//}
