using System;
using System.Threading.Tasks;
using TB.TestManagerApi.Domain;

namespace TB.TestManagerApi.Repository
{
    public interface IExamStructureCommands
    {
        Task<Guid> CreateExamAnswer(CreateExamAnswer createExamAnswer);
        Task<Guid> CreateExamAnswerMaster(CreateExamAnswerMaster examAnswer);
        Task<Guid> CreateExamMaster(CreateExamMaster createExamMaster);
        Task<Guid> CreateExamQuestion(Guid examMasterId, Guid examQuestionAnswerXrefId);
        Task<Guid> CreateExamQuestionAnswerXref(Guid testQuestionId, Guid testAnswerId, bool isCorrect);
        Task<Guid> CreateExamQuestionMaster(CreateExamQuestion createExamQuestion);
        Task<Guid> DeactivateAnswerMaster(DeactivateAnswerMaster deactivateAnswerMaster);
        Task<Guid> DeactivateExamMaster(DeactivateExamMaster deactivateExamMaster);
        Task<Guid> DeactivateQuestionMaster(DeactivateQuestionMaster deactivateQuestionMaster);
        Task<Guid> UpdateExamAnswerMaster(UpdateExamAnswerMaster updateExamAnswerMaster);
        Task<Guid> UpdateExamAnswerQuestionXrefCorrect(UpdateExamAnswerQuestionXrefCorrect updateExamAnswerQuestionXrefCorrect);
        Task<Guid> UpdateExamMaster(UpdateExamMaster updateExamMaster);
        Task<Guid> UpdateExamQuestionMaster(UpdateExamQuestionMaster updateExamQuestionMaster);
    }
}