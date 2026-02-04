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
namespace Login_Sample.ViewModels{
    /// <summary>
    /// 项目查询视图模型
    /// </summary>
    public class ProjectQueryViewModel : ObservableObject
    {
        // 命令
        public ICommand SearchCommand { get; set; }
        public ICommand ResetCommand { get; set; }
        public ICommand ExportCommand { get; set; }
        
        // 查询条件
        private DateTime _startDate;
        public DateTime StartDate
        {
            get => _startDate;
            set
            {
                _startDate = value;
                OnPropertyChanged();
            }
        }
        
        private DateTime _endDate;
        public DateTime EndDate
        {
            get => _endDate;
            set
            {
                _endDate = value;
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
        
        private string _projectName;
        public string ProjectName
        {
            get => _projectName;
            set
            {
                _projectName = value;
                OnPropertyChanged();
            }
        }
        
        private string _status;
        public string Status
        {
            get => _status;
            set
            {
                _status = value;
                OnPropertyChanged();
            }
        }
        
        // 查询结果
        private ObservableCollection<MaintenanceItem> _maintenanceItems;
        public ObservableCollection<MaintenanceItem> MaintenanceItems
        {
            get => _maintenanceItems;
            set
            {
                _maintenanceItems = value;
                OnPropertyChanged();
                UpdateStatistics();
            }
        }
        
        // 统计信息
        private int _totalItems;
        public int TotalItems
        {
            get => _totalItems;
            set
            {
                _totalItems = value;
                OnPropertyChanged();
            }
        }
        
        private decimal _totalAmount;
        public decimal TotalAmount
        {
            get => _totalAmount;
            set
            {
                _totalAmount = value;
                OnPropertyChanged();
            }
        }
        
        // 状态选项
        public ObservableCollection<string> StatusOptions { get; set; }
        
        // 构造函数
        public ProjectQueryViewModel()
        {
            // 初始化命令
            SearchCommand = new RelayCommand<object>(Search);
            ResetCommand = new RelayCommand<object>(Reset);
            ExportCommand = new RelayCommand<object>(Export);
            
            // 初始化非空字段
            _projectName = string.Empty;
            _orderNumber = string.Empty;
            _status = "全部";
            _maintenanceItems = new ObservableCollection<MaintenanceItem>();
            
            // 初始化日期范围（默认查询最近30天）
            EndDate = DateTime.Now;
            StartDate = EndDate.AddDays(-30);
            
            // 初始化状态选项
            StatusOptions = new ObservableCollection<string>
            {
                "全部",
                "待处理",
                "进行中",
                "已完成",
                "已取消"
            };
            
            // 初始化数据
            LoadSampleData();
        }
        
        /// <summary>
        /// 搜索维修项目
        /// </summary>
        private void Search(object? parameter)
        {
            // 在实际应用中，这里应该调用数据库进行查询
            // 现在使用模拟数据进行演示
            var filteredItems = _allMaintenanceItems.Where(item =>
            {
                var matchDate = item.WorkOrder.ReceptionDate >= StartDate && item.WorkOrder.ReceptionDate <= EndDate;
                var matchOrderNumber = string.IsNullOrEmpty(OrderNumber) || item.WorkOrder.OrderNumber.Contains(OrderNumber);
                var matchProjectName = string.IsNullOrEmpty(ProjectName) || item.ItemName.Contains(ProjectName);
                var matchStatus = Status == "全部" || item.Status == Status;
                
                return matchDate && matchOrderNumber && matchProjectName && matchStatus;
            }).ToList();
            
            MaintenanceItems = new ObservableCollection<MaintenanceItem>(filteredItems);
        }
        
        /// <summary>
        /// 重置查询条件
        /// </summary>
        private void Reset(object? parameter)
        {
            StartDate = DateTime.Now.AddDays(-30);
            EndDate = DateTime.Now;
            OrderNumber = string.Empty;
            ProjectName = string.Empty;
            Status = "全部";
            
            MaintenanceItems = new ObservableCollection<MaintenanceItem>(_allMaintenanceItems);
        }
        
        /// <summary>
        /// 导出查询结果
        /// </summary>
        private void Export(object? parameter)
        {
            // 这里可以实现导出功能，例如导出到Excel
            // 现在只是简单的模拟
        }
        
        /// <summary>
        /// 更新统计信息
        /// </summary>
        private void UpdateStatistics()
        {
            TotalItems = MaintenanceItems.Count;
            TotalAmount = MaintenanceItems.Sum(item => item.Subtotal);
        }
        
        // 模拟数据
        private List<MaintenanceItem> _allMaintenanceItems = new List<MaintenanceItem>();
        
        /// <summary>
        /// 加载模拟数据
        /// </summary>
        private void LoadSampleData()
        {
            // 创建模拟的维修委托书
            var workOrders = new List<WorkOrder>
            {
                new WorkOrder
                {
                    Id = 1,
                    OrderNumber = "WO20251201001",
                    CustomerId = 1,
                    VehicleId = 1,
                    ReceptionDate = new DateTime(2025, 12, 1, 9, 30, 0),
                    ServiceAdvisor = "张三",
                    ProblemDescription = "发动机异响",
                    Status = "已完成",
                    TotalAmount = 850.00m,
                    Remarks = "常规保养+更换机油",
                    CompletedDate = new DateTime(2025, 12, 1, 11, 0, 0)
                },
                new WorkOrder
                {
                    Id = 2,
                    OrderNumber = "WO20251202002",
                    CustomerId = 2,
                    VehicleId = 2,
                    ReceptionDate = new DateTime(2025, 12, 2, 10, 15, 0),
                    ServiceAdvisor = "李四",
                    ProblemDescription = "刹车异响",
                    Status = "已完成",
                    TotalAmount = 650.00m,
                    Remarks = "更换刹车片",
                    CompletedDate = new DateTime(2025, 12, 2, 12, 0, 0)
                },
                new WorkOrder
                {
                    Id = 3,
                    OrderNumber = "WO20251203003",
                    CustomerId = 3,
                    VehicleId = 3,
                    ReceptionDate = new DateTime(2025, 12, 3, 14, 20, 0),
                    ServiceAdvisor = "张三",
                    ProblemDescription = "轮胎磨损严重",
                    Status = "进行中",
                    TotalAmount = 1200.00m,
                    Remarks = "更换4条轮胎"
                }
            };
            
            // 创建模拟的维修项目
            _allMaintenanceItems = new List<MaintenanceItem>
            {
                new MaintenanceItem
                {
                    Id = 1,
                    WorkOrderId = 1,
                    WorkOrder = workOrders[0],
                    ItemName = "常规保养",
                    Description = "更换机油、机滤",
                    UnitPrice = 300.00m,
                    Quantity = 1,
                    Subtotal = 300.00m,
                    Technician = "王五",
                    Status = "已完成"
                },
                new MaintenanceItem
                {
                    Id = 2,
                    WorkOrderId = 1,
                    WorkOrder = workOrders[0],
                    ItemName = "发动机检查",
                    Description = "检查发动机异响原因",
                    UnitPrice = 150.00m,
                    Quantity = 1,
                    Subtotal = 150.00m,
                    Technician = "王五",
                    Status = "已完成"
                },
                new MaintenanceItem
                {
                    Id = 3,
                    WorkOrderId = 1,
                    WorkOrder = workOrders[0],
                    ItemName = "更换空气滤芯",
                    Description = "更换空气滤芯",
                    UnitPrice = 100.00m,
                    Quantity = 1,
                    Subtotal = 100.00m,
                    Technician = "王五",
                    Status = "已完成"
                },
                new MaintenanceItem
                {
                    Id = 4,
                    WorkOrderId = 2,
                    WorkOrder = workOrders[1],
                    ItemName = "更换前刹车片",
                    Description = "更换前轮刹车片",
                    UnitPrice = 450.00m,
                    Quantity = 1,
                    Subtotal = 450.00m,
                    Technician = "赵六",
                    Status = "已完成"
                },
                new MaintenanceItem
                {
                    Id = 5,
                    WorkOrderId = 2,
                    WorkOrder = workOrders[1],
                    ItemName = "刹车系统检查",
                    Description = "检查刹车系统",
                    UnitPrice = 200.00m,
                    Quantity = 1,
                    Subtotal = 200.00m,
                    Technician = "赵六",
                    Status = "已完成"
                },
                new MaintenanceItem
                {
                    Id = 6,
                    WorkOrderId = 3,
                    WorkOrder = workOrders[2],
                    ItemName = "更换轮胎",
                    Description = "更换4条轮胎",
                    UnitPrice = 1000.00m,
                    Quantity = 1,
                    Subtotal = 1000.00m,
                    Technician = "孙七",
                    Status = "进行中"
                },
                new MaintenanceItem
                {
                    Id = 7,
                    WorkOrderId = 3,
                    WorkOrder = workOrders[2],
                    ItemName = "四轮定位",
                    Description = "四轮定位调整",
                    UnitPrice = 200.00m,
                    Quantity = 1,
                    Subtotal = 200.00m,
                    Technician = "孙七",
                    Status = "待处理"
                }
            };
            
            MaintenanceItems = new ObservableCollection<MaintenanceItem>(_allMaintenanceItems);
        }
    }
}