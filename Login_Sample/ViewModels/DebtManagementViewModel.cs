using System;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Input;
using System.Windows.Threading;

namespace Login_Sample.ViewModels
{
    public class DebtManagementViewModel : INotifyPropertyChanged
    {
        #region 字段
        private string _ownerName = string.Empty;
        private string _licensePlate = string.Empty;
        private string _委托书No = string.Empty;
        private string _errorMessage = string.Empty;
        private Visibility _errorVisibility = Visibility.Hidden;
        private bool _isLoading = false;
        private ObservableCollection<DebtVehicle> _debtVehicles = new ObservableCollection<DebtVehicle>();
        private DebtVehicle _selectedVehicle = null;
        #endregion

        #region 属性
        /// <summary>
        /// 车主
        /// </summary>
        public string OwnerName
        {
            get { return _ownerName; }
            set 
            { 
                if (_ownerName != value)
                {
                    _ownerName = value;
                    OnPropertyChanged();
                }
            }
        }

        /// <summary>
        /// 车牌号
        /// </summary>
        public string LicensePlate
        {
            get { return _licensePlate; }
            set 
            { 
                if (_licensePlate != value)
                {
                    _licensePlate = value;
                    OnPropertyChanged();
                }
            }
        }

        /// <summary>
        /// 委托书号
        /// </summary>
        public string 委托书No
        {
            get { return _委托书No; }
            set 
            { 
                if (_委托书No != value)
                {
                    _委托书No = value;
                    OnPropertyChanged();
                }
            }
        }

        /// <summary>
        /// 错误信息
        /// </summary>
        public string ErrorMessage
        {
            get { return _errorMessage; }
            set 
            { 
                if (_errorMessage != value)
                {
                    _errorMessage = value;
                    OnPropertyChanged();
                }
            }
        }

        /// <summary>
        /// 错误提示可见性
        /// </summary>
        public Visibility ErrorVisibility
        {
            get { return _errorVisibility; }
            set 
            { 
                if (_errorVisibility != value)
                {
                    _errorVisibility = value;
                    OnPropertyChanged();
                }
            }
        }

        /// <summary>
        /// 是否正在加载
        /// </summary>
        public bool IsLoading
        {
            get { return _isLoading; }
            set 
            { 
                if (_isLoading != value)
                {
                    _isLoading = value;
                    OnPropertyChanged();
                }
            }
        }

        /// <summary>
        /// 欠账车辆集合
        /// </summary>
        public ObservableCollection<DebtVehicle> DebtVehicles
        {
            get { return _debtVehicles; }
            set 
            { 
                if (_debtVehicles != value)
                {
                    _debtVehicles = value;
                    OnPropertyChanged();
                }
            }
        }

        /// <summary>
        /// 选中的车辆
        /// </summary>
        public DebtVehicle SelectedVehicle
        {
            get { return _selectedVehicle; }
            set 
            { 
                if (_selectedVehicle != value)
                {
                    _selectedVehicle = value;
                    OnPropertyChanged();
                }
            }
        }
        #endregion

        #region 命令
        /// <summary>
        /// 搜索车辆命令
        /// </summary>
        public ICommand SearchVehiclesCommand { get; set; }

        /// <summary>
        /// 刷新车辆命令
        /// </summary>
        public ICommand RefreshVehiclesCommand { get; set; }

        /// <summary>
        /// 标记已处理命令
        /// </summary>
        public ICommand MarkAsProcessedCommand { get; set; }
        #endregion

        #region 构造函数
        /// <summary>
        /// 构造函数
        /// </summary>
        public DebtManagementViewModel()
        {
            SearchVehiclesCommand = new RelayCommand(SearchVehicles);
            RefreshVehiclesCommand = new RelayCommand(RefreshVehicles);
            MarkAsProcessedCommand = new RelayCommand(MarkAsProcessed);

            // 初始化模拟数据
            LoadMockData();
        }
        #endregion

        #region 方法
        /// <summary>
        /// 搜索车辆
        /// </summary>
        private void SearchVehicles(object? parameter)
        {
            IsLoading = true;
            ErrorVisibility = Visibility.Hidden;

            // 模拟搜索操作
            Task.Delay(1000).ContinueWith(t =>
            {
                // 模拟搜索结果
                LoadMockData();
                IsLoading = false;
            }, TaskScheduler.FromCurrentSynchronizationContext());
        }

        /// <summary>
        /// 刷新车辆
        /// </summary>
        private void RefreshVehicles(object? parameter)
        {
            IsLoading = true;
            ErrorVisibility = Visibility.Hidden;

            // 重置搜索条件
            OwnerName = string.Empty;
            LicensePlate = string.Empty;
            委托书No = string.Empty;

            // 模拟刷新操作
            Task.Delay(1000).ContinueWith(t =>
            {
                // 模拟刷新结果
                LoadMockData();
                IsLoading = false;
            }, TaskScheduler.FromCurrentSynchronizationContext());
        }

        /// <summary>
        /// 标记已处理
        /// </summary>
        private void MarkAsProcessed(object? parameter)
        {
            if (SelectedVehicle == null)
            {
                ShowError("请选择要处理的欠账车辆");
                return;
            }

            // 确认处理操作
            var result = MessageBox.Show($"确定要标记车辆 {SelectedVehicle.LicensePlate} 为已处理吗？", "确认", MessageBoxButton.YesNo, MessageBoxImage.Question);
            if (result != MessageBoxResult.Yes)
            {
                return;
            }

            IsLoading = true;
            ErrorVisibility = Visibility.Hidden;

            // 模拟处理操作
            Task.Delay(1000).ContinueWith(t =>
            {
                // 模拟处理成功
                SelectedVehicle.Status = "已处理";
                SelectedVehicle.ProcessedDate = DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss");
                SelectedVehicle.ProcessedBy = "管理员";
                
                MessageBox.Show("标记成功", "提示", MessageBoxButton.OK, MessageBoxImage.Information);
                IsLoading = false;
            }, TaskScheduler.FromCurrentSynchronizationContext());
        }

        /// <summary>
        /// 加载模拟数据
        /// </summary>
        private void LoadMockData()
        {
            _debtVehicles.Clear();

            // 添加模拟数据
            _debtVehicles.Add(new DebtVehicle
            {
                Id = "1",
                OwnerName = "张三",
                LicensePlate = "京A12345",
                委托书No = "W20260101001",
                VehicleType = "小轿车",
                DebtAmount = 1200,
                DebtDate = "2026-01-01",
                LastReminderDate = "2026-01-10",
                Status = "未处理",
                Remarks = "汽车保养维修"
            });

            _debtVehicles.Add(new DebtVehicle
            {
                Id = "2",
                OwnerName = "李四",
                LicensePlate = "沪B67890",
                委托书No = "W20260102002",
                VehicleType = "SUV",
                DebtAmount = 3500,
                DebtDate = "2026-01-02",
                LastReminderDate = "2026-01-12",
                Status = "未处理",
                Remarks = "汽车维修"
            });

            _debtVehicles.Add(new DebtVehicle
            {
                Id = "3",
                OwnerName = "王五",
                LicensePlate = "粤C24680",
                委托书No = "W20260103003",
                VehicleType = "小轿车",
                DebtAmount = 2800,
                DebtDate = "2026-01-03",
                LastReminderDate = "2026-01-15",
                Status = "未处理",
                Remarks = "汽车保养"
            });

            _debtVehicles.Add(new DebtVehicle
            {
                Id = "4",
                OwnerName = "赵六",
                LicensePlate = "川D13579",
                委托书No = "W20260104004",
                VehicleType = "面包车",
                DebtAmount = 1800,
                DebtDate = "2026-01-04",
                LastReminderDate = "2026-01-18",
                Status = "未处理",
                Remarks = "汽车维修"
            });

            _debtVehicles.Add(new DebtVehicle
            {
                Id = "5",
                OwnerName = "孙七",
                LicensePlate = "鲁E36925",
                委托书No = "W20260105005",
                VehicleType = "SUV",
                DebtAmount = 4200,
                DebtDate = "2026-01-05",
                LastReminderDate = "2026-01-20",
                Status = "未处理",
                Remarks = "汽车大修"
            });
        }

        /// <summary>
        /// 显示错误信息
        /// </summary>
        /// <param name="message">错误信息</param>
        private void ShowError(string message)
        {
            ErrorMessage = message;
            ErrorVisibility = Visibility.Visible;
        }
        #endregion

        #region INotifyPropertyChanged 实现
        public event PropertyChangedEventHandler? PropertyChanged;

        protected virtual void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
        #endregion
    }

    #region 外部类
    /// <summary>
    /// 欠账车辆
    /// </summary>
    public class DebtVehicle
    {
        /// <summary>
        /// ID
        /// </summary>
        public string Id { get; set; }

        /// <summary>
        /// 车主
        /// </summary>
        public string OwnerName { get; set; }

        /// <summary>
        /// 车牌号
        /// </summary>
        public string LicensePlate { get; set; }

        /// <summary>
        /// 委托书号
        /// </summary>
        public string 委托书No { get; set; }

        /// <summary>
        /// 车辆类型
        /// </summary>
        public string VehicleType { get; set; }

        /// <summary>
        /// 欠账金额
        /// </summary>
        public decimal DebtAmount { get; set; }

        /// <summary>
        /// 欠账日期
        /// </summary>
        public string DebtDate { get; set; }

        /// <summary>
        /// 最后提醒日期
        /// </summary>
        public string LastReminderDate { get; set; }

        /// <summary>
        /// 状态
        /// </summary>
        public string Status { get; set; }

        /// <summary>
        /// 处理日期
        /// </summary>
        public string ProcessedDate { get; set; }

        /// <summary>
        /// 处理人
        /// </summary>
        public string ProcessedBy { get; set; }

        /// <summary>
        /// 备注
        /// </summary>
        public string Remarks { get; set; }
    }
    #endregion
}