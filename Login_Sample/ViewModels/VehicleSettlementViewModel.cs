using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Windows.Input;
using System;
using Login_Sample.Data;
using Microsoft.EntityFrameworkCore;

namespace Login_Sample.ViewModels
{
    public class VehicleSettlementViewModel : INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler PropertyChanged;

        private void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }

        private readonly ApplicationDbContext _context;

        // 搜索条件
        private string _licensePlate;
        public string LicensePlate
        {
            get { return _licensePlate; }
            set { _licensePlate = value; OnPropertyChanged(); }
        }

        private string _customerName;
        public string CustomerName
        {
            get { return _customerName; }
            set { _customerName = value; OnPropertyChanged(); }
        }

        private string _orderNumber;
        public string OrderNumber
        {
            get { return _orderNumber; }
            set { _orderNumber = value; OnPropertyChanged(); }
        }

        private string _orderStatus;
        public string OrderStatus
        {
            get { return _orderStatus; }
            set { _orderStatus = value; OnPropertyChanged(); }
        }

        private DateTime? _startDate;
        public DateTime? StartDate
        {
            get { return _startDate; }
            set { _startDate = value; OnPropertyChanged(); }
        }

        private DateTime? _endDate;
        public DateTime? EndDate
        {
            get { return _endDate; }
            set { _endDate = value; OnPropertyChanged(); }
        }

        // 结算列表数据
        private ObservableCollection<SettlementItemViewModel> _settlementItems;
        public ObservableCollection<SettlementItemViewModel> SettlementItems
        {
            get { return _settlementItems; }
            set { _settlementItems = value; OnPropertyChanged(); }
        }

        // 选中的结算项
        private SettlementItemViewModel _selectedSettlementItem;
        public SettlementItemViewModel SelectedSettlementItem
        {
            get { return _selectedSettlementItem; }
            set { _selectedSettlementItem = value; OnPropertyChanged(); }
        }

        // 状态选项
        public List<string> StatusOptions { get; set; }

        // 命令
        public ICommand SearchCommand { get; }
        public ICommand ResetCommand { get; }
        public ICommand SettleCommand { get; }

        // 标题
        private string _settlementTitle = "车辆结算管理";
        public string SettlementTitle
        {
            get { return _settlementTitle; }
            set { _settlementTitle = value; OnPropertyChanged(); }
        }

        // 构造函数
        public VehicleSettlementViewModel()
        {
            _context = new ApplicationDbContext();
            
            // 初始化命令
            SearchCommand = new RelayCommand(SearchSettlements);
            ResetCommand = new RelayCommand(ResetSearch);
            SettleCommand = new RelayCommand(SettleSelectedItem, CanSettle);

            // 初始化状态选项
            StatusOptions = new List<string> { "全部", "待处理", "维修中", "已完工", "已结算" };
            OrderStatus = "已完工";

            // 初始化搜索条件
            ResetSearch(null);

            // 加载默认数据（已完工待结算）
            LoadSettlementData();
        }

        // 加载结算数据
        private void LoadSettlementData()
        {
            try
            {
                var query = _context.WorkOrders
                    .Include(w => w.Customer)
                    .Include(w => w.Vehicle)
                    .Where(w => w.Status == "已完工");

                var settlementItems = query.Select(w => new SettlementItemViewModel
                {
                    Id = w.Id,
                    OrderNumber = w.OrderNumber,
                    LicensePlate = w.Vehicle.LicensePlate,
                    CustomerName = w.Customer.Name,
                    CustomerPhone = w.Customer.Phone,
                    VehicleBrand = w.Vehicle.VehicleBrand,
                    VehicleModel = w.Vehicle.VehicleModel,
                    ReceptionDate = w.ReceptionDate,
                    CompletedDate = w.CompletedDate ?? DateTime.SpecifyKind(DateTime.Now, DateTimeKind.Utc),
                    Status = w.Status,
                    TotalAmount = w.TotalAmount,
                    ProblemDescription = w.ProblemDescription
                }).ToList();

                SettlementItems = new ObservableCollection<SettlementItemViewModel>(settlementItems);
            }
            catch (Exception ex)
            {
                // 处理异常
                System.Windows.MessageBox.Show("加载数据失败: " + ex.Message, "错误", System.Windows.MessageBoxButton.OK, System.Windows.MessageBoxImage.Error);
            }
        }

        // 搜索结算记录
        private void SearchSettlements(object obj)
        {
            try
            {
                var query = _context.WorkOrders
                    .Include(w => w.Customer)
                    .Include(w => w.Vehicle)
                    .AsQueryable();

                // 应用搜索条件
                if (!string.IsNullOrEmpty(LicensePlate))
                {
                    query = query.Where(w => w.Vehicle.LicensePlate.Contains(LicensePlate));
                }

                if (!string.IsNullOrEmpty(CustomerName))
                {
                    query = query.Where(w => w.Customer.Name.Contains(CustomerName));
                }

                if (!string.IsNullOrEmpty(OrderNumber))
                {
                    query = query.Where(w => w.OrderNumber.Contains(OrderNumber));
                }

                if (!string.IsNullOrEmpty(OrderStatus) && OrderStatus != "全部")
                {
                    query = query.Where(w => w.Status == OrderStatus);
                }

                if (StartDate.HasValue)
                {
                    DateTime startDate = DateTime.SpecifyKind(StartDate.Value.Date, DateTimeKind.Utc);
                    query = query.Where(w => w.CompletedDate >= startDate);
                }

                if (EndDate.HasValue)
                {
                    DateTime endDate = DateTime.SpecifyKind(EndDate.Value.Date.AddDays(1), DateTimeKind.Utc);
                    query = query.Where(w => w.CompletedDate <= endDate);
                }

                var settlementItems = query.Select(w => new SettlementItemViewModel
                {
                    Id = w.Id,
                    OrderNumber = w.OrderNumber,
                    LicensePlate = w.Vehicle.LicensePlate,
                    CustomerName = w.Customer.Name,
                    CustomerPhone = w.Customer.Phone,
                    VehicleBrand = w.Vehicle.VehicleBrand,
                    VehicleModel = w.Vehicle.VehicleModel,
                    ReceptionDate = w.ReceptionDate,
                    CompletedDate = w.CompletedDate ?? DateTime.SpecifyKind(DateTime.Now, DateTimeKind.Utc),
                    Status = w.Status,
                    TotalAmount = w.TotalAmount,
                    ProblemDescription = w.ProblemDescription
                }).ToList();

                SettlementItems = new ObservableCollection<SettlementItemViewModel>(settlementItems);
            }
            catch (Exception ex)
            {
                // 处理异常
                System.Windows.MessageBox.Show("搜索失败: " + ex.Message, "错误", System.Windows.MessageBoxButton.OK, System.Windows.MessageBoxImage.Error);
            }
        }

        // 重置搜索条件
        private void ResetSearch(object obj)
        {
            LicensePlate = string.Empty;
            CustomerName = string.Empty;
            OrderNumber = string.Empty;
            OrderStatus = "已完工";
            StartDate = null;
            EndDate = null;
        }

        // 结算选中项
        private void SettleSelectedItem(object obj)
        {
            if (SelectedSettlementItem != null)
            {
                try
                {
                    // 更新工单状态为已结算
                    var workOrder = _context.WorkOrders.Find(SelectedSettlementItem.Id);
                    if (workOrder != null)
                    {
                        workOrder.Status = "已结算";
                        _context.SaveChanges();

                        // 更新本地数据
                        SelectedSettlementItem.Status = "已结算";

                        System.Windows.MessageBox.Show("车辆结算成功！", "提示", System.Windows.MessageBoxButton.OK, System.Windows.MessageBoxImage.Information);
                    }
                }
                catch (Exception ex)
                {
                    System.Windows.MessageBox.Show("结算失败: " + ex.Message, "错误", System.Windows.MessageBoxButton.OK, System.Windows.MessageBoxImage.Error);
                }
            }
        }

        // 检查是否可以结算
        private bool CanSettle(object obj)
        {
            return SelectedSettlementItem != null && SelectedSettlementItem.Status == "已完工";
        }
    }

    // 结算项视图模型
    public class SettlementItemViewModel : INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler PropertyChanged;

        private void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }

        public int Id { get; set; }
        public string OrderNumber { get; set; }
        public string LicensePlate { get; set; }
        public string CustomerName { get; set; }
        public string CustomerPhone { get; set; }
        public string VehicleBrand { get; set; }
        public string VehicleModel { get; set; }
        public DateTime ReceptionDate { get; set; }
        public DateTime CompletedDate { get; set; }
        public string Status { get; set; }
        public decimal TotalAmount { get; set; }
        public string ProblemDescription { get; set; }
    }
}