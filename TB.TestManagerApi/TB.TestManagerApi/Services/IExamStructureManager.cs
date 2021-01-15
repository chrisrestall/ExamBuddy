using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using TB.TestManagerApi.Domain;

namespace TB.TestManagerApi.Services
{
    public interface IExamStructureManager
    {
        Task<ExamMaster> FetchExamMasterById(Guid examMasterId, bool activeOnly);
        Task<IEnumerable<ExamMaster>> FetchExamMasterByUserId(string userId, bool activeOnly);
        Task<IEnumerable<ExamMaster>> FetchExamMasters(bool activeOnly);
        Task<IEnumerable<ExamMaster>> FetchExamMastersByExamTypeId(Guid examTypeId, bool activeOnly);
        Task<IEnumerable<ExamQuestion>> FetchExamQuestionAnswersByQuestionMasterId(Guid examQuestionMasterId, bool activeOnly);
        Task<ExamQuestion> FetchExamQuestionById(Guid examQuestionId, bool activeOnly);
        Task<IEnumerable<ExamQuestion>> FetchExamQuestionsByExamMasterId(Guid examMasterId, bool activeOnly);
    }
}