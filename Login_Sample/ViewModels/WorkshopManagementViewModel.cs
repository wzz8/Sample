using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace Login_Sample.ViewModels
{
    public class WorkshopManagementViewModel : INotifyPropertyChanged
    {
        #region Properties
        
        // 技师列表
        private ObservableCollection<Technician> _technicians;
        public ObservableCollection<Technician> Technicians
        {
            get { return _technicians; }
            set
            {
                _technicians = value;
                OnPropertyChanged(nameof(Technicians));
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
                LoadTechnicianData();
            }
        }
        
        // 技师工作进度数据
        private ObservableCollection<TechnicianDashboardData> _technicianDashboardData;
        public ObservableCollection<TechnicianDashboardData> TechnicianDashboardData
        {
            get { return _technicianDashboardData; }
            set
            {
                _technicianDashboardData = value;
                OnPropertyChanged(nameof(TechnicianDashboardData));
            }
        }
        
        // 车辆维修进度数据
        private ObservableCollection<VehicleDashboardData> _vehicleDashboardData;
        public ObservableCollection<VehicleDashboardData> VehicleDashboardData
        {
            get { return _vehicleDashboardData; }
            set
            {
                _vehicleDashboardData = value;
                OnPropertyChanged(nameof(VehicleDashboardData));
            }
        }
        
        #endregion
        
        #region Commands
        
        // 刷新数据命令
        public ICommand RefreshDataCommand { get; set; }
        
        #endregion
        
        #region Constructors
        
        public WorkshopManagementViewModel()
        {
            // 初始化命令
            RefreshDataCommand = new RelayCommand(RefreshData);
            
            // 初始化数据
            InitializeData();
        }
        
        #endregion
        
        #region Methods
        
        // 初始化数据
        private void InitializeData()
        {
            // 加载技师列表
            Technicians = new ObservableCollection<Technician> {
                new Technician { Id = 1, Name = "张三", Position = "高级技师" },
                new Technician { Id = 2, Name = "李四", Position = "中级技师" },
                new Technician { Id = 3, Name = "王五", Position = "初级技师" },
                new Technician { Id = 4, Name = "赵六", Position = "高级技师" }
            };
            
            // 默认选中第一个技师
            if (Technicians.Count > 0)
                SelectedTechnician = Technicians[0];
                
            // 加载车辆维修数据
            LoadVehicleDashboardData();
        }
        
        // 加载技师数据
        private void LoadTechnicianData()
        {
            // 模拟加载数据
            var dashboardData = new ObservableCollection<TechnicianDashboardData>();
            
            // 技师概览卡片
            dashboardData.Add(new TechnicianDashboardData
            {
                Type = DashboardDataType.TechnicianOverview,
                TechnicianId = SelectedTechnician.Id,
                TechnicianName = SelectedTechnician.Name,
                TotalTasks = 5,
                CompletedTasks = 2,
                InProgressTasks = 2,
                PendingTasks = 1,
                OverallProgress = 60
            });
            
            // 车辆任务卡片
            dashboardData.Add(new TechnicianDashboardData
            {
                Type = DashboardDataType.VehicleTask,
                VehicleId = "京A12345",
                RepairItems = new ObservableCollection<RepairItem>
                {
                    new RepairItem { Name = "发动机保养", Progress = 80, Status = "正在进行" },
                    new RepairItem { Name = "更换机油", Progress = 100, Status = "已完成" },
                    new RepairItem { Name = "轮胎检查", Progress = 30, Status = "待料" }
                }
            });
            
            dashboardData.Add(new TechnicianDashboardData
            {
                Type = DashboardDataType.VehicleTask,
                VehicleId = "京B67890",
                RepairItems = new ObservableCollection<RepairItem>
                {
                    new RepairItem { Name = "刹车系统检查", Progress = 50, Status = "正在进行" },
                    new RepairItem { Name = "更换刹车片", Progress = 0, Status = "待进行" }
                }
            });
            
            TechnicianDashboardData = dashboardData;
        }
        
        // 加载车辆数据
        private void LoadVehicleDashboardData()
        {
            // 模拟加载数据
            var vehicleData = new ObservableCollection<VehicleDashboardData>
            {
                new VehicleDashboardData
                {
                    VehicleId = "京A12345",
                    Model = "宝马3系",
                    CustomerName = "张三",
                    TechnicianName = "李四",
                    ReceptionDate = "2023-12-01",
                    OverallProgress = 70,
                    RepairItems = new ObservableCollection<RepairItem>
                    {
                        new RepairItem { Name = "发动机保养", Status = "正在进行" },
                        new RepairItem { Name = "更换机油", Status = "已完成" },
                        new RepairItem { Name = "轮胎检查", Status = "待料" }
                    }
                },
                new VehicleDashboardData
                {
                    VehicleId = "京B67890",
                    Model = "奔驰C级",
                    CustomerName = "李四",
                    TechnicianName = "王五",
                    ReceptionDate = "2023-12-02",
                    OverallProgress = 40,
                    RepairItems = new ObservableCollection<RepairItem>
                    {
                        new RepairItem { Name = "刹车系统检查", Status = "正在进行" },
                        new RepairItem { Name = "更换刹车片", Status = "待进行" }
                    }
                },
                new VehicleDashboardData
                {
                    VehicleId = "京C24680",
                    Model = "奥迪A4",
                    CustomerName = "王五",
                    TechnicianName = "赵六",
                    ReceptionDate = "2023-12-03",
                    OverallProgress = 90,
                    RepairItems = new ObservableCollection<RepairItem>
                    {
                        new RepairItem { Name = "空调系统维修", Status = "正在进行" },
                        new RepairItem { Name = "更换空调滤芯", Status = "已完成" },
                        new RepairItem { Name = "车辆清洁", Status = "待进行" }
                    }
                }
            };
            
            VehicleDashboardData = vehicleData;
        }
        
        // 刷新数据
        private void RefreshData(object parameter)
        {
            LoadTechnicianData();
            LoadVehicleDashboardData();
        }
        
        #endregion
        
        #region INotifyPropertyChanged Implementation
        
        public event PropertyChangedEventHandler PropertyChanged;
        
        protected virtual void OnPropertyChanged(string propertyName)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
        
        #endregion
    }
    
    #region Data Models
    
    // 技师类
    public class Technician
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Position { get; set; }
    }
    
    // 维修项目类
    public class RepairItem
    {
        public string Name { get; set; }
        public int Progress { get; set; }
        public string Status { get; set; }
    }
    
    // 仪表盘数据类型枚举
    public enum DashboardDataType
    {
        TechnicianOverview,
        VehicleTask
    }
    
    // 技师仪表盘数据类
    public class TechnicianDashboardData
    {
        public DashboardDataType Type { get; set; }
        public int TechnicianId { get; set; }
        public string TechnicianName { get; set; }
        public string VehicleId { get; set; }
        public int TotalTasks { get; set; }
        public int CompletedTasks { get; set; }
        public int InProgressTasks { get; set; }
        public int PendingTasks { get; set; }
        public int OverallProgress { get; set; }
        public ObservableCollection<RepairItem> RepairItems { get; set; }
    }
    
    // 车辆仪表盘数据类
    public class VehicleDashboardData
    {
        public string VehicleId { get; set; }
        public string Model { get; set; }
        public string CustomerName { get; set; }
        public string TechnicianName { get; set; }
        public string ReceptionDate { get; set; }
        public int OverallProgress { get; set; }
        public ObservableCollection<RepairItem> RepairItems { get; set; }
    }
    
    #endregion
}