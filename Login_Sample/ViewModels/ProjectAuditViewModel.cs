using System;using System.Collections.Generic;using System.Collections.ObjectModel;using System.ComponentModel;using System.Linq;using System.Runtime.CompilerServices;using System.Windows.Input;using Login_Sample.Data;

namespace Login_Sample.ViewModels{
    /// <summary>
    /// 项目审查视图模型
    /// </summary>
    public class ProjectAuditViewModel : INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler? PropertyChanged;
        
        private void OnPropertyChanged([CallerMemberName] string? propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
        
        // 命令
        public ICommand SearchCommand { get; set; }
        public ICommand AuditCommand { get; set; }
        public ICommand ResetCommand { get; set; }
        
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
        
        // 查询结果
        private ObservableCollection<WorkOrder> _auditWorkOrders;
        public ObservableCollection<WorkOrder> AuditWorkOrders
        {
            get => _auditWorkOrders;
            set
            {
                _auditWorkOrders = value;
                OnPropertyChanged();
            }
        }
        
        // 选中的工单
        private WorkOrder _selectedWorkOrder;
        public WorkOrder SelectedWorkOrder
        {
            get => _selectedWorkOrder;
            set
            {
                _selectedWorkOrder = value;
                OnPropertyChanged();
            }
        }
        
        // 模拟数据集合
        private List<WorkOrder> _allWorkOrders;
        
        // 构造函数
        public ProjectAuditViewModel()
        {
            // 初始化命令
            SearchCommand = new RelayCommand(Search);
            AuditCommand = new RelayCommand(Audit);
            ResetCommand = new RelayCommand(Reset);
            
            // 初始化非空字段
            _licensePlate = string.Empty;
            _orderNumber = string.Empty;
            _auditWorkOrders = new ObservableCollection<WorkOrder>();
            
            // 初始化数据
            LoadSampleData();
        }
        
        /// <summary>
        /// 搜索待审查的工单
        /// </summary>
        private void Search(object? parameter)
        {
            // 在实际应用中，这里应该调用数据库进行查询
            // 现在使用模拟数据进行演示
            var filteredWorkOrders = _allWorkOrders.Where(workOrder =>
            {
                var matchLicensePlate = string.IsNullOrEmpty(LicensePlate) || (workOrder.Vehicle?.LicensePlate.Contains(LicensePlate) ?? false);
                var matchOrderNumber = string.IsNullOrEmpty(OrderNumber) || workOrder.OrderNumber.Contains(OrderNumber);
                var matchStatus = workOrder.Status == "待审查";
                
                return matchLicensePlate && matchOrderNumber && matchStatus;
            }).ToList();
            
            AuditWorkOrders = new ObservableCollection<WorkOrder>(filteredWorkOrders);
        }
        
        /// <summary>
        /// 审查项目
        /// </summary>
        private void Audit(object? parameter)
        {
            if (SelectedWorkOrder != null)
            {
                // 标记工单为已审查
                SelectedWorkOrder.Status = "已审查";
                
                // 标记所有项目为已审查
                foreach (var item in SelectedWorkOrder.MaintenanceItems)
                {
                    item.Status = "已审查";
                }
                
                // 更新UI
                OnPropertyChanged(nameof(SelectedWorkOrder));
                Search(null); // 重新搜索以更新列表
            }
        }
        
        /// <summary>
        /// 验证工单是否可以添加新项目
        /// </summary>
        /// <param name="workOrder">工单对象</param>
        /// <returns>是否可以添加新项目</returns>
        public bool CanAddMaintenanceItem(WorkOrder workOrder)
        {
            // 只有状态为"待审查"或"进行中"的工单才能添加新项目
            // 已审查或已完成的工单不能添加新项目
            return workOrder != null && (workOrder.Status == "待审查" || workOrder.Status == "进行中");
        }
        
        /// <summary>
        /// 重置查询条件
        /// </summary>
        private void Reset(object? parameter)
        {
            LicensePlate = string.Empty;
            OrderNumber = string.Empty;
            Search(null);
        }
        
        /// <summary>
        /// 加载模拟数据
        /// </summary>
        private void LoadSampleData()
        {
            // 模拟数据
            _allWorkOrders = new List<WorkOrder>
            {
                new WorkOrder
                {
                    Id = 1,
                    OrderNumber = "WO20230501001",
                    CustomerId = 1,
                    VehicleId = 1,
                    ReceptionDate = DateTime.Now.AddDays(-2),
                    ServiceAdvisor = "张三",
                    ProblemDescription = "发动机异响",
                    Status = "待审查",
                    TotalAmount = 1500,
                    Remarks = "客户反映行驶时发动机有异响",
                    Vehicle = new CustomerVehicle { Id = 1, LicensePlate = "京A12345", VehicleBrand = "大众", VehicleModel = "帕萨特" },
                    MaintenanceItems = new List<MaintenanceItem>
                    {
                        new MaintenanceItem { Id = 1, WorkOrderId = 1, ItemName = "发动机检查", Description = "全面检查发动机系统", UnitPrice = 200, Quantity = 1, Subtotal = 200, Technician = "李四", Status = "待审查" },
                        new MaintenanceItem { Id = 2, WorkOrderId = 1, ItemName = "更换机油", Description = "更换全合成机油", UnitPrice = 300, Quantity = 1, Subtotal = 300, Technician = "李四", Status = "待审查" },
                        new MaintenanceItem { Id = 3, WorkOrderId = 1, ItemName = "更换机滤", Description = "更换机油滤清器", UnitPrice = 50, Quantity = 1, Subtotal = 50, Technician = "李四", Status = "待审查" }
                    }
                },
                new WorkOrder
                {
                    Id = 2,
                    OrderNumber = "WO20230501002",
                    CustomerId = 2,
                    VehicleId = 2,
                    ReceptionDate = DateTime.Now.AddDays(-1),
                    ServiceAdvisor = "王五",
                    ProblemDescription = "刹车不灵",
                    Status = "待审查",
                    TotalAmount = 1200,
                    Remarks = "客户反映刹车距离变长",
                    Vehicle = new CustomerVehicle { Id = 2, LicensePlate = "京B67890", VehicleBrand = "丰田", VehicleModel = "凯美瑞" },
                    MaintenanceItems = new List<MaintenanceItem>
                    {
                        new MaintenanceItem { Id = 4, WorkOrderId = 2, ItemName = "刹车系统检查", Description = "检查刹车盘、刹车片和刹车油", UnitPrice = 150, Quantity = 1, Subtotal = 150, Technician = "赵六", Status = "待审查" },
                        new MaintenanceItem { Id = 5, WorkOrderId = 2, ItemName = "更换刹车片", Description = "更换前后刹车片", UnitPrice = 800, Quantity = 1, Subtotal = 800, Technician = "赵六", Status = "待审查" },
                        new MaintenanceItem { Id = 6, WorkOrderId = 2, ItemName = "更换刹车油", Description = "更换DOT4刹车油", UnitPrice = 100, Quantity = 1, Subtotal = 100, Technician = "赵六", Status = "待审查" }
                    }
                },
                new WorkOrder
                {
                    Id = 3,
                    OrderNumber = "WO20230502001",
                    CustomerId = 3,
                    VehicleId = 3,
                    ReceptionDate = DateTime.Now,
                    ServiceAdvisor = "张三",
                    ProblemDescription = "空调不制冷",
                    Status = "待审查",
                    TotalAmount = 1800,
                    Remarks = "客户反映空调制冷效果差",
                    Vehicle = new CustomerVehicle { Id = 3, LicensePlate = "京C13579", VehicleBrand = "本田", VehicleModel = "雅阁" },
                    MaintenanceItems = new List<MaintenanceItem>
                    {
                        new MaintenanceItem { Id = 7, WorkOrderId = 3, ItemName = "空调系统检查", Description = "检查空调压缩机、冷凝器和制冷剂", UnitPrice = 200, Quantity = 1, Subtotal = 200, Technician = "孙七", Status = "待审查" },
                        new MaintenanceItem { Id = 8, WorkOrderId = 3, ItemName = "更换空调滤芯", Description = "更换空调滤清器", UnitPrice = 100, Quantity = 1, Subtotal = 100, Technician = "孙七", Status = "待审查" },
                        new MaintenanceItem { Id = 9, WorkOrderId = 3, ItemName = "添加制冷剂", Description = "添加R134a制冷剂", UnitPrice = 200, Quantity = 1, Subtotal = 200, Technician = "孙七", Status = "待审查" }
                    }
                }
            };
            
            // 初始加载待审查的工单
            Search(null);
        }
    }
}