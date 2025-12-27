using Blockchain.Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace Blockchain.Infrastructure.Persistence;

public sealed class AppDbContext(DbContextOptions<AppDbContext> options) : DbContext(options)
{
    public DbSet<BlockchainSnapshot> BlockchainSnapshots => Set<BlockchainSnapshot>();
    
    protected override void OnModelCreating(ModelBuilder builder)
    {
        builder.Entity<BlockchainSnapshot>(b =>
        {
            b.ToTable("BlockchainSnapshots");
            
            b.HasKey(x => x.Id);
            b.Property(x => x.Id)
                .ValueGeneratedOnAdd();
            
            b.HasIndex(x => new { x.Blockchain, x.Id });
            b.Property(x => x.PayloadJson).HasColumnType("jsonb");
        });
    }

}