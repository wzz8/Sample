using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using System.IO;

namespace Login_Sample.Data
{
    /// <summary>
    /// 数据库上下文类
    /// </summary>
    public class ApplicationDbContext : DbContext
    {
        /// <summary>
        /// 用户表
        /// </summary>
        public DbSet<User> Users { get; set; }
        
        /// <summary>
        /// 维修委托书表
        /// </summary>
        public DbSet<WorkOrder> WorkOrders { get; set; }
        
        /// <summary>
        /// 维修项目表
        /// </summary>
        public DbSet<MaintenanceItem> MaintenanceItems { get; set; }
        
        /// <summary>
        /// 库存配件表
        /// </summary>
        public DbSet<InventoryItem> InventoryItems { get; set; }
        
        /// <summary>
        /// 配置数据库连接
        /// </summary>
        /// <param name="optionsBuilder">选项构建器</param>
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
                // 构建配置
                var configuration = new ConfigurationBuilder()
                    .SetBasePath(Directory.GetCurrentDirectory())
                    .AddJsonFile("appsettings.json", optional: false, reloadOnChange: true)
                    .Build();
                
                // 获取连接字符串
                var connectionString = configuration.GetConnectionString("DefaultConnection");
                
                // 配置PostgreSQL数据库
                optionsBuilder.UseNpgsql(connectionString);
            }
        }
        
        /// <summary>
        /// 配置实体
        /// </summary>
        /// <param name="modelBuilder">模型构建器</param>
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
            
            // 配置用户表
            modelBuilder.Entity<User>(entity =>
            {
                entity.ToTable("users");
                
                entity.HasKey(e => e.Id);
                entity.Property(e => e.Id).ValueGeneratedOnAdd();
                
                entity.Property(e => e.Username)
                    .IsRequired()
                    .HasMaxLength(50);
                
                entity.Property(e => e.Email)
                    .IsRequired()
                    .HasMaxLength(100);
                
                entity.Property(e => e.PasswordHash)
                    .IsRequired()
                    .HasMaxLength(255);
                
                entity.Property(e => e.CreatedAt)
                    .IsRequired()
                    .HasColumnType("timestamp with time zone");
                
                // 添加唯一约束
                entity.HasIndex(e => e.Username).IsUnique();
                entity.HasIndex(e => e.Email).IsUnique();
            });
            
            // 配置维修委托书表
            modelBuilder.Entity<WorkOrder>(entity =>
            {
                entity.ToTable("work_orders");
                
                entity.HasKey(e => e.Id);
                entity.Property(e => e.Id).ValueGeneratedOnAdd();
                
                entity.Property(e => e.OrderNumber)
                    .IsRequired()
                    .HasMaxLength(50);
                
                entity.Property(e => e.ReceptionDate)
                    .IsRequired()
                    .HasColumnType("timestamp with time zone");
                
                entity.Property(e => e.Status)
                    .IsRequired()
                    .HasMaxLength(20);
                
                entity.Property(e => e.TotalAmount)
                    .HasColumnType("decimal(18, 2)");
                
                entity.HasOne(e => e.Customer)
                    .WithMany()
                    .HasForeignKey(e => e.CustomerId)
                    .OnDelete(DeleteBehavior.Restrict);
                
                entity.HasOne(e => e.Vehicle)
                    .WithMany()
                    .HasForeignKey(e => e.VehicleId)
                    .OnDelete(DeleteBehavior.Restrict);
            });
            
            // 配置维修项目表
            modelBuilder.Entity<MaintenanceItem>(entity =>
            {
                entity.ToTable("maintenance_items");
                
                entity.HasKey(e => e.Id);
                entity.Property(e => e.Id).ValueGeneratedOnAdd();
                
                entity.Property(e => e.ItemName)
                    .IsRequired()
                    .HasMaxLength(100);
                
                entity.Property(e => e.UnitPrice)
                    .IsRequired()
                    .HasColumnType("decimal(18, 2)");
                
                entity.Property(e => e.Subtotal)
                    .HasColumnType("decimal(18, 2)");
                
                entity.Property(e => e.Status)
                    .HasMaxLength(20);
                
                entity.HasOne(e => e.WorkOrder)
                    .WithMany(e => e.MaintenanceItems)
                    .HasForeignKey(e => e.WorkOrderId)
                    .OnDelete(DeleteBehavior.Cascade);
            });
            
            // 配置库存配件表
            modelBuilder.Entity<InventoryItem>(entity =>
            {
                entity.ToTable("inventory_items");
                
                entity.HasKey(e => e.Id);
                entity.Property(e => e.Id).ValueGeneratedOnAdd();
                
                entity.Property(e => e.ItemName)
                    .IsRequired()
                    .HasMaxLength(100);
                
                entity.Property(e => e.ItemNumber)
                    .IsRequired()
                    .HasMaxLength(50);
                
                entity.Property(e => e.Quantity)
                    .IsRequired();
                
                entity.Property(e => e.UnitPrice)
                    .IsRequired()
                    .HasColumnType("decimal(18, 2)");
                
                entity.Property(e => e.Supplier)
                    .HasMaxLength(100);
                
                entity.Property(e => e.WarehouseLocation)
                    .HasMaxLength(100);
                
                entity.Property(e => e.Status)
                    .HasMaxLength(20);
                
                entity.Property(e => e.BranchName)
                    .HasMaxLength(50);
                
                entity.Property(e => e.LastUpdatedTime)
                    .IsRequired()
                    .HasColumnType("timestamp with time zone");
                
                // 添加索引
                entity.HasIndex(e => e.ItemNumber);
                entity.HasIndex(e => e.ItemName);
                entity.HasIndex(e => e.BranchName);
            });
        }
    }
}