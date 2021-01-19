using System;
using System.Threading.Tasks;
using TB.TestManagerApi.Domain;

namespace TB.TestManagerApi.Repository
{
    public interface IExamStructureCommands
    {
        Task<Guid> CreateExamAnswerMaster(CreateExamAnswerMaster examAnswer);
        Task<Guid> CreateExamMaster(ExamMaster examMaster);
        Task<Guid> CreateExamQuestion(Guid examMasterId, Guid examQuestionAnswerXrefId);
        Task<Guid> CreateExamQuestionAnswerXref(Guid testQuestionId, Guid testAnswerId, bool isCorrect);
        Task<Guid> CreateExamQuestionMaster(CreateExamQuestion createExamQuestion);
        Task<Guid> DeactivateExamMaster(ExamMaster examMaster);
        Task<Guid> UpdateExamMaster(ExamMaster examMaster);
    }
}