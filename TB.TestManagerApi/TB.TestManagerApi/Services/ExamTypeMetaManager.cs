using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
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

        public async Task<Guid> UpdateExamTypeMeta(UpdateExamTypeMeta updateExamTypeMeta)
        {
            try
            {
                var result = await _examTypeMetaCommands.UpdateExamTypeMeta(updateExamTypeMeta).ConfigureAwait(false);
                return result;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex.ToString());
                throw;
            }
        }
        public async Task<Guid> DeactivateExamTypeMeta(DeactivateExamTypeMeta deactivateExamTypeMeta)
        {
            try
            {
                var result = await _examTypeMetaCommands.DeactivateExamTypeMeta(deactivateExamTypeMeta).ConfigureAwait(false);
                return result;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex.ToString());
                throw;
            }
        }
        public async Task<Guid> CreateExamTypeMeta(CreateExamTypeMeta createExamTypeMeta)
        {
            try
            {
                var result = await _examTypeMetaCommands.CreateExamTypeMeta(createExamTypeMeta).ConfigureAwait(false);
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
        public async Task<Guid> CreateExamTypeSection(CreateExamTypeSection createExamTypeSection)
        {
            List<CreateExamTypeSection> examTypeSections = new List<CreateExamTypeSection>();
            try
            {
                examTypeSections.Add(createExamTypeSection);
                var sectionGuid =  await CreateExamTypeSections(examTypeSections).ConfigureAwait(false);
                return sectionGuid.FirstOrDefault();
            }
            catch (Exception ex)
            {
                _logger.LogError(ex.ToString());
                throw;
            }
        }
        public async Task<Guid> UpdateExamTypeSection(UpdateExamTypeSection examTypeSection)
        {
            List<UpdateExamTypeSection> examTypeSections = new List<UpdateExamTypeSection>();
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
        public async Task<Guid> DeactivateExamTypeSection(DeactivateExamTypeSection examTypeSection)
        {
            List<DeactivateExamTypeSection> examTypeSections = new List<DeactivateExamTypeSection>();
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
        public async Task<IEnumerable<Guid>> CreateExamTypeSections(List<CreateExamTypeSection> examTypeSections)
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
        public async Task<IEnumerable<Guid>> UpdateExamTypeSections(List<UpdateExamTypeSection> examTypeSections)
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
