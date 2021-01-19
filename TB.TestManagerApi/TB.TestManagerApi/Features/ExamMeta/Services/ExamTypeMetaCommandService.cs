using AutoMapper;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TB.TestManagerApi.Domain;

namespace TB.TestManagerApi.Services
{
    public class ExamTypeMetaCommandService : IExamTypeMetaCommandService
    {
        private readonly IExamTypeMetaManager _examTypeMetaManagerService;
        private readonly ILogger<ExamTypeMetaCommandService> _logger;
        private readonly IMapper _mapper;
        public ExamTypeMetaCommandService(ILogger<ExamTypeMetaCommandService> logger, IExamTypeMetaManager examTypeMetaManagerService, IMapper mapper)
        {
            _logger = logger;
            _examTypeMetaManagerService = examTypeMetaManagerService;
            _mapper = mapper;
        }
        public async Task<IEnumerable<Guid>> CreateExamTypeSectionsAsync(IEnumerable<CreateExamTypeSectionDto> createExamTypeSectionDto)
        {
            try
            {
                List<CreateExamTypeSection> ets = _mapper.Map<List<CreateExamTypeSection>>(createExamTypeSectionDto);
                return await _examTypeMetaManagerService.CreateExamTypeSections(ets).ConfigureAwait(false);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex.ToString());
                throw;
            }
        }

        public async Task<Guid> CreateExamTypeSectionAsync(CreateExamTypeSectionDto createExamTypeSectionDto)
        {
            try
            {
                CreateExamTypeSection ets = _mapper.Map<CreateExamTypeSection>(createExamTypeSectionDto);
                return await _examTypeMetaManagerService.CreateExamTypeSection(ets).ConfigureAwait(false);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex.ToString());
                throw;
            }
        }

        public async Task<Guid> CreateExamTypeMetaAsync(CreateExamTypeMetaDto createExamTypeMetaDto)
        {
            try
            {
                CreateExamTypeMeta etm = _mapper.Map<CreateExamTypeMeta>(createExamTypeMetaDto);

                return await _examTypeMetaManagerService.CreateExamTypeMeta(etm).ConfigureAwait(false);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex.ToString());
                throw;
            }
        }
        public async Task<Guid> DeactivateExamTypeMetaAsync(DeactivateExamTypeMetaDto deactivateExamTypeMetaDto)
        {
            try
            {
                DeactivateExamTypeMeta etm = _mapper.Map<DeactivateExamTypeMeta>(deactivateExamTypeMetaDto);
                return await _examTypeMetaManagerService.DeactivateExamTypeMeta(etm).ConfigureAwait(false);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex.ToString());
                throw;
            }
        }

        public async Task<Guid> UpdateExamTypeMetaAsync(UpdateExamTypeMetaDto examTypeMetaDto)
        {
            try
            {
                UpdateExamTypeMeta etm = _mapper.Map<UpdateExamTypeMeta>(examTypeMetaDto);
                return await _examTypeMetaManagerService.UpdateExamTypeMeta(etm).ConfigureAwait(false);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex.ToString());
                throw;
            }
        }
        public async Task<Guid> UpdateExamTypeSectionAsync(UpdateExamTypeSectionDto examTypeSectionDto)
        {
            try
            {
                UpdateExamTypeSection ets = _mapper.Map<UpdateExamTypeSection>(examTypeSectionDto);
                return await _examTypeMetaManagerService.UpdateExamTypeSection(ets).ConfigureAwait(false);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex.ToString());
                throw;
            }
        }
        public async Task<Guid> DeactivateExamTypeSectionAsync(DeactivateExamTypeSectionDto examTypeSectionDto)
        {
            try
            {
                DeactivateExamTypeSection ets = _mapper.Map<DeactivateExamTypeSection>(examTypeSectionDto);
                return await _examTypeMetaManagerService.DeactivateExamTypeSection(ets).ConfigureAwait(false);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex.ToString());
                throw;
            }
        }
        public async Task<IEnumerable<Guid>> UpdateExamTypeSectionsAsync(List<UpdateExamTypeSectionDto> examTypeSectionsDto)
        {
            try
            {
                List<UpdateExamTypeSection> ets = _mapper.Map<List<UpdateExamTypeSection>>(examTypeSectionsDto);
                return await _examTypeMetaManagerService.UpdateExamTypeSections(ets).ConfigureAwait(false);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex.ToString());
                throw;
            }
        }
    }
}
