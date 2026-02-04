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
    public class SparePartsInboundViewModel : ObservableObject
    {
        // 命令定义
        public ICommand SaveInboundCommand { get; set; }
        public ICommand AddInboundItemCommand { get; set; }
        public ICommand RemoveInboundItemCommand { get; set; }
        public ICommand ResetCommand { get; set; }
        public ICommand ReturnSparePartsCommand { get; set; }
        
        // 入库记录属性
        private string _inboundNumber;
        public string InboundNumber
        {
            get { return _inboundNumber; }
            set { _inboundNumber = value; OnPropertyChanged(); CalculateTotalAmounts(); }
        }
        
        private string _inboundPerson;
        public string InboundPerson
        {
            get { return _inboundPerson; }
            set { _inboundPerson = value; OnPropertyChanged(); }
        }
        
        private decimal _totalAmountWithTax;
        public decimal TotalAmountWithTax
        {
            get { return _totalAmountWithTax; }
            set { _totalAmountWithTax = value; OnPropertyChanged(); }
        }
        
        private decimal _totalAmountWithoutTax;
        public decimal TotalAmountWithoutTax
        {
            get { return _totalAmountWithoutTax; }
            set { _totalAmountWithoutTax = value; OnPropertyChanged(); }
        }
        
        private DateTime _inboundDate;
        public DateTime InboundDate
        {
            get { return _inboundDate; }
            set { _inboundDate = value; OnPropertyChanged(); }
        }
        
        private string _remarks;
        public string Remarks
        {
            get { return _remarks; }
            set { _remarks = value; OnPropertyChanged(); }
        }
        
        // 入库备件列表
        private ObservableCollection<InventoryInItem> _inboundItems;
        public ObservableCollection<InventoryInItem> InboundItems
        {
            get { return _inboundItems; }
            set { _inboundItems = value; OnPropertyChanged(); CalculateTotalAmounts(); }
        }
        
        // 可选择的备件列表
        private ObservableCollection<InventoryItem> _availableSpareParts;
        public ObservableCollection<InventoryItem> AvailableSpareParts
        {
            get { return _availableSpareParts; }
            set { _availableSpareParts = value; OnPropertyChanged(); }
        }
        
        // 构造函数
        public SparePartsInboundViewModel()
        {
            // 初始化命令
            SaveInboundCommand = new RelayCommand<object>(SaveInbound);
            AddInboundItemCommand = new RelayCommand<object>(AddInboundItem);
            RemoveInboundItemCommand = new RelayCommand<object>(RemoveInboundItem);
            ResetCommand = new RelayCommand<object>(Reset);
            ReturnSparePartsCommand = new RelayCommand<object>(ReturnSpareParts);
            
            // 初始化属性
            _inboundNumber = GenerateInboundNumber();
            _inboundPerson = "管理员";
            _inboundDate = DateTime.Now;
            _remarks = string.Empty;
            _inboundItems = new ObservableCollection<InventoryInItem>();
            _availableSpareParts = new ObservableCollection<InventoryItem>();
            
            // 加载模拟数据
            LoadSampleData();
        }
        
        // 生成入库单号
        private string GenerateInboundNumber()
        {
            return "RK" + DateTime.Now.ToString("yyyyMMddHHmmss");
        }
        
        // 保存入库记录
        private void SaveInbound(object? obj)
        {
            // 这里实现保存入库记录的逻辑
            // 通常会调用数据库保存方法
            
            // 模拟保存成功
            Reset(obj);
        }
        
        // 添加入库备件项
        private void AddInboundItem(object? o)
        {
            if (_availableSpareParts.Count > 0)
            {
                var newItem = new InventoryInItem
                {
                    InventoryItem = _availableSpareParts[0],
                    Quantity = 1,
                    UnitPriceWithoutTax = _availableSpareParts[0].UnitPrice,
                    TaxRate = 0.13m
                };
                
                // 计算含税单价和小计
                CalculateItemAmounts(newItem);
                
                _inboundItems.Add(newItem);
                CalculateTotalAmounts();
            }
        }
        
        // 移除入库备件项
        private void RemoveInboundItem(object? o)
        {
            // 这里需要获取选中的项，简化处理，移除最后一项
            if (_inboundItems.Count > 0)
            {
                _inboundItems.RemoveAt(_inboundItems.Count - 1);
                CalculateTotalAmounts();
            }
        }
        
        // 重置表单
        private void Reset(object? o)
        {
            _inboundNumber = GenerateInboundNumber();
            _inboundPerson = "管理员";
            _inboundDate = DateTime.Now;
            _remarks = string.Empty;
            _inboundItems.Clear();
            CalculateTotalAmounts();
            
            OnPropertyChanged(nameof(InboundNumber));
            OnPropertyChanged(nameof(InboundPerson));
            OnPropertyChanged(nameof(InboundDate));
            OnPropertyChanged(nameof(Remarks));
        }
        
        // 备件退货功能
        private void ReturnSpareParts(object? obj)
        {
            if (!_inboundItems.Any())
            {
                System.Windows.MessageBox.Show("没有可退货的备件项！", "提示", System.Windows.MessageBoxButton.OK, System.Windows.MessageBoxImage.Information);
                return;
            }

            // 这里实现退货逻辑
            // 1. 检查是否有选中的项，如果有则退货选中项，否则退货所有项
            // 2. 更新库存数量（减去退货数量）
            // 3. 记录退货信息

            // 模拟退货过程
            string returnInfo = "退货备件：\n";
            foreach (var item in _inboundItems)
            {
                // 更新库存数量
                var inventoryItem = _availableSpareParts.FirstOrDefault(i => i.Id == item.InventoryItem.Id);
                if (inventoryItem != null)
                {
                    inventoryItem.Quantity -= item.Quantity;
                    if (inventoryItem.Quantity < 0)
                        inventoryItem.Quantity = 0;
                }

                returnInfo += $"{item.InventoryItem.ItemName} - 数量：{item.Quantity}\n";
            }

            // 清空当前入库项
            _inboundItems.Clear();
            CalculateTotalAmounts();

            System.Windows.MessageBox.Show(returnInfo + "\n退货成功！", "退货完成", System.Windows.MessageBoxButton.OK, System.Windows.MessageBoxImage.Information);
        }
        
        // 计算单条入库项的金额
        private void CalculateItemAmounts(InventoryInItem item)
        {
            if (item != null)
            {
                item.UnitPriceWithTax = item.UnitPriceWithoutTax * (1 + item.TaxRate);
                item.SubtotalWithoutTax = item.UnitPriceWithoutTax * item.Quantity;
                item.TaxAmount = item.SubtotalWithoutTax * item.TaxRate;
                item.SubtotalWithTax = item.UnitPriceWithTax * item.Quantity;
            }
        }
        
        // 计算总金额
        private void CalculateTotalAmounts()
        {
            _totalAmountWithTax = 0;
            _totalAmountWithoutTax = 0;
            
            foreach (var item in _inboundItems)
            {
                CalculateItemAmounts(item);
                _totalAmountWithTax += item.SubtotalWithTax;
                _totalAmountWithoutTax += item.SubtotalWithoutTax;
            }
            
            OnPropertyChanged(nameof(TotalAmountWithTax));
            OnPropertyChanged(nameof(TotalAmountWithoutTax));
        }
        
        // 加载模拟数据
        private void LoadSampleData()
        {
            // 模拟备件数据
            _availableSpareParts.Add(new InventoryItem
            {
                Id = 1,
                ItemName = "机油滤清器",
                ItemNumber = "OIL-FILTER-001",
                Quantity = 50,
                UnitPrice = 25.00m,
                Supplier = "滤清器供应商",
                WarehouseLocation = "A区1号货架",
                Status = "正常",
                BranchName = "总部",
                LastUpdatedTime = DateTime.Now
            });
            
            _availableSpareParts.Add(new InventoryItem
            {
                Id = 2,
                ItemName = "空气滤清器",
                ItemNumber = "AIR-FILTER-001",
                Quantity = 30,
                UnitPrice = 45.00m,
                Supplier = "滤清器供应商",
                WarehouseLocation = "A区2号货架",
                Status = "正常",
                BranchName = "总部",
                LastUpdatedTime = DateTime.Now
            });
            
            _availableSpareParts.Add(new InventoryItem
            {
                Id = 3,
                ItemName = "火花塞",
                ItemNumber = "SPARK-PLUG-001",
                Quantity = 100,
                UnitPrice = 35.00m,
                Supplier = "火花塞供应商",
                WarehouseLocation = "B区1号货架",
                Status = "正常",
                BranchName = "总部",
                LastUpdatedTime = DateTime.Now
            });
            
            _availableSpareParts.Add(new InventoryItem
            {
                Id = 4,
                ItemName = "刹车片",
                ItemNumber = "BRAKE-PAD-001",
                Quantity = 20,
                UnitPrice = 150.00m,
                Supplier = "制动系统供应商",
                WarehouseLocation = "C区1号货架",
                Status = "正常",
                BranchName = "总部",
                LastUpdatedTime = DateTime.Now
            });
        }
    }
}