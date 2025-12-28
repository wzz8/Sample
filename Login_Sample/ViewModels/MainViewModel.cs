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
        // 新增项目审查视图
        public ProjectAuditView ProjectAuditView { get; set; }
        // 新增备件入库视图
        public SparePartsInboundView SparePartsInboundView { get; set; }
        // 新增备件出库视图
        public SparePartsOutboundView SparePartsOutboundView { get; set; }
        // 新增配件销售视图
        public SparePartsSaleView SparePartsSaleView { get; set; }
        // 新增库存管理视图
        public InventoryManagementView InventoryManagementView { get; set; }
        // 新增备件目录视图
        public SparePartsCatalogView SparePartsCatalogView { get; set; }
        // 新增库存盘点视图
        public InventoryCheckView InventoryCheckView { get; set; }
        // 新增备件借用视图
        public SparePartsBorrowView SparePartsBorrowView { get; set; }
        // 新增备件报废视图
        public SparePartsScrapView SparePartsScrapView { get; set; }
        // 新增备件供货商视图
        public SparePartsSupplierView SparePartsSupplierView { get; set; }
        // 新增车辆结算视图
        public VehicleSettlementView VehicleSettlementView { get; set; }
        // 新增服务预约视图
        public AppointmentManagementView AppointmentManagementView { get; set; }

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
        // 初始化派工处理ViewModel
        public DispatchManagementViewModel DispatchManagementVM { get; set; }
        // 初始化项目审查ViewModel
        public ProjectAuditViewModel ProjectAuditVM { get; set; }
        // 初始化备件入库ViewModel
        public SparePartsInboundViewModel SparePartsInboundVM { get; set; }
        // 初始化备件出库ViewModel
        public SparePartsOutboundViewModel SparePartsOutboundVM { get; set; }
        // 初始化配件销售ViewModel
        public SparePartsSaleViewModel SparePartsSaleVM { get; set; }
        // 初始化库存管理ViewModel
        public InventoryManagementViewModel InventoryManagementVM { get; set; }
        // 初始化备件目录ViewModel
        public SparePartsCatalogViewModel SparePartsCatalogVM { get; set; }
        // 初始化库存盘点ViewModel
        public InventoryCheckViewModel InventoryCheckVM { get; set; }
        // 初始化车辆结算ViewModel
        public VehicleSettlementViewModel VehicleSettlementVM { get; set; }
        // 初始化备件借用ViewModel
        public SparePartsBorrowViewModel SparePartsBorrowVM { get; set; }
        // 初始化备件报废ViewModel
        public SparePartsScrapViewModel SparePartsScrapVM { get; set; }
        // 初始化备件供货商ViewModel
        public SparePartsSupplierViewModel SparePartsSupplierVM { get; set; }
        // 新增服务预约ViewModel
        public AppointmentManagementViewModel AppointmentManagementVM { get; set; }

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
            // 初始化项目审查ViewModel
            ProjectAuditVM = new ProjectAuditViewModel();
            // 初始化备件入库ViewModel
            SparePartsInboundVM = new SparePartsInboundViewModel();
            // 初始化备件出库ViewModel
            SparePartsOutboundVM = new SparePartsOutboundViewModel();
            // 初始化配件销售ViewModel
            SparePartsSaleVM = new SparePartsSaleViewModel();
            // 初始化库存管理ViewModel
            InventoryManagementVM = new InventoryManagementViewModel();
            // 初始化备件目录ViewModel
            SparePartsCatalogVM = new SparePartsCatalogViewModel();
            // 初始化库存盘点ViewModel
            InventoryCheckVM = new InventoryCheckViewModel();
            // 初始化备件借用ViewModel
            SparePartsBorrowVM = new SparePartsBorrowViewModel();
            // 初始化备件报废ViewModel
            SparePartsScrapVM = new SparePartsScrapViewModel();
            // 初始化备件供货商ViewModel
            SparePartsSupplierVM = new SparePartsSupplierViewModel();
            // 初始化服务预约ViewModel
            AppointmentManagementVM = new AppointmentManagementViewModel();

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
            // 初始化项目审查View
            ProjectAuditView = new ProjectAuditView() { DataContext = ProjectAuditVM };
            // 初始化备件入库View
            SparePartsInboundView = new SparePartsInboundView() { DataContext = SparePartsInboundVM };
            // 初始化备件出库View
            SparePartsOutboundView = new SparePartsOutboundView() { DataContext = SparePartsOutboundVM };
            // 初始化配件销售View
            SparePartsSaleView = new SparePartsSaleView() { DataContext = SparePartsSaleVM };
            // 初始化库存管理View
            InventoryManagementView = new InventoryManagementView() { DataContext = InventoryManagementVM };
            // 初始化备件目录View
            SparePartsCatalogView = new SparePartsCatalogView() { DataContext = SparePartsCatalogVM };
            // 初始化库存盘点View
            InventoryCheckView = new InventoryCheckView() { DataContext = InventoryCheckVM };
            // 初始化备件借用View
            SparePartsBorrowView = new SparePartsBorrowView() { DataContext = SparePartsBorrowVM };
            // 初始化备件报废View
            SparePartsScrapView = new SparePartsScrapView() { DataContext = SparePartsScrapVM };
            // 初始化备件供货商View
            SparePartsSupplierView = new SparePartsSupplierView() { DataContext = SparePartsSupplierVM };
            // 初始化车辆结算ViewModel
            VehicleSettlementVM = new VehicleSettlementViewModel();
            // 初始化车辆结算View
            VehicleSettlementView = new VehicleSettlementView() { DataContext = VehicleSettlementVM };
            // 初始化服务预约View
            AppointmentManagementView = new AppointmentManagementView() { DataContext = AppointmentManagementVM };

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
                case "AppointmentManagement":
                    CurrentView = AppointmentManagementView;
                    CurrentModule = "AppointmentManagement";
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
                case "VehicleSettlement":
                    CurrentView = VehicleSettlementView;
                    CurrentModule = "VehicleSettlement";
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
                // 业务接待子功能导航
                case "CustomerAppointment":
                    CurrentView = AppointmentManagementView;
                    CurrentModule = "AppointmentManagement";
                    break;
                case "VehicleReception":
                    CurrentView = BusinessReceptionView;
                    CurrentModule = "BusinessReception";
                    break;
                case "RepairArrangement":
                    CurrentView = VehicleMaintenanceView;
                    CurrentModule = "VehicleMaintenance";
                    break;
                case "CustomerFeedback":
                    CurrentView = CustomerManagementView;
                    CurrentModule = "CustomerManagement";
                    break;
                case "CompletedServices":
                    CurrentView = MaintenanceQueryView;
                    CurrentModule = "MaintenanceQuery";
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
                    CurrentView = ProjectAuditView;
                    CurrentModule = "ProjectAudit";
                    break;
                // 新增备件入库导航
                case "SparePartsInbound":
                    CurrentView = SparePartsInboundView;
                    CurrentModule = "SparePartsInbound";
                    break;
                // 新增备件出库导航
                case "SparePartsOutbound":
                    CurrentView = SparePartsOutboundView;
                    CurrentModule = "SparePartsOutbound";
                    break;
                // 新增配件销售导航
                case "SparePartsSale":
                    CurrentView = SparePartsSaleView;
                    CurrentModule = "SparePartsSale";
                    break;
                // 新增库存管理导航
                case "InventoryManagement":
                    CurrentView = InventoryManagementView;
                    CurrentModule = "InventoryManagement";
                    break;
                // 新增备件目录导航
                case "SparePartsCatalog":
                    CurrentView = SparePartsCatalogView;
                    CurrentModule = moduleName;
                    break;
                case "InventoryCheck":
                    CurrentView = InventoryCheckView;
                    CurrentModule = "InventoryCheck";
                    break;
                // 新增备件借用导航
                case "SparePartsBorrow":
                    CurrentView = SparePartsBorrowView;
                    CurrentModule = "SparePartsBorrow";
                    break;
                // 新增备件报废导航
                case "SparePartsScrap":
                    CurrentView = SparePartsScrapView;
                    CurrentModule = "SparePartsScrap";
                    break;
                // 新增备件供货商导航
                case "SparePartsSupplier":
                    CurrentView = SparePartsSupplierView;
                    CurrentModule = "SparePartsSupplier";
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