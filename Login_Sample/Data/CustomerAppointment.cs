using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Login_Sample.Data
{
    /// <summary>
    /// 客户预约实体类
    /// </summary>
    public class CustomerAppointment
    {
        /// <summary>
        /// 主键
        /// </summary>
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }

        /// <summary>
        /// 客户ID（外键）
        /// </summary>
        public int CustomerId { get; set; }

        /// <summary>
        /// 车辆ID（外键）
        /// </summary>
        public int VehicleId { get; set; }

        /// <summary>
        /// 维修技师ID（外键）
        /// </summary>
        public int? TechnicianId { get; set; }

        /// <summary>
        /// 预约类型（如：保养、维修、检查等）
        /// </summary>
        [MaxLength(50)]
        public string AppointmentType { get; set; } = string.Empty;

        /// <summary>
        /// 预约日期
        /// </summary>
        public DateTime AppointmentDate { get; set; }

        /// <summary>
        /// 预约时间
        /// </summary>
        public string AppointmentTime { get; set; } = string.Empty;

        /// <summary>
        /// 服务内容
        /// </summary>
        [MaxLength(200)]
        public string ServiceContent { get; set; } = string.Empty;

        /// <summary>
        /// 预约状态（如：待确认、已确认、已完成、已取消等）
        /// </summary>
        [MaxLength(20)]
        public string Status { get; set; } = string.Empty;

        /// <summary>
        /// 接待员
        /// </summary>
        [MaxLength(50)]
        public string Receptionist { get; set; } = string.Empty;

        /// <summary>
        /// 备注
        /// </summary>
        [MaxLength(200)]
        public string Remarks { get; set; } = string.Empty;

        /// <summary>
        /// 创建时间
        /// </summary>
        public DateTime CreatedAt { get; set; } = DateTime.UtcNow;

        /// <summary>
        /// 更新时间
        /// </summary>
        public DateTime UpdatedAt { get; set; } = DateTime.UtcNow;

        /// <summary>
        /// 关联的客户
        /// </summary>
        [ForeignKey(nameof(CustomerId))]
        public Customer? Customer { get; set; }

        /// <summary>
        /// 关联的车辆
        /// </summary>
        [ForeignKey(nameof(VehicleId))]
        public CustomerVehicle? Vehicle { get; set; }

        /// <summary>
        /// 关联的维修技师
        /// </summary>
        [ForeignKey(nameof(TechnicianId))]
        public Technician? Technician { get; set; }
    }
}