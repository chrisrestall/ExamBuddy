using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using TB.TestManagerApi.Domain;

namespace TB.TestManagerApi.Services
{
    public interface IExamTypeMetaQueryService
    {
        Task<ExamTypeMetaDto> GetExamTypeMetaByIdAsync(Guid testTypeMetaId, bool withSections, bool activeOnly);
        Task<IEnumerable<ExamTypeMetaDto>> GetExamTypeMetasAsync(int page, int pageSize, bool withSections, bool activeOnly);
    }
}