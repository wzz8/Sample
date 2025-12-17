using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Windows.Input;
using Login_Sample.Data;

namespace Login_Sample.ViewModels
{
    /// <summary>
    /// 库存查询视图模型
    /// </summary>
    public class InventoryQueryViewModel : INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler? PropertyChanged;
        
        private void OnPropertyChanged([CallerMemberName] string? propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
        
        // 命令
        public ICommand SearchCommand { get; set; }
        public ICommand ResetCommand { get; set; }
        public ICommand ExportCommand { get; set; }
        
        // 查询条件
        private string _itemName;
        public string ItemName
        {
            get => _itemName;
            set
            {
                _itemName = value;
                OnPropertyChanged();
            }
        }
        
        private string _itemNumber;
        public string ItemNumber
        {
            get => _itemNumber;
            set
            {
                _itemNumber = value;
                OnPropertyChanged();
            }
        }
        
        // 网点选项
        public ObservableCollection<string> BranchOptions { get; set; }
        
        private string _selectedBranch;
        public string SelectedBranch
        {
            get => _selectedBranch;
            set
            {
                _selectedBranch = value;
                OnPropertyChanged();
            }
        }
        
        // 状态选项
        public ObservableCollection<string> StatusOptions { get; set; }
        
        private string _selectedStatus;
        public string SelectedStatus
        {
            get => _selectedStatus;
            set
            {
                _selectedStatus = value;
                OnPropertyChanged();
            }
        }
        
        // 库存列表
        private ObservableCollection<InventoryItem> _inventoryItems;
        public ObservableCollection<InventoryItem> InventoryItems
        {
            get => _inventoryItems;
            set
            {
                _inventoryItems = value;
                OnPropertyChanged();
                UpdatePagination();
            }
        }
        
        // 分页相关
        private int _currentPage = 1;
        public int CurrentPage
        {
            get => _currentPage;
            set
            {
                _currentPage = value;
                OnPropertyChanged();
                LoadCurrentPage();
            }
        }
        
        private int _pageSize = 10;
        public int PageSize
        {
            get => _pageSize;
            set
            {
                _pageSize = value;
                OnPropertyChanged();
                CurrentPage = 1;
                UpdatePagination();
            }
        }
        
        private int _totalItems;
        public int TotalItems
        {
            get => _totalItems;
            set
            {
                _totalItems = value;
                OnPropertyChanged();
                OnPropertyChanged(nameof(TotalPages));
            }
        }
        
        public int TotalPages => (int)Math.Ceiling((double)TotalItems / PageSize);
        
        // 所有库存数据
        private List<InventoryItem> _allInventoryItems;
        
        // 构造函数
        public InventoryQueryViewModel()
        {
            // 初始化命令
            SearchCommand = new RelayCommand(Search);
            ResetCommand = new RelayCommand(Reset);
            ExportCommand = new RelayCommand(Export);
            
            // 初始化非空字段
            _itemName = string.Empty;
            _itemNumber = string.Empty;
            
            // 初始化网点选项
            BranchOptions = new ObservableCollection<string>
            {
                "全部",
                "总厂",
                "一分厂",
                "二分厂",
                "三分厂"
            };
            SelectedBranch = "全部";
            
            // 初始化状态选项
            StatusOptions = new ObservableCollection<string>
            {
                "全部",
                "充足",
                "正常",
                "不足",
                "缺货"
            };
            SelectedStatus = "全部";
            
            // 初始化库存列表
            _inventoryItems = new ObservableCollection<InventoryItem>();
            
            // 加载模拟数据
            LoadSampleData();
        }
        
        /// <summary>
        /// 加载模拟数据
        /// </summary>
        private void LoadSampleData()
        {
            var branches = new string[] { "总厂", "一分厂", "二分厂", "三分厂" };
            var statuses = new string[] { "充足", "正常", "不足", "缺货" };
            var suppliers = new string[] { "供应商A", "供应商B", "供应商C", "供应商D" };
            var warehouseLocations = new string[] { "仓库A1", "仓库A2", "仓库B1", "仓库B2", "仓库C1" };
            
            _allInventoryItems = new List<InventoryItem>();
            
            for (int i = 1; i <= 100; i++)
            {
                var rnd = new Random();
                var statusIndex = rnd.Next(statuses.Length);
                
                _allInventoryItems.Add(new InventoryItem
                {
                    Id = i,
                    ItemNumber = $"SP{i:D4}",
                    ItemName = $"配件名称{i}",
                    BranchName = branches[rnd.Next(branches.Length)],
                    Quantity = statusIndex switch
                    {
                        0 => rnd.Next(100, 500), // 充足
                        1 => rnd.Next(50, 100),  // 正常
                        2 => rnd.Next(10, 50),   // 不足
                        _ => rnd.Next(0, 10)     // 缺货
                    },
                    UnitPrice = (decimal)(rnd.Next(100, 5000) + rnd.NextDouble()),
                    Supplier = suppliers[rnd.Next(suppliers.Length)],
                    WarehouseLocation = warehouseLocations[rnd.Next(warehouseLocations.Length)],
                    Status = statuses[statusIndex],
                    LastUpdatedTime = DateTime.Now.AddDays(-rnd.Next(30)).AddHours(-rnd.Next(24)).AddMinutes(-rnd.Next(60))
                });
            }
            
            TotalItems = _allInventoryItems.Count;
            LoadCurrentPage();
        }
        
        /// <summary>
        /// 搜索库存
        /// </summary>
        private void Search(object? parameter)
        {
            var filteredItems = _allInventoryItems;
            
            // 按配件名称筛选
            if (!string.IsNullOrWhiteSpace(ItemName))
            {
                filteredItems = filteredItems.Where(item => 
                    item.ItemName.Contains(ItemName, StringComparison.OrdinalIgnoreCase)).ToList();
            }
            
            // 按配件编号筛选
            if (!string.IsNullOrWhiteSpace(ItemNumber))
            {
                filteredItems = filteredItems.Where(item => 
                    item.ItemNumber.Contains(ItemNumber, StringComparison.OrdinalIgnoreCase)).ToList();
            }
            
            // 按网点筛选
            if (SelectedBranch != "全部")
            {
                filteredItems = filteredItems.Where(item => 
                    item.BranchName == SelectedBranch).ToList();
            }
            
            // 按状态筛选
            if (SelectedStatus != "全部")
            {
                filteredItems = filteredItems.Where(item => 
                    item.Status == SelectedStatus).ToList();
            }
            
            _allInventoryItems = filteredItems;
            TotalItems = filteredItems.Count;
            CurrentPage = 1;
            LoadCurrentPage();
        }
        
        /// <summary>
        /// 重置查询条件
        /// </summary>
        private void Reset(object? parameter)
        {
            ItemName = string.Empty;
            ItemNumber = string.Empty;
            SelectedBranch = "全部";
            SelectedStatus = "全部";
            
            // 重新加载所有数据
            LoadSampleData();
        }
        
        /// <summary>
        /// 导出数据
        /// </summary>
        private void Export(object? parameter)
        {
            // 在实际应用中，这里应该实现数据导出功能
            // 现在仅做演示
            Console.WriteLine("导出库存数据");
        }
        
        /// <summary>
        /// 更新分页信息
        /// </summary>
        private void UpdatePagination()
        {
            TotalItems = _allInventoryItems.Count;
        }
        
        /// <summary>
        /// 加载当前页数据
        /// </summary>
        private void LoadCurrentPage()
        {
            var pagedItems = _allInventoryItems
                .Skip((CurrentPage - 1) * PageSize)
                .Take(PageSize)
                .ToList();
            
            InventoryItems = new ObservableCollection<InventoryItem>(pagedItems);
        }
    }
}
