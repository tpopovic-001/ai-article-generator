namespace GOWI.AIArticleGenerator.DomainLayer.DTOs
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;
    using System.Threading.Tasks;

    public class DTOArticle
    {
        public int ArticleId { get; set; }

        public string Title { get; set; }

        public string ShortDescription { get; set; }

        public string FullDescription { get; set; }

        public int TransactionId { get; set; }
    }
}
