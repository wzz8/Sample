using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
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
    public class ConsumptionPointsViewModel : ObservableObject
    {
        #region 字段
        private string _customerName = string.Empty;
        private string _licensePlate = string.Empty;
        private string _startDate = string.Empty;
        private string _endDate = string.Empty;
        private string _errorMessage = string.Empty;
        private Visibility _errorVisibility = Visibility.Hidden;
        private bool _isLoading = false;
        private ObservableCollection<ConsumptionPointRecord> _consumptionPointRecords = new ObservableCollection<ConsumptionPointRecord>();
        private ConsumptionPointRecord _selectedRecord = null;
        #endregion

        #region 属性
        /// <summary>
        /// 客户名称
        /// </summary>
        public string CustomerName
        {
            get { return _customerName; }
            set 
            { 
                if (_customerName != value)
                {
                    _customerName = value;
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
        /// 开始日期
        /// </summary>
        public string StartDate
        {
            get { return _startDate; }
            set 
            { 
                if (_startDate != value)
                {
                    _startDate = value;
                    OnPropertyChanged();
                }
            }
        }

        /// <summary>
        /// 结束日期
        /// </summary>
        public string EndDate
        {
            get { return _endDate; }
            set 
            { 
                if (_endDate != value)
                {
                    _endDate = value;
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
        /// 消费积分记录集合
        /// </summary>
        public ObservableCollection<ConsumptionPointRecord> ConsumptionPointRecords
        {
            get { return _consumptionPointRecords; }
            set 
            { 
                if (_consumptionPointRecords != value)
                {
                    _consumptionPointRecords = value;
                    OnPropertyChanged();
                }
            }
        }

        /// <summary>
        /// 选中的记录
        /// </summary>
        public ConsumptionPointRecord SelectedRecord
        {
            get { return _selectedRecord; }
            set 
            { 
                if (_selectedRecord != value)
                {
                    _selectedRecord = value;
                    OnPropertyChanged();
                }
            }
        }
        #endregion

        #region 命令
        /// <summary>
        /// 搜索记录命令
        /// </summary>
        public ICommand SearchRecordsCommand { get; set; }

        /// <summary>
        /// 刷新记录命令
        /// </summary>
        public ICommand RefreshRecordsCommand { get; set; }
        #endregion

        #region 构造函数
        /// <summary>
        /// 构造函数
        /// </summary>
        public ConsumptionPointsViewModel()
        {
            SearchRecordsCommand = new RelayCommand<object>(SearchRecords);
            RefreshRecordsCommand = new RelayCommand<object>(RefreshRecords);

            // 初始化日期为当前月份
            var now = DateTime.Now;
            StartDate = now.ToString("yyyy-MM-01");
            EndDate = now.ToString("yyyy-MM-dd");

            // 初始化模拟数据
            LoadMockData();
        }
        #endregion

        #region 方法
        /// <summary>
        /// 搜索记录
        /// </summary>
        private void SearchRecords(object? parameter)
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
        /// 刷新记录
        /// </summary>
        private void RefreshRecords(object? parameter)
        {
            IsLoading = true;
            ErrorVisibility = Visibility.Hidden;

            // 重置搜索条件
            CustomerName = string.Empty;
            LicensePlate = string.Empty;
            var now = DateTime.Now;
            StartDate = now.ToString("yyyy-MM-01");
            EndDate = now.ToString("yyyy-MM-dd");

            // 模拟刷新操作
            Task.Delay(1000).ContinueWith(t =>
            {
                // 模拟刷新结果
                LoadMockData();
                IsLoading = false;
            }, TaskScheduler.FromCurrentSynchronizationContext());
        }

        /// <summary>
        /// 加载模拟数据
        /// </summary>
        private void LoadMockData()
        {
            _consumptionPointRecords.Clear();

            // 添加模拟数据
            _consumptionPointRecords.Add(new ConsumptionPointRecord
            {
                Id = "1",
                CustomerName = "张三",
                LicensePlate = "京A12345",
                SettlementNo = "W20260101001",
                ConsumptionDate = "2026-01-01",
                PointsUsed = 1000,
                AmountConverted = 100,
                BalancePoints = 500,
                Operator = "管理员",
                Remarks = "车辆维修结算"
            });

            _consumptionPointRecords.Add(new ConsumptionPointRecord
            {
                Id = "2",
                CustomerName = "李四",
                LicensePlate = "沪B67890",
                SettlementNo = "W20260102002",
                ConsumptionDate = "2026-01-02",
                PointsUsed = 2000,
                AmountConverted = 200,
                BalancePoints = 1000,
                Operator = "管理员",
                Remarks = "汽车保养结算"
            });

            _consumptionPointRecords.Add(new ConsumptionPointRecord
            {
                Id = "3",
                CustomerName = "王五",
                LicensePlate = "粤C24680",
                SettlementNo = "W20260103003",
                ConsumptionDate = "2026-01-03",
                PointsUsed = 1500,
                AmountConverted = 150,
                BalancePoints = 800,
                Operator = "管理员",
                Remarks = "车辆维修结算"
            });

            _consumptionPointRecords.Add(new ConsumptionPointRecord
            {
                Id = "4",
                CustomerName = "赵六",
                LicensePlate = "川D13579",
                SettlementNo = "W20260104004",
                ConsumptionDate = "2026-01-04",
                PointsUsed = 800,
                AmountConverted = 80,
                BalancePoints = 1200,
                Operator = "管理员",
                Remarks = "汽车保养结算"
            });

            _consumptionPointRecords.Add(new ConsumptionPointRecord
            {
                Id = "5",
                CustomerName = "孙七",
                LicensePlate = "鲁E36925",
                SettlementNo = "W20260105005",
                ConsumptionDate = "2026-01-05",
                PointsUsed = 2500,
                AmountConverted = 250,
                BalancePoints = 500,
                Operator = "管理员",
                Remarks = "车辆维修结算"
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

    }

    #region 外部类
    /// <summary>
    /// 消费积分记录
    /// </summary>
    public class ConsumptionPointRecord
    {
        /// <summary>
        /// ID
        /// </summary>
        public string Id { get; set; }

        /// <summary>
        /// 客户名称
        /// </summary>
        public string CustomerName { get; set; }

        /// <summary>
        /// 车牌号
        /// </summary>
        public string LicensePlate { get; set; }

        /// <summary>
        /// 结算单号
        /// </summary>
        public string SettlementNo { get; set; }

        /// <summary>
        /// 消费日期
        /// </summary>
        public string ConsumptionDate { get; set; }

        /// <summary>
        /// 使用积分
        /// </summary>
        public int PointsUsed { get; set; }

        /// <summary>
        /// 兑换金额
        /// </summary>
        public decimal AmountConverted { get; set; }

        /// <summary>
        /// 剩余积分
        /// </summary>
        public int BalancePoints { get; set; }

        /// <summary>
        /// 操作人
        /// </summary>
        public string Operator { get; set; }

        /// <summary>
        /// 备注
        /// </summary>
        public string Remarks { get; set; }
    }
    #endregion
}