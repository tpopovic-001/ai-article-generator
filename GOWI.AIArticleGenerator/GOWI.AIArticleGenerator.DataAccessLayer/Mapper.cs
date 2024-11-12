namespace GOWI.AIArticleGenerator.DataAccessLayer
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;
    using System.Threading.Tasks;
    using GOWI.AIArticleGenerator.BackgroundTask.Entities;
    using GOWI.AIArticleGenerator.DomainLayer.DTOs;
    using GOWI.AIArticleGenerator.DataAccessLayer.Interfaces;

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

        public async Task<ArticlesTeodorPopovic> MapFromDTOToEntity(DTOArticle dTOArticle)
        {
            ArticlesTeodorPopovic entityArticle = new ArticlesTeodorPopovic();
            entityArticle.ArticleId = dTOArticle.ArticleId;
            entityArticle.Title = dTOArticle.Title;
            entityArticle.ShortDescription = dTOArticle.ShortDescription;
            entityArticle.FullDescription = dTOArticle.FullDescription;
            entityArticle.TransactionId = dTOArticle.TransactionId;

            return await Task.FromResult(entityArticle);
        }

        public async Task<List<ArticlesTeodorPopovic>> MapFromDTOToEntity(List<DTOArticle> dTOArticles)
        {
            List<ArticlesTeodorPopovic> entityArticleList = new List<ArticlesTeodorPopovic>();

            foreach (DTOArticle article in dTOArticles)
            {
                var mappedDtoObject = await MapFromDTOToEntity(article);
                entityArticleList.Add(mappedDtoObject);
            }

            return await Task.FromResult(entityArticleList);
        }
     }
}
