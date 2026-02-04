using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
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
    public class WorkshopManagementViewModel : ObservableObject
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
        
        // 选中的技师（用于查询）
        private Technician _selectedTechnicianForQuery;
        public Technician SelectedTechnicianForQuery
        {
            get { return _selectedTechnicianForQuery; }
            set
            {
                _selectedTechnicianForQuery = value;
                OnPropertyChanged(nameof(SelectedTechnicianForQuery));
            }
        }
        
        // 车牌号（用于查询）
        private string _licensePlateForQuery;
        public string LicensePlateForQuery
        {
            get { return _licensePlateForQuery; }
            set
            {
                _licensePlateForQuery = value;
                OnPropertyChanged(nameof(LicensePlateForQuery));
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
        
        // 查询结果数据
        private ObservableCollection<VehicleDashboardData> _queryResultData;
        public ObservableCollection<VehicleDashboardData> QueryResultData
        {
            get { return _queryResultData; }
            set
            {
                _queryResultData = value;
                OnPropertyChanged(nameof(QueryResultData));
            }
        }
        
        #endregion
        
        #region Commands
        
        // 刷新数据命令
        public ICommand RefreshDataCommand { get; set; }
        
        // 查询命令
        public ICommand QueryCommand { get; set; }
        
        #endregion
        
        #region Constructors
        
        public WorkshopManagementViewModel()
        {
            // 初始化命令
            RefreshDataCommand = new RelayCommand<object>(RefreshData);
            QueryCommand = new RelayCommand<object>(ExecuteQuery);
            
            // 初始化数据
            InitializeData();
        }
        
        #endregion
        
        #region Methods
        
        // 初始化数据
        private void InitializeData()
        {
            // 加载技师列表，添加空选项
            Technicians = new ObservableCollection<Technician> {
                new Technician { Id = 0, Name = "", Position = "" }, // 空选项
                new Technician { Id = 1, Name = "张三", Position = "高级技师" },
                new Technician { Id = 2, Name = "李四", Position = "中级技师" },
                new Technician { Id = 3, Name = "王五", Position = "初级技师" },
                new Technician { Id = 4, Name = "赵六", Position = "高级技师" }
            };
            
            // 默认选中空选项
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
            
            // 检查是否选择了空选项
            if (SelectedTechnician != null && SelectedTechnician.Id != 0)
            {
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
            }
            
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
        
        // 执行查询
        private void ExecuteQuery(object parameter)
        {
            // 获取所有车辆维修数据
            var allVehicleData = GetAllVehicleData();
            
            // 根据查询条件过滤数据
            var filteredData = allVehicleData;
            
            // 按维修工过滤
            if (SelectedTechnicianForQuery != null)
            {
                filteredData = filteredData.Where(v => v.TechnicianName == SelectedTechnicianForQuery.Name).ToList();
            }
            
            // 按车牌号过滤
            if (!string.IsNullOrEmpty(LicensePlateForQuery))
            {
                filteredData = filteredData.Where(v => v.VehicleId.Contains(LicensePlateForQuery)).ToList();
            }
            
            // 设置查询结果
            QueryResultData = new ObservableCollection<VehicleDashboardData>(filteredData);
        }
        
        // 获取所有车辆数据（模拟从数据库获取）
        private List<VehicleDashboardData> GetAllVehicleData()
        {
            // 这里返回模拟数据，实际应该从数据库查询
            return new List<VehicleDashboardData>
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
                        new RepairItem { Name = "发动机保养", Status = "正在进行", Progress = 80 },
                        new RepairItem { Name = "更换机油", Status = "已完成", Progress = 100 },
                        new RepairItem { Name = "轮胎检查", Status = "待料", Progress = 30 }
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
                        new RepairItem { Name = "刹车系统检查", Status = "正在进行", Progress = 50 },
                        new RepairItem { Name = "更换刹车片", Status = "待进行", Progress = 0 }
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
                        new RepairItem { Name = "空调系统维修", Status = "正在进行", Progress = 90 },
                        new RepairItem { Name = "更换空调滤芯", Status = "已完成", Progress = 100 },
                        new RepairItem { Name = "车辆清洁", Status = "待进行", Progress = 0 }
                    }
                },
                new VehicleDashboardData
                {
                    VehicleId = "京D13579",
                    Model = "丰田凯美瑞",
                    CustomerName = "赵六",
                    TechnicianName = "张三",
                    ReceptionDate = "2023-12-04",
                    OverallProgress = 20,
                    RepairItems = new ObservableCollection<RepairItem>
                    {
                        new RepairItem { Name = "变速箱维修", Status = "正在进行", Progress = 20 }
                    }
                }
            };
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