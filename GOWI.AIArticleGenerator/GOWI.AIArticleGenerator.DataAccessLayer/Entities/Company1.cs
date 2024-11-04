using System;
using System.Collections.Generic;

namespace GOWI.AIArticleGenerator.BackgroundTask.Entities;

public partial class Company1
{
    public int CompanyId { get; set; }

    public Guid? Id { get; set; }

    public string Name { get; set; } = null!;

    public int AddressId { get; set; }

    public Guid? AddressGuidId { get; set; }

    public string? WebSite { get; set; }

    public DateTime? StartDate { get; set; }

    public DateTime? EndDate { get; set; }

    public string? Icaocode { get; set; }

    public string? Iatacode { get; set; }

    public int? CountryId { get; set; }

    public Guid? CountryGuidId { get; set; }

    public string? EmailAddress { get; set; }

    public string? Slug { get; set; }

    public bool IsActive { get; set; }

    public bool IsAtlasData { get; set; }

    public int? RowVersion { get; set; }

    public string? AtdbHistory { get; set; }

    public long? AirlineOperatorGroupId { get; set; }
}
