namespace Login_Sample.Data
{
    /// <summary>
    /// 保险代理实体类
    /// </summary>
    public class InsuranceAgent
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
        /// 保险公司名称
        /// </summary>
        public string InsuranceCompany { get; set; } = string.Empty;
        
        /// <summary>
        /// 保险代理人姓名
        /// </summary>
        public string AgentName { get; set; } = string.Empty;
        
        /// <summary>
        /// 保险代理人联系方式
        /// </summary>
        public string AgentContact { get; set; } = string.Empty;
        
        /// <summary>
        /// 保险类型
        /// </summary>
        public string InsuranceType { get; set; } = string.Empty;
        
        /// <summary>
        /// 保险生效日期
        /// </summary>
        public DateTime EffectiveDate { get; set; } = DateTime.UtcNow;
        
        /// <summary>
        /// 保险到期日期
        /// </summary>
        public DateTime ExpiryDate { get; set; } = DateTime.UtcNow;
        
        /// <summary>
        /// 保险金额
        /// </summary>
        public decimal InsuranceAmount { get; set; }
        
        /// <summary>
        /// 保险状态
        /// </summary>
        public string Status { get; set; } = string.Empty;
        
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