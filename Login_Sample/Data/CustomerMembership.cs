namespace Login_Sample.Data
{
    /// <summary>
    /// 客户会员实体类
    /// </summary>
    public class CustomerMembership
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
        /// 会员等级
        /// </summary>
        public string MembershipLevel { get; set; } = string.Empty;
        
        /// <summary>
        /// 会员卡号
        /// </summary>
        public string MembershipNumber { get; set; } = string.Empty;
        
        /// <summary>
        /// 积分余额
        /// </summary>
        public int PointsBalance { get; set; }
        
        /// <summary>
        /// 累计消费金额
        /// </summary>
        public decimal TotalConsumption { get; set; }
        
        /// <summary>
        /// 会员状态
        /// </summary>
        public string Status { get; set; } = string.Empty;
        
        /// <summary>
        /// 开通日期
        /// </summary>
        public DateTime ActivationDate { get; set; } = DateTime.UtcNow;
        
        /// <summary>
        /// 到期日期
        /// </summary>
        public DateTime ExpiryDate { get; set; }
        
        /// <summary>
        /// 会员权益
        /// </summary>
        public string Benefits { get; set; } = string.Empty;
        
        /// <summary>
        /// 备注
        /// </summary>
        public string Remarks { get; set; } = string.Empty;
        
        /// <summary>
        /// 关联的客户
        /// </summary>
        public Customer? Customer { get; set; }
        
        /// <summary>
        /// 积分变动记录
        /// </summary>
        public List<MembershipPointsRecord> PointsRecords { get; set; } = new List<MembershipPointsRecord>();
    }
    
    /// <summary>
    /// 会员积分记录实体类
    /// </summary>
    public class MembershipPointsRecord
    {
        /// <summary>
        /// 主键
        /// </summary>
        public int Id { get; set; }
        
        /// <summary>
        /// 会员ID
        /// </summary>
        public int MembershipId { get; set; }
        
        /// <summary>
        /// 积分变动类型
        /// </summary>
        public string TransactionType { get; set; } = string.Empty;
        
        /// <summary>
        /// 积分变动数量
        /// </summary>
        public int PointsAmount { get; set; }
        
        /// <summary>
        /// 变动日期
        /// </summary>
        public DateTime TransactionDate { get; set; } = DateTime.UtcNow;
        
        /// <summary>
        /// 变动原因
        /// </summary>
        public string Reason { get; set; } = string.Empty;
        
        /// <summary>
        /// 操作人员
        /// </summary>
        public string Operator { get; set; } = string.Empty;
        
        /// <summary>
        /// 关联的会员信息
        /// </summary>
        public CustomerMembership? Membership { get; set; }
    }
}