using Dapper;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using TB.TestManagerApi.Domain;
using TB.TestManagerApi.Providers;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Caching.Memory;
using Microsoft.Extensions.Configuration;
using System.Linq;
using System.Transactions;

namespace TB.TestManagerApi.Repository
{
    public class ExamTypeMetaCommands : BaseRepository, IExamTypeMetaCommands
    {
        private readonly ISqlServerConnectionProvider _provider;
        private readonly ILogger<ExamTypeMetaCommands> _logger;
        private readonly IMemoryCache _cache;
        private IConfiguration _configuration;

        public ExamTypeMetaCommands(ILogger<ExamTypeMetaCommands> logger, ISqlServerConnectionProvider provider, IMemoryCache memoryCache, IConfiguration configuration)
        {
            _provider = provider;
            _logger = logger;
            _cache = memoryCache;
            _configuration = configuration;
            SetCacheOptions(configuration);
        }

        public async Task<Guid> CreateExamTypeMeta(CreateExamTypeMeta examTypeMeta)
        {
            Guid result;
            try
            {
                using (var transaction = new TransactionScope(TransactionScopeOption.Required, TransactionScopeAsyncFlowOption.Enabled))
                {
                    using (var connection = _provider.GetDbConnection())
                    {
                        var command = @"INSERT INTO [ExamTypeMeta] ([name], [description], [active]) ";
                        command += "OUTPUT INSERTED.Id ";
                        command += "VALUES (@name, @description, @active);";
                        _logger.LogDebug(command);
                        result = await connection.QuerySingleAsync<Guid>(command, examTypeMeta).ConfigureAwait(false);

                        if (examTypeMeta.TestTypeSections.Any())
                        {
                            foreach (var item in examTypeMeta.TestTypeSections)
                            {
                                item.TestTypeMetaId = result;
                            }
                            _logger.LogDebug("handle ExamTypeSections as well");
                            await CreateExamTypeSections(examTypeMeta.TestTypeSections).ConfigureAwait(false);
                        }
                        transaction.Complete();
                    }
                }
                return result;

            }
            catch (Exception ex)
            {
                _logger.LogError(ex.ToString());
                throw;
            }
        }

        public async Task<Guid> UpdateExamTypeMeta(UpdateExamTypeMeta examTypeMeta)
        {
            Guid result;
            try
            {
                using (var transaction = new TransactionScope(TransactionScopeOption.Required, TransactionScopeAsyncFlowOption.Enabled))
                {
                    using (var connection = _provider.GetDbConnection())
                    {
                        var command = @"UPDATE [ExamTypeMeta] SET [name] = @name, [description] = @description, [active] = @active, [editDate] = @editDate ";
                        command += "WHERE id = @id";

                        _logger.LogDebug(command);
                        await connection.ExecuteAsync(command, examTypeMeta).ConfigureAwait(false);
                        result = examTypeMeta.Id;

                        if (examTypeMeta.TestTypeSections.Any())
                        {
                            _logger.LogDebug("handle UPDATE of ExamTypeSections as well");
                            await UpdateExamTypeSections(examTypeMeta.TestTypeSections).ConfigureAwait(false);
                        }
                        transaction.Complete();
                    }
                }
                return result;

            }
            catch (Exception ex)
            {
                _logger.LogError(ex.ToString());
                throw;
            }
        }
        public async Task<Guid> DeactivateExamTypeMeta(DeactivateExamTypeMeta examTypeMeta)
        {
            Guid result;
            try
            {
                using (var transaction = new TransactionScope(TransactionScopeOption.Required, TransactionScopeAsyncFlowOption.Enabled))
                {
                    using (var connection = _provider.GetDbConnection())
                    {
                        var command = @"UPDATE [ExamTypeMeta] SET [active] = 0, [editDate] = @editDate ";
                        command += "WHERE id = @id";

                        _logger.LogDebug(command);
                        await connection.ExecuteAsync(command, examTypeMeta).ConfigureAwait(false);
                        result = examTypeMeta.Id;                        
                        transaction.Complete();
                    }
                }
                return result;

            }
            catch (Exception ex)
            {
                _logger.LogError(ex.ToString());
                throw;
            }
        }

        public async Task<IEnumerable<Guid>> CreateExamTypeSections(List<CreateExamTypeSection> examTypeSections)
        {
            List<Guid> results = new List<Guid>();
            try
            {
                using (var transaction = new TransactionScope(TransactionScopeOption.Required, TransactionScopeAsyncFlowOption.Enabled))
                {
                    using (var connection = _provider.GetDbConnection())
                    {
                        foreach (var examTypeSection in examTypeSections)
                        {
                            var command = @"INSERT INTO [ExamTypeSection] ([testTypeMetaId], [name], [description], [active]) ";
                            command += "OUTPUT INSERTED.Id ";
                            command += "VALUES (@testTypeMetaId, @name, @description, @active); ";
                            _logger.LogDebug(command);

                            var result = await connection.QuerySingleAsync<Guid>(command, examTypeSection).ConfigureAwait(false);
                            results.Add(result);
                        }
                        transaction.Complete();
                    }
                }
                return results;

            }
            catch (Exception ex)
            {
                _logger.LogError(ex.ToString());
                throw;
            }
        }

        public async Task<IEnumerable<Guid>> UpdateExamTypeSections(List<UpdateExamTypeSection> examTypeSections)
        {
            List<Guid> results = new List<Guid>();
            try
            {
                using (var transaction = new TransactionScope(TransactionScopeOption.Required, TransactionScopeAsyncFlowOption.Enabled))
                {
                    using (var connection = _provider.GetDbConnection())
                    {
                        foreach (var examTypeSection in examTypeSections)
                        {
                            var command = @"UPDATE [ExamTypeSection] SET [testTypeMetaId] = @testTypeMetaId, [Name] = @name, [Description] = @description, [Active] = @active, [EditDate] = @editDate ";
                             command += "WHERE id = @id";
                            _logger.LogDebug(command);
                            
                            //Implicit that it succeeeded
                            await connection.ExecuteAsync(command, examTypeSection).ConfigureAwait(false);
                            results.Add(examTypeSection.Id);
                        }
                        transaction.Complete();
                    }
                }
                return results;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex.ToString());
                throw;
            }
        }

        public async Task<IEnumerable<Guid>> DeactivateExamTypeSections(List<DeactivateExamTypeSection> examTypeSections)
        {
            List<Guid> results = new List<Guid>();
            try
            {
                using (var transaction = new TransactionScope(TransactionScopeOption.Required, TransactionScopeAsyncFlowOption.Enabled))
                {
                    using (var connection = _provider.GetDbConnection())
                    {
                        foreach (var examTypeSection in examTypeSections)
                        {
                            var command = @"UPDATE [ExamTypeSection] SET [Active] = 0, [EditDate] = @editDate ";
                            command += "WHERE id = @id";
                            _logger.LogDebug(command);

                            //Implicit that it succeeeded
                            await connection.ExecuteAsync(command, examTypeSection).ConfigureAwait(false);
                            results.Add(examTypeSection.Id);
                        }
                        transaction.Complete();
                    }
                }
                return results;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex.ToString());
                throw;
            }
        }
    }
}