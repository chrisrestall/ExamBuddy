using System;
using TB.TestManagerApi.Domain;
using System.Threading.Tasks;
using System.Collections.Generic;

namespace TB.TestManagerApi.Repository
{
    public interface IExamTypeMetaCommands
    {
        Task<Guid> CreateExamTypeMeta(CreateExamTypeMeta examTypeMetaDto);
        Task<IEnumerable<Guid>> CreateExamTypeSections(List<CreateExamTypeSection> examTypeSections);
        Task<Guid> UpdateExamTypeMeta(UpdateExamTypeMeta examTypeMetaDto);
        Task<IEnumerable<Guid>> UpdateExamTypeSections(List<UpdateExamTypeSection> examTypeSections);
        Task<Guid> DeactivateExamTypeMeta(DeactivateExamTypeMeta examTypeMeta);
        Task<IEnumerable<Guid>> DeactivateExamTypeSections(List<DeactivateExamTypeSection> examTypeSections);
    }
}