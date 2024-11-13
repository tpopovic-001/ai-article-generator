namespace GOWI.AIArticleGenerator.DataAccessLayer
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;
    using System.Threading.Tasks;
    using GOWI.AIArticleGenerator.BackgroundTask.Entities;
    using GOWI.AIArticleGenerator.DataAccessLayer.Interfaces;
    using GOWI.AIArticleGenerator.DomainLayer.DTOs;

    public class Mapper : IMapper
    {
        public static Mapper _mapper;

        public static Mapper MapperInstance
        {
            get
            {
                if (_mapper == null)
                {
                    _mapper = new Mapper();
                }

                return _mapper;
            }
        }

        public ArticlesTeodorPopovic MapFromDTOToEntity(DTOArticle dTOArticle)
        {
            ArticlesTeodorPopovic entityArticle = new ArticlesTeodorPopovic();
            entityArticle.ArticleId = dTOArticle.ArticleId;
            entityArticle.Title = dTOArticle.Title;
            entityArticle.ShortDescription = dTOArticle.ShortDescription;
            entityArticle.FullDescription = dTOArticle.FullDescription;
            entityArticle.TransactionId = dTOArticle.TransactionId;

            return entityArticle;
        }

        public List<ArticlesTeodorPopovic> MapFromDTOToEntity(List<DTOArticle> dTOArticles)
        {
            List<ArticlesTeodorPopovic> entityArticleList = new List<ArticlesTeodorPopovic>();

            foreach (DTOArticle article in dTOArticles)
            {
                var mappedDtoObject = MapFromDTOToEntity(article);
                entityArticleList.Add(mappedDtoObject);
            }

            return entityArticleList;
        }
     }
}
