using System;
using Dapper;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TB.TestManagerApi.Domain;
using TB.TestManagerApi.Providers;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Caching.Memory;
using Microsoft.Extensions.Configuration;
namespace TB.TestManagerApi.Repository
{
    public class ExamStructureQueries : BaseRepository, IExamStructureQueries
    {
        private readonly ISqlServerConnectionProvider _provider;
        private readonly ILogger<ExamStructureQueries> _logger;
        private readonly IMemoryCache _cache;
        private const string cache_prefix = "examstructure_";
        public ExamStructureQueries(ILogger<ExamStructureQueries> logger, ISqlServerConnectionProvider provider, IMemoryCache memoryCache, IConfiguration configuration)
        {
            _provider = provider;
            _logger = logger;
            _cache = memoryCache;
            SetCacheOptions(configuration);
        }

        #region Exam Masters
        public async Task<ExamMaster> GetExamMasterById(Guid examMasterId)
        {
            const string fetch_key = "fetch_exam_master_examid";
            string cacheKey = string.Concat(cache_prefix, fetch_key, examMasterId.ToString());
            ExamMaster cachedExamMaster;
            try
            {
                if (!_cache.TryGetValue(cacheKey, out cachedExamMaster))
                {
                    ExamMaster examMaster;
                    using (var connection = _provider.GetDbConnection())
                    {
                        var parameters = new { examMasterId = examMasterId };
                        var query = "select * from ExamMaster ";
                        query += "where Id = @examMasterId ";
                        query += "order by createDate";
                        _logger.LogDebug(query);

                        examMaster = await connection.QueryFirstAsync<ExamMaster>(query, parameters).ConfigureAwait(false);
                        _cache.Set(cacheKey, examMaster, GetCacheOptions);
                        return examMaster;
                    }
                }
                _logger.LogDebug("Exam Master retrieved from cache by id");
                return cachedExamMaster;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex.ToString());
                throw;
            }
        }
        public async Task<IEnumerable<ExamMaster>> GetExamMastersByUserId(string userId)
        {
            const string fetch_key = "fetch_exam_master_userid";
            string cacheKey = string.Concat(cache_prefix, fetch_key, userId.ToString());
            IEnumerable<ExamMaster> cachedExamMasters;
            try
            {
                if (!_cache.TryGetValue(cacheKey, out cachedExamMasters))
                {
                    IEnumerable<ExamMaster> examMasters;
                    using (var connection = _provider.GetDbConnection())
                    {
                        var parameters = new { userId = userId };
                        var query = "select * from ExamMaster ";
                        query += "where userId = @userId ";
                        query += "order by createDate";
                        _logger.LogDebug(query);

                        examMasters = await connection.QueryAsync<ExamMaster>(query, parameters).ConfigureAwait(false);
                        _cache.Set(cacheKey, examMasters, GetCacheOptions);
                        return examMasters;
                    }
                }
                _logger.LogDebug("Exam Masters retrieved from cache by user id");
                return cachedExamMasters;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex.ToString());
                throw;
            }
        }
        public async Task<IEnumerable<ExamMaster>> GetExamMastersByExamTypeId(Guid examTypeId)
        {
            const string fetch_key = "fetch_exam_master_examtypeid";
            string cacheKey = string.Concat(cache_prefix, fetch_key, examTypeId.ToString());
            IEnumerable<ExamMaster> cachedExamMasters;
            try
            {
                if (!_cache.TryGetValue(cacheKey, out cachedExamMasters))
                {
                    IEnumerable<ExamMaster> examMasters;
                    using (var connection = _provider.GetDbConnection())
                    {
                        var parameters = new { examTypeId = examTypeId };
                        var query = "select * from ExamMaster ";
                        query += "where testTypeId = @examTypeId ";
                        query += "order by createDate";
                        _logger.LogDebug(query);

                        examMasters = await connection.QueryAsync<ExamMaster>(query, parameters).ConfigureAwait(false);
                        _cache.Set(cacheKey, examMasters, GetCacheOptions);
                        return examMasters;
                    }
                }
                _logger.LogDebug("Exam Masters retrieved from cache by user id");
                return cachedExamMasters;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex.ToString());
                throw;
            }
        }
        public async Task<IEnumerable<ExamMaster>> GetExamMasters()
        {
            const string fetch_key = "fetch_exam_masters";
            string cacheKey = string.Concat(cache_prefix, fetch_key);
            IEnumerable<ExamMaster> cachedExamMasters;
            try
            {
                if (!_cache.TryGetValue(cacheKey, out cachedExamMasters))
                {
                    IEnumerable<ExamMaster> examMasters;
                    using (var connection = _provider.GetDbConnection())
                    {
                        var query = "select * from ExamMaster ";
                        query += "order by createDate";
                        _logger.LogDebug(query);

                        examMasters = await connection.QueryAsync<ExamMaster>(query).ConfigureAwait(false);
                        _cache.Set(cacheKey, examMasters, GetCacheOptions);
                        return examMasters;
                    }
                }
                _logger.LogDebug("Exam Masters retrieved from cache ");
                return cachedExamMasters;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex.ToString());
                throw;
            }
        }
        #endregion

        //do joins to pull in xref and question/answer stuff on these
        public async Task<IEnumerable<ExamQuestion>> GetExamQuestionsByExamMasterId(Guid examMasterId)
        {
            try
            {
                using (var connection = _provider.GetDbConnection())
                {
                    var parameters = new { examMasterId };
                    var query = "select eq.*, eqx.*, eqm.*, eqa.* FROM ExamQuestions eq ";
                    query += "LEFT JOIN ExamQuestionAnswerXRef eqx ON eq.testQuestionAnswerXrefId = eqx.id ";
                    query += "LEFT JOIN ExamQuestionMaster eqm on eqx.testQuestionId = eqm.id ";
                    query += "LEFT JOIN ExamAnswerMaster eqa on eqx.testAnswerId = eqa.id ";
                    query += "where eq.testMasterID = @examMasterId ";

                    _logger.LogDebug(query);
                    List<ExamQuestion> examQuestions = (List<ExamQuestion>)await connection.QueryAsync<ExamQuestion, ExamQuestionAnswerXref, ExamQuestionMaster, ExamAnswerMaster, ExamQuestion>(query,
                    (eq, eqx, eqm, eqa) =>
                    {
                        eq.ExamAnswerMaster = eqa;
                        eq.ExamQuestionMaster = eqm;
                        eq.ExamQuestionAnswerXref = eqx;
                        return eq;
                    },
                    parameters
                    );
                    return examQuestions;
                }
            }
            catch (Exception ex)
            {
                _logger.LogError(ex.ToString());
                throw;
            }
        }
        public async Task<ExamQuestion> GetExamQuestionById(Guid examQuestionId)
        {
            try
            {
                using (var connection = _provider.GetDbConnection())
                {
                    var parameters = new { examQuestionId };
                    var query = "select eq.*, eqx.*, eqm.*, eqa.* FROM ExamQuestions eq ";
                    query += "LEFT JOIN ExamQuestionAnswerXRef eqx ON eq.testQuestionAnswerXrefId = eqx.id ";
                    query += "LEFT JOIN ExamQuestionMaster eqm on eqx.testQuestionId = eqm.id ";
                    query += "LEFT JOIN ExamAnswerMaster eqa on eqx.testAnswerId = eqa.id ";
                    query += "where eq.id = @examQuestionId ";

                    _logger.LogDebug(query);
               
                    List<ExamQuestion> examQuestions = (List<ExamQuestion> )await connection.QueryAsync<ExamQuestion, ExamQuestionAnswerXref, ExamQuestionMaster, ExamAnswerMaster, ExamQuestion>(query,
                    (eq, eqx, eqm, eqa) =>
                    {
                        eq.ExamAnswerMaster = eqa;
                        eq.ExamQuestionMaster = eqm;
                        eq.ExamQuestionAnswerXref = eqx;
                        return eq;
                    },
                    parameters
                    );
                    return examQuestions.First();
                }
            }
            catch (Exception ex)
            {
                _logger.LogError(ex.ToString());
                throw;
            }
        }

        //fIX
        public async Task<IEnumerable<ExamQuestion>> GetExamQuestionAnswersByQuestionMasterId(Guid examQuestionMasterId)
        {
            try
            {
                using (var connection = _provider.GetDbConnection())
                {

                    var parameters = new { examQuestionMasterId };
                    var query = "select eq.*, eqx.*, eqm.*, eqa.* FROM ExamQuestions eq ";
                    query += "LEFT JOIN ExamQuestionAnswerXRef eqx ON eq.testQuestionAnswerXrefId = eqx.id ";
                    query += "LEFT JOIN ExamQuestionMaster eqm on eqx.testQuestionId = eqm.id ";
                    query += "LEFT JOIN ExamAnswerMaster eqa on eqx.testAnswerId = eqa.id ";
                    query += "where eqx.testQuestionId = @examQuestionMasterId ";

                    _logger.LogDebug(query);

                    List<ExamQuestion> examQuestions = (List<ExamQuestion>)await connection.QueryAsync<ExamQuestion, ExamQuestionAnswerXref, ExamQuestionMaster, ExamAnswerMaster, ExamQuestion>(query,
                    (eq, eqx, eqm, eqa) =>
                    {
                        eq.ExamAnswerMaster = eqa;
                        eq.ExamQuestionMaster = eqm;
                        eq.ExamQuestionAnswerXref = eqx;
                        return eq;
                    },
                    parameters
                    );
                    return examQuestions;
                }
            }
            catch (Exception ex)
            {
                _logger.LogError(ex.ToString());
                throw;
            }
        }

    }
}
