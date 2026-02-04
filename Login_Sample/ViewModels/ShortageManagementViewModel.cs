using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using Login_Sample.Data;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Windows.Input;

namespace Login_Sample.ViewModels
{
    /// <summary>
    /// 缺件管理视图模型
    /// </summary>
    public class ShortageManagementViewModel : ObservableObject
    {
        #region 查询条件属性

        // 客户姓名
        private string _customerName;
        public string CustomerName
        {
            get { return _customerName; }
            set { _customerName = value; OnPropertyChanged(); }
        }

        // 车牌号码
        private string _licensePlate;
        public string LicensePlate
        {
            get { return _licensePlate; }
            set { _licensePlate = value; OnPropertyChanged(); }
        }

        // 配件名称
        private string _partName;
        public string PartName
        {
            get { return _partName; }
            set { _partName = value; OnPropertyChanged(); }
        }

        // 配件编号
        private string _partNumber;
        public string PartNumber
        {
            get { return _partNumber; }
            set { _partNumber = value; OnPropertyChanged(); }
        }

        // 状态选项
        private List<string> _statusOptions;
        public List<string> StatusOptions
        {
            get { return _statusOptions; }
            set { _statusOptions = value; OnPropertyChanged(); }
        }

        // 选中状态
        private string _selectedStatus;
        public string SelectedStatus
        {
            get { return _selectedStatus; }
            set { _selectedStatus = value; OnPropertyChanged(); }
        }

        // 开始日期
        private DateTime? _startDate;
        public DateTime? StartDate
        {
            get { return _startDate; }
            set { _startDate = value; OnPropertyChanged(); }
        }

        // 结束日期
        private DateTime? _endDate;
        public DateTime? EndDate
        {
            get { return _endDate; }
            set { _endDate = value; OnPropertyChanged(); }
        }

        #endregion

        #region 列表和分页属性

        // 缺件列表
        private ObservableCollection<ShortageItem> _shortageItems;
        public ObservableCollection<ShortageItem> ShortageItems
        {
            get { return _shortageItems; }
            set { _shortageItems = value; OnPropertyChanged(); }
        }

        // 所有缺件数据（用于分页）
        private List<ShortageItem> _allShortageItems;

        // 选中的缺件
        private ShortageItem _selectedShortageItem;
        public ShortageItem SelectedShortageItem
        {
            get { return _selectedShortageItem; }
            set { _selectedShortageItem = value; OnPropertyChanged(); }
        }

        // 当前页码
        private int _currentPage = 1;
        public int CurrentPage
        {
            get { return _currentPage; }
            set { _currentPage = value; OnPropertyChanged(); OnPropertyChanged(nameof(CanGoPrevious)); OnPropertyChanged(nameof(CanGoNext)); }
        }

        // 每页显示数量
        private int _pageSize = 20;
        public int PageSize
        {
            get { return _pageSize; }
            set { _pageSize = value; OnPropertyChanged(); }
        }

        // 总记录数
        private int _totalItems;
        public int TotalItems
        {
            get { return _totalItems; }
            set { _totalItems = value; OnPropertyChanged(); OnPropertyChanged(nameof(TotalPages)); }
        }

        // 总页数
        public int TotalPages
        {
            get { return (int)Math.Ceiling((double)_totalItems / _pageSize); }
        }

        // 是否可以上一页
        public bool CanGoPrevious
        {
            get { return _currentPage > 1; }
        }

        // 是否可以下一页
        public bool CanGoNext
        {
            get { return _currentPage < TotalPages; }
        }

        #endregion

        #region 命令定义

        // 新增缺件命令
        public ICommand AddShortageCommand { get; set; }

        // 查询命令
        public ICommand SearchCommand { get; set; }

        // 重置命令
        public ICommand ResetCommand { get; set; }

        // 刷新命令
        public ICommand RefreshCommand { get; set; }

        // 上一页命令
        public ICommand PreviousPageCommand { get; set; }

        // 下一页命令
        public ICommand NextPageCommand { get; set; }

        // 通知客户命令
        public ICommand NotifyCustomerCommand { get; set; }

        // 处理缺件命令
        public ICommand ProcessShortageCommand { get; set; }

        #endregion

        #region 构造函数

        /// <summary>
        /// 构造函数
        /// </summary>
        public ShortageManagementViewModel()
        {
            // 初始化命令
            AddShortageCommand = new RelayCommand<object>(ExecuteAddShortage);
            SearchCommand = new RelayCommand<object>(ExecuteSearch);
            ResetCommand = new RelayCommand<object>(ExecuteReset);
            RefreshCommand = new RelayCommand<object>(ExecuteRefresh);
            PreviousPageCommand = new RelayCommand<object>(ExecutePreviousPage);
            NextPageCommand = new RelayCommand<object>(ExecuteNextPage);
            NotifyCustomerCommand = new RelayCommand<object>(ExecuteNotifyCustomer);
            ProcessShortageCommand = new RelayCommand<object>(ExecuteProcessShortage);

            // 初始化状态选项
            StatusOptions = new List<string> { "全部", "已确认", "已订货", "已到货", "已通知", "已处理" };
            SelectedStatus = "全部";

            // 初始化日期范围（默认显示最近30天）
            StartDate = DateTime.Now.AddDays(-30);
            EndDate = DateTime.Now;

            // 加载模拟数据
            LoadMockData();

            // 初始查询
            ExecuteSearch(null);
        }

        #endregion

        #region 命令执行方法

        /// <summary>
        /// 执行新增缺件
        /// </summary>
        /// <param name="parameter"></param>
        private void ExecuteAddShortage(object parameter)
        {
            // 这里可以打开新增缺件窗口
            // 目前只是模拟新增一条数据
            var newShortage = new ShortageItem
            {
                ShortageId = _allShortageItems.Max(s => s.ShortageId) + 1,
                CustomerName = "新客户",
                PhoneNumber = "13800138000",
                VehicleBrand = "大众",
                VehicleModel = "帕萨特",
                LicensePlate = "沪A12345",
                PartName = "新配件",
                PartNumber = "PN-123456",
                Quantity = 1,
                Reason = "维修缺件",
                RegisteredBy = "前台人员",
                RegisteredTime = DateTime.Now,
                Status = "已确认",
                Remarks = "新登记的缺件"
            };

            _allShortageItems.Insert(0, newShortage);
            ExecuteSearch(null);
        }

        /// <summary>
        /// 执行查询
        /// </summary>
        /// <param name="parameter"></param>
        private void ExecuteSearch(object parameter)
        {
            var filteredItems = _allShortageItems.AsQueryable();

            // 根据客户姓名过滤
            if (!string.IsNullOrWhiteSpace(CustomerName))
            {
                filteredItems = filteredItems.Where(s => s.CustomerName.Contains(CustomerName));
            }

            // 根据车牌号码过滤
            if (!string.IsNullOrWhiteSpace(LicensePlate))
            {
                filteredItems = filteredItems.Where(s => s.LicensePlate.Contains(LicensePlate));
            }

            // 根据配件名称过滤
            if (!string.IsNullOrWhiteSpace(PartName))
            {
                filteredItems = filteredItems.Where(s => s.PartName.Contains(PartName));
            }

            // 根据配件编号过滤
            if (!string.IsNullOrWhiteSpace(PartNumber))
            {
                filteredItems = filteredItems.Where(s => s.PartNumber.Contains(PartNumber));
            }

            // 根据状态过滤
            if (SelectedStatus != "全部")
            {
                filteredItems = filteredItems.Where(s => s.Status == SelectedStatus);
            }

            // 根据登记日期过滤
            if (StartDate.HasValue)
            {
                filteredItems = filteredItems.Where(s => s.RegisteredTime >= StartDate.Value.Date);
            }
            if (EndDate.HasValue)
            {
                filteredItems = filteredItems.Where(s => s.RegisteredTime <= EndDate.Value.Date.AddDays(1));
            }

            // 重新计算分页
            _currentPage = 1;
            _totalItems = filteredItems.Count();

            // 分页查询
            var pagedItems = filteredItems.OrderByDescending(s => s.RegisteredTime)
                                         .Skip((_currentPage - 1) * _pageSize)
                                         .Take(_pageSize)
                                         .ToList();

            ShortageItems = new ObservableCollection<ShortageItem>(pagedItems);
        }

        /// <summary>
        /// 执行重置
        /// </summary>
        /// <param name="parameter"></param>
        private void ExecuteReset(object parameter)
        {
            CustomerName = string.Empty;
            LicensePlate = string.Empty;
            PartName = string.Empty;
            PartNumber = string.Empty;
            SelectedStatus = "全部";
            StartDate = DateTime.Now.AddDays(-30);
            EndDate = DateTime.Now;

            ExecuteSearch(null);
        }

        /// <summary>
        /// 执行刷新
        /// </summary>
        /// <param name="parameter"></param>
        private void ExecuteRefresh(object parameter)
        {
            LoadMockData();
            ExecuteSearch(null);
        }

        /// <summary>
        /// 执行上一页
        /// </summary>
        /// <param name="parameter"></param>
        private void ExecutePreviousPage(object parameter)
        {
            if (CanGoPrevious)
            {
                CurrentPage--;
                ExecuteSearch(null);
            }
        }

        /// <summary>
        /// 执行下一页
        /// </summary>
        /// <param name="parameter"></param>
        private void ExecuteNextPage(object parameter)
        {
            if (CanGoNext)
            {
                CurrentPage++;
                ExecuteSearch(null);
            }
        }

        /// <summary>
        /// 执行通知客户
        /// </summary>
        /// <param name="parameter"></param>
        private void ExecuteNotifyCustomer(object parameter)
        {
            if (SelectedShortageItem != null && SelectedShortageItem.Status == "已到货")
            {
                SelectedShortageItem.Status = "已通知";
                SelectedShortageItem.NotificationDate = DateTime.Now;
                SelectedShortageItem.Remarks = $"{SelectedShortageItem.Remarks}" + Environment.NewLine + $"{DateTime.Now:yyyy-MM-dd HH:mm} 已通知客户";
                OnPropertyChanged(nameof(ShortageItems));
            }
        }

        /// <summary>
        /// 执行处理缺件
        /// </summary>
        /// <param name="parameter"></param>
        private void ExecuteProcessShortage(object parameter)
        {
            if (SelectedShortageItem != null && SelectedShortageItem.Status == "已通知")
            {
                SelectedShortageItem.Status = "已处理";
                SelectedShortageItem.ProcessedDate = DateTime.Now;
                SelectedShortageItem.Remarks = $"{SelectedShortageItem.Remarks}" + Environment.NewLine + $"{DateTime.Now:yyyy-MM-dd HH:mm} 已处理缺件";
                OnPropertyChanged(nameof(ShortageItems));
            }
        }

        #endregion

        #region 数据加载方法

        /// <summary>
        /// 加载模拟数据
        /// </summary>
        private void LoadMockData()
        {
            _allShortageItems = new List<ShortageItem>
            {
                new ShortageItem
                {
                    ShortageId = 1,
                    CustomerName = "张三",
                    PhoneNumber = "13800138001",
                    VehicleBrand = "大众",
                    VehicleModel = "途观",
                    LicensePlate = "沪A12345",
                    PartName = "机油滤清器",
                    PartNumber = "PN-001",
                    Quantity = 1,
                    Reason = "维修缺件",
                    RegisteredBy = "前台小王",
                    RegisteredTime = DateTime.Now.AddDays(-5),
                    Status = "已确认",
                    Remarks = "常规保养缺件"
                },
                new ShortageItem
                {
                    ShortageId = 2,
                    CustomerName = "李四",
                    PhoneNumber = "13800138002",
                    VehicleBrand = "丰田",
                    VehicleModel = "凯美瑞",
                    LicensePlate = "沪A23456",
                    PartName = "空气滤清器",
                    PartNumber = "PN-002",
                    Quantity = 1,
                    Reason = "维修缺件",
                    RegisteredBy = "前台小李",
                    RegisteredTime = DateTime.Now.AddDays(-4),
                    OrderNumber = "PO-12345",
                    PurchaseDate = DateTime.Now.AddDays(-2),
                    Status = "已订货",
                    Remarks = "发动机保养缺件"
                },
                new ShortageItem
                {
                    ShortageId = 3,
                    CustomerName = "王五",
                    PhoneNumber = "13800138003",
                    VehicleBrand = "本田",
                    VehicleModel = "雅阁",
                    LicensePlate = "沪A34567",
                    PartName = "刹车片",
                    PartNumber = "PN-003",
                    Quantity = 2,
                    Reason = "维修缺件",
                    RegisteredBy = "前台小张",
                    RegisteredTime = DateTime.Now.AddDays(-3),
                    OrderNumber = "PO-12346",
                    PurchaseDate = DateTime.Now.AddDays(-2),
                    ArrivalDate = DateTime.Now.AddDays(-1),
                    Status = "已到货",
                    Remarks = "制动系统维修缺件"
                },
                new ShortageItem
                {
                    ShortageId = 4,
                    CustomerName = "赵六",
                    PhoneNumber = "13800138004",
                    VehicleBrand = "宝马",
                    VehicleModel = "X5",
                    LicensePlate = "沪A45678",
                    PartName = "机油",
                    PartNumber = "PN-004",
                    Quantity = 1,
                    Reason = "维修缺件",
                    RegisteredBy = "前台小王",
                    RegisteredTime = DateTime.Now.AddDays(-2),
                    OrderNumber = "PO-12347",
                    PurchaseDate = DateTime.Now.AddDays(-1),
                    ArrivalDate = DateTime.Now,
                    NotificationDate = DateTime.Now.AddHours(-1),
                    Status = "已通知",
                    Remarks = "常规保养缺件\n2023-12-10 10:30 已到货\n2023-12-10 11:30 已通知客户"
                },
                new ShortageItem
                {
                    ShortageId = 5,
                    CustomerName = "钱七",
                    PhoneNumber = "13800138005",
                    VehicleBrand = "奔驰",
                    VehicleModel = "C级",
                    LicensePlate = "沪A56789",
                    PartName = "机滤",
                    PartNumber = "PN-005",
                    Quantity = 1,
                    Reason = "维修缺件",
                    RegisteredBy = "前台小李",
                    RegisteredTime = DateTime.Now.AddDays(-1),
                    OrderNumber = "PO-12348",
                    PurchaseDate = DateTime.Now.AddHours(-6),
                    ArrivalDate = DateTime.Now.AddHours(-3),
                    NotificationDate = DateTime.Now.AddHours(-2),
                    ProcessedDate = DateTime.Now.AddHours(-1),
                    Status = "已处理",
                    Remarks = "发动机维修缺件\n2023-12-10 08:00 已到货\n2023-12-10 09:00 已通知客户\n2023-12-10 10:00 已处理缺件"
                },
                new ShortageItem
                {
                    ShortageId = 6,
                    CustomerName = "孙八",
                    PhoneNumber = "13800138006",
                    VehicleBrand = "奥迪",
                    VehicleModel = "A6L",
                    LicensePlate = "沪A67890",
                    PartName = "变速箱油",
                    PartNumber = "PN-006",
                    Quantity = 1,
                    Reason = "维修缺件",
                    RegisteredBy = "前台小张",
                    RegisteredTime = DateTime.Now.AddHours(-5),
                    Status = "已确认",
                    Remarks = "变速箱保养缺件"
                }
            };
        }

        #endregion
    }
}