using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using System;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Windows;
using System.Windows.Input;

namespace Login_Sample.ViewModels
{
    /// <summary>
    /// 库存盘点视图模型
    /// </summary>
    public class InventoryCheckViewModel : ObservableObject
    {
        // 构造函数
        public InventoryCheckViewModel()
        {
            // 初始化命令
            StartCheckCommand = new RelayCommand<object>(StartCheck);
            SaveCheckResultCommand = new RelayCommand<object>(SaveCheckResult);

            // 加载模拟数据
            LoadSampleData();
        }

        // 命令
        public ICommand StartCheckCommand { get; set; }
        public ICommand SaveCheckResultCommand { get; set; }

        // 盘点记录ID
        private string _checkRecordId;
        public string CheckRecordId
        {
            get => _checkRecordId;
            set
            {
                _checkRecordId = value;
                OnPropertyChanged();
            }
        }

        // 盘点日期
        private DateTime _checkDate;
        public DateTime CheckDate
        {
            get => _checkDate;
            set
            {
                _checkDate = value;
                OnPropertyChanged();
            }
        }

        // 盘点人
        private string _checkPerson;
        public string CheckPerson
        {
            get => _checkPerson;
            set
            {
                _checkPerson = value;
                OnPropertyChanged();
            }
        }

        // 盘点备注
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

        // 盘点项列表
        private ObservableCollection<InventoryCheckItem> _checkItems;
        public ObservableCollection<InventoryCheckItem> CheckItems
        {
            get => _checkItems;
            set
            {
                _checkItems = value;
                OnPropertyChanged();
                CalculateTotalDifference();
            }
        }

        // 总差异数量
        private int _totalDifference;
        public int TotalDifference
        {
            get => _totalDifference;
            set
            {
                _totalDifference = value;
                OnPropertyChanged();
            }
        }

        // 开始盘点
        private void StartCheck(object? parameter)
        {
            // 生成盘点记录ID
            CheckRecordId = "PC" + DateTime.Now.ToString("yyyyMMddHHmmss");
            CheckDate = DateTime.Now;
            CheckPerson = "管理员";
            Remarks = string.Empty;

            // 加载需要盘点的库存数据
            LoadInventoryData();
        }

        // 保存盘点结果
        private void SaveCheckResult(object? parameter)
        {
            // 这里可以添加保存盘点结果的逻辑
            // 例如更新数据库中的库存数量，记录盘点差异等

            // 显示保存成功的提示
            MessageBox.Show("库存盘点结果已保存！", "提示", MessageBoxButton.OK, MessageBoxImage.Information);
        }

        // 计算总差异
        private void CalculateTotalDifference()
        {
            TotalDifference = CheckItems?.Sum(item => item.Difference) ?? 0;
        }

        // 加载库存数据
        private void LoadInventoryData()
        {
            // 这里可以从数据库加载实际的库存数据
            // 现在使用模拟数据
            CheckItems = new ObservableCollection<InventoryCheckItem>
            {
                new InventoryCheckItem
                {
                    InventoryItemId = 1,
                    ItemName = "机油滤清器",
                    ItemNumber = "OIL-FILTER-001",
                    SystemQuantity = 100,
                    ActualQuantity = 100,
                    UnitPrice = 25.00m,
                    Supplier = "机油滤清器供应商",
                    WarehouseLocation = "A区1号货架"
                },
                new InventoryCheckItem
                {
                    InventoryItemId = 2,
                    ItemName = "空气滤清器",
                    ItemNumber = "AIR-FILTER-001",
                    SystemQuantity = 80,
                    ActualQuantity = 80,
                    UnitPrice = 45.00m,
                    Supplier = "空气滤清器供应商",
                    WarehouseLocation = "A区2号货架"
                },
                new InventoryCheckItem
                {
                    InventoryItemId = 3,
                    ItemName = "火花塞",
                    ItemNumber = "SPARK-PLUG-001",
                    SystemQuantity = 150,
                    ActualQuantity = 145, // 实际数量与系统数量有差异
                    UnitPrice = 15.00m,
                    Supplier = "火花塞供应商",
                    WarehouseLocation = "B区1号货架"
                },
                new InventoryCheckItem
                {
                    InventoryItemId = 4,
                    ItemName = "刹车片",
                    ItemNumber = "BRAKE-PAD-001",
                    SystemQuantity = 50,
                    ActualQuantity = 52, // 实际数量与系统数量有差异
                    UnitPrice = 150.00m,
                    Supplier = "制动系统供应商",
                    WarehouseLocation = "C区1号货架"
                },
                new InventoryCheckItem
                {
                    InventoryItemId = 5,
                    ItemName = "发动机机油",
                    ItemNumber = "ENGINE-OIL-001",
                    SystemQuantity = 30,
                    ActualQuantity = 30,
                    UnitPrice = 180.00m,
                    Supplier = "机油供应商",
                    WarehouseLocation = "D区1号货架"
                }
            };
        }

        // 加载模拟数据
        private void LoadSampleData()
        {
            StartCheck(null);
        }
    }

    /// <summary>
    /// 库存盘点项
    /// </summary>
    public class InventoryCheckItem : INotifyPropertyChanged
    {
        private int _actualQuantity;
        private int _systemQuantity;

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
        /// 系统库存数量
        /// </summary>
        public int SystemQuantity
        {
            get => _systemQuantity;
            set
            {
                _systemQuantity = value;
                OnPropertyChanged();
                OnPropertyChanged(nameof(Difference));
            }
        }
        
        /// <summary>
        /// 实际盘点数量
        /// </summary>
        public int ActualQuantity
        {
            get => _actualQuantity;
            set
            {
                _actualQuantity = value;
                OnPropertyChanged();
                OnPropertyChanged(nameof(Difference));
            }
        }
        
        /// <summary>
        /// 差异数量
        /// </summary>
        public int Difference => ActualQuantity - SystemQuantity;
        
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
}