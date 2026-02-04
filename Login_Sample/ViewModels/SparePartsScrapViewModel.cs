using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using Login_Sample.Data;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using System.Windows.Input;

namespace Login_Sample.ViewModels
{
    /// <summary>
    /// 备件报废视图模型
    /// </summary>
    public class SparePartsScrapViewModel : ObservableObject
    {
        #region 属性和字段

        /// <summary>
        /// 报废记录ID
        /// </summary>
        private string _scrapRecordId;
        public string ScrapRecordId
        {
            get { return _scrapRecordId; }
            set { SetProperty(ref _scrapRecordId, value); }
        }

        /// <summary>
        /// 报废日期
        /// </summary>
        private DateTime? _scrapDate;
        public DateTime? ScrapDate
        {
            get { return _scrapDate; }
            set { SetProperty(ref _scrapDate, value); }
        }

        /// <summary>
        /// 报废人员
        /// </summary>
        private string _scrapPerson;
        public string ScrapPerson
        {
            get { return _scrapPerson; }
            set { SetProperty(ref _scrapPerson, value); }
        }

        /// <summary>
        /// 报废原因
        /// </summary>
        private string _scrapReason;
        public string ScrapReason
        {
            get { return _scrapReason; }
            set { SetProperty(ref _scrapReason, value); }
        }

        /// <summary>
        /// 报废状态
        /// </summary>
        private string _scrapStatus;
        public string ScrapStatus
        {
            get { return _scrapStatus; }
            set { SetProperty(ref _scrapStatus, value); }
        }

        /// <summary>
        /// 报废备注
        /// </summary>
        private string _scrapRemarks;
        public string ScrapRemarks
        {
            get { return _scrapRemarks; }
            set { SetProperty(ref _scrapRemarks, value); }
        }

        /// <summary>
        /// 报废项列表
        /// </summary>
        private ObservableCollection<SparePartsScrapItem> _scrapItems;
        public ObservableCollection<SparePartsScrapItem> ScrapItems
        {
            get { return _scrapItems; }
            set { SetProperty(ref _scrapItems, value); }
        }

        /// <summary>
        /// 报废记录历史
        /// </summary>
        private ObservableCollection<SparePartsScrapRecord> _scrapRecords;
        public ObservableCollection<SparePartsScrapRecord> ScrapRecords
        {
            get { return _scrapRecords; }
            set { SetProperty(ref _scrapRecords, value); }
        }

        /// <summary>
        /// 选中的报废项
        /// </summary>
        private SparePartsScrapItem _selectedScrapItem;
        public SparePartsScrapItem SelectedScrapItem
        {
            get { return _selectedScrapItem; }
            set { SetProperty(ref _selectedScrapItem, value); }
        }

        #endregion

        #region 命令

        /// <summary>
        /// 开始报废命令
        /// </summary>
        public ICommand StartScrapCommand { get; private set; }

        /// <summary>
        /// 确认报废命令
        /// </summary>
        public ICommand ConfirmScrapCommand { get; private set; }

        /// <summary>
        /// 刷新报废记录命令
        /// </summary>
        public ICommand RefreshScrapRecordsCommand { get; private set; }

        #endregion

        #region 构造函数

        /// <summary>
        /// 构造函数
        /// </summary>
        public SparePartsScrapViewModel()
        {
            // 初始化命令
            StartScrapCommand = new RelayCommand<object>(StartScrap, CanStartScrap);
            ConfirmScrapCommand = new RelayCommand<object>(ConfirmScrap, CanConfirmScrap);
            RefreshScrapRecordsCommand = new RelayCommand<object>(RefreshScrapRecords, CanRefreshScrapRecords);

            // 初始化数据
            InitializeData();

            // 加载样例数据
            LoadSampleData();
        }

        #endregion

        #region 方法

        /// <summary>
        /// 初始化数据
        /// </summary>
        private void InitializeData()
        {
            ScrapItems = new ObservableCollection<SparePartsScrapItem>();
            ScrapRecords = new ObservableCollection<SparePartsScrapRecord>();
            ScrapDate = DateTime.Now;
            ScrapStatus = "未处理";
        }

        /// <summary>
        /// 加载样例数据
        /// </summary>
        private void LoadSampleData()
        {
            // 生成样例报废记录ID
            ScrapRecordId = GenerateScrapRecordId();

            // 加载样例报废项
            ScrapItems.Add(new SparePartsScrapItem
            {
                InventoryItemId = "INV001",
                ItemName = "机油滤清器",
                ItemNumber = "OF-1001",
                ScrapQuantity = 2,
                UnitPrice = 25.00,
                CurrentStock = 15,
                Supplier = "汽车配件供应商",
                WarehouseLocation = "A区-12架-03层"
            });

            ScrapItems.Add(new SparePartsScrapItem
            {
                InventoryItemId = "INV002",
                ItemName = "空气滤清器",
                ItemNumber = "AF-2002",
                ScrapQuantity = 1,
                UnitPrice = 45.00,
                CurrentStock = 8,
                Supplier = "汽车配件供应商",
                WarehouseLocation = "A区-08架-02层"
            });

            // 加载样例报废记录历史
            ScrapRecords.Add(new SparePartsScrapRecord
            {
                ScrapRecordId = "SCRAP-20240120-001",
                ScrapDate = DateTime.Parse("2024-01-20"),
                ScrapPerson = "张三",
                ScrapReason = "配件损坏",
                ScrapStatus = "已报废",
                ScrapRemarks = "定期检查发现的损坏配件"
            });

            ScrapRecords.Add(new SparePartsScrapRecord
            {
                ScrapRecordId = "SCRAP-20240115-002",
                ScrapDate = DateTime.Parse("2024-01-15"),
                ScrapPerson = "李四",
                ScrapReason = "过期失效",
                ScrapStatus = "已报废",
                ScrapRemarks = "部分配件超过保质期"
            });
        }

        /// <summary>
        /// 生成报废记录ID
        /// </summary>
        /// <returns>报废记录ID</returns>
        private string GenerateScrapRecordId()
        {
            // 生成格式：SCRAP-YYYYMMDD-XXX
            string datePart = DateTime.Now.ToString("yyyyMMdd");
            string sequencePart = (ScrapRecords.Count + 1).ToString().PadLeft(3, '0');
            return $"SCRAP-{datePart}-{sequencePart}";
        }

        /// <summary>
        /// 开始报废命令的执行方法
        /// </summary>
        private void StartScrap(object parameter)
        {
            // 生成新的报废记录ID
            ScrapRecordId = GenerateScrapRecordId();
            ScrapDate = DateTime.Now;
            ScrapStatus = "处理中";
        }

        /// <summary>
        /// 开始报废命令的可用条件
        /// </summary>
        private bool CanStartScrap(object parameter)
        {
            return true;
        }

        /// <summary>
        /// 确认报废命令的执行方法
        /// </summary>
        private void ConfirmScrap(object parameter)
        {
            // 检查是否填写了必要信息
            if (string.IsNullOrWhiteSpace(ScrapPerson) || string.IsNullOrWhiteSpace(ScrapReason))
            {
                // 可以添加错误提示逻辑
                return;
            }

            // 模拟更新库存数量（实际项目中应连接数据库）
            foreach (var item in ScrapItems)
            {
                if (item.CurrentStock >= item.ScrapQuantity)
                {
                    item.CurrentStock -= item.ScrapQuantity;
                }
            }

            // 保存报废记录
            var newRecord = new SparePartsScrapRecord
            {
                ScrapRecordId = ScrapRecordId,
                ScrapDate = ScrapDate.Value,
                ScrapPerson = ScrapPerson,
                ScrapReason = ScrapReason,
                ScrapStatus = "已报废",
                ScrapRemarks = ScrapRemarks
            };

            ScrapRecords.Insert(0, newRecord);

            // 重置表单
            ScrapStatus = "已报废";
        }

        /// <summary>
        /// 确认报废命令的可用条件
        /// </summary>
        private bool CanConfirmScrap(object parameter)
        {
            return !string.IsNullOrWhiteSpace(ScrapPerson) && 
                   !string.IsNullOrWhiteSpace(ScrapReason) &&
                   ScrapItems.Count > 0 &&
                   ScrapItems.All(item => item.ScrapQuantity > 0 && item.CurrentStock >= item.ScrapQuantity);
        }

        /// <summary>
        /// 刷新报废记录命令的执行方法
        /// </summary>
        private void RefreshScrapRecords(object parameter)
        {
            // 模拟刷新数据
            // 实际项目中应从数据库重新加载数据
        }

        /// <summary>
        /// 刷新报废记录命令的可用条件
        /// </summary>
        private bool CanRefreshScrapRecords(object parameter)
        {
            return true;
        }

        #endregion
    }

    #region 数据模型

    /// <summary>
    /// 报废项数据模型
    /// </summary>
    public class SparePartsScrapItem
    {
        /// <summary>
        /// 库存项ID
        /// </summary>
        public string InventoryItemId { get; set; }

        /// <summary>
        /// 配件名称
        /// </summary>
        public string ItemName { get; set; }

        /// <summary>
        /// 配件编号
        /// </summary>
        public string ItemNumber { get; set; }

        /// <summary>
        /// 报废数量
        /// </summary>
        public int ScrapQuantity { get; set; }

        /// <summary>
        /// 单价
        /// </summary>
        public double UnitPrice { get; set; }

        /// <summary>
        /// 当前库存
        /// </summary>
        public int CurrentStock { get; set; }

        /// <summary>
        /// 供应商
        /// </summary>
        public string Supplier { get; set; }

        /// <summary>
        /// 仓库位置
        /// </summary>
        public string WarehouseLocation { get; set; }
    }

    /// <summary>
    /// 报废记录数据模型
    /// </summary>
    public class SparePartsScrapRecord
    {
        /// <summary>
        /// 报废记录ID
        /// </summary>
        public string ScrapRecordId { get; set; }

        /// <summary>
        /// 报废日期
        /// </summary>
        public DateTime ScrapDate { get; set; }

        /// <summary>
        /// 报废人员
        /// </summary>
        public string ScrapPerson { get; set; }

        /// <summary>
        /// 报废原因
        /// </summary>
        public string ScrapReason { get; set; }

        /// <summary>
        /// 报废状态
        /// </summary>
        public string ScrapStatus { get; set; }

        /// <summary>
        /// 报废备注
        /// </summary>
        public string ScrapRemarks { get; set; }
    }

    #endregion
}