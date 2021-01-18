using AutoMapper;
using Microsoft.Extensions.Logging;
using System;
using System.Threading.Tasks;
using TB.TestManagerApi.Domain;


namespace TB.TestManagerApi.Services
{
    public class ExamMasterCommandService : IExamMasterCommandService
    {
        private readonly IExamStructureManager _examStructureManagerService;
        private readonly ILogger<ExamMasterCommandService> _logger;
        private readonly IMapper _mapper;


        public ExamMasterCommandService(ILogger<ExamMasterCommandService> logger, IExamStructureManager examStructureManagerService, IMapper mapper)
        {
            _logger = logger;
            _examStructureManagerService = examStructureManagerService;
            _mapper = mapper;
        }

        public async Task<Guid> CreateExamMasterAsync(CreateExamMasterDto createExamMasterDto)
        {
            try
            {
                ExamMaster examMaster = _mapper.Map<ExamMaster>(createExamMasterDto);
                return await _examStructureManagerService.CreateExamMaster(examMaster).ConfigureAwait(false);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex.ToString());
                throw;
            }
        }
        public async Task<Guid> UpdateExamMasterAsync(UpdateExamMasterDto updateExamMasterDto)
        {
            try
            {
                ExamMaster examMaster = _mapper.Map<ExamMaster>(updateExamMasterDto);
                return await _examStructureManagerService.UpdateExamMaster(examMaster).ConfigureAwait(false);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex.ToString());
                throw;
            }
        }
        public async Task<Guid> DeactivateExamTypeMetaAsync(DeactivateExamMasterDto deactivateExamMasterDto)
        {
            try
            {
                ExamMaster examMaster = _mapper.Map<ExamMaster>(deactivateExamMasterDto);
                return await _examStructureManagerService.DeactivateExamMaster(examMaster).ConfigureAwait(false);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex.ToString());
                throw;
            }
        }
    }
}