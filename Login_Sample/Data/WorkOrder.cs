using System;using System.Collections.Generic;using System.ComponentModel.DataAnnotations;using System.ComponentModel.DataAnnotations.Schema;

namespace Login_Sample.Data{
    /// <summary>
    /// 维修委托书
    /// </summary>
    public class WorkOrder
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }
        
        [Required]
        [MaxLength(50)]
        public string OrderNumber { get; set; }
        
        public int CustomerId { get; set; }
        
        [ForeignKey(nameof(CustomerId))]
        public Customer Customer { get; set; }
        
        public int VehicleId { get; set; }
        
        [ForeignKey(nameof(VehicleId))]
        public CustomerVehicle Vehicle { get; set; }
        
        [Required]
        public DateTime ReceptionDate { get; set; }
        
        [MaxLength(50)]
        public string ServiceAdvisor { get; set; }
        
        [MaxLength(200)]
        public string ProblemDescription { get; set; }
        
        [Required]
        [MaxLength(20)]
        public string Status { get; set; }
        
        public decimal TotalAmount { get; set; }
        
        [MaxLength(200)]
        public string Remarks { get; set; }
        
        public DateTime? CompletedDate { get; set; }
        
        // 导航属性
        public List<MaintenanceItem> MaintenanceItems { get; set; }
    }
}