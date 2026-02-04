using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using Login_Sample.Data;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Windows.Input;

namespace Login_Sample.ViewModels
{
    /// <summary>
    /// 保险管理视图模型
    /// </summary>
    public class InsuranceManagementViewModel : ObservableObject
    {
        #region 查询条件属性

        // 客户姓名
        private string _customerName;
        public string CustomerName
        {
            get { return _customerName; }
            set { _customerName = value; OnPropertyChanged(); }
        }

        // 车牌号码
        private string _licensePlate;
        public string LicensePlate
        {
            get { return _licensePlate; }
            set { _licensePlate = value; OnPropertyChanged(); }
        }

        // 事故编号
        private string _accidentNumber;
        public string AccidentNumber
        {
            get { return _accidentNumber; }
            set { _accidentNumber = value; OnPropertyChanged(); }
        }

        // 保险公司
        private string _insuranceCompany;
        public string InsuranceCompany
        {
            get { return _insuranceCompany; }
            set { _insuranceCompany = value; OnPropertyChanged(); }
        }

        // 索赔状态选项
        private List<string> _claimStatusOptions;
        public List<string> ClaimStatusOptions
        {
            get { return _claimStatusOptions; }
            set { _claimStatusOptions = value; OnPropertyChanged(); }
        }

        // 选中的索赔状态
        private string _selectedClaimStatus;
        public string SelectedClaimStatus
        {
            get { return _selectedClaimStatus; }
            set { _selectedClaimStatus = value; OnPropertyChanged(); }
        }

        // 洽谈状态选项
        private List<string> _negotiationStatusOptions;
        public List<string> NegotiationStatusOptions
        {
            get { return _negotiationStatusOptions; }
            set { _negotiationStatusOptions = value; OnPropertyChanged(); }
        }

        // 选中的洽谈状态
        private string _selectedNegotiationStatus;
        public string SelectedNegotiationStatus
        {
            get { return _selectedNegotiationStatus; }
            set { _selectedNegotiationStatus = value; OnPropertyChanged(); }
        }

        // 开始日期
        private DateTime? _startDate;
        public DateTime? StartDate
        {
            get { return _startDate; }
            set { _startDate = value; OnPropertyChanged(); }
        }

        // 结束日期
        private DateTime? _endDate;
        public DateTime? EndDate
        {
            get { return _endDate; }
            set { _endDate = value; OnPropertyChanged(); }
        }

        #endregion

        #region 列表和分页属性

        // 保险索赔列表
        private ObservableCollection<InsuranceClaim> _insuranceClaims;
        public ObservableCollection<InsuranceClaim> InsuranceClaims
        {
            get { return _insuranceClaims; }
            set { _insuranceClaims = value; OnPropertyChanged(); }
        }

        // 所有保险索赔数据（用于分页）
        private List<InsuranceClaim> _allInsuranceClaims;

        // 选中的保险索赔
        private InsuranceClaim _selectedInsuranceClaim;
        public InsuranceClaim SelectedInsuranceClaim
        {
            get { return _selectedInsuranceClaim; }
            set { _selectedInsuranceClaim = value; OnPropertyChanged(); }
        }

        // 当前页码
        private int _currentPage = 1;
        public int CurrentPage
        {
            get { return _currentPage; }
            set { _currentPage = value; OnPropertyChanged(); OnPropertyChanged(nameof(CanGoPrevious)); OnPropertyChanged(nameof(CanGoNext)); }
        }

        // 每页显示数量
        private int _pageSize = 20;
        public int PageSize
        {
            get { return _pageSize; }
            set { _pageSize = value; OnPropertyChanged(); }
        }

        // 总记录数
        private int _totalItems;
        public int TotalItems
        {
            get { return _totalItems; }
            set { _totalItems = value; OnPropertyChanged(); OnPropertyChanged(nameof(TotalPages)); }
        }

        // 总页数
        public int TotalPages
        {
            get { return (int)Math.Ceiling((double)_totalItems / _pageSize); }
        }

        // 是否可以上一页
        public bool CanGoPrevious
        {
            get { return _currentPage > 1; }
        }

        // 是否可以下一页
        public bool CanGoNext
        {
            get { return _currentPage < TotalPages; }
        }

        #endregion

        #region 命令定义

        // 新增索赔命令
        public ICommand AddClaimCommand { get; set; }

        // 查询命令
        public ICommand SearchCommand { get; set; }

        // 重置命令
        public ICommand ResetCommand { get; set; }

        // 刷新命令
        public ICommand RefreshCommand { get; set; }

        // 上一页命令
        public ICommand PreviousPageCommand { get; set; }

        // 下一页命令
        public ICommand NextPageCommand { get; set; }

        // 查看索赔命令
        public ICommand ViewClaimCommand { get; set; }

        // 洽谈命令
        public ICommand NegotiateCommand { get; set; }

        // 更新状态命令
        public ICommand UpdateStatusCommand { get; set; }

        #endregion

        #region 构造函数

        /// <summary>
        /// 构造函数
        /// </summary>
        public InsuranceManagementViewModel()
        {
            // 初始化命令
            AddClaimCommand = new RelayCommand<object>(ExecuteAddClaim);
            SearchCommand = new RelayCommand<object>(ExecuteSearch);
            ResetCommand = new RelayCommand<object>(ExecuteReset);
            RefreshCommand = new RelayCommand<object>(ExecuteRefresh);
            PreviousPageCommand = new RelayCommand<object>(ExecutePreviousPage);
            NextPageCommand = new RelayCommand<object>(ExecuteNextPage);
            ViewClaimCommand = new RelayCommand<object>(ExecuteViewClaim);
            NegotiateCommand = new RelayCommand<object>(ExecuteNegotiate);
            UpdateStatusCommand = new RelayCommand<object>(ExecuteUpdateStatus);

            // 初始化状态选项
            ClaimStatusOptions = new List<string> { "全部", "已提交", "处理中", "已赔付", "已完成", "已拒绝" };
            SelectedClaimStatus = "全部";

            NegotiationStatusOptions = new List<string> { "全部", "未开始", "进行中", "已完成" };
            SelectedNegotiationStatus = "全部";

            // 初始化日期范围（默认显示最近30天）
            StartDate = DateTime.Now.AddDays(-30);
            EndDate = DateTime.Now;

            // 加载模拟数据
            LoadMockData();

            // 初始查询
            ExecuteSearch(null);
        }

        #endregion

        #region 命令执行方法

        /// <summary>
        /// 执行新增索赔
        /// </summary>
        /// <param name="parameter"></param>
        private void ExecuteAddClaim(object parameter)
        {
            // 这里可以打开新增索赔窗口
            // 目前只是模拟新增一条数据
            var newClaim = new InsuranceClaim
            {
                Id = _allInsuranceClaims.Max(c => c.Id) + 1,
                CustomerId = 1,
                VehicleId = 1,
                InsuranceAgentId = 1,
                AccidentNumber = $"ACC-{DateTime.Now:yyyyMMddHHmmss}",
                AccidentDate = DateTime.Now,
                AccidentLocation = "上海市浦东新区张江高科技园区",
                AccidentDescription = "车辆追尾事故",
                EstimatedCost = 5000,
                InsuranceCompensation = 0,
                CustomerPayment = 0,
                InsuranceCompany = "平安保险",
                ClaimsAdjuster = "李理赔",
                ClaimsAdjusterContact = "13800138000",
                ClaimStatus = "已提交",
                NegotiationStatus = "未开始",
                Remarks = "新登记的保险索赔"
            };

            // 设置关联数据
            newClaim.Customer = _allInsuranceClaims.First().Customer;
            newClaim.Vehicle = _allInsuranceClaims.First().Vehicle;
            newClaim.InsuranceAgent = _allInsuranceClaims.First().InsuranceAgent;

            _allInsuranceClaims.Insert(0, newClaim);
            ExecuteSearch(null);
        }

        /// <summary>
        /// 执行查询
        /// </summary>
        /// <param name="parameter"></param>
        private void ExecuteSearch(object parameter)
        {
            var filteredItems = _allInsuranceClaims.AsQueryable();

            // 根据客户姓名过滤
            if (!string.IsNullOrWhiteSpace(CustomerName))
            {
                filteredItems = filteredItems.Where(c => c.Customer != null && c.Customer.Name.Contains(CustomerName));
            }

            // 根据车牌号码过滤
            if (!string.IsNullOrWhiteSpace(LicensePlate))
            {
                filteredItems = filteredItems.Where(c => c.Vehicle != null && c.Vehicle.LicensePlate.Contains(LicensePlate));
            }

            // 根据事故编号过滤
            if (!string.IsNullOrWhiteSpace(AccidentNumber))
            {
                filteredItems = filteredItems.Where(c => c.AccidentNumber.Contains(AccidentNumber));
            }

            // 根据保险公司过滤
            if (!string.IsNullOrWhiteSpace(InsuranceCompany))
            {
                filteredItems = filteredItems.Where(c => c.InsuranceCompany.Contains(InsuranceCompany));
            }

            // 根据索赔状态过滤
            if (SelectedClaimStatus != "全部")
            {
                filteredItems = filteredItems.Where(c => c.ClaimStatus == SelectedClaimStatus);
            }

            // 根据洽谈状态过滤
            if (SelectedNegotiationStatus != "全部")
            {
                filteredItems = filteredItems.Where(c => c.NegotiationStatus == SelectedNegotiationStatus);
            }

            // 根据事故日期过滤
            if (StartDate.HasValue)
            {
                filteredItems = filteredItems.Where(c => c.AccidentDate >= StartDate.Value.Date);
            }
            if (EndDate.HasValue)
            {
                filteredItems = filteredItems.Where(c => c.AccidentDate <= EndDate.Value.Date.AddDays(1));
            }

            // 重新计算分页
            _currentPage = 1;
            _totalItems = filteredItems.Count();

            // 分页查询
            var pagedItems = filteredItems.OrderByDescending(c => c.AccidentDate)
                                         .Skip((_currentPage - 1) * _pageSize)
                                         .Take(_pageSize)
                                         .ToList();

            InsuranceClaims = new ObservableCollection<InsuranceClaim>(pagedItems);
        }

        /// <summary>
        /// 执行重置
        /// </summary>
        /// <param name="parameter"></param>
        private void ExecuteReset(object parameter)
        {
            CustomerName = string.Empty;
            LicensePlate = string.Empty;
            AccidentNumber = string.Empty;
            InsuranceCompany = string.Empty;
            SelectedClaimStatus = "全部";
            SelectedNegotiationStatus = "全部";
            StartDate = DateTime.Now.AddDays(-30);
            EndDate = DateTime.Now;

            ExecuteSearch(null);
        }

        /// <summary>
        /// 执行刷新
        /// </summary>
        /// <param name="parameter"></param>
        private void ExecuteRefresh(object parameter)
        {
            LoadMockData();
            ExecuteSearch(null);
        }

        /// <summary>
        /// 执行上一页
        /// </summary>
        /// <param name="parameter"></param>
        private void ExecutePreviousPage(object parameter)
        {
            if (CanGoPrevious)
            {
                CurrentPage--;
                ExecuteSearch(null);
            }
        }

        /// <summary>
        /// 执行下一页
        /// </summary>
        /// <param name="parameter"></param>
        private void ExecuteNextPage(object parameter)
        {
            if (CanGoNext)
            {
                CurrentPage++;
                ExecuteSearch(null);
            }
        }

        /// <summary>
        /// 执行查看索赔
        /// </summary>
        /// <param name="parameter"></param>
        private void ExecuteViewClaim(object parameter)
        {
            var claim = parameter as InsuranceClaim;
            if (claim != null)
            {
                SelectedInsuranceClaim = claim;
                // 这里可以打开索赔详情窗口
            }
        }

        /// <summary>
        /// 执行洽谈
        /// </summary>
        /// <param name="parameter"></param>
        private void ExecuteNegotiate(object parameter)
        {
            if (SelectedInsuranceClaim != null)
            {
                SelectedInsuranceClaim.NegotiationStatus = "进行中";
                SelectedInsuranceClaim.Remarks = $"{SelectedInsuranceClaim.Remarks}" + Environment.NewLine + $"{DateTime.Now:yyyy-MM-dd HH:mm} 开始与保险公司洽谈";
                OnPropertyChanged(nameof(InsuranceClaims));
            }
        }

        /// <summary>
        /// 执行更新状态
        /// </summary>
        /// <param name="parameter"></param>
        private void ExecuteUpdateStatus(object parameter)
        {
            if (SelectedInsuranceClaim != null)
            {
                // 根据当前状态更新到下一个状态
                switch (SelectedInsuranceClaim.ClaimStatus)
                {
                    case "已提交":
                        SelectedInsuranceClaim.ClaimStatus = "处理中";
                        SelectedInsuranceClaim.Remarks = $"{SelectedInsuranceClaim.Remarks}" + Environment.NewLine + $"{DateTime.Now:yyyy-MM-dd HH:mm} 索赔进入处理阶段";
                        break;
                    case "处理中":
                        SelectedInsuranceClaim.ClaimStatus = "已赔付";
                        SelectedInsuranceClaim.InsuranceCompensation = SelectedInsuranceClaim.EstimatedCost * 0.8m;
                        SelectedInsuranceClaim.CustomerPayment = SelectedInsuranceClaim.EstimatedCost * 0.2m;
                        SelectedInsuranceClaim.Remarks = $"{SelectedInsuranceClaim.Remarks}" + Environment.NewLine + $"{DateTime.Now:yyyy-MM-dd HH:mm} 保险公司已赔付：{SelectedInsuranceClaim.InsuranceCompensation:C}";
                        SelectedInsuranceClaim.NegotiationStatus = "已完成";
                        break;
                    case "已赔付":
                        SelectedInsuranceClaim.ClaimStatus = "已完成";
                        SelectedInsuranceClaim.Remarks = $"{SelectedInsuranceClaim.Remarks}" + Environment.NewLine + $"{DateTime.Now:yyyy-MM-dd HH:mm} 索赔流程已完成";
                        break;
                }

                OnPropertyChanged(nameof(InsuranceClaims));
            }
        }

        #endregion

        #region 数据加载方法

        /// <summary>
        /// 加载模拟数据
        /// </summary>
        private void LoadMockData()
        {
            // 模拟客户数据
            var customer = new Customer
            {
                Id = 1,
                Name = "张三",
                Phone = "13800138001",
                Address = "上海市浦东新区张江高科技园区",
                Gender = "男"
            };

            // 模拟车辆数据
            var vehicle = new CustomerVehicle
            {
                Id = 1,
                CustomerId = 1,
                VehicleBrand = "大众",
                VehicleModel = "途观",
                LicensePlate = "沪A12345",
                PurchaseDate = new DateTime(2020, 1, 1),
                Mileage = 50000
            };

            // 模拟保险代理数据
            var agent = new InsuranceAgent
            {
                Id = 1,
                CustomerId = 1,
                InsuranceCompany = "平安保险",
                AgentName = "王保险",
                AgentContact = "13800138002",
                InsuranceType = "机动车交通事故责任强制保险",
                EffectiveDate = new DateTime(2023, 1, 1),
                ExpiryDate = new DateTime(2024, 1, 1),
                InsuranceAmount = 5000,
                Status = "生效中"
            };

            _allInsuranceClaims = new List<InsuranceClaim>
            {
                new InsuranceClaim
                {
                    Id = 1,
                    CustomerId = 1,
                    VehicleId = 1,
                    InsuranceAgentId = 1,
                    Customer = customer,
                    Vehicle = vehicle,
                    InsuranceAgent = agent,
                    AccidentNumber = "ACC-202312010001",
                    AccidentDate = DateTime.Now.AddDays(-10),
                    AccidentLocation = "上海市浦东新区张江高科技园区",
                    AccidentDescription = "车辆追尾事故，后保险杠损坏",
                    EstimatedCost = 3500,
                    InsuranceCompensation = 2800,
                    CustomerPayment = 400,
                    InsuranceCompany = "平安保险",
                    ClaimsAdjuster = "李理赔",
                    ClaimsAdjusterContact = "13800138000",
                    ClaimStatus = "已完成",
                    NegotiationStatus = "已完成",
                    Remarks = "事故处理完成，保险公司已赔付"
                },
                new InsuranceClaim
                {
                    Id = 2,
                    CustomerId = 1,
                    VehicleId = 1,
                    InsuranceAgentId = 1,
                    Customer = customer,
                    Vehicle = vehicle,
                    InsuranceAgent = agent,
                    AccidentNumber = "ACC-202312050002",
                    AccidentDate = DateTime.Now.AddDays(-5),
                    AccidentLocation = "上海市徐汇区徐家汇商圈",
                    AccidentDescription = "车辆刮擦事故，左侧车门损坏",
                    EstimatedCost = 2000,
                    InsuranceCompensation = 0,
                    CustomerPayment = 0,
                    InsuranceCompany = "人保保险",
                    ClaimsAdjuster = "王理赔",
                    ClaimsAdjusterContact = "13800138001",
                    ClaimStatus = "处理中",
                    NegotiationStatus = "进行中",
                    Remarks = "正在与保险公司洽谈赔付事宜"
                },
                new InsuranceClaim
                {
                    Id = 3,
                    CustomerId = 1,
                    VehicleId = 1,
                    InsuranceAgentId = 1,
                    Customer = customer,
                    Vehicle = vehicle,
                    InsuranceAgent = agent,
                    AccidentNumber = "ACC-202312080003",
                    AccidentDate = DateTime.Now.AddDays(-2),
                    AccidentLocation = "上海市闵行区莘庄工业区",
                    AccidentDescription = "车辆碰撞事故，前保险杠和大灯损坏",
                    EstimatedCost = 8000,
                    InsuranceCompensation = 0,
                    CustomerPayment = 0,
                    InsuranceCompany = "太平洋保险",
                    ClaimsAdjuster = "张理赔",
                    ClaimsAdjusterContact = "13800138002",
                    ClaimStatus = "已提交",
                    NegotiationStatus = "未开始",
                    Remarks = "索赔已提交，等待保险公司处理"
                },
                new InsuranceClaim
                {
                    Id = 4,
                    CustomerId = 1,
                    VehicleId = 1,
                    InsuranceAgentId = 1,
                    Customer = customer,
                    Vehicle = vehicle,
                    InsuranceAgent = agent,
                    AccidentNumber = "ACC-202312090004",
                    AccidentDate = DateTime.Now.AddDays(-1),
                    AccidentLocation = "上海市静安区南京西路",
                    AccidentDescription = "车辆追尾事故，后保险杠和后备箱损坏",
                    EstimatedCost = 6000,
                    InsuranceCompensation = 0,
                    CustomerPayment = 0,
                    InsuranceCompany = "平安保险",
                    ClaimsAdjuster = "李理赔",
                    ClaimsAdjusterContact = "13800138000",
                    ClaimStatus = "已提交",
                    NegotiationStatus = "未开始",
                    Remarks = "新提交的索赔申请"
                },
                new InsuranceClaim
                {
                    Id = 5,
                    CustomerId = 1,
                    VehicleId = 1,
                    InsuranceAgentId = 1,
                    Customer = customer,
                    Vehicle = vehicle,
                    InsuranceAgent = agent,
                    AccidentNumber = "ACC-202311250005",
                    AccidentDate = DateTime.Now.AddDays(-15),
                    AccidentLocation = "上海市普陀区金沙江路",
                    AccidentDescription = "车辆碰撞事故，右侧车门和后视镜损坏",
                    EstimatedCost = 4500,
                    InsuranceCompensation = 3780,
                    CustomerPayment = 420,
                    InsuranceCompany = "人保保险",
                    ClaimsAdjuster = "王理赔",
                    ClaimsAdjusterContact = "13800138001",
                    ClaimStatus = "已完成",
                    NegotiationStatus = "已完成",
                    Remarks = "事故处理完成，保险公司已赔付85%的费用"
                }
            };
        }

        #endregion
    }
}