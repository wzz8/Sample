using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Login_Sample.Data
{
    /// <summary>
    /// 维修技师实体类
    /// </summary>
    public class Technician
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }
        
        [Required]
        [MaxLength(50)]
        public string Name { get; set; }
        
        [MaxLength(20)]
        public string Phone { get; set; }
        
        [MaxLength(100)]
        public string Specialty { get; set; } // 专业领域
        
        [MaxLength(20)]
        public string Status { get; set; } // 在线状态：Available（可用）, Busy（忙碌）, Offline（离线）
        
        [MaxLength(200)]
        public string Remarks { get; set; }
    }
}