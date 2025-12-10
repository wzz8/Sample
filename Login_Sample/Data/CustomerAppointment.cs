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
        public int Id { get; set; }
        
        /// <summary>
        /// 客户ID
        /// </summary>
        public int CustomerId { get; set; }
        
        /// <summary>
        /// 车辆ID
        /// </summary>
        public int VehicleId { get; set; }
        
        /// <summary>
        /// 预约类型
        /// </summary>
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
        public string ServiceContent { get; set; } = string.Empty;
        
        /// <summary>
        /// 预约状态
        /// </summary>
        public string Status { get; set; } = string.Empty;
        
        /// <summary>
        /// 接待人员
        /// </summary>
        public string Receptionist { get; set; } = string.Empty;
        
        /// <summary>
        /// 备注
        /// </summary>
        public string Remarks { get; set; } = string.Empty;
        
        /// <summary>
        /// 创建时间
        /// </summary>
        public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
        
        /// <summary>
        /// 关联的客户
        /// </summary>
        public Customer? Customer { get; set; }
        
        /// <summary>
        /// 关联的车辆
        /// </summary>
        public CustomerVehicle? Vehicle { get; set; }
    }
}