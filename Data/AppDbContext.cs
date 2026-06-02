using Microsoft.EntityFrameworkCore;
using PortfolioManagementMvc.Models;

namespace PortfolioManagementMvc.Data
{
    public class AppDbContext : DbContext
    {
        public DbSet<AssetModel> Assets { get; set; }
        public DbSet<TransactionModel> Transactions { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlite("Data Source=portfolio.db");
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<AssetModel>().HasData(
                new AssetModel { AssetId = 1, AssetSymbol = "GARAN",   AssetName = "Garanti Bankasi",            TargetYear = 2028 },
                new AssetModel { AssetId = 2, AssetSymbol = "ALTINS1", AssetName = "Darphane Altin Sertifikasi", TargetYear = 2028 },
                new AssetModel { AssetId = 3, AssetSymbol = "ANSGR",   AssetName = "Anadolu Sigorta",            TargetYear = 2027 }
            );
        }
    }
}