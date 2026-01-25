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
    public class SettlementModificationViewModel : INotifyPropertyChanged
    {
        #region 字段
        private string _searchKey = string.Empty;
        private string _settlementStatus = string.Empty;
        private string _errorMessage = string.Empty;
        private Visibility _errorVisibility = Visibility.Hidden;
        private bool _isLoading = false;
        private ObservableCollection<SettlementDocument> _settlementDocuments = new ObservableCollection<SettlementDocument>();
        private SettlementDocument _selectedDocument = null;
        private string _newStatus = string.Empty;
        private decimal _amountAdjustment = 0;
        private string _modificationReason = string.Empty;
        #endregion

        #region 属性
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
        /// 结算状态
        /// </summary>
        public string SettlementStatus
        {
            get { return _settlementStatus; }
            set 
            { 
                if (_settlementStatus != value)
                {
                    _settlementStatus = value;
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
        /// 结算单据集合
        /// </summary>
        public ObservableCollection<SettlementDocument> SettlementDocuments
        {
            get { return _settlementDocuments; }
            set 
            { 
                if (_settlementDocuments != value)
                {
                    _settlementDocuments = value;
                    OnPropertyChanged();
                }
            }
        }

        /// <summary>
        /// 选中的单据
        /// </summary>
        public SettlementDocument SelectedDocument
        {
            get { return _selectedDocument; }
            set 
            { 
                if (_selectedDocument != value)
                {
                    _selectedDocument = value;
                    OnPropertyChanged();
                }
            }
        }

        /// <summary>
        /// 新状态
        /// </summary>
        public string NewStatus
        {
            get { return _newStatus; }
            set 
            { 
                if (_newStatus != value)
                {
                    _newStatus = value;
                    OnPropertyChanged();
                }
            }
        }

        /// <summary>
        /// 金额调整
        /// </summary>
        public decimal AmountAdjustment
        {
            get { return _amountAdjustment; }
            set 
            { 
                if (_amountAdjustment != value)
                {
                    _amountAdjustment = value;
                    OnPropertyChanged();
                }
            }
        }

        /// <summary>
        /// 修改原因
        /// </summary>
        public string ModificationReason
        {
            get { return _modificationReason; }
            set 
            { 
                if (_modificationReason != value)
                {
                    _modificationReason = value;
                    OnPropertyChanged();
                }
            }
        }
        #endregion

        #region 命令
        /// <summary>
        /// 搜索单据命令
        /// </summary>
        public ICommand SearchDocumentsCommand { get; set; }

        /// <summary>
        /// 改结算命令
        /// </summary>
        public ICommand ModifySettlementCommand { get; set; }

        /// <summary>
        /// 刷新单据命令
        /// </summary>
        public ICommand RefreshDocumentsCommand { get; set; }
        #endregion

        #region 构造函数
        /// <summary>
        /// 构造函数
        /// </summary>
        public SettlementModificationViewModel()
        {
            SearchDocumentsCommand = new RelayCommand(SearchDocuments);
            ModifySettlementCommand = new RelayCommand(ModifySettlement);
            RefreshDocumentsCommand = new RelayCommand(RefreshDocuments);

            // 初始化模拟数据
            LoadMockData();
        }
        #endregion

        #region 方法
        /// <summary>
        /// 搜索单据
        /// </summary>
        private void SearchDocuments(object? parameter)
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
        /// 改结算
        /// </summary>
        private void ModifySettlement(object? parameter)
        {
            if (SelectedDocument == null)
            {
                ShowError("请选择要修改的结算单据");
                return;
            }

            if (string.IsNullOrEmpty(NewStatus))
            {
                ShowError("请选择新的结算状态");
                return;
            }

            if (string.IsNullOrEmpty(ModificationReason))
            {
                ShowError("请输入修改原因");
                return;
            }

            // 确认修改操作
            var result = MessageBox.Show($"确定要将单据 {SelectedDocument.DocumentId} 从 '{SelectedDocument.Status}' 状态修改为 '{NewStatus}' 状态吗？", "确认", MessageBoxButton.YesNo, MessageBoxImage.Question);
            if (result != MessageBoxResult.Yes)
            {
                return;
            }

            IsLoading = true;
            ErrorVisibility = Visibility.Hidden;

            // 模拟修改操作
            Task.Delay(1000).ContinueWith(t =>
            {
                // 模拟修改成功
                SelectedDocument.Status = NewStatus;
                SelectedDocument.LastModifiedDate = DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss");
                SelectedDocument.LastModifiedBy = "管理员";
                
                // 如果有金额调整，更新金额
                if (AmountAdjustment != 0)
                {
                    SelectedDocument.TotalAmount += AmountAdjustment;
                }
                
                MessageBox.Show("结算修改成功", "提示", MessageBoxButton.OK, MessageBoxImage.Information);
                
                // 重置表单
                ResetForm();
                
                IsLoading = false;
            }, TaskScheduler.FromCurrentSynchronizationContext());
        }

        /// <summary>
        /// 刷新单据
        /// </summary>
        private void RefreshDocuments(object? parameter)
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
            _settlementDocuments.Clear();

            // 添加模拟数据
            _settlementDocuments.Add(new SettlementDocument
            {
                DocumentId = "WX202601010001",
                DocumentType = "维修单",
                DocumentDate = "2026-01-01",
                CustomerName = "张三",
                TotalAmount = 1200,
                PaidAmount = 1200,
                Status = "结清",
                CreationDate = "2026-01-01 10:00:00",
                CreatedBy = "管理员",
                LastModifiedDate = string.Empty,
                LastModifiedBy = string.Empty,
                Remarks = "汽车保养维修"
            });

            _settlementDocuments.Add(new SettlementDocument
            {
                DocumentId = "PX202601020002",
                DocumentType = "配件销售单",
                DocumentDate = "2026-01-02",
                CustomerName = "李四",
                TotalAmount = 3500,
                PaidAmount = 2000,
                Status = "欠账",
                CreationDate = "2026-01-02 14:30:00",
                CreatedBy = "管理员",
                LastModifiedDate = string.Empty,
                LastModifiedBy = string.Empty,
                Remarks = "销售汽车配件"
            });

            _settlementDocuments.Add(new SettlementDocument
            {
                DocumentId = "XST202601030003",
                DocumentType = "销售结算单",
                DocumentDate = "2026-01-03",
                CustomerName = "王五",
                TotalAmount = 2800,
                PaidAmount = 0,
                Status = "待结",
                CreationDate = "2026-01-03 09:45:00",
                CreatedBy = "管理员",
                LastModifiedDate = string.Empty,
                LastModifiedBy = string.Empty,
                Remarks = "销售结算"
            });

            _settlementDocuments.Add(new SettlementDocument
            {
                DocumentId = "WX202601040004",
                DocumentType = "维修单",
                DocumentDate = "2026-01-04",
                CustomerName = "赵六",
                TotalAmount = 1800,
                PaidAmount = 1800,
                Status = "结清",
                CreationDate = "2026-01-04 11:20:00",
                CreatedBy = "管理员",
                LastModifiedDate = string.Empty,
                LastModifiedBy = string.Empty,
                Remarks = "汽车维修"
            });

            _settlementDocuments.Add(new SettlementDocument
            {
                DocumentId = "PX202601050005",
                DocumentType = "配件销售单",
                DocumentDate = "2026-01-05",
                CustomerName = "孙七",
                TotalAmount = 4200,
                PaidAmount = 3000,
                Status = "欠账",
                CreationDate = "2026-01-05 16:10:00",
                CreatedBy = "管理员",
                LastModifiedDate = string.Empty,
                LastModifiedBy = string.Empty,
                Remarks = "销售汽车配件"
            });
        }

        /// <summary>
        /// 重置表单
        /// </summary>
        private void ResetForm()
        {
            NewStatus = string.Empty;
            AmountAdjustment = 0;
            ModificationReason = string.Empty;
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
    /// 结算单据
    /// </summary>
    public class SettlementDocument
    {
        /// <summary>
        /// 单据ID
        /// </summary>
        public string DocumentId { get; set; }

        /// <summary>
        /// 单据类型
        /// </summary>
        public string DocumentType { get; set; }

        /// <summary>
        /// 单据日期
        /// </summary>
        public string DocumentDate { get; set; }

        /// <summary>
        /// 客户名称
        /// </summary>
        public string CustomerName { get; set; }

        /// <summary>
        /// 总金额
        /// </summary>
        public decimal TotalAmount { get; set; }

        /// <summary>
        /// 已付金额
        /// </summary>
        public decimal PaidAmount { get; set; }

        /// <summary>
        /// 状态
        /// </summary>
        public string Status { get; set; }

        /// <summary>
        /// 创建日期
        /// </summary>
        public string CreationDate { get; set; }

        /// <summary>
        /// 创建人
        /// </summary>
        public string CreatedBy { get; set; }

        /// <summary>
        /// 最后修改日期
        /// </summary>
        public string LastModifiedDate { get; set; }

        /// <summary>
        /// 最后修改人
        /// </summary>
        public string LastModifiedBy { get; set; }

        /// <summary>
        /// 备注
        /// </summary>
        public string Remarks { get; set; }
    }
    #endregion
}