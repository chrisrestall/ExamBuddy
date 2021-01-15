using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using TB.TestManagerApi.Domain;

namespace TB.TestManagerApi.Services
{
    public interface IExamMasterQueryService
    {
        Task<ExamMasterDto> GetExamMasterByIdAsync(Guid examMasterId, bool activeOnly);
        Task<IEnumerable<ExamMasterDto>> GetExamMastersAsync(bool activeOnly);
        Task<IEnumerable<ExamMasterDto>> GetExamMastersByExamTypeIdAsync(Guid examTypeId, bool activeOnly);
        Task<IEnumerable<ExamMasterDto>> GetExamMastersByUserIdAsync(string userId, bool activeOnly);
    }
}