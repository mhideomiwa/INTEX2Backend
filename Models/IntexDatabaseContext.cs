using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;

namespace Intex2Backend.Models;

public partial class IntexDatabaseContext : DbContext
{
    public IntexDatabaseContext()
    {
    }

    public IntexDatabaseContext(DbContextOptions<IntexDatabaseContext> options)
        : base(options)
    {
    }

    public virtual DbSet<ContentFilteringRecommendation> ContentFilteringRecommendations { get; set; }

    public virtual DbSet<Customer> Customers { get; set; }

    public virtual DbSet<LineItem> LineItems { get; set; }

    public virtual DbSet<Order> Orders { get; set; }

    public virtual DbSet<Product> Products { get; set; }

    public virtual DbSet<ProductRecommendation> ProductRecommendations { get; set; }

    public virtual DbSet<UserRecommendation> UserRecommendations { get; set; }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
#warning To protect potentially sensitive information in your connection string, you should move it out of source code. You can avoid scaffolding the connection string by using the Name= syntax to read it from configuration - see https://go.microsoft.com/fwlink/?linkid=2131148. For more guidance on storing connection strings, see https://go.microsoft.com/fwlink/?LinkId=723263.
        => optionsBuilder.UseSqlServer("Server=tcp:myfreesqldbserverintexdatabase.database.windows.net,1433;Initial Catalog=IntexDatabase;Persist Security Info=False;User ID=IntexDatabaseAdmin;Password=IntexPassword!;MultipleActiveResultSets=False;Encrypt=True;TrustServerCertificate=False;Connection Timeout=30;");

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<ContentFilteringRecommendation>(entity =>
        {
            entity.HasKey(e => e.Index);

            entity.ToTable("ContentFilteringRecommendation");

            entity.Property(e => e.Index).HasColumnName("index");
            entity.Property(e => e.IfYouLiked)
                .HasMaxLength(100)
                .HasColumnName("If_you_liked");
            entity.Property(e => e.Recommendation1)
                .HasMaxLength(100)
                .HasColumnName("Recommendation_1");
            entity.Property(e => e.Recommendation2)
                .HasMaxLength(50)
                .HasColumnName("Recommendation_2");
            entity.Property(e => e.Recommendation3)
                .HasMaxLength(100)
                .HasColumnName("Recommendation_3");
            entity.Property(e => e.Recommendation4)
                .HasMaxLength(50)
                .HasColumnName("Recommendation_4");
            entity.Property(e => e.Recommendation5)
                .HasMaxLength(50)
                .HasColumnName("Recommendation_5");
        });

        modelBuilder.Entity<Customer>(entity =>
        {
            entity.ToTable("Customer");

            entity.Property(e => e.CustomerId)
                .ValueGeneratedNever()
                .HasColumnName("customer_ID");
            entity.Property(e => e.Age).HasColumnName("age");
            entity.Property(e => e.BirthDate).HasColumnName("birth_date");
            entity.Property(e => e.CountryOfResidence)
                .HasMaxLength(50)
                .HasColumnName("country_of_residence");
            entity.Property(e => e.FirstName)
                .HasMaxLength(50)
                .HasColumnName("first_name");
            entity.Property(e => e.Gender)
                .HasMaxLength(50)
                .HasColumnName("gender");
            entity.Property(e => e.LastName)
                .HasMaxLength(50)
                .HasColumnName("last_name");
        });

        modelBuilder.Entity<LineItem>(entity =>
        {
            entity.HasKey(e => new { e.TransactionId, e.ProductId });

            entity.ToTable("LineItem");

            entity.Property(e => e.TransactionId).HasColumnName("transaction_ID");
            entity.Property(e => e.ProductId).HasColumnName("product_ID");
            entity.Property(e => e.Qty).HasColumnName("qty");
            entity.Property(e => e.Rating).HasColumnName("rating");
        });

        modelBuilder.Entity<Order>(entity =>
        {
            entity.HasKey(e => new { e.TransactionId, e.CustomerId });

            entity.ToTable("Order");

            entity.Property(e => e.TransactionId).HasColumnName("transaction_ID");
            entity.Property(e => e.CustomerId).HasColumnName("customer_ID");
            entity.Property(e => e.Amount).HasColumnName("amount");
            entity.Property(e => e.Bank)
                .HasMaxLength(50)
                .HasColumnName("bank");
            entity.Property(e => e.CountryOfTransaction)
                .HasMaxLength(50)
                .HasColumnName("country_of_transaction");
            entity.Property(e => e.Date).HasColumnName("date");
            entity.Property(e => e.DayOfWeek)
                .HasMaxLength(50)
                .HasColumnName("day_of_week");
            entity.Property(e => e.EntryMode)
                .HasMaxLength(50)
                .HasColumnName("entry_mode");
            entity.Property(e => e.Fraud).HasColumnName("fraud");
            entity.Property(e => e.ShippingAddress)
                .HasMaxLength(50)
                .HasColumnName("shipping_address");
            entity.Property(e => e.Time).HasColumnName("time");
            entity.Property(e => e.TypeOfCard)
                .HasMaxLength(50)
                .HasColumnName("type_of_card");
            entity.Property(e => e.TypeOfTransaction)
                .HasMaxLength(50)
                .HasColumnName("type_of_transaction");
        });

        modelBuilder.Entity<Product>(entity =>
        {
            entity.ToTable("Product");

            entity.Property(e => e.ProductId).HasColumnName("product_ID");
            entity.Property(e => e.Category)
                .HasMaxLength(50)
                .HasColumnName("category");
            entity.Property(e => e.Description)
                .HasMaxLength(2800)
                .HasColumnName("description");
            entity.Property(e => e.ImgLink)
                .HasMaxLength(150)
                .HasColumnName("img_link");
            entity.Property(e => e.Name)
                .HasMaxLength(100)
                .HasColumnName("name");
            entity.Property(e => e.NumParts).HasColumnName("num_parts");
            entity.Property(e => e.Price).HasColumnName("price");
            entity.Property(e => e.PrimaryColor)
                .HasMaxLength(50)
                .HasColumnName("primary_color");
            entity.Property(e => e.SecondaryColor)
                .HasMaxLength(50)
                .HasColumnName("secondary_color");
            entity.Property(e => e.Slug)
                .HasMaxLength(100)
                .HasColumnName("slug");
            entity.Property(e => e.Year).HasColumnName("year");
        });

        modelBuilder.Entity<ProductRecommendation>(entity =>
        {
            entity.HasKey(e => e.Index);

            entity.ToTable("ProductRecommendation");

            entity.Property(e => e.Index).HasColumnName("index");
            entity.Property(e => e.IfYouLiked)
                .HasMaxLength(100)
                .HasColumnName("If_you_liked");
            entity.Property(e => e.Recommendation1)
                .HasMaxLength(50)
                .HasColumnName("Recommendation_1");
            entity.Property(e => e.Recommendation2)
                .HasMaxLength(50)
                .HasColumnName("Recommendation_2");
            entity.Property(e => e.Recommendation3)
                .HasMaxLength(50)
                .HasColumnName("Recommendation_3");
            entity.Property(e => e.Recommendation4)
                .HasMaxLength(50)
                .HasColumnName("Recommendation_4");
            entity.Property(e => e.Recommendation5)
                .HasMaxLength(50)
                .HasColumnName("Recommendation_5");
        });

        modelBuilder.Entity<UserRecommendation>(entity =>
        {
            entity.HasKey(e => e.UserId);

            entity.Property(e => e.UserId)
                .ValueGeneratedNever()
                .HasColumnName("User_ID");
            entity.Property(e => e.Recommendation1)
                .HasMaxLength(50)
                .HasColumnName("Recommendation_1");
            entity.Property(e => e.Recommendation2)
                .HasMaxLength(50)
                .HasColumnName("Recommendation_2");
            entity.Property(e => e.Recommendation3)
                .HasMaxLength(50)
                .HasColumnName("Recommendation_3");
            entity.Property(e => e.Recommendation4)
                .HasMaxLength(50)
                .HasColumnName("Recommendation_4");
            entity.Property(e => e.Recommendation5)
                .HasMaxLength(50)
                .HasColumnName("Recommendation_5");
            entity.Property(e => e.TopRatedProduct)
                .HasMaxLength(100)
                .HasColumnName("Top_Rated_Product");
        });

        OnModelCreatingPartial(modelBuilder);
    }

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}
