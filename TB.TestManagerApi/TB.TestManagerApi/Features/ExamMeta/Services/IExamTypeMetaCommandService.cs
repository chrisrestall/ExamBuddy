using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using TB.TestManagerApi.Domain;

namespace TB.TestManagerApi.Services
{
    public interface IExamTypeMetaCommandService
    {
        Task<Guid> CreateExamTypeMetaAsync(CreateExamTypeMetaDto createExamTypeMetaDto);
        Task<Guid> CreateExamTypeSectionAsync(CreateExamTypeSectionDto createExamTypeSectionDto);
        Task<Guid> DeactivateExamTypeMetaAsync(DeactivateExamTypeMetaDto deactivateExamTypeMetaDto);
        Task<Guid> DeactivateExamTypeSectionAsync(DeactivateExamTypeSectionDto deactivateExamTypeSectionDto);
        Task<IEnumerable<Guid>> CreateExamTypeSectionsAsync(IEnumerable<CreateExamTypeSectionDto> createExamTypeSectionDto);
        Task<Guid> UpdateExamTypeMetaAsync(UpdateExamTypeMetaDto updateExamTypeMetaDto);
        Task<Guid> UpdateExamTypeSectionAsync(UpdateExamTypeSectionDto updateExamTypeSectionDto);
        Task<IEnumerable<Guid>> UpdateExamTypeSectionsAsync(List<UpdateExamTypeSectionDto> updateExamTypeSectionsDto);
    }
}