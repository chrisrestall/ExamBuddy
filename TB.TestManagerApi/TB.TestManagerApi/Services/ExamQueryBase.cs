using AutoMapper;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TB.TestManagerApi.Domain;

namespace TB.TestManagerApi.Services
{
    public class ExamQueryBase
    {
        private readonly IExamTypeMetaManager _examTypeMetaManagerService;
        private readonly IMapper _mapper;
        private readonly ILogger _logger;
        public ExamQueryBase(IExamTypeMetaManager examTypeMetaManagerService, IMapper mapper, ILogger logger)
        {
            _examTypeMetaManagerService = examTypeMetaManagerService;
            _mapper = mapper;
            _logger = logger;
        }
        
        public async Task<IEnumerable<ExamMasterDto>> AddExamTypes(IEnumerable<ExamMaster> examMasters)
        {
            List<ExamMasterDto> examMastersDto = new List<ExamMasterDto>();
            ExamTypeMeta examTypeMeta = null;
            ExamMasterDto examMasterDto = null;
            try
            {
                examMastersDto = _mapper.Map<List<ExamMasterDto>>(examMasters);
                foreach (var examMaster in examMasters)
                {
                    examTypeMeta = await _examTypeMetaManagerService.FetchExamTypeMetaById(examMaster.TestTypeId, true, false).ConfigureAwait(false);
                    examMasterDto = examMastersDto.SingleOrDefault(p => p.Id == examMaster.Id);
                    examMasterDto.ExamType = _mapper.Map<ExamTypeMetaDto>(examTypeMeta);
                }
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
