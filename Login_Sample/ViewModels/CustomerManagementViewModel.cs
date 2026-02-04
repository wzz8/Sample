using CommunityToolkit.Mvvm.ComponentModel;
using Login_Sample.Data;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Runtime.CompilerServices;

namespace Login_Sample.ViewModels
{
    /// <summary>
    /// 客户管理视图模型
    /// </summary>
    public class CustomerManagementViewModel : ObservableObject
    {
        private ObservableCollection<Customer> _customers;
        private Customer? _selectedCustomer;
        private string _searchQuery;
        private ObservableCollection<CustomerVehicle> _customerVehicles;
        private ObservableCollection<CustomerAppointment> _customerAppointments;
        private ObservableCollection<CustomerVisit> _customerVisits;
        private ObservableCollection<CustomerMembership> _customerMemberships;
        private ObservableCollection<CustomerCare> _customerCares;
        private ObservableCollection<CustomerFeedback> _customerFeedbacks;
        private ObservableCollection<SpecialCustomer> _specialCustomers;
        private ObservableCollection<InsuranceAgent> _insuranceAgents;
        private ObservableCollection<CustomerCard> _customerCards;

        public CustomerManagementViewModel()
        {
            _customers = new ObservableCollection<Customer>();
            _customerVehicles = new ObservableCollection<CustomerVehicle>();
            _customerAppointments = new ObservableCollection<CustomerAppointment>();
            _customerVisits = new ObservableCollection<CustomerVisit>();
            _customerMemberships = new ObservableCollection<CustomerMembership>();
            _customerCares = new ObservableCollection<CustomerCare>();
            _customerFeedbacks = new ObservableCollection<CustomerFeedback>();
            _specialCustomers = new ObservableCollection<SpecialCustomer>();
            _insuranceAgents = new ObservableCollection<InsuranceAgent>();
            _customerCards = new ObservableCollection<CustomerCard>();
            _searchQuery = string.Empty;

            // 初始化模拟数据
            InitializeMockData();
        }

        /// <summary>
        /// 客户列表
        /// </summary>
        public ObservableCollection<Customer> Customers
        {
            get => _customers;
            set => SetProperty(ref _customers, value);
        }

        /// <summary>
        /// 选中的客户
        /// </summary>
        public Customer? SelectedCustomer
        {
            get => _selectedCustomer;
            set
            {
                SetProperty(ref _selectedCustomer, value);
                if (value != null)
                {
                    UpdateCustomerRelatedData(value);
                }
            }
        }

        /// <summary>
        /// 搜索查询
        /// </summary>
        public string SearchQuery
        {
            get => _searchQuery;
            set
            {
                SetProperty(ref _searchQuery, value);
                SearchCustomers();
            }
        }

        /// <summary>
        /// 客户车辆列表
        /// </summary>
        public ObservableCollection<CustomerVehicle> CustomerVehicles
        {
            get => _customerVehicles;
            set => SetProperty(ref _customerVehicles, value);
        }

        /// <summary>
        /// 客户预约列表
        /// </summary>
        public ObservableCollection<CustomerAppointment> CustomerAppointments
        {
            get => _customerAppointments;
            set => SetProperty(ref _customerAppointments, value);
        }

        /// <summary>
        /// 客户回访列表
        /// </summary>
        public ObservableCollection<CustomerVisit> CustomerVisits
        {
            get => _customerVisits;
            set => SetProperty(ref _customerVisits, value);
        }

        /// <summary>
        /// 客户会员列表
        /// </summary>
        public ObservableCollection<CustomerMembership> CustomerMemberships
        {
            get => _customerMemberships;
            set => SetProperty(ref _customerMemberships, value);
        }

        /// <summary>
        /// 客户关怀列表
        /// </summary>
        public ObservableCollection<CustomerCare> CustomerCares
        {
            get => _customerCares;
            set => SetProperty(ref _customerCares, value);
        }

        /// <summary>
        /// 客户意见列表
        /// </summary>
        public ObservableCollection<CustomerFeedback> CustomerFeedbacks
        {
            get => _customerFeedbacks;
            set => SetProperty(ref _customerFeedbacks, value);
        }
        
        /// <summary>
        /// 特殊客户列表
        /// </summary>
        public ObservableCollection<SpecialCustomer> SpecialCustomers
        {
            get => _specialCustomers;
            set => SetProperty(ref _specialCustomers, value);
        }
        
        /// <summary>
        /// 保险代理列表
        /// </summary>
        public ObservableCollection<InsuranceAgent> InsuranceAgents
        {
            get => _insuranceAgents;
            set => SetProperty(ref _insuranceAgents, value);
        }
        
        /// <summary>
        /// 消费卡列表
        /// </summary>
        public ObservableCollection<CustomerCard> CustomerCards
        {
            get => _customerCards;
            set => SetProperty(ref _customerCards, value);
        }

        /// <summary>
        /// 更新选中客户的相关数据
        /// </summary>
        /// <param name="customer">选中的客户</param>
        private void UpdateCustomerRelatedData(Customer customer)
        {
            CustomerVehicles = new ObservableCollection<CustomerVehicle>(customer.Vehicles);
            CustomerAppointments = new ObservableCollection<CustomerAppointment>(customer.Appointments);
            CustomerVisits = new ObservableCollection<CustomerVisit>(customer.Visits);
            
            if (customer.Membership != null)
            {
                CustomerMemberships = new ObservableCollection<CustomerMembership> { customer.Membership };
            } 
            else
            {
                CustomerMemberships.Clear();
            }
            
            CustomerCares = new ObservableCollection<CustomerCare>(customer.Cares);
            CustomerFeedbacks = new ObservableCollection<CustomerFeedback>(customer.Feedbacks);
            SpecialCustomers = new ObservableCollection<SpecialCustomer>(customer.SpecialCustomers);
            InsuranceAgents = new ObservableCollection<InsuranceAgent>(customer.InsuranceAgents);
            CustomerCards = new ObservableCollection<CustomerCard>(customer.CustomerCards);
        }

        /// <summary>
        /// 搜索客户
        /// </summary>
        private void SearchCustomers()
        {
            if (string.IsNullOrWhiteSpace(_searchQuery))
            {
                // 重新加载所有客户
                InitializeMockData();
                return;
            }

            var filteredCustomers = _customers.Where(c => 
                c.Name.Contains(_searchQuery, StringComparison.OrdinalIgnoreCase) ||
                c.Phone.Contains(_searchQuery, StringComparison.OrdinalIgnoreCase) ||
                c.Email.Contains(_searchQuery, StringComparison.OrdinalIgnoreCase) ||
                c.Id.ToString().Contains(_searchQuery)).ToList();

            Customers = new ObservableCollection<Customer>(filteredCustomers);
        }

        /// <summary>
        /// 初始化模拟数据
        /// </summary>
        private void InitializeMockData()
        {
            // 创建客户数据
            var customer1 = new Customer
            {
                Id = 1,
                Name = "张三",
                Gender = "男",
                Phone = "13800138001",
                Email = "zhangsan@example.com",
                Address = "北京市朝阳区",
                CustomerLevel = "VIP",
                CreatedAt = DateTime.Now.AddMonths(-6)
            };

            var customer2 = new Customer
            {
                Id = 2,
                Name = "李四",
                Gender = "女",
                Phone = "13900139002",
                Email = "lisi@example.com",
                Address = "上海市浦东新区",
                CustomerLevel = "普通",
                CreatedAt = DateTime.Now.AddMonths(-3)
            };

            var customer3 = new Customer
            {
                Id = 3,
                Name = "王五",
                Gender = "男",
                Phone = "13700137003",
                Email = "wangwu@example.com",
                Address = "广州市天河区",
                CustomerLevel = "VIP",
                CreatedAt = DateTime.Now.AddMonths(-12)
            };

            // 添加客户车辆
            customer1.Vehicles.Add(new CustomerVehicle
            {
                Id = 1,
                CustomerId = 1,
                LicensePlate = "京A12345",
                VehicleModel = "奥迪A6L",
                VehicleBrand = "奥迪",
                VehicleColor = "黑色",
                EngineNumber = "ABC123456",
                ChassisNumber = "DEF78901234567890",
                PurchaseDate = DateTime.Now.AddYears(-2),
                Mileage = 35000,
                LastMaintenanceDate = DateTime.Now.AddMonths(-1),
                NextMaintenanceMileage = 40000,
                Status = "正常"
            });

            customer2.Vehicles.Add(new CustomerVehicle
            {
                Id = 2,
                CustomerId = 2,
                LicensePlate = "沪B67890",
                VehicleModel = "宝马3系",
                VehicleBrand = "宝马",
                VehicleColor = "白色",
                EngineNumber = "GHI789012",
                ChassisNumber = "JKL12345678901234",
                PurchaseDate = DateTime.Now.AddYears(-1),
                Mileage = 18000,
                LastMaintenanceDate = DateTime.Now.AddMonths(-2),
                NextMaintenanceMileage = 25000,
                Status = "正常"
            });

            // 添加客户预约
            customer1.Appointments.Add(new CustomerAppointment
            {
                Id = 1,
                CustomerId = 1,
                VehicleId = 1,
                AppointmentType = "保养",
                AppointmentDate = DateTime.Now.AddDays(2),
                AppointmentTime = "10:00",
                ServiceContent = "常规保养",
                Status = "已确认",
                Receptionist = "接待员A"
            });

            customer2.Appointments.Add(new CustomerAppointment
            {
                Id = 2,
                CustomerId = 2,
                VehicleId = 2,
                AppointmentType = "维修",
                AppointmentDate = DateTime.Now.AddDays(5),
                AppointmentTime = "14:30",
                ServiceContent = "更换轮胎",
                Status = "已预约",
                Receptionist = "接待员B"
            });

            // 添加客户回访
            customer1.Visits.Add(new CustomerVisit
            {
                Id = 1,
                CustomerId = 1,
                VisitType = "保养后回访",
                VisitDate = DateTime.Now.AddDays(-10),
                Visitor = "回访员A",
                VisitMethod = "电话",
                VisitContent = "询问保养后的车辆使用情况",
                CustomerFeedback = "非常满意",
                SatisfactionScore = 5,
                HandlingResult = "完成"
            });

            // 添加会员信息
            customer1.Membership = new CustomerMembership
            {
                Id = 1,
                CustomerId = 1,
                MembershipLevel = "金卡",
                MembershipNumber = "VIP0001",
                PointsBalance = 1500,
                TotalConsumption = 12000.00m,
                Status = "有效",
                ActivationDate = DateTime.Now.AddYears(-1),
                ExpiryDate = DateTime.Now.AddYears(1),
                Benefits = "享受9折优惠，免费洗车"
            };

            customer3.Membership = new CustomerMembership
            {
                Id = 2,
                CustomerId = 3,
                MembershipLevel = "银卡",
                MembershipNumber = "VIP0002",
                PointsBalance = 800,
                TotalConsumption = 6500.00m,
                Status = "有效",
                ActivationDate = DateTime.Now.AddMonths(-8),
                ExpiryDate = DateTime.Now.AddMonths(4),
                Benefits = "享受9.5折优惠"
            };

            // 添加客户关怀
            customer1.Cares.Add(new CustomerCare
            {
                Id = 1,
                CustomerId = 1,
                CareType = "生日祝福",
                CareSubject = "生日快乐",
                CareContent = "为客户送上生日祝福和礼品",
                CareDate = DateTime.Now.AddDays(-5),
                CareMethod = "短信",
                Executor = "关怀专员",
                ExecutionResult = "已发送",
                CustomerFeedback = "感谢"
            });

            // 添加客户反馈
            customer2.Feedbacks.Add(new CustomerFeedback
            {
                Id = 1,
                CustomerId = 2,
                FeedbackType = "建议",
                FeedbackSubject = "服务改进建议",
                FeedbackContent = "希望可以提供上门取车服务",
                FeedbackDate = DateTime.Now.AddDays(-3),
                Status = "处理中",
                Priority = "中"
            });
            
            // 添加特殊客户信息
            customer1.SpecialCustomers.Add(new SpecialCustomer
            {
                Id = 1,
                CustomerId = 1,
                SpecialType = "VIP客户",
                SpecialReason = "年消费超过10万元",
                Status = "有效",
                StartDate = DateTime.Now.AddMonths(-3),
                ExpiryDate = DateTime.Now.AddMonths(9),
                Remarks = "享受专属客户经理服务"
            });
            
            // 添加保险代理信息
            customer1.InsuranceAgents.Add(new InsuranceAgent
            {
                Id = 1,
                CustomerId = 1,
                InsuranceCompany = "平安保险",
                AgentName = "王保险",
                AgentContact = "13500135001",
                InsuranceType = "车辆保险",
                EffectiveDate = DateTime.Now.AddMonths(-6),
                ExpiryDate = DateTime.Now.AddMonths(6),
                InsuranceAmount = 5000.00m,
                Status = "有效",
                Remarks = "全险"
            });
            
            // 添加消费卡信息
            customer1.CustomerCards.Add(new CustomerCard
            {
                Id = 1,
                CustomerId = 1,
                CardNumber = "CARD0001",
                CardType = "储值卡",
                InitialAmount = 10000.00m,
                CurrentBalance = 7500.00m,
                TotalConsumption = 2500.00m,
                IssueDate = DateTime.Now.AddMonths(-6),
                ExpiryDate = DateTime.Now.AddYears(2),
                Status = "有效",
                Remarks = "无有效期限制"
            });
            
            customer3.CustomerCards.Add(new CustomerCard
            {
                Id = 2,
                CustomerId = 3,
                CardNumber = "CARD0002",
                CardType = "折扣卡",
                InitialAmount = 0.00m,
                CurrentBalance = 0.00m,
                TotalConsumption = 6500.00m,
                IssueDate = DateTime.Now.AddMonths(-8),
                ExpiryDate = DateTime.Now.AddMonths(4),
                Status = "有效",
                Remarks = "享受9.5折优惠"
            });

            // 添加客户到列表
            Customers.Add(customer1);
            Customers.Add(customer2);
            Customers.Add(customer3);
        }

        #region INotifyPropertyChanged 实现
        public event PropertyChangedEventHandler? PropertyChanged;

        protected virtual void OnPropertyChanged([CallerMemberName] string? propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }

        protected bool SetProperty<T>(ref T field, T newValue, [CallerMemberName] string? propertyName = null)
        {
            if (!EqualityComparer<T>.Default.Equals(field, newValue))
            {
                field = newValue;
                OnPropertyChanged(propertyName);
                return true;
            }
            return false;
        }
        #endregion
    }
}
