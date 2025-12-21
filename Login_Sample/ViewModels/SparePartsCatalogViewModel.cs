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
    public class SparePartsCatalogViewModel : INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler PropertyChanged;

        private void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }

        private ApplicationDbContext _dbContext;

        // 备件目录列表
        private ObservableCollection<InventoryItem> _catalogItems;
        public ObservableCollection<InventoryItem> CatalogItems
        {
            get { return _catalogItems; }
            set { _catalogItems = value; OnPropertyChanged(); }
        }

        // 查询条件
        private string _sparePartVehicleType;
        public string SparePartVehicleType
        {
            get { return _sparePartVehicleType; }
            set { _sparePartVehicleType = value; OnPropertyChanged(); }
        }

        private string _sparePartNumber;
        public string SparePartNumber
        {
            get { return _sparePartNumber; }
            set { _sparePartNumber = value; OnPropertyChanged(); }
        }

        private string _sparePartName;
        public string SparePartName
        {
            get { return _sparePartName; }
            set { _sparePartName = value; OnPropertyChanged(); }
        }

        // 选中的备件
        private InventoryItem _selectedItem;
        public InventoryItem SelectedItem
        {
            get { return _selectedItem; }
            set { _selectedItem = value; OnPropertyChanged(); }
        }

        // 命令
        public ICommand RefreshCommand { get; set; }
        public ICommand ResetCommand { get; set; }

        // 构造函数
        public SparePartsCatalogViewModel()
        {
            _dbContext = new ApplicationDbContext();
            // 初始化数据
            LoadCatalogItems();
            // 初始化命令
            RefreshCommand = new RelayCommand(FilterCatalogItems);
            ResetCommand = new RelayCommand(ResetFilters);
        }

        // 加载备件目录
        private void LoadCatalogItems()
        {
            CatalogItems = new ObservableCollection<InventoryItem>(_dbContext.InventoryItems.ToList());
        }

        // 过滤备件目录
        private void FilterCatalogItems(object parameter = null)
        {
            var query = _dbContext.InventoryItems.AsQueryable();

            // 应用查询条件
            if (!string.IsNullOrEmpty(SparePartVehicleType))
            {
                // 注意：InventoryItem模型中没有VehicleType属性，这里暂时注释掉
                // query = query.Where(item => item.VehicleType.Contains(SparePartVehicleType));
            }

            if (!string.IsNullOrEmpty(SparePartNumber))
            {
                query = query.Where(item => item.ItemNumber.Contains(SparePartNumber));
            }

            if (!string.IsNullOrEmpty(SparePartName))
            {
                query = query.Where(item => item.ItemName.Contains(SparePartName));
            }

            CatalogItems = new ObservableCollection<InventoryItem>(query.ToList());
        }

        // 重置过滤器
        private void ResetFilters(object parameter)
        {
            SparePartVehicleType = string.Empty;
            SparePartNumber = string.Empty;
            SparePartName = string.Empty;
            LoadCatalogItems();
        }
    }
}