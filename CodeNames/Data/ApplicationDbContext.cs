using System;
using CodeNames.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;

namespace CodeNames.Data
{
    public partial class ApplicationDbContext : DbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
            : base(options)
        {

        }

        public virtual DbSet<Efmigrationshistory> Efmigrationshistory { get; set; }
        public virtual DbSet<Games> Games { get; set; }
        public virtual DbSet<GamesView> GamesView { get; set; }
        public virtual DbSet<Gameswords> Gameswords { get; set; }
        public virtual DbSet<Parameters> Parameters { get; set; }
        public virtual DbSet<Teams> Teams { get; set; }
        public virtual DbSet<Themes> Themes { get; set; }
        public virtual DbSet<Themeswords> Themeswords { get; set; }
        public virtual DbSet<Words> Words { get; set; }
        public virtual DbSet<WordsView> WordsView { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Efmigrationshistory>(entity =>
            {
                entity.HasKey(e => e.MigrationId)
                    .HasName("PRIMARY");

                entity.ToTable("__efmigrationshistory");

                entity.Property(e => e.MigrationId)
                    .HasColumnType("varchar(95)")
                    .HasCharSet("utf8")
                    .HasCollation("utf8_general_ci");

                entity.Property(e => e.ProductVersion)
                    .IsRequired()
                    .HasColumnType("varchar(32)")
                    .HasCharSet("utf8")
                    .HasCollation("utf8_general_ci");
            });

            modelBuilder.Entity<Games>(entity =>
            {
                entity.ToTable("games");

                entity.HasIndex(e => e.NextToPlayTeamId)
                    .HasName("FK_Games_Teams_NextToPlayTeamId");

                entity.HasIndex(e => e.StartTeamId)
                    .HasName("FK_Games_Teams_StartTeamId");

                entity.Property(e => e.Id).HasColumnType("int(11)");

                entity.Property(e => e.NextToPlayTeamId).HasColumnType("smallint(6)");

                entity.Property(e => e.RoundBlueTeam).HasColumnType("smallint(6)");

                entity.Property(e => e.RoundRedTeam).HasColumnType("smallint(6)");

                entity.Property(e => e.ScoreBlueTeam).HasColumnType("smallint(6)");

                entity.Property(e => e.ScoreRedTeam).HasColumnType("smallint(6)");

                entity.Property(e => e.StartTeamId).HasColumnType("smallint(6)");

                entity.Property(e => e.UpdatedAt).HasDefaultValueSql("'NULL'");

                entity.HasOne(d => d.NextToPlayTeam)
                    .WithMany(p => p.GamesNextToPlayTeam)
                    .HasForeignKey(d => d.NextToPlayTeamId)
                    .HasConstraintName("FK_Games_Teams_NextToPlayTeamId");

                entity.HasOne(d => d.StartTeam)
                    .WithMany(p => p.GamesStartTeam)
                    .HasForeignKey(d => d.StartTeamId)
                    .HasConstraintName("FK_Games_Teams_StartTeamId");
            });

            modelBuilder.Entity<GamesView>(entity =>
            {
                entity.HasNoKey();

                entity.ToView("_gamesview");

                entity.Property(e => e.BackgroundColorName)
                    .HasColumnType("varchar(7)")
                    .HasDefaultValueSql("'NULL'")
                    .HasCharSet("utf8mb4")
                    .HasCollation("utf8mb4_general_ci");

                entity.Property(e => e.ColorName)
                    .IsRequired()
                    .HasColumnType("varchar(7)")
                    .HasCharSet("utf8mb4")
                    .HasCollation("utf8mb4_general_ci");

                entity.Property(e => e.GameId).HasColumnType("int(11)");

                entity.Property(e => e.Order)
                    .HasColumnName("order")
                    .HasColumnType("smallint(6)");

                entity.Property(e => e.TeamId).HasColumnType("smallint(6)");

                entity.Property(e => e.WordId).HasColumnType("int(11)");

                entity.Property(e => e.WordName)
                    .IsRequired()
                    .HasColumnType("varchar(32)")
                    .HasCharSet("utf8mb4")
                    .HasCollation("utf8mb4_general_ci");
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

                entity.Property(e => e.Description)
                    .IsRequired()
                    .HasColumnType("varchar(128)")
                    .HasCharSet("utf8mb4")
                    .HasCollation("utf8mb4_general_ci");

                entity.Property(e => e.Name)
                    .IsRequired()
                    .HasColumnType("varchar(64)")
                    .HasCharSet("utf8mb4")
                    .HasCollation("utf8mb4_general_ci");

                entity.Property(e => e.Value)
                    .IsRequired()
                    .HasColumnType("varchar(128)")
                    .HasCharSet("utf8mb4")
                    .HasCollation("utf8mb4_general_ci");
            });

            modelBuilder.Entity<Teams>(entity =>
            {
                entity.ToTable("teams");

                entity.HasIndex(e => e.Name)
                    .HasName("TeamNameIndex")
                    .IsUnique();

                entity.Property(e => e.Id).HasColumnType("smallint(6)");

                entity.Property(e => e.BackgroundColor)
                    .HasColumnType("varchar(7)")
                    .HasDefaultValueSql("'NULL'")
                    .HasCharSet("utf8mb4")
                    .HasCollation("utf8mb4_general_ci");

                entity.Property(e => e.Color)
                    .IsRequired()
                    .HasColumnType("varchar(7)")
                    .HasCharSet("utf8mb4")
                    .HasCollation("utf8mb4_general_ci");

                entity.Property(e => e.Name)
                    .IsRequired()
                    .HasColumnType("varchar(64)")
                    .HasCharSet("utf8mb4")
                    .HasCollation("utf8mb4_general_ci");
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
                    .HasColumnType("varchar(64)")
                    .HasCharSet("utf8mb4")
                    .HasCollation("utf8mb4_general_ci");
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
                    .HasColumnType("varchar(32)")
                    .HasCharSet("utf8mb4")
                    .HasCollation("utf8mb4_general_ci");
            });

            modelBuilder.Entity<WordsView>(entity =>
            {
                entity.HasNoKey();

                entity.ToView("_wordsview");

                entity.Property(e => e.Id)
                    .HasColumnName("id")
                    .HasColumnType("int(11)");

                entity.Property(e => e.Name)
                    .IsRequired()
                    .HasColumnType("varchar(32)")
                    .HasCharSet("utf8mb4")
                    .HasCollation("utf8mb4_general_ci");

                entity.Property(e => e.ThemesName)
                    .HasColumnType("mediumtext")
                    .HasDefaultValueSql("'NULL'")
                    .HasCharSet("utf8mb4")
                    .HasCollation("utf8mb4_general_ci");
            });

            OnModelCreatingPartial(modelBuilder);
        }

        partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
    }
}
