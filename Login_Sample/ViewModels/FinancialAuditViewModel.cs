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
    public class FinancialAuditViewModel : INotifyPropertyChanged
    {
        #region 字段
        private string _searchKey = string.Empty;
        private string _documentType = string.Empty;
        private bool _isAudited = false;
        private string _errorMessage = string.Empty;
        private Visibility _errorVisibility = Visibility.Hidden;
        private bool _isLoading = false;
        private ObservableCollection<AuditDocument> _auditDocuments = new ObservableCollection<AuditDocument>();
        private AuditDocument _selectedDocument = null;
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
        /// 单据类型
        /// </summary>
        public string DocumentType
        {
            get { return _documentType; }
            set 
            { 
                if (_documentType != value)
                {
                    _documentType = value;
                    OnPropertyChanged();
                }
            }
        }

        /// <summary>
        /// 是否已审核
        /// </summary>
        public bool IsAudited
        {
            get { return _isAudited; }
            set 
            { 
                if (_isAudited != value)
                {
                    _isAudited = value;
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
        /// 审核单据集合
        /// </summary>
        public ObservableCollection<AuditDocument> AuditDocuments
        {
            get { return _auditDocuments; }
            set 
            { 
                if (_auditDocuments != value)
                {
                    _auditDocuments = value;
                    OnPropertyChanged();
                }
            }
        }

        /// <summary>
        /// 选中的单据
        /// </summary>
        public AuditDocument SelectedDocument
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
        #endregion

        #region 命令
        /// <summary>
        /// 搜索单据命令
        /// </summary>
        public ICommand SearchDocumentsCommand { get; set; }

        /// <summary>
        /// 审核单据命令
        /// </summary>
        public ICommand AuditDocumentCommand { get; set; }

        /// <summary>
        /// 撤销审核命令
        /// </summary>
        public ICommand CancelAuditCommand { get; set; }

        /// <summary>
        /// 刷新单据命令
        /// </summary>
        public ICommand RefreshDocumentsCommand { get; set; }
        #endregion

        #region 构造函数
        /// <summary>
        /// 构造函数
        /// </summary>
        public FinancialAuditViewModel()
        {
            SearchDocumentsCommand = new RelayCommand(SearchDocuments);
            AuditDocumentCommand = new RelayCommand(AuditDocument);
            CancelAuditCommand = new RelayCommand(CancelAudit);
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
        /// 审核单据
        /// </summary>
        private void AuditDocument(object? parameter)
        {
            if (SelectedDocument == null)
            {
                ShowError("请选择要审核的单据");
                return;
            }

            if (SelectedDocument.IsAudited)
            {
                ShowError("该单据已经审核过了");
                return;
            }

            IsLoading = true;
            ErrorVisibility = Visibility.Hidden;

            // 模拟审核操作
            Task.Delay(1000).ContinueWith(t =>
            {
                // 模拟审核成功
                SelectedDocument.IsAudited = true;
                SelectedDocument.AuditStatus = "已审核";
                SelectedDocument.AuditDate = DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss");
                SelectedDocument.Auditor = "财务审核员";
                
                MessageBox.Show("单据审核成功", "提示", MessageBoxButton.OK, MessageBoxImage.Information);
                
                IsLoading = false;
            }, TaskScheduler.FromCurrentSynchronizationContext());
        }

        /// <summary>
        /// 撤销审核
        /// </summary>
        private void CancelAudit(object? parameter)
        {
            if (SelectedDocument == null)
            {
                ShowError("请选择要撤销审核的单据");
                return;
            }

            if (!SelectedDocument.IsAudited)
            {
                ShowError("该单据尚未审核");
                return;
            }

            // 确认撤销审核
            var result = MessageBox.Show("确定要撤销该单据的审核吗？", "确认", MessageBoxButton.YesNo, MessageBoxImage.Question);
            if (result != MessageBoxResult.Yes)
            {
                return;
            }

            IsLoading = true;
            ErrorVisibility = Visibility.Hidden;

            // 模拟撤销审核操作
            Task.Delay(1000).ContinueWith(t =>
            {
                // 模拟撤销审核成功
                SelectedDocument.IsAudited = false;
                SelectedDocument.AuditStatus = "未审核";
                SelectedDocument.AuditDate = string.Empty;
                SelectedDocument.Auditor = string.Empty;
                
                MessageBox.Show("单据撤销审核成功", "提示", MessageBoxButton.OK, MessageBoxImage.Information);
                
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
            _auditDocuments.Clear();

            // 添加模拟数据
            _auditDocuments.Add(new AuditDocument
            {
                DocumentId = "WX202601010001",
                DocumentType = "维修单",
                DocumentDate = "2026-01-01",
                CustomerName = "张三",
                TotalAmount = 1200,
                IsAudited = false,
                AuditStatus = "未审核",
                AuditDate = string.Empty,
                Auditor = string.Empty,
                Remarks = "汽车保养维修"
            });

            _auditDocuments.Add(new AuditDocument
            {
                DocumentId = "PX202601020002",
                DocumentType = "配件销售单",
                DocumentDate = "2026-01-02",
                CustomerName = "李四",
                TotalAmount = 3500,
                IsAudited = true,
                AuditStatus = "已审核",
                AuditDate = "2026-01-02 14:30:00",
                Auditor = "财务审核员",
                Remarks = "销售汽车配件"
            });

            _auditDocuments.Add(new AuditDocument
            {
                DocumentId = "RK202601030003",
                DocumentType = "采购入库单",
                DocumentDate = "2026-01-03",
                CustomerName = "供应商A",
                TotalAmount = 5000,
                IsAudited = false,
                AuditStatus = "未审核",
                AuditDate = string.Empty,
                Auditor = string.Empty,
                Remarks = "采购汽车配件"
            });

            _auditDocuments.Add(new AuditDocument
            {
                DocumentId = "XST202601040004",
                DocumentType = "销售结算单",
                DocumentDate = "2026-01-04",
                CustomerName = "王五",
                TotalAmount = 2800,
                IsAudited = false,
                AuditStatus = "未审核",
                AuditDate = string.Empty,
                Auditor = string.Empty,
                Remarks = "销售结算"
            });

            _auditDocuments.Add(new AuditDocument
            {
                DocumentId = "KX202601050005",
                DocumentType = "款项单",
                DocumentDate = "2026-01-05",
                CustomerName = "赵六",
                TotalAmount = 1500,
                IsAudited = true,
                AuditStatus = "已审核",
                AuditDate = "2026-01-05 09:45:00",
                Auditor = "财务审核员",
                Remarks = "收款记录"
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
    /// 审核单据
    /// </summary>
    public class AuditDocument
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
        /// 是否已审核
        /// </summary>
        public bool IsAudited { get; set; }

        /// <summary>
        /// 审核状态
        /// </summary>
        public string AuditStatus { get; set; }

        /// <summary>
        /// 审核日期
        /// </summary>
        public string AuditDate { get; set; }

        /// <summary>
        /// 审核人
        /// </summary>
        public string Auditor { get; set; }

        /// <summary>
        /// 备注
        /// </summary>
        public string Remarks { get; set; }
    }
    #endregion
}