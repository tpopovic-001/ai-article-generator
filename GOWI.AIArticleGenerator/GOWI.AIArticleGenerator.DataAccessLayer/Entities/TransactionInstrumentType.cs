﻿using System;
using System.Collections.Generic;

namespace GOWI.AIArticleGenerator.BackgroundTask.Entities;

public partial class TransactionInstrumentType
{
    public int TransactionInstrumentTypeId { get; set; }

    public string? TransactionInstrumentTypeName { get; set; }

    public virtual ICollection<Transaction> Transactions { get; set; } = new List<Transaction>();
}
