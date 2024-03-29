﻿using Core.Security.Entities;
using Kodlama.io.Devs2.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;

namespace Kodlama.io.Devs2.Persistence.Contexts;

public class BaseDbContext : DbContext
{
    protected IConfiguration Configuration { get; set; }
    public DbSet<ProgrammingLanguage> ProgrammingLanguages { get; set; }
    public DbSet<Technology> Technologies { get; set; }


    #region JWT (Json Web Token)
    public DbSet<User> Users { get; set; }
    public DbSet<OperationClaim> OperationClaims { get; set; }
    public DbSet<UserOperationClaim> UserOperationClaims { get; set; }
    public DbSet<RefreshToken> RefreshTokens { get; set; }
    #endregion

    public BaseDbContext(DbContextOptions dbContextOptions, IConfiguration configuration) : base(dbContextOptions)
    {
        Configuration = configuration;
    }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
        //if (!optionsBuilder.IsConfigured)
        //    base.OnConfiguring(
        //        optionsBuilder.UseSqlServer(Configuration.GetConnectionString("SomeConnectionString")));
    }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        // Sql de dahada geliştirme yapılmak istenirse "EF Fluent Mapping" yazarak bakabiliriz


        // Model oluşturulduğunda Program Dilleri nesnesi hangi tabloya karşılık gelmesi gerektiği yazılır "programmingLanguages"
        modelBuilder.Entity<ProgrammingLanguage>(a =>
        {
            a.ToTable("ProgrammingLanguages").HasKey(k => k.Id);
            a.Property(p => p.Id).HasColumnName("Id");
            a.Property(p => p.Name).HasColumnName("Name");


            #region Teknolojiler ile bağlantı
            a.HasMany(p => p.Technologies);  // Bir programlama dilinin birden fazla teknolojisi olabileceği için bu şekilde yazıldı
            #endregion
        });

        modelBuilder.Entity<Technology>(a =>
        {
            a.ToTable("Technologies").HasKey(k => k.Id); // PK
            a.Property(p => p.Id).HasColumnName("Id");
            a.Property(p => p.ProgrammingLanguageId).HasColumnName("ProgrammingLanguageId"); // FK ( yabancıl anahtar / foreing key )
            a.Property(p => p.Name).HasColumnName("Name");


            #region Programlama Dili ile bağlantı
            a.HasOne(p => p.ProgrammingLanguage); // Bir teknolojinin Bir programlama dili olur 
            #endregion
        });

        modelBuilder.Entity<User>(a =>
        {
            a.ToTable("Users").HasKey(k => k.Id);
            a.Property(p => p.Id).HasColumnName("Id");
            a.Property(p => p.FirstName).HasColumnName("FirstName");
            a.Property(p => p.LastName).HasColumnName("LastName");
            a.Property(p => p.Email).HasColumnName("Email");
            a.Property(p => p.PasswordSalt).HasColumnName("PasswordSalt");
            a.Property(p => p.PasswordHash).HasColumnName("PasswordHash");
            a.Property(p => p.Status).HasColumnName("Status");
            a.Property(p => p.AuthenticatorType).HasColumnName("AuthenticatorType");

            #region Diğer Tablolar ile bağlantı
            a.HasMany(p => p.UserOperationClaims);
            a.HasMany(p => p.RefreshTokens);
            #endregion
        });

        modelBuilder.Entity<OperationClaim>(a =>
        {
            a.ToTable("OperationClaims").HasKey(k => k.Id);
            a.Property(p => p.Id).HasColumnName("Id");
            a.Property(p => p.Name).HasColumnName("Name");
        });

        modelBuilder.Entity<UserOperationClaim>(a =>
        {
            a.ToTable("UserOperationClaims").HasKey(k => k.Id);
            a.Property(p => p.Id).HasColumnName("Id");
            a.Property(p => p.UserId).HasColumnName("UserId");
            a.Property(p => p.OperationClaimId).HasColumnName("OperationClaimId");

            #region User ve OperationClaim ile Bağlantı
            a.HasOne(p => p.User);
            a.HasOne(p => p.OperationClaim);
            #endregion
        });

        modelBuilder.Entity<RefreshToken>(a =>
        {
            a.ToTable("RefreshTokens").HasKey(k => k.Id);
            a.Property(p => p.Id).HasColumnName("Id");
            a.Property(p => p.UserId).HasColumnName("UserId");
            a.Property(p => p.Token).HasColumnName("Token");
            a.Property(p => p.Expires).HasColumnName("Expires");
            a.Property(p => p.Created).HasColumnName("Created");
            a.Property(p => p.CreatedByIp).HasColumnName("CreatedByIp");
            a.Property(p => p.Revoked).HasColumnName("Revoked");
            a.Property(p => p.RevokedByIp).HasColumnName("RevokedByIp");
            a.Property(p => p.ReplacedByToken).HasColumnName("ReplacedByToken");
            a.Property(p => p.ReasonRevoked).HasColumnName("ReasonRevoked");

            #region User ile Bağlantı
            a.HasOne(p => p.User);
            #endregion
        });

        ProgrammingLanguage[] programmingLanguageSeeds = { new(1, "c#"), new(2, "java"), new(3, "c++") };
        modelBuilder.Entity<ProgrammingLanguage>().HasData(programmingLanguageSeeds);

        Technology[] technologyEntitySeeds = { new(1, 1, ".net Core"), new(2, 2, "react"), new(3, 3, "CCNA") };
        modelBuilder.Entity<Technology>().HasData(technologyEntitySeeds);
    }
}
