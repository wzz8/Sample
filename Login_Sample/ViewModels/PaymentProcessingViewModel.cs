using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using System.Windows;
using System.Windows.Input;

namespace Login_Sample.ViewModels
{
    public class PaymentProcessingViewModel : ObservableObject
    {
        #region 字段
        private string _paymentUnit = string.Empty;
        private string _remarks = string.Empty;
        private decimal _amount = 0;
        private bool _isIncome = true;
        private string _errorMessage = string.Empty;
        private Visibility _errorVisibility = Visibility.Hidden;
        private bool _isLoading = false;
        private ObservableCollection<PaymentRecord> _paymentRecords = new ObservableCollection<PaymentRecord>();
        private PaymentRecord _selectedPaymentRecord = null;
        #endregion

        #region 属性
        /// <summary>
        /// 款项单位
        /// </summary>
        public string PaymentUnit
        {
            get { return _paymentUnit; }
            set 
            { 
                if (_paymentUnit != value)
                {
                    _paymentUnit = value;
                    OnPropertyChanged();
                }
            }
        }

        /// <summary>
        /// 备注
        /// </summary>
        public string Remarks
        {
            get { return _remarks; }
            set 
            { 
                if (_remarks != value)
                {
                    _remarks = value;
                    OnPropertyChanged();
                }
            }
        }

        /// <summary>
        /// 金额
        /// </summary>
        public decimal Amount
        {
            get { return _amount; }
            set 
            { 
                if (_amount != value)
                {
                    _amount = value;
                    OnPropertyChanged();
                }
            }
        }

        /// <summary>
        /// 是否为收入
        /// </summary>
        public bool IsIncome
        {
            get { return _isIncome; }
            set 
            { 
                if (_isIncome != value)
                {
                    _isIncome = value;
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
        /// 款项记录集合
        /// </summary>
        public ObservableCollection<PaymentRecord> PaymentRecords
        {
            get { return _paymentRecords; }
            set 
            { 
                if (_paymentRecords != value)
                {
                    _paymentRecords = value;
                    OnPropertyChanged();
                }
            }
        }

        /// <summary>
        /// 选中的款项记录
        /// </summary>
        public PaymentRecord SelectedPaymentRecord
        {
            get { return _selectedPaymentRecord; }
            set 
            { 
                if (_selectedPaymentRecord != value)
                {
                    _selectedPaymentRecord = value;
                    OnPropertyChanged();
                }
            }
        }
        #endregion

        #region 命令
        /// <summary>
        /// 收入按钮命令
        /// </summary>
        public ICommand IncomeCommand { get; set; }

        /// <summary>
        /// 支出按钮命令
        /// </summary>
        public ICommand ExpenseCommand { get; set; }

        /// <summary>
        /// 收款改错命令
        /// </summary>
        public ICommand CorrectPaymentCommand { get; set; }

        /// <summary>
        /// 保存款项命令
        /// </summary>
        public ICommand SavePaymentCommand { get; set; }

        /// <summary>
        /// 刷新记录命令
        /// </summary>
        public ICommand RefreshRecordsCommand { get; set; }
        #endregion

        #region 构造函数
        /// <summary>
        /// 构造函数
        /// </summary>
        public PaymentProcessingViewModel()
        {
            IncomeCommand = new RelayCommand<object>(ShowIncomeWindow);
            ExpenseCommand = new RelayCommand<object>(ShowExpenseWindow);
            CorrectPaymentCommand = new RelayCommand<object>(CorrectPayment);
            SavePaymentCommand = new RelayCommand<object>(SavePayment);
            RefreshRecordsCommand = new RelayCommand<object>(RefreshRecords);

            // 初始化模拟数据
            LoadMockData();
        }
        #endregion

        #region 方法
        /// <summary>
        /// 显示收入窗口
        /// </summary>
        private void ShowIncomeWindow(object? parameter)
        {
            IsIncome = true;
            ShowPaymentWindow();
        }

        /// <summary>
        /// 显示支出窗口
        /// </summary>
        private void ShowExpenseWindow(object? parameter)
        {
            IsIncome = false;
            ShowPaymentWindow();
        }

        /// <summary>
        /// 显示款项窗口
        /// </summary>
        private void ShowPaymentWindow()
        {
            // 重置表单
            PaymentUnit = string.Empty;
            Remarks = string.Empty;
            Amount = 0;
            ErrorVisibility = Visibility.Hidden;

            // 显示款项登记对话框
            // 这里可以实现一个自定义对话框，目前使用MessageBox模拟
            var result = MessageBox.Show("款项登记功能将在此实现", "提示", MessageBoxButton.OK, MessageBoxImage.Information);
        }

        /// <summary>
        /// 保存款项记录
        /// </summary>
        private void SavePayment(object? parameter)
        {
            if (string.IsNullOrEmpty(PaymentUnit))
            {
                ShowError("请输入款项单位");
                return;
            }

            if (Amount <= 0)
            {
                ShowError("请输入有效的金额");
                return;
            }

            IsLoading = true;
            ErrorVisibility = Visibility.Hidden;

            // 模拟保存操作
            Task.Delay(1000).ContinueWith(t =>
            {
                // 模拟保存成功
                MessageBox.Show($"{(IsIncome ? "收入" : "支出")}记录保存成功", "提示", MessageBoxButton.OK, MessageBoxImage.Information);
                
                // 重置表单
                ResetForm();
                
                // 刷新记录
                LoadMockData();
                
                IsLoading = false;
            }, TaskScheduler.FromCurrentSynchronizationContext());
        }

        /// <summary>
        /// 收款改错
        /// </summary>
        private void CorrectPayment(object? parameter)
        {
            if (SelectedPaymentRecord == null)
            {
                ShowError("请选择要修改的收款记录");
                return;
            }

            // 检查是否符合改错条件
            if (SelectedPaymentRecord.PaymentType != "维修")
            {
                ShowError("只有款项性质为'维修'的收款单才能改错");
                return;
            }

            if (SelectedPaymentRecord.Amount <= 0)
            {
                ShowError("收入金额小于等于0的收款单不能改错");
                return;
            }

            IsLoading = true;
            ErrorVisibility = Visibility.Hidden;

            // 模拟收款改错操作
            Task.Delay(1000).ContinueWith(t =>
            {
                // 模拟改错成功
                MessageBox.Show("收款改错操作成功", "提示", MessageBoxButton.OK, MessageBoxImage.Information);
                
                // 刷新记录
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

            // 模拟刷新操作
            Task.Delay(1000).ContinueWith(t =>
            {
                // 重新加载模拟数据
                LoadMockData();
                IsLoading = false;
            }, TaskScheduler.FromCurrentSynchronizationContext());
        }

        /// <summary>
        /// 加载模拟数据
        /// </summary>
        private void LoadMockData()
        {
            _paymentRecords.Clear();

            // 添加模拟数据
            _paymentRecords.Add(new PaymentRecord { Id = 1, PaymentUnit = "客户A", PaymentType = "维修", Amount = 500, PaymentDate = "2026-01-18", Remarks = "维修车辆费用" });
            _paymentRecords.Add(new PaymentRecord { Id = 2, PaymentUnit = "客户B", PaymentType = "配件销售", Amount = 1200, PaymentDate = "2026-01-19", Remarks = "购买配件费用" });
            _paymentRecords.Add(new PaymentRecord { Id = 3, PaymentUnit = "供应商C", PaymentType = "配件采购", Amount = -800, PaymentDate = "2026-01-19", Remarks = "支付配件采购款" });
            _paymentRecords.Add(new PaymentRecord { Id = 4, PaymentUnit = "客户D", PaymentType = "维修", Amount = 350, PaymentDate = "2026-01-20", Remarks = "维修保养费用" });
            _paymentRecords.Add(new PaymentRecord { Id = 5, PaymentUnit = "房租", PaymentType = "其他支出", Amount = -2000, PaymentDate = "2026-01-20", Remarks = "支付本月房租" });
        }

        /// <summary>
        /// 重置表单
        /// </summary>
        private void ResetForm()
        {
            PaymentUnit = string.Empty;
            Remarks = string.Empty;
            Amount = 0;
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

        #region 内部类
        /// <summary>
        /// 款项记录
        /// </summary>
        public class PaymentRecord
        {
            /// <summary>
            /// 记录ID
            /// </summary>
            public int Id { get; set; }

            /// <summary>
            /// 款项单位
            /// </summary>
            public string PaymentUnit { get; set; }

            /// <summary>
            /// 款项类型
            /// </summary>
            public string PaymentType { get; set; }

            /// <summary>
            /// 金额
            /// </summary>
            public decimal Amount { get; set; }

            /// <summary>
            /// 款项日期
            /// </summary>
            public string PaymentDate { get; set; }

            /// <summary>
            /// 备注
            /// </summary>
            public string Remarks { get; set; }
        }
        #endregion
    }
}