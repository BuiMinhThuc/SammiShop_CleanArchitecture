using Microsoft.EntityFrameworkCore;
using SammiShop_CleanArchitecture.Domain.Entities;

namespace SammiShop_CleanArchitecture.Infrastructure.Data
{
    public class AppDbContext : DbContext
    {
        public AppDbContext(DbContextOptions options) : base(options)
        {
        }

        public virtual DbSet<User> Users { get; set; }
        public virtual DbSet<Bill> Bills { get; set; }
        public virtual DbSet<Card> Cards { get; set; }
        public virtual DbSet<ConfirmEmail> ConfirmEmails { get; set; }
        public virtual DbSet<Feedback> Feedbacks { get; set; }
        public virtual DbSet<HistoryPay> HistorryPays { get; set; }
        public virtual DbSet<Product> Products { get; set; }
        public virtual DbSet<ProductType> ProductTypes { get; set; }
        public virtual DbSet<RefreshToken> RefreshTokens { get; set; }
        public virtual DbSet<Role> Roles { get; set; }
        public virtual DbSet<Trademark> Trademarks { get; set; }



    }
}
