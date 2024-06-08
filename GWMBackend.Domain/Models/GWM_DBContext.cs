using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;

namespace GWMBackend.Domain.Models
{
    public partial class GWM_DBContext : DbContext
    {
        public GWM_DBContext()
        {
        }

        public GWM_DBContext(DbContextOptions<GWM_DBContext> options)
            : base(options)
        {
        }

        public virtual DbSet<BucketAmount> BucketAmounts { get; set; } = null!;
        public virtual DbSet<ChatLog> ChatLogs { get; set; } = null!;
        public virtual DbSet<Customer> Customers { get; set; } = null!;
        public virtual DbSet<HubConnection> HubConnections { get; set; } = null!;
        public virtual DbSet<Order> Orders { get; set; } = null!;
        public virtual DbSet<Picture> Pictures { get; set; } = null!;
        public virtual DbSet<Product> Products { get; set; } = null!;
        public virtual DbSet<Role> Roles { get; set; } = null!;
        public virtual DbSet<ShopItem> ShopItems { get; set; } = null!;
        public virtual DbSet<User> Users { get; set; } = null!;

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
#warning To protect potentially sensitive information in your connection string, you should move it out of source code. You can avoid scaffolding the connection string by using the Name= syntax to read it from configuration - see https://go.microsoft.com/fwlink/?linkid=2131148. For more guidance on storing connection strings, see http://go.microsoft.com/fwlink/?LinkId=723263.
                optionsBuilder.UseSqlServer("data source=195.248.242.221; initial catalog=GWM_DB;User Id=sa;Password=P@$w0Rd!(2024)@gWm;TrustServerCertificate=true;");
            }
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<BucketAmount>(entity =>
            {
                entity.Property(e => e.Title).HasMaxLength(20);
            });

            modelBuilder.Entity<ChatLog>(entity =>
            {
                entity.Property(e => e.ChatRoom).HasMaxLength(20);

                entity.Property(e => e.CreationDateTime)
                    .HasColumnType("datetime")
                    .HasDefaultValueSql("(getdate())");

                entity.Property(e => e.SenderUsername).HasMaxLength(20);
            });

            modelBuilder.Entity<Customer>(entity =>
            {
                entity.Property(e => e.Email).HasMaxLength(25);

                entity.Property(e => e.FirstName).HasMaxLength(15);

                entity.Property(e => e.IsActive)
                    .IsRequired()
                    .HasDefaultValueSql("((1))");

                entity.Property(e => e.JoinDate)
                    .HasColumnType("datetime")
                    .HasDefaultValueSql("(getdate())");

                entity.Property(e => e.LastName).HasMaxLength(15);

                entity.Property(e => e.Password).HasMaxLength(30);

                entity.Property(e => e.PhoneNumber).HasMaxLength(15);

                entity.Property(e => e.RefreshToken).HasMaxLength(50);

                entity.Property(e => e.RestaurantName).HasMaxLength(25);

                entity.Property(e => e.RoleId).HasDefaultValueSql("((1))");

                entity.Property(e => e.Username).HasMaxLength(20);

                entity.Property(e => e.VerificationCode).HasMaxLength(5);

                entity.Property(e => e.ZipCode).HasMaxLength(10);
            });

            modelBuilder.Entity<HubConnection>(entity =>
            {
                entity.Property(e => e.ChatRoom).HasMaxLength(30);

                entity.Property(e => e.ConnectionId).HasMaxLength(50);

                entity.Property(e => e.CreationDateTime)
                    .HasColumnType("datetime")
                    .HasDefaultValueSql("(getdate())");

                entity.Property(e => e.Username).HasMaxLength(30);
            });

            modelBuilder.Entity<Order>(entity =>
            {
                entity.Property(e => e.CreationDatetime)
                    .HasColumnType("datetime")
                    .HasDefaultValueSql("(getdate())");

                entity.Property(e => e.PickupDate).HasColumnType("datetime");
            });

            modelBuilder.Entity<Picture>(entity =>
            {
                entity.Property(e => e.Address).HasMaxLength(80);

                entity.Property(e => e.ImageName).HasMaxLength(30);

                entity.Property(e => e.Thumbnail).HasMaxLength(80);
            });

            modelBuilder.Entity<Product>(entity =>
            {
                entity.Property(e => e.Description).HasMaxLength(100);

                entity.Property(e => e.IsActive)
                    .IsRequired()
                    .HasDefaultValueSql("((1))");

                entity.Property(e => e.Photo).HasMaxLength(100);

                entity.Property(e => e.Price).HasMaxLength(50);

                entity.Property(e => e.Title).HasMaxLength(50);
            });

            modelBuilder.Entity<Role>(entity =>
            {
                entity.Property(e => e.CreationDateTime)
                    .HasColumnType("datetime")
                    .HasDefaultValueSql("(getdate())");

                entity.Property(e => e.IsActive)
                    .IsRequired()
                    .HasDefaultValueSql("((1))");

                entity.Property(e => e.Title).HasMaxLength(50);
            });

            modelBuilder.Entity<ShopItem>(entity =>
            {
                entity.Property(e => e.CreationDatetime)
                    .HasColumnType("datetime")
                    .HasDefaultValueSql("(getdate())");
            });

            modelBuilder.Entity<User>(entity =>
            {
                entity.Property(e => e.Email).HasMaxLength(25);

                entity.Property(e => e.FirstName).HasMaxLength(15);

                entity.Property(e => e.IsActive)
                    .IsRequired()
                    .HasDefaultValueSql("((1))");

                entity.Property(e => e.JoinDate)
                    .HasColumnType("datetime")
                    .HasDefaultValueSql("(getdate())");

                entity.Property(e => e.LastName).HasMaxLength(15);

                entity.Property(e => e.MobileNumber).HasMaxLength(15);

                entity.Property(e => e.Password).HasMaxLength(50);

                entity.Property(e => e.RefreshToken).HasMaxLength(50);

                entity.Property(e => e.UserName).HasMaxLength(20);

                entity.Property(e => e.VerificationCode).HasMaxLength(5);
            });

            OnModelCreatingPartial(modelBuilder);
        }

        partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
    }
}
