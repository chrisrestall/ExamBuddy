using System;
using TB.TestManagerApi.Domain;
using System.Threading.Tasks;
using System.Collections.Generic;

namespace TB.TestManagerApi.Repository
{
    public interface IExamTypeMetaCommands
    {
        Task<Guid> CreateExamTypeMeta(ExamTypeMeta examTypeMetaDto);
        Task<IEnumerable<Guid>> CreateExamTypeSections(List<ExamTypeSection> examTypeSections);
        Task<Guid> UpdateExamTypeMeta(ExamTypeMeta examTypeMetaDto);
        Task<IEnumerable<Guid>> UpdateExamTypeSections(List<ExamTypeSection> examTypeSections);
        Task<Guid> DeactivateExamTypeMeta(ExamTypeMeta examTypeMeta);
        Task<IEnumerable<Guid>> DeactivateExamTypeSections(List<ExamTypeSection> examTypeSections);
    }
}