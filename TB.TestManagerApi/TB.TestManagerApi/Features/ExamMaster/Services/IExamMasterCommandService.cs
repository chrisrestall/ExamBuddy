﻿using System;
using System.Threading.Tasks;
using TB.TestManagerApi.Domain;

namespace TB.TestManagerApi.Services
{
    public interface IExamMasterCommandService
    {
        Task<Guid> CreateExamMasterAsync(CreateExamMasterDto createExamMasterDto);
        Task<Guid> DeactivateExamTypeMetaAsync(DeactivateExamMasterDto deactivateExamMasterDto);
        Task<Guid> UpdateExamMasterAsync(UpdateExamMasterDto updateExamMasterDto);
    }
}