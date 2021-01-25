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
                CreateExamMaster createExamMasterCommand = _mapper.Map<CreateExamMaster>(createExamMasterDto);
                return await _examStructureManagerService.CreateExamMaster(createExamMasterCommand).ConfigureAwait(false);
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
                UpdateExamMaster updateExamMasterCommand = _mapper.Map<UpdateExamMaster>(updateExamMasterDto);
                return await _examStructureManagerService.UpdateExamMaster(updateExamMasterCommand).ConfigureAwait(false);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex.ToString());
                throw;
            }
        }
        public async Task<Guid> DeactivateExamMasterAsync(DeactivateExamMasterDto deactivateExamMasterDto)
        {
            try
            {
                DeactivateExamMaster deactivateExamMasterCommand = _mapper.Map<DeactivateExamMaster>(deactivateExamMasterDto);
                return await _examStructureManagerService.DeactivateExamMaster(deactivateExamMasterCommand).ConfigureAwait(false);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex.ToString());
                throw;
            }
        }
    }
}