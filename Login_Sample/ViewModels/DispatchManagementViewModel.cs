using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using System;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Windows.Input;

namespace Login_Sample.ViewModels
{
    public class DispatchManagementViewModel : ObservableObject
    {
        // 内部车辆类
        public class Vehicle
        {
            public string VehicleId { get; set; }
            public string LicensePlate { get; set; }
            public string Model { get; set; }
            public string CustomerName { get; set; }
            public DateTime ReceptionDate { get; set; }
        }

        // 内部维修项目类
        public class RepairItem
        {
            public int Id { get; set; }
            public string Name { get; set; }
            public int Progress { get; set; }
            public string Status { get; set; }
            public string AssignedTechnician { get; set; }
        }

        // 内部技师类
        public class Technician
        {
            public int Id { get; set; }
            public string Name { get; set; }
            public string Position { get; set; }
            public int CurrentWorkload { get; set; }
        }

        #region Properties
        
        // 在修车辆列表
        private ObservableCollection<Vehicle> _vehiclesInRepair;
        public ObservableCollection<Vehicle> VehiclesInRepair
        {
            get { return _vehiclesInRepair; }
            set
            {
                _vehiclesInRepair = value;
                OnPropertyChanged(nameof(VehiclesInRepair));
            }
        }
        
        // 选中的车辆
        private Vehicle _selectedVehicle;
        public Vehicle SelectedVehicle
        {
            get { return _selectedVehicle; }
            set
            {
                _selectedVehicle = value;
                OnPropertyChanged(nameof(SelectedVehicle));
                // 当选中车辆改变时，加载该车辆的维修项目和可用技师
                if (value != null)
                {
                    LoadRepairItems(value);
                    LoadAvailableTechnicians();
                }
            }
        }
        
        // 维修项目列表
        private ObservableCollection<RepairItem> _repairItems;
        public ObservableCollection<RepairItem> RepairItems
        {
            get { return _repairItems; }
            set
            {
                _repairItems = value;
                OnPropertyChanged(nameof(RepairItems));
            }
        }
        
        // 可用技师列表
        private ObservableCollection<Technician> _availableTechnicians;
        public ObservableCollection<Technician> AvailableTechnicians
        {
            get { return _availableTechnicians; }
            set
            {
                _availableTechnicians = value;
                OnPropertyChanged(nameof(AvailableTechnicians));
            }
        }
        
        // 选中的技师
        private Technician _selectedTechnician;
        public Technician SelectedTechnician
        {
            get { return _selectedTechnician; }
            set
            {
                _selectedTechnician = value;
                OnPropertyChanged(nameof(SelectedTechnician));
            }
        }
        
        // 新维修项目名称
        private string _newRepairItemName;
        public string NewRepairItemName
        {
            get { return _newRepairItemName; }
            set
            {
                _newRepairItemName = value;
                OnPropertyChanged(nameof(NewRepairItemName));
            }
        }
        
        #endregion
        
        #region Commands
        
        // 添加维修项目命令
        public ICommand AddRepairItemCommand { get; set; }
        
        // 指派技师命令
        public ICommand AssignTechnicianCommand { get; set; }
        
        // 保存派工命令
        public ICommand SaveDispatchCommand { get; set; }
        
        #endregion
        
        #region Constructors
        
        public DispatchManagementViewModel()
        {
            // 初始化命令
            AddRepairItemCommand = new RelayCommand<object>(AddRepairItem);
            AssignTechnicianCommand = new RelayCommand<object>(AssignTechnician);
            SaveDispatchCommand = new RelayCommand<object>(SaveDispatch);
            
            // 初始化数据
            LoadVehiclesInRepair();
        }
        
        #endregion
        
        #region Methods
        
        // 加载在修车辆列表
        private void LoadVehiclesInRepair()
        {
            // 模拟数据
            VehiclesInRepair = new ObservableCollection<Vehicle>
            {
                new Vehicle { VehicleId = "京A12345", LicensePlate = "京A12345", Model = "宝马3系", CustomerName = "张三", ReceptionDate = DateTime.Now.AddDays(-2) },
                new Vehicle { VehicleId = "京B67890", LicensePlate = "京B67890", Model = "奔驰C级", CustomerName = "李四", ReceptionDate = DateTime.Now.AddDays(-1) },
                new Vehicle { VehicleId = "京C24680", LicensePlate = "京C24680", Model = "奥迪A4", CustomerName = "王五", ReceptionDate = DateTime.Now }
            };
        }
        
        // 加载维修项目
        private void LoadRepairItems(Vehicle vehicle)
        {
            // 模拟数据，根据不同车辆加载不同的维修项目
            if (vehicle.VehicleId == "京A12345")
            {
                RepairItems = new ObservableCollection<RepairItem>
                {
                    new RepairItem { Id = 1, Name = "发动机保养", Progress = 80, Status = "正在进行", AssignedTechnician = "张三" },
                    new RepairItem { Id = 2, Name = "更换机油", Progress = 100, Status = "已完成", AssignedTechnician = "张三" }
                };
            }
            else if (vehicle.VehicleId == "京B67890")
            {
                RepairItems = new ObservableCollection<RepairItem>
                {
                    new RepairItem { Id = 3, Name = "刹车系统检查", Progress = 50, Status = "正在进行", AssignedTechnician = "李四" }
                };
            }
            else
            {
                RepairItems = new ObservableCollection<RepairItem>
                {
                    new RepairItem { Id = 4, Name = "空调系统维修", Progress = 0, Status = "待进行", AssignedTechnician = "" }
                };
            }
        }
        
        // 加载可用技师
        private void LoadAvailableTechnicians()
        {
            // 模拟数据
            AvailableTechnicians = new ObservableCollection<Technician>
            {
                new Technician { Id = 1, Name = "张三", Position = "高级技师", CurrentWorkload = 2 },
                new Technician { Id = 2, Name = "李四", Position = "中级技师", CurrentWorkload = 1 },
                new Technician { Id = 3, Name = "王五", Position = "初级技师", CurrentWorkload = 0 },
                new Technician { Id = 4, Name = "赵六", Position = "高级技师", CurrentWorkload = 3 }
            };
        }
        
        // 添加维修项目
        private void AddRepairItem(object parameter)
        {
            if (SelectedVehicle != null && !string.IsNullOrWhiteSpace(NewRepairItemName))
            {
                // 创建新的维修项目
                var newItem = new RepairItem
                {
                    Id = RepairItems.Count + 1,
                    Name = NewRepairItemName,
                    Progress = 0,
                    Status = "待进行",
                    AssignedTechnician = string.Empty
                };
                
                // 添加到维修项目列表
                RepairItems.Add(newItem);
                
                // 清空输入框
                NewRepairItemName = string.Empty;
            }
        }
        
        // 指派技师
        private void AssignTechnician(object parameter)
        {
            if (SelectedVehicle != null && SelectedTechnician != null)
            {
                // 这里可以实现指派技师的逻辑
                // 例如，将选中的技师分配给当前选中的维修项目
                // 由于界面设计可能需要用户先选择维修项目，这里可以根据实际需求调整
            }
        }
        
        // 保存派工信息
        private void SaveDispatch(object parameter)
        {
            if (SelectedVehicle != null)
            {
                // 这里可以实现保存派工信息的逻辑
                // 例如，将维修项目和技师分配信息保存到数据库
                // 可以显示保存成功的提示
            }
        }
        
        #endregion
    }
}