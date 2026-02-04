using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using Login_Sample.Data;
using System;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Windows.Input;

namespace Login_Sample.ViewModels
{
    public class SparePartsSaleViewModel : ObservableObject
    {
        private readonly ApplicationDbContext _context;

        // 配件列表
        private ObservableCollection<InventoryItem> _inventoryItems;
        public ObservableCollection<InventoryItem> InventoryItems
        {
            get => _inventoryItems;
            set
            {
                _inventoryItems = value;
                OnPropertyChanged();
            }
        }

        // 搜索关键词
        private string _searchKeyword;
        public string SearchKeyword
        {
            get => _searchKeyword;
            set
            {
                _searchKeyword = value;
                OnPropertyChanged();
                SearchItems();
            }
        }

        // 选中的配件
        private InventoryItem _selectedItem;
        public InventoryItem? SelectedItem
        {
            get => _selectedItem;
            set
            {
                _selectedItem = value;
                OnPropertyChanged();
                if (_selectedItem != null)
                {
                    // 当选择配件时，自动填充单价
                    UnitPrice = _selectedItem.UnitPrice;
                    // 重置销售数量为1
                    QuantitySold = 1;
                }
            }
        }

        // 客户姓名
        private string _customerName;
        public string CustomerName
        {
            get => _customerName;
            set
            {
                _customerName = value;
                OnPropertyChanged();
            }
        }

        // 客户电话
        private string _customerPhone;
        public string CustomerPhone
        {
            get => _customerPhone;
            set
            {
                _customerPhone = value;
                OnPropertyChanged();
            }
        }

        // 销售数量
        private int _quantitySold;
        public int QuantitySold
        {
            get => _quantitySold;
            set
            {
                _quantitySold = value;
                OnPropertyChanged();
                // 计算销售总额
                CalculateTotalAmount();
            }
        }

        // 单价
        private decimal _unitPrice;
        public decimal UnitPrice
        {
            get => _unitPrice;
            set
            {
                _unitPrice = value;
                OnPropertyChanged();
                // 计算销售总额
                CalculateTotalAmount();
            }
        }

        // 销售总额
        private decimal _totalAmount;
        public decimal TotalAmount
        {
            get => _totalAmount;
            set
            {
                _totalAmount = value;
                OnPropertyChanged();
            }
        }

        // 销售人
        private string _salesPerson;
        public string SalesPerson
        {
            get => _salesPerson;
            set
            {
                _salesPerson = value;
                OnPropertyChanged();
            }
        }

        // 备注
        private string _remarks;
        public string Remarks
        {
            get => _remarks;
            set
            {
                _remarks = value;
                OnPropertyChanged();
            }
        }

        // 销售命令
        public ICommand SellCommand { get; }

        // 构造函数
        public SparePartsSaleViewModel()
        {
            _context = new ApplicationDbContext();
            _inventoryItems = new ObservableCollection<InventoryItem>();
            _searchKeyword = string.Empty;
            _customerName = string.Empty;
            _customerPhone = string.Empty;
            _salesPerson = string.Empty;
            _remarks = string.Empty;
            _quantitySold = 1;
            _unitPrice = 0;
            _totalAmount = 0;

            // 初始化命令
            SellCommand = new RelayCommand<object>(SellSpareParts);

            // 加载库存配件数据
            LoadInventoryItems();
        }

        // 加载库存配件数据
        private void LoadInventoryItems()
        {
            try
            {
                // 从数据库加载所有库存配件
                var items = _context.InventoryItems.ToList();
                InventoryItems = new ObservableCollection<InventoryItem>(items);
            }
            catch (Exception ex)
            {
                // 处理异常
                Console.WriteLine($"加载库存配件失败: {ex.Message}");
            }
        }

        // 搜索配件
        private void SearchItems()
        {
            try
            {
                var items = _context.InventoryItems
                    .Where(item => 
                        item.ItemName.Contains(SearchKeyword, StringComparison.OrdinalIgnoreCase) || 
                        item.ItemNumber.Contains(SearchKeyword, StringComparison.OrdinalIgnoreCase))
                    .ToList();
                InventoryItems = new ObservableCollection<InventoryItem>(items);
            }
            catch (Exception ex)
            {
                // 处理异常
                Console.WriteLine($"搜索配件失败: {ex.Message}");
            }
        }

        // 计算销售总额
        private void CalculateTotalAmount()
        {
            TotalAmount = QuantitySold * UnitPrice;
        }

        // 执行配件销售
        private void SellSpareParts(object? parameter)
        {
            try
            {
                // 验证输入
                if (SelectedItem == null)
                {
                    System.Windows.MessageBox.Show("请选择要销售的配件");
                    return;
                }

                if (string.IsNullOrWhiteSpace(CustomerName))
                {
                    System.Windows.MessageBox.Show("请输入客户姓名");
                    return;
                }

                if (QuantitySold <= 0)
                {
                    System.Windows.MessageBox.Show("销售数量必须大于0");
                    return;
                }

                if (SelectedItem.Quantity < QuantitySold)
                {
                    System.Windows.MessageBox.Show($"库存不足，当前库存: {SelectedItem.Quantity}");
                    return;
                }

                // 更新库存数量
                SelectedItem.Quantity -= QuantitySold;
                SelectedItem.LastUpdatedTime = DateTime.Now;

                // 创建销售记录
                var sale = new SparePartsSale
                {
                    InventoryItemId = SelectedItem.Id,
                    CustomerName = CustomerName,
                    CustomerPhone = CustomerPhone,
                    QuantitySold = QuantitySold,
                    UnitPrice = UnitPrice,
                    TotalAmount = TotalAmount,
                    SalesPerson = SalesPerson,
                    Remarks = Remarks,
                    SaleDate = DateTime.Now
                };

                // 保存到数据库
                _context.SparePartsSales.Add(sale);
                _context.SaveChanges();

                // 刷新配件列表
                LoadInventoryItems();

                // 重置表单
                ResetForm();

                System.Windows.MessageBox.Show("配件销售成功");
            }
            catch (Exception ex)
            {
                // 处理异常
                Console.WriteLine($"配件销售失败: {ex.Message}");
                System.Windows.MessageBox.Show($"配件销售失败: {ex.Message}");
            }
        }

        // 重置表单
        private void ResetForm()
        {
            SelectedItem = null;
            CustomerName = string.Empty;
            CustomerPhone = string.Empty;
            QuantitySold = 1;
            UnitPrice = 0;
            TotalAmount = 0;
            Remarks = string.Empty;
        }
    }
}