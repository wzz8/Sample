using System;using System.Collections.Generic;using System.Collections.ObjectModel;using System.ComponentModel;using System.Linq;using System.Runtime.CompilerServices;using System.Windows.Input;using Login_Sample.Data;

namespace Login_Sample.ViewModels{
    /// <summary>
    /// 维修进度视图模型
    /// </summary>
    public class RepairProgressViewModel : INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler? PropertyChanged;
        
        private void OnPropertyChanged([CallerMemberName] string? propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
        
        // 命令
        public ICommand SearchCommand { get; set; }
        public ICommand RefreshCommand { get; set; }
        
        // 查询条件
        private string _licensePlate;
        public string LicensePlate
        {
            get => _licensePlate;
            set
            {
                _licensePlate = value;
                OnPropertyChanged();
            }
        }
        
        private string _orderNumber;
        public string OrderNumber
        {
            get => _orderNumber;
            set
            {
                _orderNumber = value;
                OnPropertyChanged();
            }
        }
        
        // 维修进度列表
        private ObservableCollection<RepairProgressItem> _repairProgressItems;
        public ObservableCollection<RepairProgressItem> RepairProgressItems
        {
            get => _repairProgressItems;
            set
            {
                _repairProgressItems = value;
                OnPropertyChanged();
            }
        }
        
        // 构造函数
        public RepairProgressViewModel()
        {
            // 初始化命令
            SearchCommand = new RelayCommand(Search);
            RefreshCommand = new RelayCommand(Refresh);
            
            // 初始化非空字段
            _licensePlate = string.Empty;
            _orderNumber = string.Empty;
            _repairProgressItems = new ObservableCollection<RepairProgressItem>();
            
            // 初始化数据
            LoadSampleData();
        }
        
        /// <summary>
        /// 搜索维修进度
        /// </summary>
        private void Search(object? parameter)
        {
            // 在实际应用中，这里应该调用数据库进行查询
            // 现在使用模拟数据进行演示
            var filteredItems = _allRepairProgressItems;
            
            if (!string.IsNullOrEmpty(LicensePlate))
            {
                filteredItems = filteredItems.Where(item => 
                    item.LicensePlate.Contains(LicensePlate, StringComparison.OrdinalIgnoreCase)).ToList();
            }
            
            if (!string.IsNullOrEmpty(OrderNumber))
            {
                filteredItems = filteredItems.Where(item => 
                    item.OrderNumber.Contains(OrderNumber, StringComparison.OrdinalIgnoreCase)).ToList();
            }
            
            RepairProgressItems = new ObservableCollection<RepairProgressItem>(filteredItems);
        }
        
        /// <summary>
        /// 刷新维修进度
        /// </summary>
        private void Refresh(object? parameter)
        {
            LoadSampleData();
        }
        
        // 模拟数据
        private List<RepairProgressItem> _allRepairProgressItems = new List<RepairProgressItem>();
        
        /// <summary>
        /// 加载模拟数据
        /// </summary>
        private void LoadSampleData()
        {
            // 创建模拟的维修进度数据
            _allRepairProgressItems = new List<RepairProgressItem>
            {
                new RepairProgressItem
                {
                    Id = 1,
                    OrderNumber = "WO20251201001",
                    CustomerName = "张三",
                    LicensePlate = "京A12345",
                    VehicleModel = "奥迪A6L",
                    ReceptionDate = new DateTime(2025, 12, 1, 9, 30, 0),
                    RepairStatus = "正在进行",
                    CurrentStage = "已派工",
                    Technician = "王五",
                    EstimatedCompletionTime = new DateTime(2025, 12, 1, 16, 0, 0),
                    RepairItems = new List<RepairItemStatus>
                    {
                        new RepairItemStatus { ItemName = "常规保养", Status = "已完成" },
                        new RepairItemStatus { ItemName = "发动机检查", Status = "正在进行" },
                        new RepairItemStatus { ItemName = "更换空气滤芯", Status = "待进行" }
                    }
                },
                new RepairProgressItem
                {
                    Id = 2,
                    OrderNumber = "WO20251202002",
                    CustomerName = "李四",
                    LicensePlate = "京B67890",
                    VehicleModel = "宝马3系",
                    ReceptionDate = new DateTime(2025, 12, 2, 10, 15, 0),
                    RepairStatus = "正在进行",
                    CurrentStage = "已领料",
                    Technician = "赵六",
                    EstimatedCompletionTime = new DateTime(2025, 12, 2, 15, 30, 0),
                    RepairItems = new List<RepairItemStatus>
                    {
                        new RepairItemStatus { ItemName = "更换前刹车片", Status = "正在进行" },
                        new RepairItemStatus { ItemName = "刹车系统检查", Status = "待进行" }
                    }
                },
                new RepairProgressItem
                {
                    Id = 3,
                    OrderNumber = "WO20251203003",
                    CustomerName = "王五",
                    LicensePlate = "京C24680",
                    VehicleModel = "奔驰E级",
                    ReceptionDate = new DateTime(2025, 12, 3, 14, 20, 0),
                    RepairStatus = "待诊断",
                    CurrentStage = "待料",
                    Technician = "孙七",
                    EstimatedCompletionTime = new DateTime(2025, 12, 4, 11, 0, 0),
                    RepairItems = new List<RepairItemStatus>
                    {
                        new RepairItemStatus { ItemName = "更换轮胎", Status = "待进行" },
                        new RepairItemStatus { ItemName = "四轮定位", Status = "待进行" }
                    }
                },
                new RepairProgressItem
                {
                    Id = 4,
                    OrderNumber = "WO20251204004",
                    CustomerName = "赵六",
                    LicensePlate = "京D13579",
                    VehicleModel = "丰田凯美瑞",
                    ReceptionDate = new DateTime(2025, 12, 4, 8, 45, 0),
                    RepairStatus = "正在进行",
                    CurrentStage = "已派工",
                    Technician = "周八",
                    EstimatedCompletionTime = new DateTime(2025, 12, 4, 14, 0, 0),
                    RepairItems = new List<RepairItemStatus>
                    {
                        new RepairItemStatus { ItemName = "更换机油机滤", Status = "已完成" },
                        new RepairItemStatus { ItemName = "空调系统清洗", Status = "正在进行" }
                    }
                },
                new RepairProgressItem
                {
                    Id = 5,
                    OrderNumber = "WO20251205005",
                    CustomerName = "孙七",
                    LicensePlate = "京E97531",
                    VehicleModel = "本田雅阁",
                    ReceptionDate = new DateTime(2025, 12, 5, 11, 30, 0),
                    RepairStatus = "已完工",
                    CurrentStage = "完工",
                    Technician = "吴九",
                    EstimatedCompletionTime = new DateTime(2025, 12, 5, 16, 0, 0),
                    RepairItems = new List<RepairItemStatus>
                    {
                        new RepairItemStatus { ItemName = "变速箱维修", Status = "已完成" },
                        new RepairItemStatus { ItemName = "更换变速箱油", Status = "已完成" }
                    }
                }
            };
            
            RepairProgressItems = new ObservableCollection<RepairProgressItem>(_allRepairProgressItems);
        }
        
        /// <summary>
        /// 维修进度项类
        /// </summary>
        public class RepairProgressItem
        {
            public int Id { get; set; }
            public string OrderNumber { get; set; }
            public string CustomerName { get; set; }
            public string LicensePlate { get; set; }
            public string VehicleModel { get; set; }
            public DateTime ReceptionDate { get; set; }
            public string RepairStatus { get; set; }
            public string CurrentStage { get; set; }
            public string Technician { get; set; }
            public DateTime EstimatedCompletionTime { get; set; }
            public List<RepairItemStatus> RepairItems { get; set; }
        }
        
        /// <summary>
        /// 维修项目状态类
        /// </summary>
        public class RepairItemStatus
        {
            public string ItemName { get; set; }
            public string Status { get; set; }
        }
    }
}