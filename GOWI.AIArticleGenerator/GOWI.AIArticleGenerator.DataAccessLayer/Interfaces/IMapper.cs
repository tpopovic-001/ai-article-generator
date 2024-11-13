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
        ArticlesTeodorPopovic MapFromDTOToEntity(DTOArticle dTOArticle);

        List<ArticlesTeodorPopovic> MapFromDTOToEntity(List<DTOArticle> dTOArticles);

    }
}
