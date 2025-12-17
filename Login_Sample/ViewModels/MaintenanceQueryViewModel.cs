using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Windows.Input;

namespace Login_Sample.ViewModels
{
    public class MaintenanceQueryViewModel : INotifyPropertyChanged
    {
        // 状态枚举
        public enum MaintenanceStatus
        {
            全部,
            在修,
            已审查,
            已结算,
            作废
        }

        // 查询条件属性
        private MaintenanceStatus _statusFilter = MaintenanceStatus.全部;
        public MaintenanceStatus StatusFilter
        {
            get { return _statusFilter; }
            set { _statusFilter = value; OnPropertyChanged(nameof(StatusFilter)); }
        }

        private bool _isNewUser = false;
        public bool IsNewUser
        {
            get { return _isNewUser; }
            set { _isNewUser = value; OnPropertyChanged(nameof(IsNewUser)); }
        }

        private string _factory = "全部";
        public string Factory
        {
            get { return _factory; }
            set { _factory = value; OnPropertyChanged(nameof(Factory)); }
        }

        private string _category = "全部";
        public string Category
        {
            get { return _category; }
            set { _category = value; OnPropertyChanged(nameof(Category)); }
        }

        private string _advisor = "全部";
        public string Advisor
        {
            get { return _advisor; }
            set { _advisor = value; OnPropertyChanged(nameof(Advisor)); }
        }

        private DateTime? _startDate;
        public DateTime? StartDate
        {
            get { return _startDate; }
            set { _startDate = value; OnPropertyChanged(nameof(StartDate)); }
        }

        private DateTime? _endDate;
        public DateTime? EndDate
        {
            get { return _endDate; }
            set { _endDate = value; OnPropertyChanged(nameof(EndDate)); }
        }

        private string _searchKeyword;
        public string SearchKeyword
        {
            get { return _searchKeyword; }
            set { _searchKeyword = value; OnPropertyChanged(nameof(SearchKeyword)); }
        }

        // 下拉列表数据源
        public List<string> FactoryList { get; } = new List<string> { "全部", "总厂", "一分厂", "二分厂", "三分厂" };
        public List<string> CategoryList { get; } = new List<string> { "全部", "常规保养", "故障维修", "事故维修", "改装升级", "其他" };
        public List<string> AdvisorList { get; } = new List<string> { "全部", "张三", "李四", "王五", "赵六", "钱七" };

        // 维修记录列表
        private List<MaintenanceRecord> _maintenanceRecords;
        public List<MaintenanceRecord> MaintenanceRecords
        {
            get { return _maintenanceRecords; }
            set { _maintenanceRecords = value; OnPropertyChanged(nameof(MaintenanceRecords)); }
        }

        // 命令
        public ICommand QueryCommand { get; }
        public ICommand ResetCommand { get; }

        // 构造函数
        public MaintenanceQueryViewModel()
        {
            QueryCommand = new RelayCommand(ExecuteQuery);
            ResetCommand = new RelayCommand(ExecuteReset);

            // 初始化模拟数据
            InitializeMockData();
        }

        // 模拟数据初始化
        private void InitializeMockData()
        {
            MaintenanceRecords = new List<MaintenanceRecord>
            {
                new MaintenanceRecord
                {
                    Id = "WX20240101001",
                    PlateNumber = "京A12345",
                    CustomerName = "张三",
                    VehicleModel = "奥迪A4L",
                    Status = MaintenanceStatus.在修,
                    IsNewUser = false,
                    Factory = "总厂",
                    Category = "故障维修",
                    Advisor = "李四",
                    EntryDate = new DateTime(2024, 1, 1),
                    ExitDate = null,
                    TotalAmount = 2500.00,
                    Description = "发动机故障灯亮，检查维修"
                },
                new MaintenanceRecord
                {
                    Id = "WX20240101002",
                    PlateNumber = "京B67890",
                    CustomerName = "李四",
                    VehicleModel = "宝马3系",
                    Status = MaintenanceStatus.已结算,
                    IsNewUser = true,
                    Factory = "一分厂",
                    Category = "常规保养",
                    Advisor = "张三",
                    EntryDate = new DateTime(2024, 1, 1),
                    ExitDate = new DateTime(2024, 1, 2),
                    TotalAmount = 800.00,
                    Description = "50000公里常规保养"
                },
                new MaintenanceRecord
                {
                    Id = "WX20240102001",
                    PlateNumber = "京C13579",
                    CustomerName = "王五",
                    VehicleModel = "奔驰C级",
                    Status = MaintenanceStatus.已审查,
                    IsNewUser = false,
                    Factory = "二分厂",
                    Category = "事故维修",
                    Advisor = "王五",
                    EntryDate = new DateTime(2024, 1, 2),
                    ExitDate = null,
                    TotalAmount = 12000.00,
                    Description = "右侧碰撞事故维修"
                },
                new MaintenanceRecord
                {
                    Id = "WX20240103001",
                    PlateNumber = "京D24680",
                    CustomerName = "赵六",
                    VehicleModel = "大众迈腾",
                    Status = MaintenanceStatus.作废,
                    IsNewUser = false,
                    Factory = "三分厂",
                    Category = "改装升级",
                    Advisor = "赵六",
                    EntryDate = new DateTime(2024, 1, 3),
                    ExitDate = null,
                    TotalAmount = 5000.00,
                    Description = "音响改装项目（客户取消）"
                },
                new MaintenanceRecord
                {
                    Id = "WX20240104001",
                    PlateNumber = "京E97531",
                    CustomerName = "孙七",
                    VehicleModel = "丰田凯美瑞",
                    Status = MaintenanceStatus.在修,
                    IsNewUser = true,
                    Factory = "总厂",
                    Category = "其他",
                    Advisor = "钱七",
                    EntryDate = new DateTime(2024, 1, 4),
                    ExitDate = null,
                    TotalAmount = 1500.00,
                    Description = "空调不制冷检查维修"
                }
            };
        }

        // 执行查询
        private void ExecuteQuery(object parameter)
        {
            var query = MaintenanceRecords.AsQueryable();

            // 状态过滤
            if (StatusFilter != MaintenanceStatus.全部)
            {
                query = query.Where(r => r.Status == StatusFilter);
            }

            // 新用户过滤
            if (IsNewUser)
            {
                query = query.Where(r => r.IsNewUser);
            }

            // 分厂过滤
            if (Factory != "全部")
            {
                query = query.Where(r => r.Factory == Factory);
            }

            // 类别过滤
            if (Category != "全部")
            {
                query = query.Where(r => r.Category == Category);
            }

            // 顾问过滤
            if (Advisor != "全部")
            {
                query = query.Where(r => r.Advisor == Advisor);
            }

            // 进厂日期范围过滤
            if (StartDate.HasValue)
            {
                query = query.Where(r => r.EntryDate >= StartDate.Value);
            }
            if (EndDate.HasValue)
            {
                query = query.Where(r => r.EntryDate <= EndDate.Value);
            }

            // 关键词搜索
            if (!string.IsNullOrEmpty(SearchKeyword))
            {
                var keyword = SearchKeyword.ToLower();
                query = query.Where(r => 
                    r.Id.ToLower().Contains(keyword) ||
                    r.PlateNumber.ToLower().Contains(keyword) ||
                    r.CustomerName.ToLower().Contains(keyword) ||
                    r.VehicleModel.ToLower().Contains(keyword) ||
                    r.Description.ToLower().Contains(keyword)
                );
            }

            // 更新结果列表
            MaintenanceRecords = query.ToList();
        }

        // 重置查询条件
        private void ExecuteReset(object parameter)
        {
            StatusFilter = MaintenanceStatus.全部;
            IsNewUser = false;
            Factory = "全部";
            Category = "全部";
            Advisor = "全部";
            StartDate = DateTime.MinValue;
            EndDate = DateTime.MaxValue;
            SearchKeyword = string.Empty;

            // 重新加载全部数据
            InitializeMockData();
        }

        // INotifyPropertyChanged 实现
        public event PropertyChangedEventHandler PropertyChanged;
        protected virtual void OnPropertyChanged(string propertyName)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }

    // 维修记录模型
    public class MaintenanceRecord
    {
        public string Id { get; set; }
        public string PlateNumber { get; set; }
        public string CustomerName { get; set; }
        public string VehicleModel { get; set; }
        public MaintenanceQueryViewModel.MaintenanceStatus Status { get; set; }
        public bool IsNewUser { get; set; }
        public string Factory { get; set; }
        public string Category { get; set; }
        public string Advisor { get; set; }
        public DateTime EntryDate { get; set; }
        public DateTime? ExitDate { get; set; }
        public double TotalAmount { get; set; }
        public string Description { get; set; }
    }
}