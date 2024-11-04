using System;
using System.Collections.Generic;

namespace GOWI.AIArticleGenerator.BackgroundTask.Entities;

public partial class TransactionStage
{
    public int TransactionStageId { get; set; }

    public string? Name { get; set; }

    public virtual ICollection<Transaction> Transactions { get; set; } = new List<Transaction>();
}
