using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;

#nullable disable

namespace P2DbContext.Models
{
    public partial class P2DbClass : DbContext
    {
        public P2DbClass()
        {
        }

        public P2DbClass(DbContextOptions<P2DbClass> options)
            : base(options)
        {
        }

        public virtual DbSet<CardCollection> CardCollections { get; set; }
        public virtual DbSet<DisplayBoard> DisplayBoards { get; set; }
        public virtual DbSet<PokemonCard> PokemonCards { get; set; }
        public virtual DbSet<Post> Posts { get; set; }
        public virtual DbSet<PostType> PostTypes { get; set; }
        public virtual DbSet<RarityType> RarityTypes { get; set; }
        public virtual DbSet<User> Users { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
                optionsBuilder.UseSqlServer("Server=tcp:p2pokelootserver.database.windows.net,1433;Initial Catalog=PokeLoot;Persist Security Info=False;User ID=christian.romero@revature.net@p2pokelootserver;Password=P2PokeLoot;MultipleActiveResultSets=False;Encrypt=True;TrustServerCertificate=False;Connection Timeout=30;");
            }
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.HasAnnotation("Relational:Collation", "SQL_Latin1_General_CP1_CI_AS");

            modelBuilder.Entity<CardCollection>(entity =>
            {
                entity.HasKey(e => new { e.PokemonId, e.UserId })
                    .HasName("PK_orders");

                entity.ToTable("CardCollection");

                entity.HasIndex(e => e.PokemonId, "fkIdx_90");

                entity.HasIndex(e => e.UserId, "fkIdx_93");

                entity.HasOne(d => d.Pokemon)
                    .WithMany(p => p.CardCollections)
                    .HasForeignKey(d => d.PokemonId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_89");

                entity.HasOne(d => d.User)
                    .WithMany(p => p.CardCollections)
                    .HasForeignKey(d => d.UserId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_92");
            });

            modelBuilder.Entity<DisplayBoard>(entity =>
            {
                entity.HasKey(e => e.PostId)
                    .HasName("PK_displayboard");

                entity.ToTable("DisplayBoard");

                entity.HasIndex(e => e.UserId, "fkIdx_102");

                entity.HasIndex(e => e.PostId, "fkIdx_109");

                entity.HasIndex(e => e.PostType, "fkIdx_99");

                entity.Property(e => e.PostId).ValueGeneratedNever();

                entity.HasOne(d => d.Post)
                    .WithOne(p => p.DisplayBoard)
                    .HasForeignKey<DisplayBoard>(d => d.PostId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_108");

                entity.HasOne(d => d.PostTypeNavigation)
                    .WithMany(p => p.DisplayBoards)
                    .HasForeignKey(d => d.PostType)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_98");

                entity.HasOne(d => d.User)
                    .WithMany(p => p.DisplayBoards)
                    .HasForeignKey(d => d.UserId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_101");
            });

            modelBuilder.Entity<PokemonCard>(entity =>
            {
                entity.HasKey(e => e.PokemonId)
                    .HasName("PK_table_70");

                entity.HasIndex(e => e.RarityId, "fkIdx_77");

                entity.Property(e => e.PokemonId).ValueGeneratedNever();

                entity.Property(e => e.PokemonName)
                    .IsRequired()
                    .HasMaxLength(20)
                    .IsUnicode(false);

                entity.Property(e => e.SpriteLink)
                    .IsRequired()
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.Property(e => e.SpriteLinkShiny)
                    .IsRequired()
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.HasOne(d => d.Rarity)
                    .WithMany(p => p.PokemonCards)
                    .HasForeignKey(d => d.RarityId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_76");
            });

            modelBuilder.Entity<Post>(entity =>
            {
                entity.HasIndex(e => e.PokemonId, "fkIdx_114");

                entity.Property(e => e.PostDescription)
                    .HasMaxLength(200)
                    .IsUnicode(false);

                entity.Property(e => e.PostTime).HasColumnType("datetime");

                entity.HasOne(d => d.Pokemon)
                    .WithMany(p => p.Posts)
                    .HasForeignKey(d => d.PokemonId)
                    .HasConstraintName("FK_113");
            });

            modelBuilder.Entity<PostType>(entity =>
            {
                entity.HasKey(e => e.PostType1)
                    .HasName("PK_posttypes");

                entity.Property(e => e.PostType1)
                    .ValueGeneratedNever()
                    .HasColumnName("PostType");

                entity.Property(e => e.PostCategory)
                    .IsRequired()
                    .HasMaxLength(20)
                    .IsUnicode(false);
            });

            modelBuilder.Entity<RarityType>(entity =>
            {
                entity.HasKey(e => e.RarityId)
                    .HasName("PK_raritytypes");

                entity.Property(e => e.RarityId).ValueGeneratedNever();

                entity.Property(e => e.RarityCategory)
                    .IsRequired()
                    .HasMaxLength(20)
                    .IsUnicode(false);
            });

            modelBuilder.Entity<User>(entity =>
            {
                entity.HasIndex(e => e.Email, "unique_email")
                    .IsUnique();

                entity.HasIndex(e => e.UserName, "unique_username")
                    .IsUnique();

                entity.Property(e => e.Email)
                    .IsRequired()
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.Property(e => e.FirstName)
                    .IsRequired()
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.Property(e => e.LastName)
                    .IsRequired()
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.Property(e => e.Password)
                    .IsRequired()
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.Property(e => e.UserName)
                    .IsRequired()
                    .HasMaxLength(50)
                    .IsUnicode(false);
            });

            OnModelCreatingPartial(modelBuilder);
        }

        partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
    }
}
