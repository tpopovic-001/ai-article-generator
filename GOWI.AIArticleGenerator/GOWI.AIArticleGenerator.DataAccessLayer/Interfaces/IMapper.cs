namespace GOWI.AIArticleGenerator.DataAccessLayer.Interfaces
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;
    using System.Threading.Tasks;
    using GOWI.AIArticleGenerator.BackgroundTask.Entities;
    using GOWI.AIArticleGenerator.DomainLayer.DTOs;

    public interface IMapper
    {
        Task<ArticlesTeodorPopovic> MapFromDTOToEntity(DTOArticle dTOArticle);

        Task<List<ArticlesTeodorPopovic>> MapFromDTOToEntity(List<DTOArticle> dTOArticles);

    }
}
