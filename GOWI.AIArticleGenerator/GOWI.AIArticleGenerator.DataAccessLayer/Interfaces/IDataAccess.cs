namespace GOWI.AIArticleGenerator.DataAccessLayer.Interfaces
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;
    using System.Threading.Tasks;
    using System.Transactions;
    using GOWI.AIArticleGenerator.DomainLayer.DTOs;

    public interface IDataAccess
    {
        Task<List<DTOTransaction>> GetTransactions();

        void SaveFormattedArticles(List<DTOArticle> articles);
    }
}
