namespace Login_Sample.Data
{
    /// <summary>
    /// 客户意见反馈实体类
    /// </summary>
    public class CustomerFeedback
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
        /// 反馈类型
        /// </summary>
        public string FeedbackType { get; set; } = string.Empty;
        
        /// <summary>
        /// 反馈主题
        /// </summary>
        public string FeedbackSubject { get; set; } = string.Empty;
        
        /// <summary>
        /// 反馈内容
        /// </summary>
        public string FeedbackContent { get; set; } = string.Empty;
        
        /// <summary>
        /// 反馈日期
        /// </summary>
        public DateTime FeedbackDate { get; set; } = DateTime.UtcNow;
        
        /// <summary>
        /// 处理状态
        /// </summary>
        public string Status { get; set; } = string.Empty;
        
        /// <summary>
        /// 处理人员
        /// </summary>
        public string Handler { get; set; } = string.Empty;
        
        /// <summary>
        /// 处理日期
        /// </summary>
        public DateTime? HandlingDate { get; set; }
        
        /// <summary>
        /// 处理结果
        /// </summary>
        public string HandlingResult { get; set; } = string.Empty;
        
        /// <summary>
        /// 优先级
        /// </summary>
        public string Priority { get; set; } = string.Empty;
        
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