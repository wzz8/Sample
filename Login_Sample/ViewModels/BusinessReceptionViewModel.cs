using System.ComponentModel;
using System.Runtime.CompilerServices;
using System.Windows.Input;
using MaterialDesignThemes.Wpf;

namespace Login_Sample.ViewModels
{
    public class BusinessReceptionViewModel : INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler? PropertyChanged;

        private void OnPropertyChanged([CallerMemberName] string? propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }

        // 统计属性
        private int _totalCustomers;
        public int TotalCustomers
        {
            get { return _totalCustomers; }
            set { _totalCustomers = value; OnPropertyChanged(); }
        }

        private int _todayAppointments;
        public int TodayAppointments
        {
            get { return _todayAppointments; }
            set { _todayAppointments = value; OnPropertyChanged(); }
        }

        private int _pendingVehicles;
        public int PendingVehicles
        {
            get { return _pendingVehicles; }
            set { _pendingVehicles = value; OnPropertyChanged(); }
        }

        private int _completedServices;
        public int CompletedServices
        {
            get { return _completedServices; }
            set { _completedServices = value; OnPropertyChanged(); }
        }

        // 子功能状态
        private bool _customerAppointmentEnabled = true;
        public bool CustomerAppointmentEnabled
        {
            get { return _customerAppointmentEnabled; }
            set { _customerAppointmentEnabled = value; OnPropertyChanged(); }
        }

        private bool _vehicleReceptionEnabled = true;
        public bool VehicleReceptionEnabled
        {
            get { return _vehicleReceptionEnabled; }
            set { _vehicleReceptionEnabled = value; OnPropertyChanged(); }
        }

        private bool _repairArrangementEnabled = true;
        public bool RepairArrangementEnabled
        {
            get { return _repairArrangementEnabled; }
            set { _repairArrangementEnabled = value; OnPropertyChanged(); }
        }

        private bool _customerFeedbackEnabled = true;
        public bool CustomerFeedbackEnabled
        {
            get { return _customerFeedbackEnabled; }
            set { _customerFeedbackEnabled = value; OnPropertyChanged(); }
        }

        // 构造函数
        public BusinessReceptionViewModel()
        {
            // 初始化数据
            InitializeDashboardData();
        }

        // 初始化仪表盘数据
        private void InitializeDashboardData()
        {
            // 模拟数据 - 实际应用中应从数据库获取
            TotalCustomers = 1520;
            TodayAppointments = 28;
            PendingVehicles = 12;
            CompletedServices = 45;
        }
    }
}