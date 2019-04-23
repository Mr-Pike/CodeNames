using System;
using System.Collections.Generic;
using System.Text;
using CodeNames.Models;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace CodeNames.Data
{
    public class ApplicationDbContext : IdentityDbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
            : base(options)
        {

        }

        public virtual DbSet<Games> Games { get; set; }
        public virtual DbSet<Gameswords> Gameswords { get; set; }
        public virtual DbSet<Parameters> Parameters { get; set; }
        public virtual DbSet<Teams> Teams { get; set; }
        public virtual DbSet<Themes> Themes { get; set; }
        public virtual DbSet<Themeswords> Themeswords { get; set; }
        public virtual DbSet<Words> Words { get; set; }

        public virtual DbSet<GamesView> GamesView { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.Entity<Games>(entity =>
            {
                entity.ToTable("games");

                entity.HasIndex(e => e.NextToPlayTeamId)
                    .HasName("FK_Games_Teams_NextToPlayTeamId");

                entity.HasIndex(e => e.StartTeamId)
                    .HasName("FK_Games_Teams_StartTeamId");

                entity.Property(e => e.Id).HasColumnType("int(11)");

                entity.Property(e => e.NextToPlayTeamId).HasColumnType("smallint(6)");

                entity.Property(e => e.RoundBlueTeam)
                    .HasColumnType("smallint(6)")
                    .HasDefaultValueSql("'0'");

                entity.Property(e => e.RoundRedTeam)
                    .HasColumnType("smallint(6)")
                    .HasDefaultValueSql("'0'");

                entity.Property(e => e.ScoreBlueTeam)
                    .HasColumnType("smallint(6)")
                    .HasDefaultValueSql("'0'");

                entity.Property(e => e.ScoreRedTeam)
                    .HasColumnType("smallint(6)")
                    .HasDefaultValueSql("'0'");

                entity.Property(e => e.StartTeamId).HasColumnType("smallint(6)");

                entity.HasOne(d => d.NextToPlayTeam)
                    .WithMany(p => p.GamesNextToPlayTeam)
                    .HasForeignKey(d => d.NextToPlayTeamId)
                    .HasConstraintName("FK_Games_Teams_NextToPlayTeamId");

                entity.HasOne(d => d.StartTeam)
                    .WithMany(p => p.GamesStartTeam)
                    .HasForeignKey(d => d.StartTeamId)
                    .HasConstraintName("FK_Games_Teams_StartTeamId");
            });

            modelBuilder.Entity<Gameswords>(entity =>
            {
                entity.HasKey(e => new { e.GameId, e.WordId })
                    .HasName("PRIMARY");

                entity.ToTable("gameswords");

                entity.HasIndex(e => e.TeamId)
                    .HasName("FK_GamesWords_Teams_TeamId");

                entity.HasIndex(e => e.WordId)
                    .HasName("FK_GamesWords_Words_WordId");

                entity.Property(e => e.GameId).HasColumnType("int(11)");

                entity.Property(e => e.WordId).HasColumnType("int(11)");

                entity.Property(e => e.Find)
                    .IsRequired()
                    .HasColumnType("bit(1)")
                    .HasDefaultValueSql("'b\\'0\\''");

                entity.Property(e => e.Order).HasColumnType("smallint(6)");

                entity.Property(e => e.TeamId).HasColumnType("smallint(6)");

                entity.HasOne(d => d.Game)
                    .WithMany(p => p.Gameswords)
                    .HasForeignKey(d => d.GameId)
                    .HasConstraintName("FK_GamesWords_Games_GameId");

                entity.HasOne(d => d.Team)
                    .WithMany(p => p.Gameswords)
                    .HasForeignKey(d => d.TeamId)
                    .HasConstraintName("FK_GamesWords_Teams_TeamId");

                entity.HasOne(d => d.Word)
                    .WithMany(p => p.Gameswords)
                    .HasForeignKey(d => d.WordId)
                    .HasConstraintName("FK_GamesWords_Words_WordId");
            });

            modelBuilder.Entity<Parameters>(entity =>
            {
                entity.ToTable("parameters");

                entity.HasIndex(e => e.Name)
                    .HasName("ParameterNameIndex")
                    .IsUnique();

                entity.Property(e => e.Id).HasColumnType("smallint(6)");

                entity.Property(e => e.Name)
                    .IsRequired()
                    .HasColumnType("varchar(64)");
            });

            modelBuilder.Entity<Teams>(entity =>
            {
                entity.ToTable("teams");

                entity.HasIndex(e => e.Name)
                    .HasName("TeamNameIndex")
                    .IsUnique();

                entity.Property(e => e.Id).HasColumnType("smallint(6)");

                entity.Property(e => e.Color).HasColumnType("varchar(7)");

                entity.Property(e => e.BackgroundColor).HasColumnType("varchar(7)");

                entity.Property(e => e.Name)
                    .IsRequired()
                    .HasColumnType("varchar(64)");
            });

            modelBuilder.Entity<Themes>(entity =>
            {
                entity.ToTable("themes");

                entity.HasIndex(e => e.Name)
                    .HasName("ThemeNameIndex")
                    .IsUnique();

                entity.Property(e => e.Id).HasColumnType("int(11)");

                entity.Property(e => e.Name)
                    .IsRequired()
                    .HasColumnType("varchar(64)");
            });

            modelBuilder.Entity<Themeswords>(entity =>
            {
                entity.HasKey(e => new { e.ThemeId, e.WordId })
                    .HasName("PRIMARY");

                entity.ToTable("themeswords");

                entity.HasIndex(e => e.WordId)
                    .HasName("FK_ThemesWords_Words_WordId");

                entity.Property(e => e.ThemeId).HasColumnType("int(11)");

                entity.Property(e => e.WordId).HasColumnType("int(11)");

                entity.HasOne(d => d.Theme)
                    .WithMany(p => p.Themeswords)
                    .HasForeignKey(d => d.ThemeId)
                    .HasConstraintName("FK_ThemesWords_Themes_ThemeId");

                entity.HasOne(d => d.Word)
                    .WithMany(p => p.Themeswords)
                    .HasForeignKey(d => d.WordId)
                    .HasConstraintName("FK_ThemesWords_Words_WordId");
            });

            modelBuilder.Entity<Words>(entity =>
            {
                entity.ToTable("words");

                entity.HasIndex(e => e.Name)
                    .HasName("WordsNameIndex")
                    .IsUnique();

                entity.Property(e => e.Id).HasColumnType("int(11)");

                entity.Property(e => e.Name)
                    .IsRequired()
                    .HasColumnType("varchar(128)");
            });

            modelBuilder.Entity<GamesView>(entity =>
            {
                entity.ToTable("_gamesview");

                entity.HasKey(e => new { e.GameId, e.WordId })
                    .HasName("PRIMARY");

                entity.Property(e => e.GameId);

                entity.Property(e => e.WordId);

                entity.Property(e => e.Find);

                entity.Property(e => e.Order);

                entity.Property(e => e.TeamId);

                entity.Property(e => e.WordName);

                entity.Property(e => e.ColorName);

                entity.Property(e => e.BackgroundColorName);
            });
        }
    }
}
