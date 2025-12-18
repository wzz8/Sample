namespace Login_Sample.Data
{
    /// <summary>
    /// 保险索赔实体类
    /// 用于记录客户车辆事故情况、费用估计和保险公司洽谈信息
    /// </summary>
    public class InsuranceClaim
    {
        /// <summary>
        /// 主键
        /// </summary>
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
        /// 保险代理ID（外键）
        /// </summary>
        public int InsuranceAgentId { get; set; }
        
        /// <summary>
        /// 事故编号
        /// </summary>
        public string AccidentNumber { get; set; } = string.Empty;
        
        /// <summary>
        /// 事故日期
        /// </summary>
        public DateTime AccidentDate { get; set; } = DateTime.UtcNow;
        
        /// <summary>
        /// 事故地点
        /// </summary>
        public string AccidentLocation { get; set; } = string.Empty;
        
        /// <summary>
        /// 事故原因
        /// </summary>
        public string AccidentCause { get; set; } = string.Empty;
        
        /// <summary>
        /// 事故描述
        /// </summary>
        public string AccidentDescription { get; set; } = string.Empty;
        
        /// <summary>
        /// 车辆损伤情况
        /// </summary>
        public string VehicleDamage { get; set; } = string.Empty;
        
        /// <summary>
        /// 估计维修费用
        /// </summary>
        public decimal EstimatedCost { get; set; }
        
        /// <summary>
        /// 保险赔付金额
        /// </summary>
        public decimal InsuranceCompensation { get; set; }
        
        /// <summary>
        /// 客户自付金额
        /// </summary>
        public decimal CustomerPayment { get; set; }
        
        /// <summary>
        /// 保险公司名称
        /// </summary>
        public string InsuranceCompany { get; set; } = string.Empty;
        
        /// <summary>
        /// 理赔员姓名
        /// </summary>
        public string ClaimsAdjuster { get; set; } = string.Empty;
        
        /// <summary>
        /// 理赔员联系方式
        /// </summary>
        public string ClaimsAdjusterContact { get; set; } = string.Empty;
        
        /// <summary>
        /// 洽谈状态
        /// </summary>
        public string NegotiationStatus { get; set; } = string.Empty;
        
        /// <summary>
        /// 索赔状态
        /// </summary>
        public string ClaimStatus { get; set; } = string.Empty;
        
        /// <summary>
        /// 预计完成时间
        /// </summary>
        public DateTime? EstimatedCompletionDate { get; set; }
        
        /// <summary>
        /// 实际完成时间
        /// </summary>
        public DateTime? ActualCompletionDate { get; set; }
        
        /// <summary>
        /// 创建时间
        /// </summary>
        public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
        
        /// <summary>
        /// 更新时间
        /// </summary>
        public DateTime UpdatedAt { get; set; } = DateTime.UtcNow;
        
        /// <summary>
        /// 备注
        /// </summary>
        public string Remarks { get; set; } = string.Empty;
        
        /// <summary>
        /// 关联的客户
        /// </summary>
        public Customer? Customer { get; set; }
        
        /// <summary>
        /// 关联的车辆
        /// </summary>
        public CustomerVehicle? Vehicle { get; set; }
        
        /// <summary>
        /// 关联的保险代理
        /// </summary>
        public InsuranceAgent? InsuranceAgent { get; set; }
    }
}