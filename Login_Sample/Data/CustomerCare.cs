namespace Login_Sample.Data
{
    /// <summary>
    /// 客户关怀实体类
    /// </summary>
    public class CustomerCare
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
        /// 关怀类型
        /// </summary>
        public string CareType { get; set; } = string.Empty;
        
        /// <summary>
        /// 关怀主题
        /// </summary>
        public string CareSubject { get; set; } = string.Empty;
        
        /// <summary>
        /// 关怀内容
        /// </summary>
        public string CareContent { get; set; } = string.Empty;
        
        /// <summary>
        /// 关怀日期
        /// </summary>
        public DateTime CareDate { get; set; }
        
        /// <summary>
        /// 关怀方式
        /// </summary>
        public string CareMethod { get; set; } = string.Empty;
        
        /// <summary>
        /// 执行人员
        /// </summary>
        public string Executor { get; set; } = string.Empty;
        
        /// <summary>
        /// 执行结果
        /// </summary>
        public string ExecutionResult { get; set; } = string.Empty;
        
        /// <summary>
        /// 客户反馈
        /// </summary>
        public string CustomerFeedback { get; set; } = string.Empty;
        
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