using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using System.Windows;
using System.Windows.Input;

namespace Login_Sample.ViewModels
{
    public class SalesSettlementViewModel : ObservableObject
    {
        private string _purchaseUnit = string.Empty;
        private string _salesOrderNumber = string.Empty;
        private string _salesDate = string.Empty;
        private decimal _totalAmount = 0;
        private decimal _discount = 0;
        private decimal _actualReceivedAmount = 0;
        private string _errorMessage = string.Empty;
        private Visibility _errorVisibility = Visibility.Hidden;
        private bool _isLoading = false;

        /// <summary>
        /// 购货单位
        /// </summary>
        public string PurchaseUnit
        {
            get { return _purchaseUnit; }
            set 
            { 
                if (_purchaseUnit != value)
                {
                    _purchaseUnit = value;
                    OnPropertyChanged();
                }
            }
        }

        /// <summary>
        /// 销售单号
        /// </summary>
        public string SalesOrderNumber
        {
            get { return _salesOrderNumber; }
            set 
            { 
                if (_salesOrderNumber != value)
                {
                    _salesOrderNumber = value;
                    OnPropertyChanged();
                }
            }
        }

        /// <summary>
        /// 销售日期
        /// </summary>
        public string SalesDate
        {
            get { return _salesDate; }
            set 
            { 
                if (_salesDate != value)
                {
                    _salesDate = value;
                    OnPropertyChanged();
                }
            }
        }

        /// <summary>
        /// 总金额
        /// </summary>
        public decimal TotalAmount
        {
            get { return _totalAmount; }
            set 
            { 
                if (_totalAmount != value)
                {
                    _totalAmount = value;
                    OnPropertyChanged();
                    CalculateActualAmount();
                }
            }
        }

        /// <summary>
        /// 折扣金额
        /// </summary>
        public decimal Discount
        {
            get { return _discount; }
            set 
            { 
                if (_discount != value)
                {
                    _discount = value;
                    OnPropertyChanged();
                    CalculateActualAmount();
                }
            }
        }

        /// <summary>
        /// 实收金额
        /// </summary>
        public decimal ActualReceivedAmount
        {
            get { return _actualReceivedAmount; }
            set 
            { 
                if (_actualReceivedAmount != value)
                {
                    _actualReceivedAmount = value;
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
        /// 检索命令
        /// </summary>
        public ICommand SearchCommand { get; set; }

        /// <summary>
        /// 结算命令
        /// </summary>
        public ICommand SettleCommand { get; set; }

        /// <summary>
        /// 构造函数
        /// </summary>
        public SalesSettlementViewModel()
        {
            SearchCommand = new RelayCommand<object>(SearchSalesOrder);
            SettleCommand = new RelayCommand<object>(SettleSalesOrder);
            
            // 初始化销售日期为当前日期
            SalesDate = DateTime.Now.ToString("yyyy-MM-dd");
        }

        /// <summary>
        /// 检索销售单
        /// </summary>
        private void SearchSalesOrder(object? parameter)
        {
            if (string.IsNullOrEmpty(PurchaseUnit) && string.IsNullOrEmpty(SalesOrderNumber) && string.IsNullOrEmpty(SalesDate))
            {
                ShowError("请至少输入一个检索条件");
                return;
            }

            IsLoading = true;
            ErrorVisibility = Visibility.Hidden;

            // 这里可以添加实际的检索逻辑，例如从数据库查询
            // 为了演示，我们使用模拟数据
            Task.Delay(1000).ContinueWith(t =>
            {
                // 模拟检索到的销售单数据
                TotalAmount = 1000;
                Discount = 0;
                ActualReceivedAmount = 1000;

                IsLoading = false;
            }, TaskScheduler.FromCurrentSynchronizationContext());
        }

        /// <summary>
        /// 结算销售单
        /// </summary>
        private void SettleSalesOrder(object? parameter)
        {
            if (TotalAmount == 0)
            {
                ShowError("请先检索销售单");
                return;
            }

            if (ActualReceivedAmount < 0)
            {
                ShowError("实收金额不能为负数");
                return;
            }

            IsLoading = true;
            ErrorVisibility = Visibility.Hidden;

            // 这里可以添加实际的结算逻辑，例如保存到数据库
            Task.Delay(1000).ContinueWith(t =>
            {
                // 模拟结算成功
                MessageBox.Show("销售单结算成功", "提示", MessageBoxButton.OK, MessageBoxImage.Information);
                ResetForm();
                IsLoading = false;
            }, TaskScheduler.FromCurrentSynchronizationContext());
        }

        /// <summary>
        /// 计算实际金额
        /// </summary>
        private void CalculateActualAmount()
        {
            decimal actualAmount = TotalAmount - Discount;
            if (actualAmount < 0) actualAmount = 0;
            ActualReceivedAmount = actualAmount;
        }

        /// <summary>
        /// 重置表单
        /// </summary>
        private void ResetForm()
        {
            PurchaseUnit = string.Empty;
            SalesOrderNumber = string.Empty;
            SalesDate = DateTime.Now.ToString("yyyy-MM-dd");
            TotalAmount = 0;
            Discount = 0;
            ActualReceivedAmount = 0;
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
    }
}