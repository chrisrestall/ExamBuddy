using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using Microsoft.Extensions.Logging;
using TB.TestManagerApi.Domain;
using TB.TestManagerApi.Repository;


namespace TB.TestManagerApi.Services
{
    public class ExamTypeMetaManager : IExamTypeMetaManager
    {
        private IExamTypeMetaQueries _examTypeMetaQueries;
        private IExamTypeMetaCommands _examTypeMetaCommands;
        private readonly ILogger<ExamTypeMetaManager> _logger;       
        public ExamTypeMetaManager(IExamTypeMetaQueries examTypeMetaQueries, IExamTypeMetaCommands examTypeMetaCommands, ILogger<ExamTypeMetaManager> logger)
        {
            _examTypeMetaQueries = examTypeMetaQueries;
            _examTypeMetaCommands = examTypeMetaCommands;
            _logger = logger;           
        }

        #region Exam Type Meta
   
        public async Task<IEnumerable<ExamTypeMeta>> FetchExamMetas(int page, int pageSize, bool withSections, bool activeOnly)
        {
            try
            {
                var examMetas = (List<ExamTypeMeta>)await _examTypeMetaQueries.GetExamMetas(page, pageSize, withSections).ConfigureAwait(false);
                if (activeOnly) examMetas = examMetas.FindAll(p => p.Active).ToList();             
                return examMetas;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex.ToString());
                throw;
            }
        }
        public async Task<ExamTypeMeta> FetchExamTypeMetaById(Guid testTypeMetaId, bool withSections, bool activeOnly)
        {           
            try
            {
                var examMeta = await _examTypeMetaQueries.GetExamMetaById(testTypeMetaId, withSections).ConfigureAwait(false);
                if (activeOnly && !examMeta?.Active == true) return null;
                return examMeta;          
            }
            catch (Exception ex)
            {
                _logger.LogError(ex.ToString());
                throw;
            }
        }

        public async Task<Guid> UpdateExamTypeMeta(ExamTypeMeta examTypeMeta)
        {
            try
            {
                var result = await _examTypeMetaCommands.UpdateExamTypeMeta(examTypeMeta).ConfigureAwait(false);
                return result;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex.ToString());
                throw;
            }
        }
        public async Task<Guid> DeactivateExamTypeMeta(ExamTypeMeta examTypeMeta)
        {
            try
            {
                var result = await _examTypeMetaCommands.DeactivateExamTypeMeta(examTypeMeta).ConfigureAwait(false);
                return result;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex.ToString());
                throw;
            }
        }
        public async Task<Guid> CreateExamTypeMeta(ExamTypeMeta examTypeMeta)
        {
            try
            {
                var result = await _examTypeMetaCommands.CreateExamTypeMeta(examTypeMeta).ConfigureAwait(false);
                return result;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex.ToString());
                throw;
            }
        }
        #endregion
        #region Exam Type Sections
        
       
        public async Task<IEnumerable<ExamTypeSection>> FetchExamTypeSectionsByExamMetaId(Guid examTypeMetaId, bool activeOnly)
        {           
            try
            {
                var examTypeSections = (List<ExamTypeSection>)await _examTypeMetaQueries.GetExamTypeSections(examTypeMetaId).ConfigureAwait(false);
                if (activeOnly) examTypeSections = examTypeSections.FindAll(p => p.Active).ToList();
                return examTypeSections;           
            }
            catch (Exception ex)
            {
                _logger.LogError(ex.ToString());
                throw;
            }
        }
        public async Task<ExamTypeSection> FetchExamTypeSectionById(Guid examTypeSectionId, bool activeOnly)
        {           
            try
            {
                var examTypeSection = await _examTypeMetaQueries.GetExamTypeSectionById(examTypeSectionId).ConfigureAwait(false);
                if (activeOnly && !examTypeSection?.Active == true) return null;
                return examTypeSection;        
            }
            catch (Exception ex)
            {
                _logger.LogError(ex.ToString());
                throw;
            }
        }
        public async Task<Guid> CreateExamTypeSection(ExamTypeSection examTypeSection)
        {
            List<ExamTypeSection> examTypeSections = new List<ExamTypeSection>();
            try
            {
                examTypeSections.Add(examTypeSection);
                var sectionGuid =  await CreateExamTypeSections(examTypeSections).ConfigureAwait(false);
                return sectionGuid.FirstOrDefault();
            }
            catch (Exception ex)
            {
                _logger.LogError(ex.ToString());
                throw;
            }
        }
        public async Task<Guid> UpdateExamTypeSection(ExamTypeSection examTypeSection)
        {
            List<ExamTypeSection> examTypeSections = new List<ExamTypeSection>();
            try
            {
                examTypeSections.Add(examTypeSection);
                var sectionGuid = await UpdateExamTypeSections(examTypeSections).ConfigureAwait(false);
                return sectionGuid.FirstOrDefault();
            }
            catch (Exception ex)
            {
                _logger.LogError(ex.ToString());
                throw;
            }
        }
        public async Task<Guid> DeactivateExamTypeSection(ExamTypeSection examTypeSection)
        {
            List<ExamTypeSection> examTypeSections = new List<ExamTypeSection>();
            try
            {
                examTypeSections.Add(examTypeSection);
                var result = (List<Guid>)await _examTypeMetaCommands.DeactivateExamTypeSections(examTypeSections).ConfigureAwait(false);
                return result.FirstOrDefault();
            }
            catch (Exception ex)
            {
                _logger.LogError(ex.ToString());
                throw;
            }
        }
        public async Task<IEnumerable<Guid>> CreateExamTypeSections(List<ExamTypeSection> examTypeSections)
        {
            try
            {                
                var result = (List<Guid>)await _examTypeMetaCommands.CreateExamTypeSections(examTypeSections).ConfigureAwait(false);
                return result;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex.ToString());
                throw;
            }
        }
        public async Task<IEnumerable<Guid>> UpdateExamTypeSections(List<ExamTypeSection> examTypeSections)
        {
            try
            {
                var result = (List<Guid> )await _examTypeMetaCommands.UpdateExamTypeSections(examTypeSections).ConfigureAwait(false);
                return result;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex.ToString());
                throw;
            }
        }

        #endregion
    }
}
