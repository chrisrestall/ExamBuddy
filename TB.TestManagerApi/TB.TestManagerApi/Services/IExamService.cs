using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using TB.TestManagerApi.Domain;

namespace TB.TestManagerApi.Services
{
    public interface IExamService
    {
        //Task<ExamMasterDto> GetExamMasterByIdAsync(Guid examMasterId, bool activeOnly);
        //Task<IEnumerable<ExamMasterDto>> GetExamMastersAsync(bool activeOnly);
        //Task<IEnumerable<ExamMasterDto>> GetExamMastersByExamTypeIdAsync(Guid examTypeId, bool activeOnly);
        //Task<IEnumerable<ExamMasterDto>> GetExamMastersByUserIdAsync(string userId, bool activeOnly);
        Task<ExamQuestionsDto> GetExamQuestionByQuestionMasterIdAsync(Guid examQuestionMasterId, bool activeOnly);
        Task<ExamQuestionsDto> GetExamQuestionByIdAsync(Guid examQuestionId, bool activeOnly);
        Task<ExamQuestionsDto> GetExamQuestionsByExamMasterIdAsync(Guid examMasterId, bool activeOnly);
       // Task<ExamTypeMetaDto> GetExamTypeMetaByIdAsync(Guid testTypeMetaId, bool withSections, bool activeOnly);
       // Task<IEnumerable<ExamTypeMetaDto>> GetExamTypeMetasAsync(int page, int pageSize, bool withSections, bool activeOnly);
        //Task<Guid> CreateExamTypeMetaAsync(CreateExamTypeMetaDto examTypeMeta);
        //Task<IEnumerable<Guid>> CreateExamTypeSectionsAsync(IEnumerable<CreateExamTypeSectionDto> examTypeSections);
        ////Task<Guid> CreateExamTypeSectionAsync(CreateExamTypeSectionDto examTypeSection);
        //Task<Guid> UpdateExamTypeSectionAsync(ExamTypeSectionDto examTypeSection);
        //Task<IEnumerable<Guid>> UpdateExamTypeSectionsAsync(List<ExamTypeSectionDto> examTypeSections);
        //Task<Guid> UpdateExamTypeMetaAsync(ExamTypeMetaDto examTypeMeta);
    }
}