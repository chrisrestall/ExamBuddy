using AutoMapper;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TB.TestManagerApi.Domain;

namespace TB.TestManagerApi.Services
{
    public class ExamTypeMetaQueryService : IExamTypeMetaQueryService
    {
        private readonly IExamTypeMetaManager _examTypeMetaManagerService;
        private readonly ILogger<ExamTypeMetaQueryService> _logger;
        private readonly IMapper _mapper;

        public ExamTypeMetaQueryService(ILogger<ExamTypeMetaQueryService> logger, IExamTypeMetaManager examTypeMetaManagerService, IMapper mapper)
        {
            _logger = logger;
            _examTypeMetaManagerService = examTypeMetaManagerService;
            _mapper = mapper;
        }
        public async Task<ExamTypeMetaDto> GetExamTypeMetaByIdAsync(Guid testTypeMetaId, bool withSections, bool activeOnly)
        {
            try
            {
                var examMeta = await _examTypeMetaManagerService.FetchExamTypeMetaById(testTypeMetaId, withSections, activeOnly);
                var examMetaDto = _mapper.Map<ExamTypeMetaDto>(examMeta);
                return examMetaDto;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex.ToString());
                throw;
            }
        }
        public async Task<IEnumerable<ExamTypeMetaDto>> GetExamTypeMetasAsync(int page, int pageSize, bool withSections, bool activeOnly)
        {
            try
            {
                var examMetas = await _examTypeMetaManagerService.FetchExamMetas(page, pageSize, withSections, activeOnly);
                var examMetasDto = _mapper.Map<List<ExamTypeMetaDto>>(examMetas);
                return examMetasDto;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex.ToString());
                throw;
            }
        }
    }
}
