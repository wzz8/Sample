namespace Login_Sample.Data
{
    /// <summary>
    /// 特殊客户实体类
    /// </summary>
    public class SpecialCustomer
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
        /// 特殊客户类型
        /// </summary>
        public string SpecialType { get; set; } = string.Empty;
        
        /// <summary>
        /// 特殊客户状态
        /// </summary>
        public string Status { get; set; } = string.Empty;
        
        /// <summary>
        /// 特殊原因
        /// </summary>
        public string SpecialReason { get; set; } = string.Empty;
        
        /// <summary>
        /// 开始日期
        /// </summary>
        public DateTime StartDate { get; set; } = DateTime.UtcNow;
        
        /// <summary>
        /// 到期日期
        /// </summary>
        public DateTime? ExpiryDate { get; set; }
        
        /// <summary>
        /// 特殊权限说明
        /// </summary>
        public string SpecialPermissions { get; set; } = string.Empty;
        
        /// <summary>
        /// 备注
        /// </summary>
        public string Remarks { get; set; } = string.Empty;
        
        /// <summary>
        /// 关联的客户
        /// </summary>
        public Customer Customer { get; set; } = null!;
    }
}