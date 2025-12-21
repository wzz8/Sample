using System;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Windows.Input;
using Login_Sample.Data;
using MaterialDesignThemes.Wpf;

namespace Login_Sample.ViewModels
{
    public class InventoryManagementViewModel : INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler PropertyChanged;

        private void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }

        private ApplicationDbContext _dbContext;

        // 库存配件列表
        private ObservableCollection<InventoryItem> _inventoryItems;
        public ObservableCollection<InventoryItem> InventoryItems
        {
            get { return _inventoryItems; }
            set { _inventoryItems = value; OnPropertyChanged(); }
        }

        // 入库记录
        private ObservableCollection<InventoryInRecord> _inventoryInRecords;
        public ObservableCollection<InventoryInRecord> InventoryInRecords
        {
            get { return _inventoryInRecords; }
            set { _inventoryInRecords = value; OnPropertyChanged(); }
        }

        // 销售记录
        private ObservableCollection<SparePartsSale> _sparePartsSales;
        public ObservableCollection<SparePartsSale> SparePartsSales
        {
            get { return _sparePartsSales; }
            set { _sparePartsSales = value; OnPropertyChanged(); }
        }

        // 搜索关键词
        private string _searchKeyword;
        public string SearchKeyword
        {
            get { return _searchKeyword; }
            set { _searchKeyword = value; OnPropertyChanged(); FilterInventoryItems(); }
        }

        // 选中的配件
        private InventoryItem _selectedItem;
        public InventoryItem SelectedItem
        {
            get { return _selectedItem; }
            set { _selectedItem = value; OnPropertyChanged(); }
        }

        // 价格调整
        private decimal _newPrice;
        public decimal NewPrice
        {
            get { return _newPrice; }
            set { _newPrice = value; OnPropertyChanged(); }
        }

        // 出库控制
        private int _minInventory;
        public int MinInventory
        {
            get { return _minInventory; }
            set { _minInventory = value; OnPropertyChanged(); }
        }

        // 命令
        public ICommand RefreshCommand { get; set; }
        public ICommand AdjustPriceCommand { get; set; }
        public ICommand UpdateMinInventoryCommand { get; set; }

        // 构造函数
        public InventoryManagementViewModel()
        {
            _dbContext = new ApplicationDbContext();

            // 初始化数据
            LoadInventoryItems();
            LoadInventoryInRecords();
            LoadSparePartsSales();

            // 初始化命令
            RefreshCommand = new RelayCommand(RefreshData);
            AdjustPriceCommand = new RelayCommand(AdjustPrice);
            UpdateMinInventoryCommand = new RelayCommand(UpdateMinInventory);
        }

        // 加载库存配件
        private void LoadInventoryItems()
        {
            InventoryItems = new ObservableCollection<InventoryItem>(_dbContext.InventoryItems.ToList());
        }

        // 加载入库记录
        private void LoadInventoryInRecords()
        {
            InventoryInRecords = new ObservableCollection<InventoryInRecord>(_dbContext.InventoryInRecords.OrderByDescending(r => r.InboundDate).Take(20).ToList());
        }

        // 加载销售记录
        private void LoadSparePartsSales()
        {
            SparePartsSales = new ObservableCollection<SparePartsSale>(_dbContext.SparePartsSales.OrderByDescending(s => s.SaleDate).Take(20).ToList());
        }

        // 过滤库存配件
        private void FilterInventoryItems()
        {
            if (string.IsNullOrEmpty(SearchKeyword))
            {
                LoadInventoryItems();
            }
            else
            {
                var filteredItems = _dbContext.InventoryItems
                    .Where(item => item.ItemName.Contains(SearchKeyword) || item.ItemNumber.Contains(SearchKeyword))
                    .ToList();
                InventoryItems = new ObservableCollection<InventoryItem>(filteredItems);
            }
        }

        // 刷新数据
        private void RefreshData(object parameter)
        {
            LoadInventoryItems();
            LoadInventoryInRecords();
            LoadSparePartsSales();
        }

        // 调整价格
        private void AdjustPrice(object parameter)
        {
            if (SelectedItem != null && NewPrice > 0)
            {
                SelectedItem.UnitPrice = NewPrice;
                SelectedItem.LastUpdatedTime = DateTime.Now;
                _dbContext.SaveChanges();
                LoadInventoryItems(); // 重新加载以更新列表
            }
        }

        // 更新最低库存
        private void UpdateMinInventory(object parameter)
        {
            // 注意：当前InventoryItem模型没有MinInventory字段，这里只是示例
            // 如果需要实现这个功能，需要先在InventoryItem类中添加MinInventory字段
            if (SelectedItem != null && MinInventory >= 0)
            {
                // 这里可以添加更新最低库存的逻辑
            }
        }
    }
}