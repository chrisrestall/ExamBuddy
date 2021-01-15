using System;
using System.Threading.Tasks;
using TB.TestManagerApi.Domain;

namespace TB.TestManagerApi.Services
{
    public interface IExamQuestionQueryService
    {
        Task<ExamQuestionsDto> GetExamQuestionByIdAsync(Guid examQuestionId, bool activeOnly);
        Task<ExamQuestionsDto> GetExamQuestionByQuestionMasterIdAsync(Guid examQuestionMasterId, bool activeOnly);
        Task<ExamQuestionsDto> GetExamQuestionsByExamMasterIdAsync(Guid examMasterId, bool activeOnly);
    }
}