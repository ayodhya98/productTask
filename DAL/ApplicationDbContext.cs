using DAL.Model;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace DAL
{
    public class ApplicationDbContext : IdentityDbContext<IdentityUser>
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options)
        {
        }
        public DbSet<Product> Products { get; set; }
        public DbSet<Review> Reviews { get; set; }
        public DbSet<Category> categories { get; set; }
        public DbSet<Reviewer> reviewers { get; set; }
        public DbSet<ProductCategory> productCat { get; set; }
        public DbSet<ApplicationUser> ApplicationUsers { get; set; }
        public DbSet<Customer> customers { get; set; }
        public DbSet<ShoppingCart> shoppingCarts { get; set; }
        public DbSet<RefreshToken> refreshTokens { get; set; }


        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);

            builder.Entity<ProductCategory>()
                .HasKey(pc => new { pc.ProductId, pc.CategoryId });
            builder.Entity<ProductCategory>()
                .HasOne(p => p.product)
                .WithMany(pc => pc.productCategories)
                .HasForeignKey(c => c.ProductId);

            builder.Entity<ProductCategory>()
            .HasOne(p => p.category)
            .WithMany(pc => pc.productCategories)
            .HasForeignKey(c => c.CategoryId);
        }
    }
}