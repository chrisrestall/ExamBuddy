using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using TB.TestManagerApi.Domain;

namespace TB.TestManagerApi.Services
{
    public interface IExamTypeMetaManager
    {  
        Task<Guid> CreateExamTypeMeta(CreateExamTypeMeta createExamTypeMeta);
        Task<Guid> CreateExamTypeSection(CreateExamTypeSection createExamTypeSection);
        Task<IEnumerable<Guid>> CreateExamTypeSections(List<CreateExamTypeSection> examTypeSections);
        Task<Guid> DeactivateExamTypeMeta(DeactivateExamTypeMeta deactivateExamTypeMeta);
        Task<Guid> DeactivateExamTypeSection(DeactivateExamTypeSection examTypeSection);
        Task<IEnumerable<ExamTypeMeta>> FetchExamMetas(int page, int pageSize, bool withSections, bool activeOnly);
        Task<ExamTypeMeta> FetchExamTypeMetaById(Guid testTypeMetaId, bool withSections, bool activeOnly);
        Task<ExamTypeSection> FetchExamTypeSectionById(Guid testTypeSectionId, bool activeOnly);
        Task<IEnumerable<ExamTypeSection>> FetchExamTypeSectionsByExamMetaId(Guid testTypeMetaId, bool activeOnly);
        Task<Guid> UpdateExamTypeMeta(UpdateExamTypeMeta examTypeMeta);
        Task<Guid> UpdateExamTypeSection(UpdateExamTypeSection examTypeSection);
        Task<IEnumerable<Guid>> UpdateExamTypeSections(List<UpdateExamTypeSection> examTypeSections);
    }
}