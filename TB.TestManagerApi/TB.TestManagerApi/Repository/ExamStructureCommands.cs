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

        public async Task<Guid> CreateExamMaster(CreateExamMaster createExamMaster)
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
                        result = await connection.QuerySingleAsync<Guid>(command, createExamMaster).ConfigureAwait(false);

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

        public async Task<Guid> UpdateExamMaster(UpdateExamMaster updateExamMaster)
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
                        await connection.ExecuteAsync(command, updateExamMaster).ConfigureAwait(false);
                        result = updateExamMaster.Id;

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
        public async Task<Guid> DeactivateExamMaster(DeactivateExamMaster deactivateExamMaster)
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
                        await connection.ExecuteAsync(command, deactivateExamMaster).ConfigureAwait(false);
                        result = deactivateExamMaster.Id;
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

        public async Task<Guid> CreateExamQuestionMaster(CreateExamQuestion createExamQuestion)
        {
            Guid questionMasterResultId;
            
            try
            {
                using (var transaction = new TransactionScope(TransactionScopeOption.Required, TransactionScopeAsyncFlowOption.Enabled))
                {
                    using (var connection = _provider.GetDbConnection())
                    {
                        var command = @"INSERT INTO [ExamQuestionMaster] ([question], [testTypeSectionId]) ";
                        command += "OUTPUT INSERTED.Id ";
                        command += "VALUES (@question, @examTypeSectionId);";
                        _logger.LogDebug(command);
                        questionMasterResultId = await connection.QuerySingleAsync<Guid>(command, createExamQuestion.ExamQuestion).ConfigureAwait(false);
                        var createExamAnswer = new CreateExamAnswer()
                        {
                            ExamQuestionMasterId = questionMasterResultId,
                            ExamMasterId = createExamQuestion.ExamMasterId,
                            ExamAnswers = createExamQuestion.ExamAnswers
                        };
                        await CreateExamAnswer(createExamAnswer).ConfigureAwait(false);
                        transaction.Complete();
                    }
                }
                return questionMasterResultId;
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
                using (var transaction = new TransactionScope(TransactionScopeOption.Required, TransactionScopeAsyncFlowOption.Enabled))
                {
                    using (var connection = _provider.GetDbConnection())
                    {
                        foreach (var examAnswer in createExamAnswer.ExamAnswers)
                        {
                            Guid examAnswerMasterId = await CreateExamAnswerMaster(examAnswer).ConfigureAwait(false);
                            Guid examQuestionAnswerXrefId = await CreateExamQuestionAnswerXref(createExamAnswer.ExamQuestionMasterId, examAnswerMasterId, examAnswer.isCorrect).ConfigureAwait(false);
                            Guid examQuestion = await CreateExamQuestion(createExamAnswer.ExamMasterId, examQuestionAnswerXrefId).ConfigureAwait(false);
                        }
                        transaction.Complete();
                    }
                }
                return createExamAnswer.ExamQuestionMasterId;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex.ToString());
                throw;
            }
        }
        public async Task<Guid> CreateExamAnswerMaster(CreateExamAnswerMaster examAnswer)
        {
            Guid answerResultId;
            try
            {
                using (var transaction = new TransactionScope(TransactionScopeOption.Required, TransactionScopeAsyncFlowOption.Enabled))
                {
                    using (var connection = _provider.GetDbConnection())
                    {
                        var command = @"INSERT INTO [ExamAnswerMaster] ([answer]) ";
                        command += "OUTPUT INSERTED.Id ";
                        command += "VALUES (@answer);";
                        _logger.LogDebug(command);
                        answerResultId = await connection.QuerySingleAsync<Guid>(command, new { examAnswer.Answer }).ConfigureAwait(false);                      

                        transaction.Complete();
                    }
                }
                return answerResultId;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex.ToString());
                throw;
            }
        }

        public async Task<Guid> CreateExamQuestionAnswerXref(Guid testQuestionId, Guid testAnswerId, bool isCorrect)
        {
            Guid questionAnswerXrefResultId;
            try
            {
                using (var transaction = new TransactionScope(TransactionScopeOption.Required, TransactionScopeAsyncFlowOption.Enabled))
                {
                    using (var connection = _provider.GetDbConnection())
                    {
                        var command = @"INSERT INTO [ExamQuestionAnswerXref] ([testQuestionId], [testAnswerId], [isCorrect]) ";
                        command += "OUTPUT INSERTED.Id ";
                        command += "VALUES (@testQuestionId, @testAnswerId, @isCorrect);";
                        _logger.LogDebug(command);
                        questionAnswerXrefResultId = await connection.QuerySingleAsync<Guid>(command, new { testQuestionId, testAnswerId, isCorrect }).ConfigureAwait(false);

                        transaction.Complete();
                    }
                }
                return questionAnswerXrefResultId;

            }
            catch (Exception ex)
            {
                _logger.LogError(ex.ToString());
                throw;
            }
        }
        public async Task<Guid> CreateExamQuestion(Guid examMasterId, Guid examQuestionAnswerXrefId)
        {
            Guid questionAnswerXrefResultId;
            try
            {
                using (var transaction = new TransactionScope(TransactionScopeOption.Required, TransactionScopeAsyncFlowOption.Enabled))
                {
                    using (var connection = _provider.GetDbConnection())
                    {
                        var command = @"INSERT INTO [ExamQuestions] ([testMasterId], [testQuestionAnswerXrefId]) ";
                        command += "OUTPUT INSERTED.Id ";
                        command += "VALUES (@examMasterId, @examQuestionAnswerXrefId);";
                        _logger.LogDebug(command);
                        questionAnswerXrefResultId = await connection.QuerySingleAsync<Guid>(command, new { examMasterId, examQuestionAnswerXrefId }).ConfigureAwait(false);

                        transaction.Complete();
                    }
                }
                return questionAnswerXrefResultId;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex.ToString());
                throw;
            }
        }

        public async Task<Guid> UpdateExamQuestionMaster(UpdateExamQuestionMaster updateExamQuestionMaster)
        {
            Guid result;
            try
            {
                using (var transaction = new TransactionScope(TransactionScopeOption.Required, TransactionScopeAsyncFlowOption.Enabled))
                {
                    using (var connection = _provider.GetDbConnection())
                    {
                        var command = @"UPDATE [ExamQuestionMaster] SET [question] = @question, [testTypeSectionId] = @examTypeSectionId, [editDate] = @editDate ";
                        command += "WHERE id = @id";

                        _logger.LogDebug(command);
                        await connection.ExecuteAsync(command, updateExamQuestionMaster).ConfigureAwait(false);
                        result = updateExamQuestionMaster.Id;

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

        public async Task<Guid> UpdateExamAnswerMaster(UpdateExamAnswerMaster updateExamAnswerMaster)
        {
            Guid result;
            try
            {
                using (var transaction = new TransactionScope(TransactionScopeOption.Required, TransactionScopeAsyncFlowOption.Enabled))
                {
                    using (var connection = _provider.GetDbConnection())
                    {
                        var command = @"UPDATE [ExamAnswerMaster] SET [answer] = @answer, [editDate] = @editDate ";
                        command += "WHERE id = @id";

                        _logger.LogDebug(command);
                        await connection.ExecuteAsync(command, updateExamAnswerMaster).ConfigureAwait(false);
                        result = updateExamAnswerMaster.Id;

                        await UpdateExamAnswerQuestionXrefCorrect(updateExamAnswerMaster.ExamAnswerQuestionXrefCorrect).ConfigureAwait(false);

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

        public async Task<Guid> UpdateExamAnswerQuestionXrefCorrect(UpdateExamAnswerQuestionXrefCorrect updateExamAnswerQuestionXrefCorrect)
        {
            Guid result;
            try
            {
                using (var transaction = new TransactionScope(TransactionScopeOption.Required, TransactionScopeAsyncFlowOption.Enabled))
                {
                    using (var connection = _provider.GetDbConnection())
                    {      
                        var command = @"UPDATE [ExamQuestionAnswerXref] SET [isCorrect] = @isCorrect ";
                        command += "WHERE id = @id";

                        _logger.LogDebug(command);
                        await connection.ExecuteAsync(command, updateExamAnswerQuestionXrefCorrect).ConfigureAwait(false);
                        result = updateExamAnswerQuestionXrefCorrect.Id;

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
