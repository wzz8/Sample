using System;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Windows;
using System.Windows.Input;
using Login_Sample.Data;

namespace Login_Sample.ViewModels
{
    public class SparePartsOutboundViewModel : INotifyPropertyChanged
    {
        // 工单号
        private string _workOrderNumber;
        public string WorkOrderNumber
        {
            get { return _workOrderNumber; }
            set { _workOrderNumber = value; OnPropertyChanged(); }
        }

        // 工单列表
        private ObservableCollection<WorkOrder> _workOrders;
        public ObservableCollection<WorkOrder> WorkOrders
        {
            get { return _workOrders; }
            set { _workOrders = value; OnPropertyChanged(); }
        }

        // 选中的工单
        private WorkOrder? _selectedWorkOrder;
        public WorkOrder? SelectedWorkOrder
        {
            get { return _selectedWorkOrder; }
            set
            {
                if (_selectedWorkOrder != value)
                {
                    _selectedWorkOrder = value;
                    OnPropertyChanged(nameof(SelectedWorkOrder));
                    if (_selectedWorkOrder != null)
                    {
                        LoadRequiredSpareParts(_selectedWorkOrder.OrderNumber);
                    }
                }
            }
        }

        // 所需配件列表
        private ObservableCollection<InventoryItem> _requiredSpareParts;
        public ObservableCollection<InventoryItem> RequiredSpareParts
        {
            get { return _requiredSpareParts; }
            set { _requiredSpareParts = value; OnPropertyChanged(); }
        }

        // 操作人
        private string _operator;
        public string Operator
        {
            get { return _operator; }
            set { _operator = value; OnPropertyChanged(); }
        }

        // 操作时间
        private DateTime _operationTime;
        public DateTime OperationTime
        {
            get { return _operationTime; }
            set { _operationTime = value; OnPropertyChanged(); }
        }

        // 备注
        private string _remarks;
        public string Remarks
        {
            get { return _remarks; }
            set { _remarks = value; OnPropertyChanged(); }
        }

        // 命令
        public ICommand SearchWorkOrderCommand { get; set; }
        public ICommand OutboundCommand { get; set; }

        // 构造函数
        public SparePartsOutboundViewModel()
        {
            _workOrderNumber = string.Empty;
            _operator = "张三"; // 默认操作人
            _operationTime = DateTime.Now;
            _remarks = string.Empty;

            WorkOrders = new ObservableCollection<WorkOrder>();
            RequiredSpareParts = new ObservableCollection<InventoryItem>();

            // 初始化命令
            SearchWorkOrderCommand = new RelayCommand(SearchWorkOrder);
            OutboundCommand = new RelayCommand(OutboundSpareParts);

            // 加载示例数据
            LoadSampleWorkOrders();
        }

        // 加载示例工单数据
        private void LoadSampleWorkOrders()
        {
            WorkOrders = new ObservableCollection<WorkOrder>();
        }

        // 查询工单
        private void SearchWorkOrder(object parameter)
        {
            // 这里可以实现实际的查询逻辑，目前只是演示
            WorkOrders.Clear();
            
            // 添加示例数据
            WorkOrders.Add(new WorkOrder
            {
                Id = 1,
                OrderNumber = "WO20240501001",
                CustomerId = 1,
                Customer = new Customer { Id = 1, Name = "张三" },
                VehicleId = 1,
                Vehicle = new CustomerVehicle { Id = 1, LicensePlate = "京A12345", VehicleBrand = "大众", VehicleModel = "帕萨特" },
                ReceptionDate = new DateTime(2024, 5, 1, 10, 0, 0),
                ServiceAdvisor = "王五",
                ProblemDescription = "发动机异响",
                Status = "进行中",
                Remarks = string.Empty,
                MaintenanceItems = new List<MaintenanceItem>()
            });

            WorkOrders.Add(new WorkOrder
            {
                Id = 2,
                OrderNumber = "WO20240502002",
                CustomerId = 2,
                Customer = new Customer { Id = 2, Name = "李四" },
                VehicleId = 2,
                Vehicle = new CustomerVehicle { Id = 2, LicensePlate = "京B67890", VehicleBrand = "丰田", VehicleModel = "卡罗拉" },
                ReceptionDate = new DateTime(2024, 5, 2, 14, 30, 0),
                ServiceAdvisor = "赵六",
                ProblemDescription = "刹车不灵",
                Status = "进行中",
                Remarks = string.Empty,
                MaintenanceItems = new List<MaintenanceItem>()
            });

            WorkOrders.Add(new WorkOrder
            {
                Id = 3,
                OrderNumber = "WO20240503003",
                CustomerId = 3,
                Customer = new Customer { Id = 3, Name = "王五" },
                VehicleId = 3,
                Vehicle = new CustomerVehicle { Id = 3, LicensePlate = "京C24680", VehicleBrand = "本田", VehicleModel = "思域" },
                ReceptionDate = new DateTime(2024, 5, 3, 9, 15, 0),
                ServiceAdvisor = "孙七",
                ProblemDescription = "空调不制冷",
                Status = "进行中",
                Remarks = string.Empty,
                MaintenanceItems = new List<MaintenanceItem>()
            });
        }

        // 查询工单
        // private void SearchWorkOrder(object parameter)
        // {
        //     // 这里可以实现实际的查询逻辑，目前只是演示
        //     if (string.IsNullOrEmpty(WorkOrderNumber))
        //     {
        //         // 如果工单号为空，显示所有工单
        //         // 实际应用中可能需要从数据库查询
        //     }
        //     else
        //     {
        //         // 根据工单号筛选工单
        //         // 实际应用中可能需要从数据库查询
        //         var filtered = new ObservableCollection<WorkOrder>(
        //             WorkOrders.Where(wo => wo.OrderNumber.Contains(WorkOrderNumber)));
        //         WorkOrders = filtered;
        //     }
        // }

        // 加载所需配件
        private void LoadRequiredSpareParts(string orderNumber)
        {
            RequiredSpareParts.Clear();

            // 根据工单号加载所需配件
            if (orderNumber == "WO20240501001")
            {
                RequiredSpareParts.Add(new InventoryItem
                {
                    ItemNumber = "SP001",
                    ItemName = "机油滤清器",
                    Quantity = 1,
                    UnitPrice = 50.00m,
                    Supplier = "大众原厂",
                    WarehouseLocation = "A区1号货架",
                    Status = "正常"
                });

                RequiredSpareParts.Add(new InventoryItem
                {
                    ItemNumber = "SP002",
                    ItemName = "空气滤清器",
                    Quantity = 1,
                    UnitPrice = 80.00m,
                    Supplier = "大众原厂",
                    WarehouseLocation = "A区2号货架",
                    Status = "正常"
                });
            }
            else if (orderNumber == "WO20240502002")
            {
                RequiredSpareParts.Add(new InventoryItem
                {
                    ItemNumber = "SP003",
                    ItemName = "刹车片",
                    Quantity = 1,
                    UnitPrice = 200.00m,
                    Supplier = "丰田原厂",
                    WarehouseLocation = "B区3号货架",
                    Status = "正常"
                });

                RequiredSpareParts.Add(new InventoryItem
                {
                    ItemNumber = "SP004",
                    ItemName = "刹车盘",
                    Quantity = 1,
                    UnitPrice = 300.00m,
                    Supplier = "丰田原厂",
                    WarehouseLocation = "B区4号货架",
                    Status = "正常"
                });
            }
            else if (orderNumber == "WO20240503003")
            {
                RequiredSpareParts.Add(new InventoryItem
                {
                    ItemNumber = "SP005",
                    ItemName = "空调滤芯",
                    Quantity = 1,
                    UnitPrice = 100.00m,
                    Supplier = "本田原厂",
                    WarehouseLocation = "C区5号货架",
                    Status = "正常"
                });

                RequiredSpareParts.Add(new InventoryItem
                {
                    ItemNumber = "SP006",
                    ItemName = "制冷剂",
                    Quantity = 1,
                    UnitPrice = 150.00m,
                    Supplier = "本田原厂",
                    WarehouseLocation = "C区6号货架",
                    Status = "正常"
                });
            }
        }

        // 备件出库
        private void OutboundSpareParts(object parameter)
        {
            if (SelectedWorkOrder == null)
            {
                MessageBox.Show("请先选择一个工单", "提示", MessageBoxButton.OK, MessageBoxImage.Warning);
                return;
            }

            if (RequiredSpareParts.Count == 0)
            {
                MessageBox.Show("该工单没有需要出库的配件", "提示", MessageBoxButton.OK, MessageBoxImage.Warning);
                return;
            }

            // 这里可以实现实际的出库逻辑
            // 例如：更新库存数量、生成出库记录等

            // 显示成功提示
            MessageBox.Show($"工单{SelectedWorkOrder.OrderNumber}的配件出库成功！\n操作人：{Operator}\n操作时间：{OperationTime}", "成功", MessageBoxButton.OK, MessageBoxImage.Information);

            // 重置表单
            WorkOrderNumber = string.Empty;
            SelectedWorkOrder = null;
            RequiredSpareParts.Clear();
            Remarks = string.Empty;
            OperationTime = DateTime.Now;
        }

        // INotifyPropertyChanged 实现
        public event PropertyChangedEventHandler PropertyChanged;
        protected virtual void OnPropertyChanged([System.Runtime.CompilerServices.CallerMemberName] string propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}