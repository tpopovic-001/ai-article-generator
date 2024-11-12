namespace GOWI.AIArticleGenerator.DataAccessLayer
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;
    using System.Threading.Tasks;
    using GOWI.AIArticleGenerator.BackgroundTask.Entities;
    using GOWI.AIArticleGenerator.BackgroundTask.Entities.Context;
    using GOWI.AIArticleGenerator.DataAccessLayer.Interfaces;
    using GOWI.AIArticleGenerator.DomainLayer.DTOs;
    using Microsoft.Extensions.Logging;

    public class DataAccess : IDataAccess
    {
        private readonly ILogger<DataAccess> _logger;
        private readonly Mapper _mapper;

        public DataAccess(ILogger<DataAccess> logger)
        {
            _logger = logger;
            _mapper = Mapper.MapperInstance;
        }

        public async Task<List<DTOTransaction>> GetTransactions()
        {
            var transactions = new List<DTOTransaction>();
            try
            {
                var valueCheck = Convert.ToDecimal(0.00000);

                using (var context = new DevAfjPp18032024Context())
                {
                    transactions = context.Transactions
                                     .Where(w => !w.Name.Contains("Test") && w.Value != valueCheck)
                                     .Join(context.Tranches,
                                            transaction => transaction.TransactionId,
                                            tranche => tranche.TransactionId,
                                            (transaction, tranche) => new { transaction, tranche }
                                    ).Join
                                        (context.TrancheCompanyRelationships,
                                            temp => temp.tranche.TrancheId,
                                            companyRelationship => companyRelationship.TrancheId,
                                            (temp, companyRelationship) => new
                                            {
                                                temp.transaction,
                                                temp.tranche,
                                                companyRelationship
                                            })
                                    .Join(context.Companies,
                                          temp => temp.companyRelationship.CompanyId,
                                          company => company.CompanyId,
                                          (temp, company) => new DTOTransaction
                                          {
                                              TransactionId = temp.transaction.TransactionId,
                                              Name = temp.transaction.Name,
                                              Value = temp.transaction.Value,
                                              Description = temp.transaction.Description,
                                              DraftedOn = temp.transaction.DraftedOn,
                                              SelectedCurrency = temp.transaction.SelectedCurrency,
                                              TrancheName = temp.tranche.Name,
                                              TrancheValue = temp.transaction.Value,
                                              CompanyName = company.Name,
                                          }
                                         ).ToList();
                    _logger.LogInformation("DataAccess GetTransactions method executed " +
                                            "successfully at: {time}", DateTimeOffset.Now);
                }
            }
            catch (Exception ex)
            {
                _logger.LogError("Error happened inside of DataAccess GetTransactions method at: {time}." +
                "Error message: {error}", DateTimeOffset.Now, ex.Message);
            }
            return await Task.FromResult(transactions);
        }

        public async void SaveFormattedArticles(List<DTOArticle> articles)
        {
            try
            {
                var mappedArticles = await _mapper.MapFromDTOToEntity(articles);

                using (var connection = new DevAfjPp18032024Context())
                {
                    await connection.ArticlesTeodorPopovics.AddRangeAsync(mappedArticles);
                }

                _logger.LogInformation("DataAccess SaveFormattedArticles method executed " +
                        "successfully at: {time}", DateTimeOffset.Now);
            }
            catch (Exception ex)
            {
                _logger.LogError("Error happened inside of DataAccess SaveFormattedArticles method at: {time}." +
                "Error message: {error}", DateTimeOffset.Now, ex.Message);

            }
        }
    }
}
