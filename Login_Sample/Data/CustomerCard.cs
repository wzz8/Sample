namespace Login_Sample.Data
{
    /// <summary>
    /// 消费卡实体类
    /// </summary>
    public class CustomerCard
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
        /// 消费卡号
        /// </summary>
        public string CardNumber { get; set; } = string.Empty;
        
        /// <summary>
        /// 卡类型
        /// </summary>
        public string CardType { get; set; } = string.Empty;
        
        /// <summary>
        /// 初始金额
        /// </summary>
        public decimal InitialAmount { get; set; }
        
        /// <summary>
        /// 当前余额
        /// </summary>
        public decimal CurrentBalance { get; set; }
        
        /// <summary>
        /// 累计消费金额
        /// </summary>
        public decimal TotalConsumption { get; set; }
        
        /// <summary>
        /// 开卡日期
        /// </summary>
        public DateTime IssueDate { get; set; } = DateTime.UtcNow;
        
        /// <summary>
        /// 有效期至
        /// </summary>
        public DateTime ExpiryDate { get; set; } = DateTime.UtcNow;
        
        /// <summary>
        /// 卡状态
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