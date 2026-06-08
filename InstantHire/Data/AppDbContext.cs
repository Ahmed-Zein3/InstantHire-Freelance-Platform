using Microsoft.EntityFrameworkCore;
using InstantHire.Domain.Entities;

namespace InstantHire.Data;

public class AppDbContext : DbContext
{
    public AppDbContext(DbContextOptions<AppDbContext> options)
        : base(options)
    {
    }

    // =========================
    // DbSets (Tables)
    // =========================

    public DbSet<Client> Clients { get; set; }
    public DbSet<Freelancer> Freelancers { get; set; }
    public DbSet<Project> Projects { get; set; }
    public DbSet<Bid> Bids { get; set; }
    public DbSet<Skill> Skills { get; set; }
    public DbSet<Review> Reviews { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);

        // =====================================================
        // Client → Projects (1 : Many)
        // =====================================================
        modelBuilder.Entity<Project>()
            .HasOne(p => p.Client)
            .WithMany(c => c.Projects)
            .HasForeignKey(p => p.ClientId)
            .OnDelete(DeleteBehavior.Restrict);

        // =====================================================
        // Freelancer → Skills (1 : Many)
        // =====================================================
        modelBuilder.Entity<Skill>()
            .HasOne(s => s.Freelancer)
            .WithMany(f => f.Skills)
            .HasForeignKey(s => s.FreelancerId)
            .OnDelete(DeleteBehavior.Cascade);

        // =====================================================
        // Freelancer → Bids (1 : Many)
        // =====================================================
        modelBuilder.Entity<Bid>()
            .HasOne(b => b.Freelancer)
            .WithMany(f => f.Bids)
            .HasForeignKey(b => b.FreelancerId)
            .OnDelete(DeleteBehavior.Restrict);

        // =====================================================
        // Project → Bids (1 : Many)
        // =====================================================
        modelBuilder.Entity<Bid>()
            .HasOne(b => b.Project)
            .WithMany(p => p.Bids)
            .HasForeignKey(b => b.ProjectId)
            .OnDelete(DeleteBehavior.Cascade);

        // =====================================================
        // Project → Review (1 : 1)
        // =====================================================
        modelBuilder.Entity<Project>()
            .HasOne(p => p.Review)
            .WithOne(r => r.Project)
            .HasForeignKey<Review>(r => r.ProjectId)
            .OnDelete(DeleteBehavior.Restrict);

        // =====================================================
        // Freelancer → Review (1 : Many)
        // =====================================================
        modelBuilder.Entity<Review>()
            .HasOne(r => r.Freelancer)
            .WithMany()
            .HasForeignKey(r => r.FreelancerId)
            .OnDelete(DeleteBehavior.Restrict);

        // =====================================================
        // Decimal Precision
        // =====================================================
        modelBuilder.Entity<Project>()
            .Property(p => p.Budget)
            .HasColumnType("decimal(12,2)");

        modelBuilder.Entity<Bid>()
            .Property(b => b.Amount)
            .HasColumnType("decimal(12,2)");

        // =====================================================
        // SQL Logging (Development only)
        // =====================================================
        modelBuilder
            .HasAnnotation("Relational:LogToConsole", true);
    }
}