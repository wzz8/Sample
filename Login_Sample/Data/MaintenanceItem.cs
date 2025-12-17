using System.ComponentModel.DataAnnotations;using System.ComponentModel.DataAnnotations.Schema;

namespace Login_Sample.Data{
    /// <summary>
    /// 维修项目
    /// </summary>
    public class MaintenanceItem
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }
        
        public int WorkOrderId { get; set; }
        
        [ForeignKey(nameof(WorkOrderId))]
        public WorkOrder WorkOrder { get; set; }
        
        [Required]
        [MaxLength(100)]
        public string ItemName { get; set; }
        
        [MaxLength(200)]
        public string Description { get; set; }
        
        [Required]
        public decimal UnitPrice { get; set; }
        
        [Required]
        public int Quantity { get; set; }
        
        public decimal Subtotal { get; set; }
        
        [MaxLength(50)]
        public string Technician { get; set; }
        
        [MaxLength(20)]
        public string Status { get; set; }
    }
}