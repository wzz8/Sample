namespace Login_Sample.Data
{
    /// <summary>
    /// 客户实体类
    /// </summary>
    public class Customer
    {
        /// <summary>
        /// 主键
        /// </summary>
        public int Id { get; set; }
        
        /// <summary>
        /// 客户姓名
        /// </summary>
        public string Name { get; set; } = string.Empty;
        
        /// <summary>
        /// 性别
        /// </summary>
        public string Gender { get; set; } = string.Empty;
        
        /// <summary>
        /// 手机号码
        /// </summary>
        public string Phone { get; set; } = string.Empty;
        
        /// <summary>
        /// 邮箱
        /// </summary>
        public string Email { get; set; } = string.Empty;
        
        /// <summary>
        /// 地址
        /// </summary>
        public string Address { get; set; } = string.Empty;
        
        /// <summary>
        /// 客户等级
        /// </summary>
        public string CustomerLevel { get; set; } = string.Empty;
        
        /// <summary>
        /// 创建时间
        /// </summary>
        public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
        
        /// <summary>
        /// 备注
        /// </summary>
        public string Remarks { get; set; } = string.Empty;
        
        /// <summary>
        /// 关联的车辆
        /// </summary>
        public List<CustomerVehicle> Vehicles { get; set; } = new List<CustomerVehicle>();
        
        /// <summary>
        /// 关联的预约
        /// </summary>
        public List<CustomerAppointment> Appointments { get; set; } = new List<CustomerAppointment>();
        
        /// <summary>
        /// 关联的回访
        /// </summary>
        public List<CustomerVisit> Visits { get; set; } = new List<CustomerVisit>();
        
        /// <summary>
        /// 关联的会员信息
        /// </summary>
        public CustomerMembership? Membership { get; set; }
        
        /// <summary>
        /// 关联的关怀记录
        /// </summary>
        public List<CustomerCare> Cares { get; set; } = new List<CustomerCare>();
        
        /// <summary>
        /// 关联的意见反馈
        /// </summary>
        public List<CustomerFeedback> Feedbacks { get; set; } = new List<CustomerFeedback>();
        
        /// <summary>
        /// 关联的特殊客户信息
        /// </summary>
        public List<SpecialCustomer> SpecialCustomers { get; set; } = new List<SpecialCustomer>();
        
        /// <summary>
        /// 关联的保险代理信息
        /// </summary>
        public List<InsuranceAgent> InsuranceAgents { get; set; } = new List<InsuranceAgent>();
        
        /// <summary>
        /// 关联的消费卡信息
        /// </summary>
        public List<CustomerCard> CustomerCards { get; set; } = new List<CustomerCard>();
    }
}