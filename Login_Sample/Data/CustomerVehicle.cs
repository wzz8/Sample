namespace Login_Sample.Data
{
    /// <summary>
    /// 客户车辆实体类
    /// </summary>
    public class CustomerVehicle
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
        /// 车牌号
        /// </summary>
        public string LicensePlate { get; set; } = string.Empty;
        
        /// <summary>
        /// 车型
        /// </summary>
        public string VehicleModel { get; set; } = string.Empty;
        
        /// <summary>
        /// 车辆品牌
        /// </summary>
        public string VehicleBrand { get; set; } = string.Empty;
        
        /// <summary>
        /// 车辆颜色
        /// </summary>
        public string VehicleColor { get; set; } = string.Empty;
        
        /// <summary>
        /// 发动机号
        /// </summary>
        public string EngineNumber { get; set; } = string.Empty;
        
        /// <summary>
        /// 车架号
        /// </summary>
        public string ChassisNumber { get; set; } = string.Empty;
        
        /// <summary>
        /// 购买日期
        /// </summary>
        public DateTime PurchaseDate { get; set; }
        
        /// <summary>
        /// 行驶里程
        /// </summary>
        public int Mileage { get; set; }
        
        /// <summary>
        /// 上次保养日期
        /// </summary>
        public DateTime? LastMaintenanceDate { get; set; }
        
        /// <summary>
        /// 下次保养里程
        /// </summary>
        public int? NextMaintenanceMileage { get; set; }
        
        /// <summary>
        /// 车辆状态
        /// </summary>
        public string Status { get; set; } = string.Empty;
        
        /// <summary>
        /// 备注
        /// </summary>
        public string Remarks { get; set; } = string.Empty;
        
        /// <summary>
        /// 关联的客户
        /// </summary>
        public Customer? Customer { get; set; }
    }
}