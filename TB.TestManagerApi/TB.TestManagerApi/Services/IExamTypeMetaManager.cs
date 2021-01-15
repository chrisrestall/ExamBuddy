using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using TB.TestManagerApi.Domain;

namespace TB.TestManagerApi.Services
{
    public interface IExamTypeMetaManager
    {
        Task<Guid> CreateExamTypeMeta(ExamTypeMeta examTypeMetaDto);       
        Task<Guid> CreateExamTypeSection(ExamTypeSection examTypeSectionDto);
        Task<IEnumerable<Guid>> CreateExamTypeSections(List<ExamTypeSection> examTypeSectionDtos);
        Task<Guid> DeactivateExamTypeMeta(ExamTypeMeta examTypeMeta);
        Task<Guid> DeactivateExamTypeSection(ExamTypeSection examTypeSection);
        Task<IEnumerable<ExamTypeMeta>> FetchExamMetas(int page, int pageSize, bool withSections, bool activeOnly);
        Task<ExamTypeMeta> FetchExamTypeMetaById(Guid testTypeMetaId, bool withSections, bool activeOnly);
        Task<ExamTypeSection> FetchExamTypeSectionById(Guid testTypeSectionId, bool activeOnly);
        Task<IEnumerable<ExamTypeSection>> FetchExamTypeSectionsByExamMetaId(Guid testTypeMetaId, bool activeOnly);
        Task<Guid> UpdateExamTypeMeta(ExamTypeMeta examTypeMetaDto);
        Task<Guid> UpdateExamTypeSection(ExamTypeSection examTypeSectionDto);
        Task<IEnumerable<Guid>> UpdateExamTypeSections(List<ExamTypeSection> examTypeSectionDtos);
    }
}