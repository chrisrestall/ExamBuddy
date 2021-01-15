using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using TB.TestManagerApi.Domain;

namespace TB.TestManagerApi.Repository
{
    public interface IExamStructureQueries
    {
        Task<ExamMaster> GetExamMasterById(Guid examMasterId);
        Task<IEnumerable<ExamMaster>> GetExamMasters();
        Task<IEnumerable<ExamMaster>> GetExamMastersByExamTypeId(Guid examTypeId);
        Task<IEnumerable<ExamMaster>> GetExamMastersByUserId(string userId);
        Task<IEnumerable<ExamQuestion>> GetExamQuestionAnswersByQuestionMasterId(Guid examQuestionMasterId);
        Task<ExamQuestion> GetExamQuestionById(Guid examQuestionId);
        Task<IEnumerable<ExamQuestion>> GetExamQuestionsByExamMasterId(Guid examMasterId);
    }
}