using System;
using System.Collections.Generic;

namespace GOWI.AIArticleGenerator.BackgroundTask.Entities;

public partial class ProductType
{
    public int ProductTypeId { get; set; }

    public string Name { get; set; } = null!;

    public virtual ICollection<Transaction> Transactions { get; set; } = new List<Transaction>();
}
