﻿using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using TB.TestManagerApi.Domain;

namespace TB.TestManagerApi.Services
{
    public interface IExamStructureManager
    {
        Task<Guid> CreateExamAnswer(CreateExamAnswer createExamAnswer);
        Task<Guid> CreateExamMaster(CreateExamMaster examMaster);
        Task<Guid> CreateExamQuestion(CreateExamQuestion createExamQuestion);
        Task<Guid> DeactivateAnswerMaster(DeactivateAnswerMaster deactivateAnswerMaster);
        Task<Guid> DeactivateExamMaster(DeactivateExamMaster examMaster);
        Task<Guid> DeactivateQuestionMaster(DeactivateQuestionMaster deactivateQuestionMaster);
        Task<ExamMaster> FetchExamMasterById(Guid examMasterId, bool activeOnly);
        Task<IEnumerable<ExamMaster>> FetchExamMasterByUserId(string userId, bool activeOnly);
        Task<IEnumerable<ExamMaster>> FetchExamMasters(bool activeOnly);
        Task<IEnumerable<ExamMaster>> FetchExamMastersByExamTypeId(Guid examTypeId, bool activeOnly);
        Task<IEnumerable<ExamQuestion>> FetchExamQuestionAnswersByQuestionMasterId(Guid examQuestionMasterId, bool activeOnly);
        Task<ExamQuestion> FetchExamQuestionById(Guid examQuestionId, bool activeOnly);
        Task<IEnumerable<ExamQuestion>> FetchExamQuestionsByExamMasterId(Guid examMasterId, bool activeOnly);
        Task<Guid> UpdateExamAnswer(UpdateExamAnswerMaster updateExamAnswerMaster);
        Task<Guid> UpdateExamMaster(UpdateExamMaster examMaster);
        Task<Guid> UpdateExamQuestion(UpdateExamQuestionMaster updateExamQuestionMaster);
    }
}