namespace GOWI.AIArticleGenerator.BackgroundTask.Entities.Context;
using System;
using System.Collections.Generic;
using GOWI.AIArticleGenerator.BackgroundTask.Entities;
using Microsoft.EntityFrameworkCore;

public partial class DevAfjPp18032024Context : DbContext
{
    public DevAfjPp18032024Context()
    {
    }

    public DevAfjPp18032024Context(DbContextOptions<DevAfjPp18032024Context> options)
        : base(options)
    {
    }

    public virtual DbSet<ArticlesTeodorPopovic> ArticlesTeodorPopovics { get; set; }

    public virtual DbSet<Company> Companies { get; set; }

    public virtual DbSet<Company1> Companies1 { get; set; }

    public virtual DbSet<Tranch> Tranches { get; set; }

    public virtual DbSet<TrancheCompanyRelationship> TrancheCompanyRelationships { get; set; }

    public virtual DbSet<Transaction> Transactions { get; set; }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        => optionsBuilder.UseSqlServer(Environment.GetEnvironmentVariable("CONNECTION_STRING"));

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.UseCollation("Latin1_General_CI_AS");

        modelBuilder.Entity<ArticlesTeodorPopovic>(entity =>
        {
            entity.HasKey(e => e.ArticleId).HasName("PK__Articles__9C6270E87A2AC021");

            entity.ToTable("Articles_TeodorPopovic");

            entity.Property(e => e.ShortDescription).HasMaxLength(400);
            entity.Property(e => e.Title).HasMaxLength(200);

            entity.HasOne(d => d.Transaction).WithMany(p => p.ArticlesTeodorPopovics)
                .HasForeignKey(d => d.TransactionId)
                .HasConstraintName("FK__Articles___Trans__7BCBC157");
        });

        modelBuilder.Entity<Company>(entity =>
        {
            entity.HasKey(e => e.CompanyId).HasName("PK__Companie__2D971CAC7A521F79");

            entity.HasIndex(e => e.CompanyOwnershipTypeId, "idx_CompanyCompanyOwnershipTypeId").HasFillFactor(80);

            entity.HasIndex(e => e.CompanyStatusId, "idx_CompanyCompanyStatus").HasFillFactor(80);

            entity.HasIndex(e => e.CompanyTypeId, "idx_CompanyCompanyType").HasFillFactor(80);

            entity.HasIndex(e => e.EmployeeRangeId, "idx_CompanyEmployeeRange").HasFillFactor(80);

            entity.HasIndex(e => e.LeagueTableAccreditationCompanyId, "idx_CompanyLeagueTableAccreditationCompany").HasFillFactor(80);

            entity.HasIndex(e => e.FileId, "idx_CompanyLogo").HasFillFactor(80);

            entity.HasIndex(e => e.Name, "idx_CompanyName").HasFillFactor(80);

            entity.HasIndex(e => e.PrimaryAddressId, "idx_CompanyPrimaryAddress").HasFillFactor(80);

            entity.Property(e => e.DateAdded).HasColumnType("datetime");
            entity.Property(e => e.DateUpdated).HasColumnType("datetime");
            entity.Property(e => e.EmailAddress).HasMaxLength(255);
            entity.Property(e => e.ForceCompanyReportPage).HasDefaultValue(false);
            entity.Property(e => e.IcaoCode).HasMaxLength(10);
            entity.Property(e => e.LinkedIn).HasMaxLength(500);
            entity.Property(e => e.Name).HasMaxLength(256);
            entity.Property(e => e.RssLink).HasMaxLength(500);
            entity.Property(e => e.TelephoneNumber).HasMaxLength(40);
            entity.Property(e => e.TurnOverInMillionsOfDollars).HasColumnType("decimal(18, 0)");
            entity.Property(e => e.Twitter).HasMaxLength(500);
            entity.Property(e => e.WebAddress).HasMaxLength(500);
            entity.Property(e => e.YouTube).HasMaxLength(500);

            entity.HasOne(d => d.LeagueTableAccreditationCompany).WithMany(p => p.InverseLeagueTableAccreditationCompany)
                .HasForeignKey(d => d.LeagueTableAccreditationCompanyId)
                .HasConstraintName("FK2C073D85603871C3");
        });

        modelBuilder.Entity<Company1>(entity =>
        {
            entity.HasKey(e => e.CompanyId).HasName("PK_Company");

            entity.ToTable("Companies", "fleet");

            entity.Property(e => e.EmailAddress).HasMaxLength(255);
            entity.Property(e => e.EndDate).HasColumnType("datetime");
            entity.Property(e => e.Iatacode)
                .HasMaxLength(4)
                .HasColumnName("IATACode");
            entity.Property(e => e.Icaocode)
                .HasMaxLength(4)
                .HasColumnName("ICAOCode");
            entity.Property(e => e.IsActive).HasDefaultValue(true);
            entity.Property(e => e.Name).HasMaxLength(255);
            entity.Property(e => e.RowVersion).HasDefaultValue(0);
            entity.Property(e => e.Slug).HasMaxLength(128);
            entity.Property(e => e.StartDate).HasColumnType("datetime");
            entity.Property(e => e.WebSite).HasMaxLength(225);
        });

        modelBuilder.Entity<Tranch>(entity =>
        {
            entity.HasKey(e => e.TrancheId).HasName("PK__Tranches__EED47AC6F6188C7C");

            entity.HasIndex(e => e.TranchDebtServiceCoverRatioId, "idx_TranchDebtServiceCoverRatioId").HasFillFactor(80);

            entity.HasIndex(e => e.TranchLoanLifeCoverRatioId, "idx_TranchLoanLifeCoverRatioId").HasFillFactor(80);

            entity.HasIndex(e => e.TrancheFeeId, "idx_TrancheFeeId").HasFillFactor(80);

            entity.HasIndex(e => e.TranchPrimaryTypeId, "idx_TranchePrimaryType").HasFillFactor(80);

            entity.HasIndex(e => e.TranchSecondaryTypeId, "idx_TrancheSecondaryType").HasFillFactor(80);

            entity.HasIndex(e => e.TranchTertiaryTypeId, "idx_TrancheTertiaryType").HasFillFactor(80);

            entity.HasIndex(e => e.TransactionId, "idx_TrancheTransaction").HasFillFactor(80);

            entity.Property(e => e.AuthorNotes).HasMaxLength(2000);
            entity.Property(e => e.Balloon).HasColumnType("decimal(18, 4)");
            entity.Property(e => e.BalloonValue).HasColumnType("decimal(18, 4)");
            entity.Property(e => e.Coupon).HasColumnType("decimal(18, 4)");
            entity.Property(e => e.Cusip).HasMaxLength(30);
            entity.Property(e => e.Duration).HasColumnType("decimal(19, 5)");
            entity.Property(e => e.EndDate).HasColumnType("datetime");
            entity.Property(e => e.LegalMaturityDate).HasColumnType("datetime");
            entity.Property(e => e.Ltv)
                .HasColumnType("decimal(18, 4)")
                .HasColumnName("LTV");
            entity.Property(e => e.Name).HasMaxLength(200);
            entity.Property(e => e.Notes).HasMaxLength(2000);
            entity.Property(e => e.Ratings).HasMaxLength(18);
            entity.Property(e => e.RefBenchmarkId).HasColumnName("RefBenchmarkID");
            entity.Property(e => e.RefSpread).HasColumnType("decimal(18, 4)");
            entity.Property(e => e.Spid).HasColumnName("SPId");
            entity.Property(e => e.Spread).HasColumnType("decimal(18, 4)");
            entity.Property(e => e.StartDate).HasColumnType("datetime");
            entity.Property(e => e.TrancheClass).HasMaxLength(5);
            entity.Property(e => e.Value).HasColumnType("decimal(19, 5)");
            entity.Property(e => e.WeightedAverageLife).HasColumnType("decimal(19, 5)");

            entity.HasOne(d => d.Transaction).WithMany(p => p.Tranches)
                .HasForeignKey(d => d.TransactionId)
                .HasConstraintName("FKFEBCB7A0AD2A1557");
        });

        modelBuilder.Entity<TrancheCompanyRelationship>(entity =>
        {
            entity.HasKey(e => e.TrancheCompanyRelationshipId).HasName("PK__TrancheC__56EFA3362F2C5BDB");

            entity.HasIndex(e => e.CompanyId, "idx_TrancheCompanyRelationshipCompany").HasFillFactor(80);

            entity.HasIndex(e => e.LeagueTableCreditId, "idx_TrancheCompanyRelationshipLeagueTableCredit").HasFillFactor(80);

            entity.HasIndex(e => e.TranchCompanyRelationshipSubRoleId, "idx_TrancheCompanyRelationshipTranchCompanyRelationshipSubRole").HasFillFactor(80);

            entity.HasIndex(e => e.TrancheId, "idx_TrancheCompanyRelationshipTranche").HasFillFactor(80);

            entity.HasIndex(e => e.TranchCompanyRelationshipRoleId, "idx_TrancheCompanyRelationshipTrancheCompanyRelationshipRole").HasFillFactor(80);

            entity.Property(e => e.EquityValue).HasColumnType("decimal(19, 5)");
            entity.Property(e => e.LeagueTableCreditValue).HasColumnType("decimal(19, 5)");
            entity.Property(e => e.Percentage).HasColumnType("decimal(19, 5)");
            entity.Property(e => e.UnderwrittenValue).HasColumnType("decimal(19, 5)");

            entity.HasOne(d => d.Company).WithMany(p => p.TrancheCompanyRelationshipCompanies)
                .HasForeignKey(d => d.CompanyId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK2C60EB77961EB50A");

            entity.HasOne(d => d.LeagueTableCredit).WithMany(p => p.TrancheCompanyRelationshipLeagueTableCredits)
                .HasForeignKey(d => d.LeagueTableCreditId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK2C60EB77ABA87A17");

            entity.HasOne(d => d.Tranche).WithMany(p => p.TrancheCompanyRelationships)
                .HasForeignKey(d => d.TrancheId)
                .HasConstraintName("FKCBFF66ACFF030609");
        });

        modelBuilder.Entity<Transaction>(entity =>
        {
            entity.HasKey(e => e.TransactionId).HasName("PK__Transact__55433A6B343D6508");

            entity.HasIndex(e => e.Live, "IX_Transactions_Live_INC2");

            entity.HasIndex(e => e.Assignee, "idx_TransactionAssignee");

            entity.HasIndex(e => e.CashflowModelId, "idx_TransactionCashflowModel");

            entity.HasIndex(e => e.CreatedBy, "idx_TransactionCreatedBy");

            entity.HasIndex(e => e.TransactionFinanceTypeId, "idx_TransactionFinanceType");

            entity.HasIndex(e => e.RejectedBy, "idx_TransactionRejectedBy");

            entity.HasIndex(e => e.TransactionConcessionTypeId, "idx_TransactionTransactionConcessionType");

            entity.HasIndex(e => e.TransactionProcurementStageId, "idx_TransactionTransactionProcurementStage");

            entity.HasIndex(e => e.TransactionStageId, "idx_TransactionTransactionStage");

            entity.HasIndex(e => e.TransactionTypeId, "idx_TransactionTransactionType");

            entity.HasIndex(e => e.LastUpdatedByUserId, "idx_TransactionUserId");

            entity.HasIndex(e => e.DraftedBy, "idx_TransactionsDraftedBy");

            entity.HasIndex(e => e.PublishedBy, "idx_TransactionsPublishedBy");

            entity.HasIndex(e => e.SubmittedBy, "idx_TransactionsSubmittedBy");

            entity.HasIndex(e => e.UnpublishedBy, "idx_TransactionsUnpublishedBy");

            entity.Property(e => e.AlternateName).HasMaxLength(200);
            entity.Property(e => e.BankBenchMarkId).HasColumnName("BankBenchMarkID");
            entity.Property(e => e.BankOfferPrice).HasColumnType("decimal(18, 4)");
            entity.Property(e => e.BankPricing).HasColumnType("decimal(18, 4)");
            entity.Property(e => e.CashflowNotes).HasMaxLength(255);
            entity.Property(e => e.ConcessionDuration).HasColumnType("decimal(19, 5)");
            entity.Property(e => e.ConcessionEndDate).HasColumnType("datetime");
            entity.Property(e => e.ConcessionNotes).HasMaxLength(255);
            entity.Property(e => e.ConcessionStartDate).HasColumnType("datetime");
            entity.Property(e => e.CreatedBy).HasDefaultValue(1);
            entity.Property(e => e.CreatedOn).HasColumnType("datetime");
            entity.Property(e => e.DateAdded).HasColumnType("datetime");
            entity.Property(e => e.DateUpdated).HasColumnType("datetime");
            entity.Property(e => e.Debt).HasColumnType("decimal(19, 5)");
            entity.Property(e => e.DebtEquityRatio).HasMaxLength(255);
            entity.Property(e => e.DraftedOn).HasColumnType("datetime");
            entity.Property(e => e.Equity).HasColumnType("decimal(19, 5)");
            entity.Property(e => e.LeaseEndDate).HasColumnType("datetime");
            entity.Property(e => e.LeaseStartDate).HasColumnType("datetime");
            entity.Property(e => e.LeaseTerm).HasColumnType("decimal(4, 1)");
            entity.Property(e => e.MarketTypeId).HasColumnName("MarketTypeID");
            entity.Property(e => e.Name).HasMaxLength(200);
            entity.Property(e => e.Ppp).HasColumnName("PPP");
            entity.Property(e => e.ProductCategoryId).HasColumnName("ProductCategoryID");
            entity.Property(e => e.ProjectCreationUserId).HasColumnName("ProjectCreationUserID");
            entity.Property(e => e.PublishedOn).HasColumnType("datetime");
            entity.Property(e => e.Rbl)
                .HasDefaultValue(false)
                .HasColumnName("RBL");
            entity.Property(e => e.RejectedOn).HasColumnType("datetime");
            entity.Property(e => e.RejectionReasons).HasMaxLength(4000);
            entity.Property(e => e.Rfpclosed).HasColumnName("RFPClosed");
            entity.Property(e => e.RfpconsideredStructures)
                .HasMaxLength(30)
                .HasColumnName("RFPConsideredStructures");
            entity.Property(e => e.SelectedCurrency)
                .HasMaxLength(3)
                .IsUnicode(false)
                .IsFixedLength();
            entity.Property(e => e.Spread).HasColumnType("decimal(18, 4)");
            entity.Property(e => e.Spv)
                .HasMaxLength(255)
                .HasColumnName("SPV");
            entity.Property(e => e.Status).HasDefaultValue(3);
            entity.Property(e => e.StructureId).HasColumnName("StructureID");
            entity.Property(e => e.SubStructureId).HasColumnName("SubStructureID");
            entity.Property(e => e.SubmittedOn).HasColumnType("datetime");
            entity.Property(e => e.TransactionInstrumentTypeId).HasColumnName("TransactionInstrumentTypeID");
            entity.Property(e => e.UnpublishedOn).HasColumnType("datetime");
            entity.Property(e => e.Value).HasColumnType("decimal(19, 5)");
        });

        OnModelCreatingPartial(modelBuilder);
    }

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}
