using System;
using System.Collections.Generic;

namespace GOWI.AIArticleGenerator.BackgroundTask.Entities;

public partial class ArticlesTeodorPopovic
{
    public int ArticleId { get; set; }

    public string? Title { get; set; }

    public string? ShortDescription { get; set; }

    public string? FullDescription { get; set; }

    public int? TransactionId { get; set; }

    public virtual Transaction? Transaction { get; set; }
}
