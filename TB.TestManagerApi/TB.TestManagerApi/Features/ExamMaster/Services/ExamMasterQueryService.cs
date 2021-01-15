using AutoMapper;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TB.TestManagerApi.Domain;

namespace TB.TestManagerApi.Services
{
    public class ExamMasterQueryService : ExamQueryBase, IExamMasterQueryService
    {
        private readonly IExamTypeMetaManager _examTypeMetaManagerService;
        private readonly IExamStructureManager _examStructureManagerService;
        private readonly ILogger<ExamMasterQueryService> _logger;
        private readonly IMapper _mapper;

        public ExamMasterQueryService(ILogger<ExamMasterQueryService> logger, IExamTypeMetaManager examTypeMetaManagerService, IExamStructureManager examStructureManagerService, IMapper mapper) : base(examTypeMetaManagerService, mapper, logger)
        {
            _logger = logger;
            _examTypeMetaManagerService = examTypeMetaManagerService;
            _examStructureManagerService = examStructureManagerService;
            _mapper = mapper;
        }

        public async Task<ExamMasterDto> GetExamMasterByIdAsync(Guid examMasterId, bool activeOnly)
        {
            ExamMasterDto examMasterDto = null;
            try
            {
                var examMaster = await _examStructureManagerService.FetchExamMasterById(examMasterId, activeOnly).ConfigureAwait(false);
                if (examMaster != null)
                {
                    var examTypeMeta = await _examTypeMetaManagerService.FetchExamTypeMetaById(examMaster.TestTypeId, true, true).ConfigureAwait(false);
                    examMasterDto = _mapper.Map<ExamMasterDto>(examMaster);
                    var examTypeMetaDto = _mapper.Map<ExamTypeMetaDto>(examTypeMeta);
                    examMasterDto.ExamType = examTypeMetaDto;
                }
                return examMasterDto;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex.ToString());
                throw;
            }
        }

        public async Task<IEnumerable<ExamMasterDto>> GetExamMastersByUserIdAsync(string userId, bool activeOnly)
        {
            try
            {
                var examMasters = await _examStructureManagerService.FetchExamMasterByUserId(userId, activeOnly).ConfigureAwait(false);
                var examMastersDto = await AddExamTypes(examMasters).ConfigureAwait(false);
                return examMastersDto;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex.ToString());
                throw;
            }
        }

        public async Task<IEnumerable<ExamMasterDto>> GetExamMastersByExamTypeIdAsync(Guid examTypeId, bool activeOnly)
        {
            try
            {
                var examMasters = await _examStructureManagerService.FetchExamMastersByExamTypeId(examTypeId, activeOnly).ConfigureAwait(false);
                var examMastersDto = await AddExamTypes(examMasters).ConfigureAwait(false);
                return examMastersDto;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex.ToString());
                throw;
            }
        }
        public async Task<IEnumerable<ExamMasterDto>> GetExamMastersAsync(bool activeOnly)
        {
            try
            {
                var examMasters = await _examStructureManagerService.FetchExamMasters(activeOnly).ConfigureAwait(false);
                var examMastersDto = await AddExamTypes(examMasters).ConfigureAwait(false);
                return examMastersDto;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex.ToString());
                throw;
            }
        }
    }
}
