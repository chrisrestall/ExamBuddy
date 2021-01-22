using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TB.TestManagerApi.Domain;
using TB.TestManagerApi.Repository;

namespace TB.TestManagerApi.Services
{
    public class ExamStructureManager : IExamStructureManager
    {
        private IExamStructureQueries _examStructureQueries;
        private IExamStructureCommands _examStructureCommands;
        private readonly ILogger<ExamStructureManager> _logger;
       
        public ExamStructureManager(IExamStructureQueries examStructureQueries, IExamStructureCommands examStructureCommands, ILogger<ExamStructureManager> logger)
        {
            _examStructureQueries = examStructureQueries;
            _examStructureCommands = examStructureCommands;
            _logger = logger;          
        }

        public async Task<ExamMaster> FetchExamMasterById(Guid examMasterId, bool activeOnly)
        {            
            try
            {
                var examMaster = await _examStructureQueries.GetExamMasterById(examMasterId).ConfigureAwait(false);
                if (activeOnly && !examMaster?.Active == true) return null;              
                return examMaster;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex.ToString());
                throw;
            }
        }
        public async Task<IEnumerable<ExamMaster>> FetchExamMasterByUserId(string userId, bool activeOnly)
        {            
            try
            {
                var examMasters = (List<ExamMaster>)await _examStructureQueries.GetExamMastersByUserId(userId).ConfigureAwait(false);
                if (activeOnly) examMasters = examMasters.FindAll(p => p.Active).ToList();
                return examMasters;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex.ToString());
                throw;
            }
        }
        public async Task<IEnumerable<ExamMaster>> FetchExamMastersByExamTypeId(Guid examTypeId, bool activeOnly)
        {            
            try
            {
                var examMasters = (List<ExamMaster>)await _examStructureQueries.GetExamMastersByExamTypeId(examTypeId).ConfigureAwait(false);
                if (activeOnly) examMasters = examMasters.FindAll(p => p.Active).ToList();
                return examMasters;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex.ToString());
                throw;
            }
        }
        public async Task<IEnumerable<ExamMaster>> FetchExamMasters(bool activeOnly)
        {            
            try
            {
                var examMasters = (List<ExamMaster>)await _examStructureQueries.GetExamMasters().ConfigureAwait(false);
                if (activeOnly) examMasters = examMasters.FindAll(p => p.Active).ToList();
                return examMasters;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex.ToString());
                throw;
            }
        }

        
        public async Task<Guid> CreateExamQuestion(CreateExamQuestion createExamQuestion)
        {
            try
            {
                var result = await _examStructureCommands.CreateExamQuestionMaster(createExamQuestion).ConfigureAwait(false);
                return result;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex.ToString());
                throw;
            }
        }

        public async Task<Guid> UpdateExamQuestion(UpdateExamQuestionMaster updateExamQuestionMaster)
        {
            try
            {
                var result = await _examStructureCommands.UpdateExamQuestionMaster(updateExamQuestionMaster).ConfigureAwait(false);
                return result;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex.ToString());
                throw;
            }
        }

        public async Task<Guid> UpdateExamAnswer(UpdateExamAnswerMaster updateExamAnswerMaster)
        {
            try
            {
                var result = await _examStructureCommands.UpdateExamAnswerMaster(updateExamAnswerMaster).ConfigureAwait(false);
                return result;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex.ToString());
                throw;
            }
        }
        public async Task<Guid> CreateExamAnswer(CreateExamAnswer createExamAnswer)
        {
            try
            {
                var result = await _examStructureCommands.CreateExamAnswer(createExamAnswer).ConfigureAwait(false);
                return result;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex.ToString());
                throw;
            }
        }
        public async Task<Guid> CreateExamMaster(CreateExamMaster createExamMaster)
        {
            try
            {
                var result = await _examStructureCommands.CreateExamMaster(createExamMaster).ConfigureAwait(false);
                return result;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex.ToString());
                throw;
            }
        }
        public async Task<Guid> UpdateExamMaster(UpdateExamMaster updateExamMaster)
        {
            try
            {
                var result = await _examStructureCommands.UpdateExamMaster(updateExamMaster).ConfigureAwait(false);
                return result;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex.ToString());
                throw;
            }
        }
        public async Task<Guid> DeactivateExamMaster(DeactivateExamMaster deactivateExamMaster)
        {
            try
            {
                var result = await _examStructureCommands.DeactivateExamMaster(deactivateExamMaster).ConfigureAwait(false);
                return result;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex.ToString());
                throw;
            }
        }

        public async Task<IEnumerable<ExamQuestion>> FetchExamQuestionAnswersByQuestionMasterId(Guid examQuestionMasterId, bool activeOnly)
        {
            try
            {
                var examQuestions = (List<ExamQuestion>)await _examStructureQueries.GetExamQuestionAnswersByQuestionMasterId(examQuestionMasterId).ConfigureAwait(false);
                if (activeOnly) examQuestions = examQuestions.FindAll(p => p.Active == true).ToList();
                return examQuestions;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex.ToString());
                throw;
            }
        }
        public async Task<ExamQuestion> FetchExamQuestionById(Guid examQuestionId, bool activeOnly)
        {            
            try
            {
               var examQuestion = await _examStructureQueries.GetExamQuestionById(examQuestionId).ConfigureAwait(false);
                if (activeOnly && !examQuestion?.Active == true) return null;
                return examQuestion;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex.ToString());
                throw;
            }
        }

        public async Task<IEnumerable<ExamQuestion>> FetchExamQuestionsByExamMasterId(Guid examMasterId, bool activeOnly)
        {        
            try
            {
                var examQuestions = (List <ExamQuestion> )await _examStructureQueries.GetExamQuestionsByExamMasterId(examMasterId).ConfigureAwait(false);
                if (activeOnly) examQuestions = examQuestions.FindAll(p => p.Active == true).ToList();
                return examQuestions;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex.ToString());
                throw;
            }
        }

        //IExamStructureCommands

    }
}
