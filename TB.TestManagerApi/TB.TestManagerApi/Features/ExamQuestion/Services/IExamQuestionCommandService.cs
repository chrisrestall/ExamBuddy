using System;
using System.Threading.Tasks;
using TB.TestManagerApi.Domain;

namespace TB.TestManagerApi.Services
{
    public interface IExamQuestionCommandService
    {
        Task<Guid> CreateExamAnswerAsync(CreateExamAnswerDto createExamAnswerDto);
        Task<Guid> CreateExamQuestionAsync(CreateExamQuestionDto createExamQuestionDto);
        Task<Guid> UpdateExamAnswerMasterAsync(UpdateExamAnswerMasterDto updateExamAnswerMasterDto);
        Task<Guid> UpdateExamQuestionMasterAsync(UpdateExamQuestionMasterDto updateExamQuestionMasterDto);
    }
}