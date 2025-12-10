namespace Login_Sample.Data
{
    /// <summary>
    /// 客户回访实体类
    /// </summary>
    public class CustomerVisit
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
        /// 回访类型
        /// </summary>
        public string VisitType { get; set; } = string.Empty;
        
        /// <summary>
        /// 回访日期
        /// </summary>
        public DateTime VisitDate { get; set; }
        
        /// <summary>
        /// 回访人员
        /// </summary>
        public string Visitor { get; set; } = string.Empty;
        
        /// <summary>
        /// 回访方式
        /// </summary>
        public string VisitMethod { get; set; } = string.Empty;
        
        /// <summary>
        /// 回访内容
        /// </summary>
        public string VisitContent { get; set; } = string.Empty;
        
        /// <summary>
        /// 客户反馈
        /// </summary>
        public string CustomerFeedback { get; set; } = string.Empty;
        
        /// <summary>
        /// 满意度评分
        /// </summary>
        public int SatisfactionScore { get; set; }
        
        /// <summary>
        /// 处理结果
        /// </summary>
        public string HandlingResult { get; set; } = string.Empty;
        
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