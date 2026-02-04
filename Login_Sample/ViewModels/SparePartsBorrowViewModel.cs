using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using Login_Sample.Data;
using System;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using System.Windows;
using System.Windows.Input;

namespace Login_Sample.ViewModels
{
    /// <summary>
    /// 备件借用视图模型
    /// </summary>
    public class SparePartsBorrowViewModel : ObservableObject
    {
        public SparePartsBorrowViewModel()
        {
            // 初始化命令
            BorrowSparePartsCommand = new RelayCommand<object>(BorrowSpareParts);
            ReturnSparePartsCommand = new RelayCommand<object>(ReturnSpareParts);
            RefreshBorrowRecordsCommand = new RelayCommand<object>(RefreshBorrowRecords);

            // 加载模拟数据
            LoadSampleData();
        }

        // 命令
        public ICommand BorrowSparePartsCommand { get; set; }
        public ICommand ReturnSparePartsCommand { get; set; }
        public ICommand RefreshBorrowRecordsCommand { get; set; }

        // 借用记录ID
        private string _borrowRecordId;
        public string BorrowRecordId
        {
            get => _borrowRecordId;
            set
            {
                _borrowRecordId = value;
                OnPropertyChanged();
            }
        }

        // 借用日期
        private DateTime _borrowDate;
        public DateTime BorrowDate
        {
            get => _borrowDate;
            set
            {
                _borrowDate = value;
                OnPropertyChanged();
            }
        }

        // 借用人
        private string _borrower;
        public string Borrower
        {
            get => _borrower;
            set
            {
                _borrower = value;
                OnPropertyChanged();
            }
        }

        // 归还日期
        private DateTime? _returnDate;
        public DateTime? ReturnDate
        {
            get => _returnDate;
            set
            {
                _returnDate = value;
                OnPropertyChanged();
            }
        }

        // 借用备注
        private string _borrowRemarks;
        public string BorrowRemarks
        {
            get => _borrowRemarks;
            set
            {
                _borrowRemarks = value;
                OnPropertyChanged();
            }
        }

        // 借用状态
        private string _borrowStatus;
        public string BorrowStatus
        {
            get => _borrowStatus;
            set
            {
                _borrowStatus = value;
                OnPropertyChanged();
            }
        }

        // 借用项列表
        private ObservableCollection<SparePartsBorrowItem> _borrowItems;
        public ObservableCollection<SparePartsBorrowItem> BorrowItems
        {
            get => _borrowItems;
            set
            {
                _borrowItems = value;
                OnPropertyChanged();
            }
        }

        // 借用记录列表
        private ObservableCollection<SparePartsBorrowRecord> _borrowRecords;
        public ObservableCollection<SparePartsBorrowRecord> BorrowRecords
        {
            get => _borrowRecords;
            set
            {
                _borrowRecords = value;
                OnPropertyChanged();
            }
        }

        // 开始借用备件
        private void BorrowSpareParts(object? parameter)
        {
            // 生成借用记录ID
            BorrowRecordId = "BR" + DateTime.Now.ToString("yyyyMMddHHmmss");
            BorrowDate = DateTime.Now;
            Borrower = "管理员";
            BorrowRemarks = string.Empty;
            BorrowStatus = "借用中";

            // 加载需要借用的备件数据
            LoadBorrowItemsData();
        }

        // 归还备件
        private void ReturnSpareParts(object? parameter)
        {
            // 更新归还日期和状态
            ReturnDate = DateTime.Now;
            BorrowStatus = "已归还";

            // 这里可以添加保存归还记录的逻辑
            // 例如更新数据库中的库存数量，记录归还信息等

            // 显示归还成功的提示
            MessageBox.Show("备件已成功归还！", "提示", MessageBoxButton.OK, MessageBoxImage.Information);
        }

        // 刷新借用记录
        private void RefreshBorrowRecords(object? parameter)
        {
            // 这里可以从数据库加载实际的借用记录
            // 现在使用模拟数据
            LoadBorrowRecordsData();
        }

        // 加载借用项数据
        private void LoadBorrowItemsData()
        {
            BorrowItems = new ObservableCollection<SparePartsBorrowItem>
            {
                new SparePartsBorrowItem
                {
                    InventoryItemId = 1,
                    ItemName = "机油滤清器",
                    ItemNumber = "OIL-FILTER-001",
                    BorrowQuantity = 2,
                    UnitPrice = 25.00m,
                    Supplier = "机油滤清器供应商",
                    WarehouseLocation = "A区1号货架"
                },
                new SparePartsBorrowItem
                {
                    InventoryItemId = 2,
                    ItemName = "空气滤清器",
                    ItemNumber = "AIR-FILTER-001",
                    BorrowQuantity = 1,
                    UnitPrice = 45.00m,
                    Supplier = "空气滤清器供应商",
                    WarehouseLocation = "A区2号货架"
                }
            };
        }

        // 加载借用记录数据
        private void LoadBorrowRecordsData()
        {
            BorrowRecords = new ObservableCollection<SparePartsBorrowRecord>
            {
                new SparePartsBorrowRecord
                {
                    BorrowRecordId = "BR202401151030",
                    BorrowDate = new DateTime(2024, 1, 15, 10, 30, 0),
                    Borrower = "张三",
                    ReturnDate = new DateTime(2024, 1, 20, 14, 45, 0),
                    BorrowRemarks = "维修车间借用",
                    BorrowStatus = "已归还"
                },
                new SparePartsBorrowRecord
                {
                    BorrowRecordId = "BR202401180915",
                    BorrowDate = new DateTime(2024, 1, 18, 9, 15, 0),
                    Borrower = "李四",
                    ReturnDate = null,
                    BorrowRemarks = "研发部测试借用",
                    BorrowStatus = "借用中"
                },
                new SparePartsBorrowRecord
                {
                    BorrowRecordId = "BR202401201620",
                    BorrowDate = new DateTime(2024, 1, 20, 16, 20, 0),
                    Borrower = "王五",
                    ReturnDate = null,
                    BorrowRemarks = "客户展示借用",
                    BorrowStatus = "借用中"
                }
            };
        }

        // 加载模拟数据
        private void LoadSampleData()
        {
            BorrowSpareParts(null);
            LoadBorrowRecordsData();
        }
    }

    /// <summary>
    /// 备件借用项
    /// </summary>
    public class SparePartsBorrowItem : INotifyPropertyChanged
    {
        private int _borrowQuantity;

        public event PropertyChangedEventHandler PropertyChanged;

        private void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }

        /// <summary>
        /// 库存配件ID
        /// </summary>
        public int InventoryItemId { get; set; }
        
        /// <summary>
        /// 配件名称
        /// </summary>
        public string ItemName { get; set; } = string.Empty;
        
        /// <summary>
        /// 配件编号
        /// </summary>
        public string ItemNumber { get; set; } = string.Empty;
        
        /// <summary>
        /// 借用数量
        /// </summary>
        public int BorrowQuantity
        {
            get => _borrowQuantity;
            set
            {
                _borrowQuantity = value;
                OnPropertyChanged();
            }
        }
        
        /// <summary>
        /// 单价
        /// </summary>
        public decimal UnitPrice { get; set; }
        
        /// <summary>
        /// 供应商
        /// </summary>
        public string Supplier { get; set; } = string.Empty;
        
        /// <summary>
        /// 仓库位置
        /// </summary>
        public string WarehouseLocation { get; set; } = string.Empty;
    }

    /// <summary>
    /// 备件借用记录
    /// </summary>
    public class SparePartsBorrowRecord : INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler PropertyChanged;

        private void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }

        /// <summary>
        /// 借用记录ID
        /// </summary>
        public string BorrowRecordId { get; set; } = string.Empty;
        
        /// <summary>
        /// 借用日期
        /// </summary>
        public DateTime BorrowDate { get; set; }
        
        /// <summary>
        /// 借用人
        /// </summary>
        public string Borrower { get; set; } = string.Empty;
        
        /// <summary>
        /// 归还日期
        /// </summary>
        public DateTime? ReturnDate { get; set; }
        
        /// <summary>
        /// 借用备注
        /// </summary>
        public string BorrowRemarks { get; set; } = string.Empty;
        
        /// <summary>
        /// 借用状态
        /// </summary>
        public string BorrowStatus { get; set; } = string.Empty;
    }
}