using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using TB.TestManagerApi.Domain;

namespace TB.TestManagerApi.Repository
{
    public interface IExamTypeMetaQueries
    {
        Task<IEnumerable<ExamTypeMeta>> GetExamMetas(int page, int pageSize, bool withSections = true);
        Task<IEnumerable<ExamTypeSection>> GetExamTypeSections(Guid testTypeMetaId);
        Task<ExamTypeMeta> GetExamMetaById(Guid testTypeMetaId, bool withSections = true);
        Task<ExamTypeSection> GetExamTypeSectionById(Guid testTypeSectionId);
    }
}