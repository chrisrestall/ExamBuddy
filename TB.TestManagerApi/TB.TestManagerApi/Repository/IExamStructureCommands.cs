using System;
using System.Threading.Tasks;
using TB.TestManagerApi.Domain;

namespace TB.TestManagerApi.Repository
{
    public interface IExamStructureCommands
    {
        Task<Guid> CreateExamMaster(ExamMaster examMaster);
    }
}