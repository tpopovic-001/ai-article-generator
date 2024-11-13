namespace GOWI.AIArticleGenerator.BusinessLogicLayer.Interfaces
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;
    using System.Threading.Tasks;
    using GOWI.AIArticleGenerator.DomainLayer.DTOs;
    using GOWI.AIArticleGenerator.ServiceLayer.Helper_classes;

    public interface IBusinessLogic
    {
        Task<List<Choice>> GetArticlesAsync();

        List<DTOArticle> FormatArticles(List<Choice> articles);

        Task SaveFormattedArticlesAsync(List<DTOArticle> articles);
    }
}
