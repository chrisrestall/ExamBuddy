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
    public class ExamStructureCommands : BaseRepository, IExamStructureCommands
    {
        private readonly ISqlServerConnectionProvider _provider;
        private readonly ILogger<ExamStructureCommands> _logger;
        private readonly IMemoryCache _cache;       

        public ExamStructureCommands(ILogger<ExamStructureCommands> logger, ISqlServerConnectionProvider provider, IMemoryCache memoryCache, IConfiguration configuration)
        {
            _provider = provider;
            _logger = logger;
            _cache = memoryCache;           
            SetCacheOptions(configuration);
        }

        public async Task<Guid> CreateExamMaster(ExamMaster examMaster)
        {
            Guid result;
            try
            {
                using (var transaction = new TransactionScope(TransactionScopeOption.Required, TransactionScopeAsyncFlowOption.Enabled))
                {
                    using (var connection = _provider.GetDbConnection())
                    {
                        var command = @"INSERT INTO [ExamMaster] ([name], [userId], [testTypeId]) ";
                        command += "OUTPUT INSERTED.Id ";
                        command += "VALUES (@name, @userId, @testTypeId);";
                        _logger.LogDebug(command);
                        result = await connection.QuerySingleAsync<Guid>(command, examMaster).ConfigureAwait(false);

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

        public async Task<Guid> UpdateExamMaster(ExamMaster examMaster)
        {
            Guid result;
            try
            {
                using (var transaction = new TransactionScope(TransactionScopeOption.Required, TransactionScopeAsyncFlowOption.Enabled))
                {
                    using (var connection = _provider.GetDbConnection())
                    {
                        var command = @"UPDATE [ExamMaster] SET [name] = @name, [userId] = @userId, [active] = @active, [editDate] = @editDate ";
                        command += "WHERE id = @id";

                        _logger.LogDebug(command);
                        await connection.ExecuteAsync(command, examMaster).ConfigureAwait(false);
                        result = examMaster.Id;

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
        public async Task<Guid> DeactivateExamMaster(ExamMaster examMaster)
        {
            Guid result;
            try
            {
                using (var transaction = new TransactionScope(TransactionScopeOption.Required, TransactionScopeAsyncFlowOption.Enabled))
                {
                    using (var connection = _provider.GetDbConnection())
                    {
                        var command = @"UPDATE [ExamMaster] SET [active] = 0, [editDate] = @editDate ";
                        command += "WHERE id = @id";

                        _logger.LogDebug(command);
                        await connection.ExecuteAsync(command, examMaster).ConfigureAwait(false);
                        result = examMaster.Id;
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
    }
}
