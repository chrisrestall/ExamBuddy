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
        public async Task<Guid> CreateExamAnswerAsync(CreateExamAnswerDto createExamAnswerDto)
        {
            try
            {
                CreateExamAnswer cea = _mapper.Map<CreateExamAnswer>(createExamAnswerDto);
                return await _examStructureManagerService.CreateExamAnswer(cea).ConfigureAwait(false);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex.ToString());
                throw;
            }
        }

        public async Task<Guid> UpdateExamQuestionMasterAsync(UpdateExamQuestionMasterDto updateExamQuestionMasterDto)
        {
            try
            {
                UpdateExamQuestionMaster ueqm = _mapper.Map<UpdateExamQuestionMaster>(updateExamQuestionMasterDto);
                return await _examStructureManagerService.UpdateExamQuestion(ueqm).ConfigureAwait(false);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex.ToString());
                throw;
            }
        }

        public async Task<Guid> UpdateExamAnswerMasterAsync(UpdateExamAnswerMasterDto updateExamAnswerMasterDto)
        {
            try
            {
                UpdateExamAnswerMaster uea = _mapper.Map<UpdateExamAnswerMaster>(updateExamAnswerMasterDto);
                return await _examStructureManagerService.UpdateExamAnswer(uea).ConfigureAwait(false);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex.ToString());
                throw;
            }
        }
    }
}
