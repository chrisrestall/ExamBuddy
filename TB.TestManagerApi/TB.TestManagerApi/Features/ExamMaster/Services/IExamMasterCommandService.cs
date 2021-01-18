using System;
using System.Threading.Tasks;
using TB.TestManagerApi.Domain;

namespace TB.TestManagerApi.Services
{
    public interface IExamMasterCommandService
    {
        Task<Guid> CreateExamMasterAsync(CreateExamMasterDto createExamMasterDto);
    }
}