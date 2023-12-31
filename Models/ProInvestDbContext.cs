using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;
using Pomelo.EntityFrameworkCore.MySql.Scaffolding.Internal;

namespace ProInvestAPI.Models;

public partial class ProInvestDbContext : DbContext
{
    public ProInvestDbContext()
    {
    }

    public ProInvestDbContext(DbContextOptions<ProInvestDbContext> options)
        : base(options)
    {
    }

    public virtual DbSet<Adress> Adresses { get; set; }

    public virtual DbSet<Client> Clients { get; set; }

    public virtual DbSet<Document> Documents { get; set; }

    public virtual DbSet<DocumentType> DocumentTypes { get; set; }

    public virtual DbSet<InvestmentRequest> InvestmentRequests { get; set; }

    public virtual DbSet<InvestmentSimulator> InvestmentSimulators { get; set; }

    public virtual DbSet<InvestmentType> InvestmentTypes { get; set; }

    public virtual DbSet<Municipality> Municipalities { get; set; }

    public virtual DbSet<Neighborhood> Neighborhoods { get; set; }

    public virtual DbSet<OriginOfFound> OriginOfFounds { get; set; }

    public virtual DbSet<PostalCode> PostalCodes { get; set; }

    public virtual DbSet<RequestStatus> RequestStatuses { get; set; }

    public virtual DbSet<State> States { get; set; }

    public virtual DbSet<User> Users { get; set; }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
#warning To protect potentially sensitive information in your connection string, you should move it out of source code. You can avoid scaffolding the connection string by using the Name= syntax to read it from configuration - see https://go.microsoft.com/fwlink/?linkid=2131148. For more guidance on storing connection strings, see https://go.microsoft.com/fwlink/?LinkId=723263.
        => optionsBuilder.UseMySql("server=viaduct.proxy.rlwy.net;port=26597;database=ProInvestDB;user=proInvest;password=Pinv02@c34d;protocol=TCP", Microsoft.EntityFrameworkCore.ServerVersion.Parse("8.2.0-mysql"));

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder
            .UseCollation("utf8mb3_general_ci")
            .HasCharSet("utf8mb3");

        modelBuilder.Entity<Adress>(entity =>
        {
            entity.HasKey(e => e.IdAdress).HasName("PRIMARY");

            entity.ToTable("Adress");

            entity.HasIndex(e => e.NeighborhoodId, "neighborhoodId_idx");

            entity.Property(e => e.IdAdress)
                .ValueGeneratedNever()
                .HasColumnName("idAdress");
            entity.Property(e => e.InteriorNumber)
                .HasMaxLength(45)
                .HasColumnName("interiorNumber");
            entity.Property(e => e.NeighborhoodId).HasColumnName("neighborhoodId");
            entity.Property(e => e.Number)
                .HasMaxLength(45)
                .HasColumnName("number");
            entity.Property(e => e.PostalCode)
                .HasMaxLength(45)
                .HasColumnName("postalCode");
            entity.Property(e => e.Street)
                .HasMaxLength(45)
                .HasColumnName("street");

            entity.HasOne(d => d.Neighborhood).WithMany(p => p.Adresses)
                .HasForeignKey(d => d.NeighborhoodId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("neighborhoodId");
        });

        modelBuilder.Entity<Client>(entity =>
        {
            entity.HasKey(e => e.IdClient).HasName("PRIMARY");

            entity.ToTable("Client");

            entity.HasIndex(e => e.AdressId, "adressId_idx");

            entity.Property(e => e.IdClient).HasColumnName("idClient");
            entity.Property(e => e.AcademicDegree)
                .HasMaxLength(45)
                .HasColumnName("academicDegree");
            entity.Property(e => e.AdressId).HasColumnName("adressId");
            entity.Property(e => e.BirthDay)
                .HasMaxLength(45)
                .HasColumnName("birthDay");
            entity.Property(e => e.CompanyName)
                .HasMaxLength(45)
                .HasColumnName("companyName");
            entity.Property(e => e.LastName)
                .HasMaxLength(45)
                .HasColumnName("lastName");
            entity.Property(e => e.Name)
                .HasMaxLength(45)
                .HasColumnName("name");
            entity.Property(e => e.PhoneNumber)
                .HasMaxLength(45)
                .HasColumnName("phoneNumber");
            entity.Property(e => e.Profession)
                .HasMaxLength(45)
                .HasColumnName("profession");
            entity.Property(e => e.Rfc)
                .HasMaxLength(45)
                .HasColumnName("RFC");

            entity.HasOne(d => d.Adress).WithMany(p => p.Clients)
                .HasForeignKey(d => d.AdressId)
                .HasConstraintName("adressId");
        });

        modelBuilder.Entity<Document>(entity =>
        {
            entity.HasKey(e => e.IdDocuments).HasName("PRIMARY");

            entity.ToTable("Document");

            entity.HasIndex(e => e.DocumentTypeId, "documentTypeId_idx");

            entity.HasIndex(e => e.InvestmentRequestId, "investmentRequestId_idx");

            entity.Property(e => e.IdDocuments)
                .ValueGeneratedNever()
                .HasColumnName("idDocuments");
            entity.Property(e => e.DocumentTypeId).HasColumnName("documentTypeId");
            entity.Property(e => e.File)
                .HasColumnType("blob")
                .HasColumnName("file");
            entity.Property(e => e.FileFormat)
                .HasMaxLength(45)
                .HasColumnName("fileFormat");
            entity.Property(e => e.FileName)
                .HasMaxLength(45)
                .HasColumnName("fileName");
            entity.Property(e => e.FileSize)
                .HasMaxLength(45)
                .HasColumnName("fileSize");
            entity.Property(e => e.InvestmentRequestId).HasColumnName("investmentRequestId");

            entity.HasOne(d => d.DocumentType).WithMany(p => p.Documents)
                .HasForeignKey(d => d.DocumentTypeId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("documentTypeId");

            entity.HasOne(d => d.InvestmentRequest).WithMany(p => p.Documents)
                .HasForeignKey(d => d.InvestmentRequestId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("investmentRequestId");
        });

        modelBuilder.Entity<DocumentType>(entity =>
        {
            entity.HasKey(e => e.IdDocumentType).HasName("PRIMARY");

            entity.ToTable("DocumentType");

            entity.Property(e => e.IdDocumentType)
                .ValueGeneratedNever()
                .HasColumnName("idDocumentType");
            entity.Property(e => e.TypeName)
                .HasMaxLength(45)
                .HasColumnName("typeName");
        });

        modelBuilder.Entity<InvestmentRequest>(entity =>
        {
            entity.HasKey(e => e.IdInvestmentRequest).HasName("PRIMARY");

            entity.ToTable("InvestmentRequest");

            entity.HasIndex(e => e.ClientId, "clientId_idx");

            entity.HasIndex(e => e.InvestmentSimulatorId, "investmentSimulatorId_idx");

            entity.HasIndex(e => e.OriginOfFounds, "originOfFounds_idx");

            entity.HasIndex(e => e.Status, "requestStatusId_idx");

            entity.Property(e => e.IdInvestmentRequest)
                .ValueGeneratedNever()
                .HasColumnName("idInvestmentRequest");
            entity.Property(e => e.ClientId).HasColumnName("clientId");
            entity.Property(e => e.Date)
                .HasColumnType("datetime")
                .HasColumnName("date");
            entity.Property(e => e.InvestmentFolio)
                .HasMaxLength(45)
                .HasColumnName("investmentFolio");
            entity.Property(e => e.InvestmentSimulatorId).HasColumnName("investmentSimulatorId");
            entity.Property(e => e.Ipaddress)
                .HasMaxLength(45)
                .HasColumnName("IPAddress");
            entity.Property(e => e.OriginOfFounds).HasColumnName("originOfFounds");
            entity.Property(e => e.Status).HasColumnName("status");

            entity.HasOne(d => d.Client).WithMany(p => p.InvestmentRequests)
                .HasForeignKey(d => d.ClientId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("clientId");

            entity.HasOne(d => d.InvestmentSimulator).WithMany(p => p.InvestmentRequests)
                .HasForeignKey(d => d.InvestmentSimulatorId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("investmentSimulatorId");

            entity.HasOne(d => d.OriginOfFoundsNavigation).WithMany(p => p.InvestmentRequests)
                .HasForeignKey(d => d.OriginOfFounds)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("originOfFounds");

            entity.HasOne(d => d.StatusNavigation).WithMany(p => p.InvestmentRequests)
                .HasForeignKey(d => d.Status)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("requestStatusId");
        });

        modelBuilder.Entity<InvestmentSimulator>(entity =>
        {
            entity.HasKey(e => e.IdInvestmentSimulator).HasName("PRIMARY");

            entity.ToTable("InvestmentSimulator");

            entity.HasIndex(e => e.InvestmentType, "investmentType_idx");

            entity.Property(e => e.IdInvestmentSimulator)
                .ValueGeneratedNever()
                .HasColumnName("idInvestmentSimulator");
            entity.Property(e => e.EstimatedResult).HasColumnName("estimatedResult");
            entity.Property(e => e.InvestmentAmount).HasColumnName("investmentAmount");
            entity.Property(e => e.InvestmentTerm).HasColumnName("investmentTerm");
            entity.Property(e => e.InvestmentType).HasColumnName("investmentType");
            entity.Property(e => e.SimulationDate)
                .HasMaxLength(45)
                .HasColumnName("simulationDate");

            entity.HasOne(d => d.InvestmentTypeNavigation).WithMany(p => p.InvestmentSimulators)
                .HasForeignKey(d => d.InvestmentType)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("investmentType");
        });

        modelBuilder.Entity<InvestmentType>(entity =>
        {
            entity.HasKey(e => e.IdInvestmentType).HasName("PRIMARY");

            entity.ToTable("InvestmentType");

            entity.Property(e => e.IdInvestmentType)
                .ValueGeneratedNever()
                .HasColumnName("idInvestmentType");
            entity.Property(e => e.AnualInterestRate).HasColumnName("anualInterestRate");
            entity.Property(e => e.GatNominal).HasColumnName("GAT_Nominal");
            entity.Property(e => e.GatReal).HasColumnName("GAT_Real");
            entity.Property(e => e.TypeName)
                .HasMaxLength(45)
                .HasColumnName("typeName");
        });

        modelBuilder.Entity<Municipality>(entity =>
        {
            entity.HasKey(e => e.IdMunicipality).HasName("PRIMARY");

            entity.ToTable("Municipality");

            entity.Property(e => e.IdMunicipality)
                .ValueGeneratedNever()
                .HasColumnName("idMunicipality");
            entity.Property(e => e.Name)
                .HasMaxLength(45)
                .HasColumnName("name");
        });

        modelBuilder.Entity<Neighborhood>(entity =>
        {
            entity.HasKey(e => e.IdNeighborhood).HasName("PRIMARY");

            entity.ToTable("Neighborhood");

            entity.HasIndex(e => e.PostalCodeId, "postalCodeId_idx");

            entity.Property(e => e.IdNeighborhood)
                .ValueGeneratedNever()
                .HasColumnName("idNeighborhood");
            entity.Property(e => e.Name)
                .HasMaxLength(45)
                .HasColumnName("name");
            entity.Property(e => e.PostalCodeId).HasColumnName("postalCodeId");

            entity.HasOne(d => d.PostalCode).WithMany(p => p.Neighborhoods)
                .HasForeignKey(d => d.PostalCodeId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("postalCodeId");
        });

        modelBuilder.Entity<OriginOfFound>(entity =>
        {
            entity.HasKey(e => e.IdOriginOfFounds).HasName("PRIMARY");

            entity.Property(e => e.IdOriginOfFounds)
                .ValueGeneratedNever()
                .HasColumnName("idOriginOfFounds");
            entity.Property(e => e.NameOfOrigin)
                .HasMaxLength(45)
                .HasColumnName("nameOfOrigin");
        });

        modelBuilder.Entity<PostalCode>(entity =>
        {
            entity.HasKey(e => e.IdpostalCode).HasName("PRIMARY");

            entity.ToTable("postalCode");

            entity.HasIndex(e => e.MunicipalityId, "municipalityId_idx");

            entity.HasIndex(e => e.StateId, "stateId_idx");

            entity.Property(e => e.IdpostalCode)
                .ValueGeneratedNever()
                .HasColumnName("idpostalCode");
            entity.Property(e => e.MunicipalityId).HasColumnName("municipalityId");
            entity.Property(e => e.StateId).HasColumnName("stateId");

            entity.HasOne(d => d.Municipality).WithMany(p => p.PostalCodes)
                .HasForeignKey(d => d.MunicipalityId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("municipalityId");

            entity.HasOne(d => d.State).WithMany(p => p.PostalCodes)
                .HasForeignKey(d => d.StateId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("stateId");
        });

        modelBuilder.Entity<RequestStatus>(entity =>
        {
            entity.HasKey(e => e.IdRequestStatus).HasName("PRIMARY");

            entity.ToTable("RequestStatus");

            entity.Property(e => e.IdRequestStatus)
                .ValueGeneratedNever()
                .HasColumnName("idRequestStatus");
            entity.Property(e => e.Name)
                .HasMaxLength(45)
                .HasColumnName("name");
        });

        modelBuilder.Entity<State>(entity =>
        {
            entity.HasKey(e => e.IdStates).HasName("PRIMARY");

            entity.ToTable("State");

            entity.Property(e => e.IdStates)
                .ValueGeneratedNever()
                .HasColumnName("idStates");
            entity.Property(e => e.Name)
                .HasMaxLength(45)
                .HasColumnName("name");
        });

        modelBuilder.Entity<User>(entity =>
        {
            entity.HasKey(e => e.IdUser).HasName("PRIMARY");

            entity.ToTable("User");

            entity.HasIndex(e => e.Email, "email");

            entity.Property(e => e.IdUser)
                .ValueGeneratedNever()
                .HasColumnName("idUser");
            entity.Property(e => e.CreateTime)
                .HasDefaultValueSql("CURRENT_TIMESTAMP")
                .HasColumnType("timestamp")
                .HasColumnName("create_time");
            entity.Property(e => e.Email).HasColumnName("email");
            entity.Property(e => e.Password)
                .HasMaxLength(32)
                .HasColumnName("password");
            entity.Property(e => e.Username)
                .HasMaxLength(16)
                .HasColumnName("username");

            entity.HasOne(d => d.IdUserNavigation).WithOne(p => p.User)
                .HasForeignKey<User>(d => d.IdUser)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("cliendId");
        });

        OnModelCreatingPartial(modelBuilder);
    }

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}
