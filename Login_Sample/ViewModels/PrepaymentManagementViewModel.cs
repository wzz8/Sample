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
    public class PrepaymentManagementViewModel : INotifyPropertyChanged
    {
        #region 字段
        private string _customerName = string.Empty;
        private string _prepaymentType = string.Empty;
        private string _searchKey = string.Empty;
        private decimal _amount = 0;
        private string _remarks = string.Empty;
        private string _errorMessage = string.Empty;
        private Visibility _errorVisibility = Visibility.Hidden;
        private bool _isLoading = false;
        private ObservableCollection<PrepaymentRecord> _prepaymentRecords = new ObservableCollection<PrepaymentRecord>();
        private PrepaymentRecord _selectedRecord = null;
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
        /// 预收预付类型
        /// </summary>
        public string PrepaymentType
        {
            get { return _prepaymentType; }
            set 
            { 
                if (_prepaymentType != value)
                {
                    _prepaymentType = value;
                    OnPropertyChanged();
                }
            }
        }

        /// <summary>
        /// 搜索关键字
        /// </summary>
        public string SearchKey
        {
            get { return _searchKey; }
            set 
            { 
                if (_searchKey != value)
                {
                    _searchKey = value;
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
        /// 预收预付记录集合
        /// </summary>
        public ObservableCollection<PrepaymentRecord> PrepaymentRecords
        {
            get { return _prepaymentRecords; }
            set 
            { 
                if (_prepaymentRecords != value)
                {
                    _prepaymentRecords = value;
                    OnPropertyChanged();
                }
            }
        }

        /// <summary>
        /// 选中的记录
        /// </summary>
        public PrepaymentRecord SelectedRecord
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
        /// 登记预收预付命令
        /// </summary>
        public ICommand RegisterPrepaymentCommand { get; set; }

        /// <summary>
        /// 搜索记录命令
        /// </summary>
        public ICommand SearchRecordsCommand { get; set; }

        /// <summary>
        /// 刷新记录命令
        /// </summary>
        public ICommand RefreshRecordsCommand { get; set; }

        /// <summary>
        /// 删除记录命令
        /// </summary>
        public ICommand DeleteRecordCommand { get; set; }
        #endregion

        #region 构造函数
        /// <summary>
        /// 构造函数
        /// </summary>
        public PrepaymentManagementViewModel()
        {
            RegisterPrepaymentCommand = new RelayCommand(RegisterPrepayment);
            SearchRecordsCommand = new RelayCommand(SearchRecords);
            RefreshRecordsCommand = new RelayCommand(RefreshRecords);
            DeleteRecordCommand = new RelayCommand(DeleteRecord);

            // 初始化模拟数据
            LoadMockData();
        }
        #endregion

        #region 方法
        /// <summary>
        /// 登记预收预付
        /// </summary>
        private void RegisterPrepayment(object? parameter)
        {
            if (string.IsNullOrEmpty(CustomerName))
            {
                ShowError("请输入客户名称");
                return;
            }

            if (string.IsNullOrEmpty(PrepaymentType))
            {
                ShowError("请选择预收预付类型");
                return;
            }

            if (Amount <= 0)
            {
                ShowError("请输入有效的金额");
                return;
            }

            IsLoading = true;
            ErrorVisibility = Visibility.Hidden;

            // 模拟登记操作
            Task.Delay(1000).ContinueWith(t =>
            {
                // 模拟登记成功
                var newRecord = new PrepaymentRecord
                {
                    Id = (PrepaymentRecords.Count + 1).ToString(),
                    CustomerName = CustomerName,
                    PrepaymentType = PrepaymentType,
                    Amount = Amount,
                    RecordDate = DateTime.Now.ToString("yyyy-MM-dd"),
                    Operator = "管理员",
                    Remarks = Remarks
                };

                PrepaymentRecords.Add(newRecord);
                
                MessageBox.Show("登记成功", "提示", MessageBoxButton.OK, MessageBoxImage.Information);
                
                // 重置表单
                ResetForm();
                
                IsLoading = false;
            }, TaskScheduler.FromCurrentSynchronizationContext());
        }

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
            SearchKey = string.Empty;

            // 模拟刷新操作
            Task.Delay(1000).ContinueWith(t =>
            {
                // 模拟刷新结果
                LoadMockData();
                IsLoading = false;
            }, TaskScheduler.FromCurrentSynchronizationContext());
        }

        /// <summary>
        /// 删除记录
        /// </summary>
        private void DeleteRecord(object? parameter)
        {
            if (SelectedRecord == null)
            {
                ShowError("请选择要删除的记录");
                return;
            }

            // 确认删除操作
            var result = MessageBox.Show($"确定要删除客户 {SelectedRecord.CustomerName} 的预收预付记录吗？", "确认", MessageBoxButton.YesNo, MessageBoxImage.Question);
            if (result != MessageBoxResult.Yes)
            {
                return;
            }

            IsLoading = true;
            ErrorVisibility = Visibility.Hidden;

            // 模拟删除操作
            Task.Delay(1000).ContinueWith(t =>
            {
                // 模拟删除成功
                PrepaymentRecords.Remove(SelectedRecord);
                
                MessageBox.Show("删除成功", "提示", MessageBoxButton.OK, MessageBoxImage.Information);
                IsLoading = false;
            }, TaskScheduler.FromCurrentSynchronizationContext());
        }

        /// <summary>
        /// 重置表单
        /// </summary>
        private void ResetForm()
        {
            CustomerName = string.Empty;
            PrepaymentType = string.Empty;
            Amount = 0;
            Remarks = string.Empty;
        }

        /// <summary>
        /// 加载模拟数据
        /// </summary>
        private void LoadMockData()
        {
            _prepaymentRecords.Clear();

            // 添加模拟数据
            _prepaymentRecords.Add(new PrepaymentRecord
            {
                Id = "1",
                CustomerName = "张三",
                PrepaymentType = "预收",
                Amount = 5000,
                RecordDate = "2026-01-01",
                Operator = "管理员",
                Remarks = "汽车保养预付款"
            });

            _prepaymentRecords.Add(new PrepaymentRecord
            {
                Id = "2",
                CustomerName = "李四",
                PrepaymentType = "预付",
                Amount = 3000,
                RecordDate = "2026-01-02",
                Operator = "管理员",
                Remarks = "配件采购预付款"
            });

            _prepaymentRecords.Add(new PrepaymentRecord
            {
                Id = "3",
                CustomerName = "王五",
                PrepaymentType = "预收",
                Amount = 8000,
                RecordDate = "2026-01-03",
                Operator = "管理员",
                Remarks = "汽车维修预付款"
            });

            _prepaymentRecords.Add(new PrepaymentRecord
            {
                Id = "4",
                CustomerName = "赵六",
                PrepaymentType = "预付",
                Amount = 2000,
                RecordDate = "2026-01-04",
                Operator = "管理员",
                Remarks = "配件采购预付款"
            });

            _prepaymentRecords.Add(new PrepaymentRecord
            {
                Id = "5",
                CustomerName = "孙七",
                PrepaymentType = "预收",
                Amount = 6000,
                RecordDate = "2026-01-05",
                Operator = "管理员",
                Remarks = "汽车保养预付款"
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
    /// 预收预付记录
    /// </summary>
    public class PrepaymentRecord
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
        /// 预收预付类型
        /// </summary>
        public string PrepaymentType { get; set; }

        /// <summary>
        /// 金额
        /// </summary>
        public decimal Amount { get; set; }

        /// <summary>
        /// 记录日期
        /// </summary>
        public string RecordDate { get; set; }

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