using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Login_Sample.Data
{
    /// <summary>
    /// 备件供应商实体类
    /// </summary>
    public class SparePartsSupplier
    {
        /// <summary>
        /// 主键
        /// </summary>
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }
        
        /// <summary>
        /// 供应商ID
        /// </summary>
        [Required]
        [MaxLength(50)]
        public string SupplierId { get; set; }
        
        /// <summary>
        /// 供应商名称
        /// </summary>
        [Required]
        [MaxLength(100)]
        public string SupplierName { get; set; }
        
        /// <summary>
        /// 联系人
        /// </summary>
        [MaxLength(50)]
        public string ContactPerson { get; set; }
        
        /// <summary>
        /// 联系电话
        /// </summary>
        [MaxLength(20)]
        public string ContactPhone { get; set; }
        
        /// <summary>
        /// 电子邮箱
        /// </summary>
        [MaxLength(100)]
        public string Email { get; set; }
        
        /// <summary>
        /// 地址
        /// </summary>
        [MaxLength(200)]
        public string Address { get; set; }
        
        /// <summary>
        /// 备注
        /// </summary>
        [MaxLength(500)]
        public string Remarks { get; set; }
        
        /// <summary>
        /// 创建时间
        /// </summary>
        public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
        
        /// <summary>
        /// 更新时间
        /// </summary>
        public DateTime UpdatedAt { get; set; } = DateTime.UtcNow;
        
        /// <summary>
        /// 构造函数
        /// </summary>
        public SparePartsSupplier()
        {
            SupplierId = string.Empty;
            SupplierName = string.Empty;
            ContactPerson = string.Empty;
            ContactPhone = string.Empty;
            Email = string.Empty;
            Address = string.Empty;
            Remarks = string.Empty;
        }
    }
}