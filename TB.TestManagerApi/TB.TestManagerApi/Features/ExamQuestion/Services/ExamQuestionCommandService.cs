using AutoMapper;
using Microsoft.Extensions.Logging;
using System;
using System.Threading.Tasks;
using TB.TestManagerApi.Domain;
using TB.TestManagerApi.Services;

namespace TB.TestManagerApi.Services
{
    public class ExamQuestionCommandService : IExamQuestionCommandService
    {

        private readonly IExamStructureManager _examStructureManagerService;
        private readonly ILogger<IExamQuestionCommandService> _logger;
        private readonly IMapper _mapper;
        public ExamQuestionCommandService(ILogger<IExamQuestionCommandService> logger, IExamStructureManager examStructureManagerService, IMapper mapper)
        {
            _logger = logger;
            _examStructureManagerService = examStructureManagerService;
            _mapper = mapper;
        }
        public async Task<Guid> CreateExamQuestionAsync(CreateExamQuestionDto createExamQuestionDto)
        {
            try
            {
                CreateExamQuestion ceq = _mapper.Map<CreateExamQuestion>(createExamQuestionDto);
                return await _examStructureManagerService.CreateExamQuestion(ceq).ConfigureAwait(false);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex.ToString());
                throw;
            }
        }        
    }
}
