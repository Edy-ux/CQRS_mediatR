using Microsoft.EntityFrameworkCore;
using CQRS_mediatR.Domain;
using CQRS_mediatR.Domain.ValueObjects;

namespace CQRS_mediatR.Data;
public class GamePlayerDbContext : DbContext
{
    public GamePlayerDbContext(DbContextOptions<CQRS_mediatR.Data.GamePlayerDbContext> options)
       : base(options)
    {
    }
    public DbSet<GamePlayer> GamePlayers { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);

        // Configuração da tabela GamePlayer
        modelBuilder.Entity<GamePlayer>(entity =>
        {
            entity.ToTable("GamePlayers");

            // Chave primária
            entity.HasKey(e => e.Id);

            // Configuração das colunas
            entity.Property(e => e.Id)
                .HasColumnName("Id")
                .IsRequired();

            entity.Property(e => e.Name)
                .HasColumnName("Name")
                .HasMaxLength(100)
                .IsRequired();

            entity.Property(e => e.Email)
                .HasColumnName("Email")
                .HasMaxLength(255)
                .IsRequired();

            entity.Property(e => e.Password)
                .HasColumnName("Password")
                .HasMaxLength(255)
                .IsRequired();

            // ValueObject PlayerRole - mapeado como string
            entity.Property(e => e.Role)
                .HasColumnName("Role")
                .HasMaxLength(20)
                .HasConversion(
                    // Converter do ValueObject para string (para salvar no banco)
                    v => v.Value,
                    // Converter da string para ValueObject (para carregar do banco)
                    v => PlayerRole.Create(v).Value
                        )
                        .IsRequired();

            // ValueObject PlayerStatus - mapeado como string
            entity.Property(e => e.Status)
                .HasColumnName("Status")
                .HasMaxLength(20)
                .HasConversion(
                    // Converter do ValueObject para string (para salvar no banco)
                    v => v.Value,
                    // Converter da string para ValueObject (para carregar do banco)
                    v => PlayerStatus.Create(v)
                )
                .IsRequired();

            entity.Property(e => e.CreatedAt)
                .IsRequired();

            entity.Property(e => e.UpdatedAt)
                .IsRequired();

            // Índices para melhor performance
            entity.HasIndex(e => e.Email).IsUnique();
            entity.HasIndex(e => e.Role);
            entity.HasIndex(e => e.Status);
            entity.HasIndex(e => e.CreatedAt);
        });
    }
}