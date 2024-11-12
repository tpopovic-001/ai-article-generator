namespace GOWI.AIArticleGenerator.DomainLayer.DTOs
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;
    using System.Threading.Tasks;

    public class DTOTransaction
    {
        public int TransactionId { get; set; }

        public string? Name { get; set; }

        public decimal? Value { get; set; }

        public string? Description { get; set; }

        public DateTime? DraftedOn { get; set; }

        public string? SelectedCurrency { get; set; }

        public string? TrancheName { get; set; }

        public decimal? TrancheValue { get; set; }

        public string? CompanyName { get; set; }
    }
}
