using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations;

namespace ShopTechWave.Models
{

    //Hello
    //helo mn nè 
    public class AppDbContext : IdentityDbContext<AppUser>
    {
        //Tạo các DbSet (tự động kết nối các bảng dữ liệu trong CSDL)

        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options)
        {
            Database.EnsureCreated();   // Tự động tạo database nếu database không tồn tại
        }

        protected override void OnConfiguring(DbContextOptionsBuilder builder)
        {
            base.OnConfiguring(builder);
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            // Loại bỏ tiền tố "AspNet" cho các bảng trong Identity nếu có
            foreach (var entityType in modelBuilder.Model.GetEntityTypes())
            {
                var tableName = entityType.GetTableName();
                if (tableName.StartsWith("AspNet"))
                {
                    entityType.SetTableName(tableName.Substring(6));
                }
            }

        }

        public DbSet<Product> Products { get; set; }
        public DbSet<Category> Categorys { get; set; }

        public class Product
        {
            public int ProductId { get; set; }
            [MaxLength(100)]
            public string NameProduct { get; set; } = "";
            [MaxLength(100)]
            public string Brand { get; set; } = "";
            [Precision(16, 0)]
            public decimal Price { get; set; } //Giá bán bé hơn giá OldPrice => giảm giá
            [Precision(16, 0)]
            public decimal OldPrice { get; set; } //Giá cũ
            public int Quantity { get; set; }
            public string Description { get; set; } = "";
            [MaxLength(100)]
            public string ImageFileName { get; set; } = "";
            public bool IsActive { get; set; }
            public DateTime CreatedAt { get; set; }

            public int CategoryId { get; set; }
            public Category Category { get; set; } = new Category();
        }

        public class Category
        {
            public int CategoryId { get; set; }
            [MaxLength(100)]
            public string NameCategory { get; set; } = "";
            public bool IsActive { get; set; }
            public DateTime CreatedAt { get; set; }

            public List<Product> Product { get; set; } = new List<Product>();
        }
    }
}