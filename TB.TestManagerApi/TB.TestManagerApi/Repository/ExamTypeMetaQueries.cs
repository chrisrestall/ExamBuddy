using Dapper;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using TB.TestManagerApi.Domain;
using TB.TestManagerApi.Providers;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Caching.Memory;
using Microsoft.Extensions.Configuration;
namespace TB.TestManagerApi.Repository
{
    public class ExamTypeMetaQueries : BaseRepository, IExamTypeMetaQueries
    {
        private readonly ISqlServerConnectionProvider _provider;
        private readonly ILogger<ExamTypeMetaQueries> _logger;
        private readonly IMemoryCache _cache;
        private IConfiguration _configuration;
        private const string cache_prefix = "exammeta_";

        public ExamTypeMetaQueries(ILogger<ExamTypeMetaQueries> logger, ISqlServerConnectionProvider provider, IMemoryCache memoryCache, IConfiguration configuration)
        {
            _provider = provider;
            _logger = logger;
            _cache = memoryCache;
            _configuration = configuration;
            SetCacheOptions(configuration);
        }
        public async Task<IEnumerable<ExamTypeMeta>> GetExamMetas(int page, int pageSize, bool withSections)
        {
            const string fetch_key = "fetch_exam_metas_";
            string cacheKey = string.Concat(cache_prefix, fetch_key);
            IEnumerable<ExamTypeMeta> cachedTestTypeMetas;
            try
            {
                if (!_cache.TryGetValue(cacheKey, out cachedTestTypeMetas))
                {
                    IEnumerable<ExamTypeMeta> testTypeMetas;
                    using (var connection = _provider.GetDbConnection())
                    {
                        var parameters = new { Skip = (page - 1) * pageSize, Take = pageSize };
                        string query = "select * from ExamTypeMeta ";
                        query += "order by createDate ";

                        if (page != 0)
                        {
                            query += "OFFSET @Skip ROWS ";
                            query += "FETCH NEXT @Take ROWS ONLY";
                        }
                        _logger.LogDebug(query);

                        testTypeMetas = await connection.QueryAsync<ExamTypeMeta>(query, parameters) as List<ExamTypeMeta>;
                    }
                    if (testTypeMetas != null && withSections)
                    {
                        foreach (var testTypeMeta in testTypeMetas)
                        {
                            testTypeMeta.TestTypeSections = (List<ExamTypeSection>)await GetExamTypeSections(testTypeMeta.Id).ConfigureAwait(false);
                        }
                    }
                    _cache.Set(cacheKey, testTypeMetas, GetCacheOptions);
                    return testTypeMetas;
                }
                _logger.LogDebug("Test type metas retrieved from cache");
                return cachedTestTypeMetas;
                                
            }
            catch (Exception ex)
            {
                _logger.LogError(ex.ToString());
                throw;
            }
        }
        public async Task<ExamTypeMeta> GetExamMetaById(Guid testTypeMetaId, bool withSections)
        {            
            const string fetch_key = "fetch_exam_meta_id";
            string cacheKey = string.Concat(cache_prefix, fetch_key, testTypeMetaId.ToString());
            ExamTypeMeta cachedTestTypeMeta;
            try
            {
                if (!_cache.TryGetValue(cacheKey, out cachedTestTypeMeta))
                {
                    ExamTypeMeta testTypeMeta;
                    using (var connection = _provider.GetDbConnection())
                    {
                        var parameters = new { testTypeMetaId = testTypeMetaId };
                        var query = "select * from ExamTypeMeta ";
                        query += "where Id = @testTypeMetaId ";
                        query += "order by createDate";
                        _logger.LogDebug(query);

                        testTypeMeta = await connection.QueryFirstAsync<ExamTypeMeta>(query, parameters).ConfigureAwait(false);
                    }
                    if (testTypeMeta != null && withSections)
                    {
                        testTypeMeta.TestTypeSections = (List<ExamTypeSection>)await GetExamTypeSections(testTypeMeta.Id).ConfigureAwait(false);
                    }
                    _cache.Set(cacheKey, testTypeMeta, GetCacheOptions);
                    return testTypeMeta;
                }               
                _logger.LogDebug("Test type meta retrieved from cache by id");
                return cachedTestTypeMeta;
                
            }
            catch (Exception ex)
            {
                _logger.LogError(ex.ToString());
                throw;
            }
        }
        public async Task<IEnumerable<ExamTypeSection>> GetExamTypeSections(Guid testTypeMetaId)
        {
            const string fetch_key = "fetch_exam_sections_";
            string cacheKey = string.Concat(cache_prefix, fetch_key);
            IEnumerable<ExamTypeSection> cachedTestTypeSections;
            try
            {
                if (!_cache.TryGetValue(cacheKey, out cachedTestTypeSections))
                {
                    IEnumerable<ExamTypeSection> examTypeSections;
                    using (var connection = _provider.GetDbConnection())
                    {
                        var parameters = new { testTypeMetaId = testTypeMetaId };

                        var query = "select * from ExamTypeSection ";
                        query += "where testTypeMetaId = @testTypeMetaId ";
                        query += "order by createDate";
                        _logger.LogDebug(query);

                        examTypeSections = await connection.QueryAsync<ExamTypeSection>(query, parameters).ConfigureAwait(false) as List<ExamTypeSection>;
                        _cache.Set(cacheKey, examTypeSections, GetCacheOptions);
                        return examTypeSections;
                    }
                }                
                _logger.LogDebug("Test type sections retrieved from cache");
                return cachedTestTypeSections;
                
            }
            catch (Exception ex)
            {
                _logger.LogError(ex.ToString());
                throw;
            }
        }
        public async Task<ExamTypeSection> GetExamTypeSectionById(Guid testTypeSectionId)
        {
            const string fetch_key = "fetch_exam_section_id";
            string cacheKey = string.Concat(cache_prefix, fetch_key, testTypeSectionId.ToString());
            ExamTypeSection cachedExamTypeSection;
            try
            {
                if (!_cache.TryGetValue(cacheKey, out cachedExamTypeSection))
                {
                    ExamTypeSection examTypeSection;
                    using (var connection = _provider.GetDbConnection())
                    {
                        var parameters = new { testTypeSectionId = testTypeSectionId };

                        var query = "select * from ExamTypeSection ";
                        query += "where Id = @testTypeSectionId ";
                        _logger.LogDebug(query);

                        examTypeSection = await connection.QueryFirstAsync<ExamTypeSection>(query, parameters).ConfigureAwait(false);
                        _cache.Set(cacheKey, examTypeSection, GetCacheOptions);
                        return examTypeSection;
                    }
                }
                _logger.LogDebug("Test type section by ID retrieved from cache");
                return cachedExamTypeSection;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex.ToString());
                throw;
            }
        }
    }
}

