using System;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using Login_Sample.Views;
using MaterialDesignThemes.Wpf;

namespace Login_Sample.ViewModels
{
    public class MainViewModel : INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler PropertyChanged;

        private void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }

        // 当前选中的模块
        private UserControl _currentView;
        public UserControl CurrentView
        {
            get { return _currentView; }
            set { _currentView = value; OnPropertyChanged(); }
        }

        // 各模块的View实例
        public DashboardView DashboardView { get; set; }
        public BusinessReceptionView BusinessReceptionView { get; set; }
        public VehicleMaintenanceView VehicleMaintenanceView { get; set; }
        public MaintenanceQueryView MaintenanceQueryView { get; set; }
        public ProjectQueryView ProjectQueryView { get; set; }
        public FinancialSettlementView FinancialSettlementView { get; set; }
        public SparePartsManagementView SparePartsManagementView { get; set; }
        public WorkshopManagementView WorkshopManagementView { get; set; }
        public LeadershipQueryView LeadershipQueryView { get; set; }
        public SystemManagementView SystemManagementView { get; set; }
        public RawInventoryView RawInventoryView { get; set; }
        public CustomerManagementView CustomerManagementView { get; set; }
        public RepairProgressView RepairProgressView { get; set; }
        public InventoryQueryView InventoryQueryView { get; set; }
        public ShortageManagementView ShortageManagementView { get; set; }
        public InsuranceManagementView InsuranceManagementView { get; set; }
        // 新增派工处理视图
        public DispatchManagementView DispatchManagementView { get; set; }
        
        // 各模块的ViewModel实例
        public DashboardViewModel DashboardVM { get; set; }
        public BusinessReceptionViewModel BusinessReceptionVM { get; set; }
        public VehicleMaintenanceViewModel VehicleMaintenanceVM { get; set; }
        public MaintenanceQueryViewModel MaintenanceQueryVM { get; set; }
        public ProjectQueryViewModel ProjectQueryVM { get; set; }
        public FinancialSettlementViewModel FinancialSettlementVM { get; set; }
        public SparePartsManagementViewModel SparePartsManagementVM { get; set; }
        public WorkshopManagementViewModel WorkshopManagementVM { get; set; }
        public LeadershipQueryViewModel LeadershipQueryVM { get; set; }
        public SystemManagementViewModel SystemManagementVM { get; set; }
        public RawInventoryViewModel RawInventoryVM { get; set; }
        public CustomerManagementViewModel CustomerManagementVM { get; set; }
        public RepairProgressViewModel RepairProgressVM { get; set; }
        public InventoryQueryViewModel InventoryQueryVM { get; set; }
        public ShortageManagementViewModel ShortageManagementVM { get; set; }
        public InsuranceManagementViewModel InsuranceManagementVM { get; set; }
        // 新增派工处理ViewModel
        public DispatchManagementViewModel DispatchManagementVM { get; set; }
        
        // 命令
        public ICommand NavigateCommand { get; set; }
        public ICommand LogoutCommand { get; set; }

        // 当前用户信息
        private string _currentUser;
        public string CurrentUser
        {
            get { return _currentUser; }
            set { _currentUser = value; OnPropertyChanged(); }
        }

        // 当前时间
        private string _currentTime;
        public string CurrentTime
        {
            get { return _currentTime; }
            set { _currentTime = value; OnPropertyChanged(); }
        }

        // 当前选中的模块
        private string _currentModule = "Dashboard";
        public string CurrentModule
        {
            get { return _currentModule; }
            set { _currentModule = value; OnPropertyChanged(); }
        }

        // 侧边栏位置
        private bool _sidebarOnLeft = true;
        public bool SidebarOnLeft
        {
            get { return _sidebarOnLeft; }
            set { _sidebarOnLeft = value; OnPropertyChanged(); }
        }

        // 构造函数
        public MainViewModel()
        {
            // 初始化各模块的ViewModel
            DashboardVM = new DashboardViewModel();
            BusinessReceptionVM = new BusinessReceptionViewModel();
            VehicleMaintenanceVM = new VehicleMaintenanceViewModel();
            MaintenanceQueryVM = new MaintenanceQueryViewModel();
            ProjectQueryVM = new ProjectQueryViewModel();
            FinancialSettlementVM = new FinancialSettlementViewModel();
            SparePartsManagementVM = new SparePartsManagementViewModel();
            WorkshopManagementVM = new WorkshopManagementViewModel();
            LeadershipQueryVM = new LeadershipQueryViewModel();
            SystemManagementVM = new SystemManagementViewModel();
            RawInventoryVM = new RawInventoryViewModel();
            CustomerManagementVM = new CustomerManagementViewModel();
            RepairProgressVM = new RepairProgressViewModel();
            InventoryQueryVM = new InventoryQueryViewModel();
            ShortageManagementVM = new ShortageManagementViewModel();
            InsuranceManagementVM = new InsuranceManagementViewModel();
            // 初始化派工处理ViewModel
            DispatchManagementVM = new DispatchManagementViewModel();
            
            // 初始化各模块的View
            DashboardView = new DashboardView() { DataContext = DashboardVM };
            BusinessReceptionView = new BusinessReceptionView() { DataContext = BusinessReceptionVM };
            VehicleMaintenanceView = new VehicleMaintenanceView() { DataContext = VehicleMaintenanceVM };
            MaintenanceQueryView = new MaintenanceQueryView() { DataContext = MaintenanceQueryVM };
            ProjectQueryView = new ProjectQueryView() { DataContext = ProjectQueryVM };
            FinancialSettlementView = new FinancialSettlementView() { DataContext = FinancialSettlementVM };
            SparePartsManagementView = new SparePartsManagementView() { DataContext = SparePartsManagementVM };
            WorkshopManagementView = new WorkshopManagementView() { DataContext = WorkshopManagementVM };
            LeadershipQueryView = new LeadershipQueryView() { DataContext = LeadershipQueryVM };
            SystemManagementView = new SystemManagementView() { DataContext = SystemManagementVM };
            RawInventoryView = new RawInventoryView() { DataContext = RawInventoryVM };
            CustomerManagementView = new CustomerManagementView() { DataContext = CustomerManagementVM };
            RepairProgressView = new RepairProgressView() { DataContext = RepairProgressVM };
            InventoryQueryView = new InventoryQueryView() { DataContext = InventoryQueryVM };
            ShortageManagementView = new ShortageManagementView() { DataContext = ShortageManagementVM };
            InsuranceManagementView = new InsuranceManagementView() { DataContext = InsuranceManagementVM };
            // 初始化派工处理View
            DispatchManagementView = new DispatchManagementView() { DataContext = DispatchManagementVM };
            
            // 初始化命令
            NavigateCommand = new RelayCommand(Navigate);
            LogoutCommand = new RelayCommand(Logout);

            // 初始化用户信息和时间
            CurrentUser = "管理员";
            CurrentTime = DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss");

            // 默认显示仪表盘
            CurrentView = DashboardView;

            // 每秒更新时间
            System.Windows.Threading.DispatcherTimer timer = new System.Windows.Threading.DispatcherTimer();
            timer.Interval = TimeSpan.FromSeconds(1);
            timer.Tick += Timer_Tick;
            timer.Start();
        }

        // 导航命令处理
        private void Navigate(object? parameter)
        {
            string moduleName = parameter as string ?? "";
            switch (moduleName)
            {
                case "Dashboard":
                    CurrentView = DashboardView;
                    CurrentModule = "Dashboard";
                    break;
                case "BusinessReception":
                    CurrentView = BusinessReceptionView;
                    CurrentModule = "BusinessReception";
                    break;
                case "VehicleMaintenance":
                    CurrentView = VehicleMaintenanceView;
                    CurrentModule = "VehicleMaintenance";
                    break;
                case "MaintenanceQuery":
                    CurrentView = MaintenanceQueryView;
                    CurrentModule = "MaintenanceQuery";
                    break;
                case "ProjectQuery":
                    CurrentView = ProjectQueryView;
                    CurrentModule = "ProjectQuery";
                    break;
                case "FinancialSettlement":
                    CurrentView = FinancialSettlementView;
                    CurrentModule = "FinancialSettlement";
                    break;
                case "SparePartsManagement":
                    CurrentView = SparePartsManagementView;
                    CurrentModule = "SparePartsManagement";
                    break;
                case "WorkshopManagement":
                    CurrentView = WorkshopManagementView;
                    CurrentModule = "WorkshopManagement";
                    break;
                case "LeadershipQuery":
                    CurrentView = LeadershipQueryView;
                    CurrentModule = "LeadershipQuery";
                    break;
                case "SystemManagement":
                    CurrentView = SystemManagementView;
                    CurrentModule = "SystemManagement";
                    break;
                case "RawInventory":
                    CurrentView = RawInventoryView;
                    CurrentModule = "RawInventory";
                    break;
                case "CustomerManagement":
                    CurrentView = CustomerManagementView;
                    CurrentModule = "CustomerManagement";
                    break;
                case "RepairProgress":
                    CurrentView = RepairProgressView;
                    CurrentModule = "RepairProgress";
                    break;
                case "InventoryQuery":
                    CurrentView = InventoryQueryView;
                    CurrentModule = "InventoryQuery";
                    break;
                case "ShortageManagement":
                    CurrentView = ShortageManagementView;
                    CurrentModule = "ShortageManagement";
                    break;
                case "InsuranceManagement":
                    CurrentView = InsuranceManagementView;
                    CurrentModule = "InsuranceManagement";
                    break;
                // 新增派工处理导航
                case "DispatchManagement":
                    CurrentView = DispatchManagementView;
                    CurrentModule = "DispatchManagement";
                    break;
                // 新增项目审核导航
                case "ProjectAudit":
                    CurrentView = WorkshopManagementView;
                    CurrentModule = "ProjectAudit";
                    break;
            }
        }

        // 退出登录命令处理
        private void Logout(object? parameter)
        {
            // 新建登录窗口
            LoginWindow loginWindow = new LoginWindow();

            // 关闭当前窗口
            foreach (System.Windows.Window window in System.Windows.Application.Current.Windows)
            {
                if (window is MainWindow)
                {
                    window.Close();
                    break;
                }
            }

            // 打开登录窗口
            loginWindow.Show();
        }

        // 更新时间
        private void Timer_Tick(object sender, EventArgs e)
        {
            CurrentTime = DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss");
        }
    }
}