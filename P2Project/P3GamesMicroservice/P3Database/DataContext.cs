using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;
using Models;

#nullable disable

namespace P3Database
{
    public partial class DataContext : DbContext
    {
        public DataContext()
        {
        }

        public DataContext(DbContextOptions<DataContext> options)
            : base(options)
        {
        }

        public virtual DbSet<CapgameStat> CapgameStats { get; set; }
        public virtual DbSet<GameInfo> GameInfos { get; set; }
        public virtual DbSet<RpsgameStat> RpsgameStats { get; set; }
        public virtual DbSet<WamgameStat> WamgameStats { get; set; }
        public virtual DbSet<WtpgameStat> WtpgameStats { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
//#warning To protect potentially sensitive information in your connection string, you should move it out of source code. You can avoid scaffolding the connection string by using the Name= syntax to read it from configuration - see https://go.microsoft.com/fwlink/?linkid=2131148. For more guidance on storing connection strings, see http://go.microsoft.com/fwlink/?LinkId=723263.
                optionsBuilder.UseSqlServer("Server=tcp:databasetempp3.database.windows.net,1433;Initial Catalog=GamesMicroserviceDB;Persist Security Info=False;User ID=P3Group;Password=Cheeseburger!;MultipleActiveResultSets=False;Encrypt=True;TrustServerCertificate=False;Connection Timeout=30;");
            }
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.HasAnnotation("Relational:Collation", "SQL_Latin1_General_CP1_CI_AS");

            modelBuilder.Entity<CapgameStat>(entity =>
            {
                entity.ToTable("CAPGameStats");
            });

            modelBuilder.Entity<GameInfo>(entity =>
            {
                entity.ToTable("GameInfo");

                entity.Property(e => e.Description)
                    .HasMaxLength(500)
                    .IsUnicode(false);

                entity.Property(e => e.ImagePath)
                    .HasMaxLength(100)
                    .IsUnicode(false);

                entity.Property(e => e.Route)
                    .HasMaxLength(100)
                    .IsUnicode(false);

                entity.Property(e => e.Title)
                    .HasMaxLength(50)
                    .IsUnicode(false);
            });

            modelBuilder.Entity<RpsgameStat>(entity =>
            {
                entity.ToTable("RPSGameStats");
            });

            modelBuilder.Entity<WamgameStat>(entity =>
            {
                entity.ToTable("WAMGameStats");
            });

            modelBuilder.Entity<WtpgameStat>(entity =>
            {
                entity.ToTable("WTPGameStats");
            });

            OnModelCreatingPartial(modelBuilder);
        }

        partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
    }
}
