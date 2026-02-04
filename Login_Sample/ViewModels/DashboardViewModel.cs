using CommunityToolkit.Mvvm.ComponentModel;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using System.Windows.Threading;

namespace Login_Sample.ViewModels
{
    public class DashboardViewModel : ObservableObject
    {
        // 业务接待模块数据
        private int _todayReceptions;
        private int _pendingBusinesses;
        private double _businessCompletionRate;
        
        // 财务结算模块数据
        private decimal _todayRevenue;
        private decimal _pendingSettlement;
        private decimal _monthRevenue;
        
        // 备件管理模块数据
        private int _totalSpareParts;
        private int _lowStockAlerts;
        private int _todayConsumptions;
        
        // 车间管理模块数据
        private int _repairingVehicles;
        private double _workshopCompletionRate;
        private int _todayCompletedRepairs;
        
        // 领导查询模块数据
        private double _overallEfficiency;
        private decimal _profitMargin;
        private int _customerSatisfaction;
        
        // 系统管理模块数据
        private int _onlineUsers;
        private int _systemAlerts;
        private string _systemStatus;
        
        // 原始库存模块数据
        private decimal _totalInventoryValue;
        private double _inventoryTurnover;
        private int _totalInventoryItems;
        
        // 定时器用于模拟实时数据更新
        private DispatcherTimer _dataUpdateTimer;
        
        public event PropertyChangedEventHandler? PropertyChanged;
        
        // 业务接待模块属性
        public int TodayReceptions
        {
            get => _todayReceptions;
            set => SetProperty(ref _todayReceptions, value);
        }
        
        public int PendingBusinesses
        {
            get => _pendingBusinesses;
            set => SetProperty(ref _pendingBusinesses, value);
        }
        
        public double BusinessCompletionRate
        {
            get => _businessCompletionRate;
            set => SetProperty(ref _businessCompletionRate, value);
        }
        
        // 财务结算模块属性
        public decimal TodayRevenue
        {
            get => _todayRevenue;
            set => SetProperty(ref _todayRevenue, value);
        }
        
        public decimal PendingSettlement
        {
            get => _pendingSettlement;
            set => SetProperty(ref _pendingSettlement, value);
        }
        
        public decimal MonthRevenue
        {
            get => _monthRevenue;
            set => SetProperty(ref _monthRevenue, value);
        }
        
        // 备件管理模块属性
        public int TotalSpareParts
        {
            get => _totalSpareParts;
            set => SetProperty(ref _totalSpareParts, value);
        }
        
        public int LowStockAlerts
        {
            get => _lowStockAlerts;
            set => SetProperty(ref _lowStockAlerts, value);
        }
        
        public int TodayConsumptions
        {
            get => _todayConsumptions;
            set => SetProperty(ref _todayConsumptions, value);
        }
        
        // 车间管理模块属性
        public int RepairingVehicles
        {
            get => _repairingVehicles;
            set => SetProperty(ref _repairingVehicles, value);
        }
        
        public double WorkshopCompletionRate
        {
            get => _workshopCompletionRate;
            set => SetProperty(ref _workshopCompletionRate, value);
        }
        
        public int TodayCompletedRepairs
        {
            get => _todayCompletedRepairs;
            set => SetProperty(ref _todayCompletedRepairs, value);
        }
        
        // 领导查询模块属性
        public double OverallEfficiency
        {
            get => _overallEfficiency;
            set => SetProperty(ref _overallEfficiency, value);
        }
        
        public decimal ProfitMargin
        {
            get => _profitMargin;
            set => SetProperty(ref _profitMargin, value);
        }
        
        public int CustomerSatisfaction
        {
            get => _customerSatisfaction;
            set => SetProperty(ref _customerSatisfaction, value);
        }
        
        // 系统管理模块属性
        public int OnlineUsers
        {
            get => _onlineUsers;
            set => SetProperty(ref _onlineUsers, value);
        }
        
        public int SystemAlerts
        {
            get => _systemAlerts;
            set => SetProperty(ref _systemAlerts, value);
        }
        
        public string SystemStatus
        {
            get => _systemStatus;
            set => SetProperty(ref _systemStatus, value);
        }
        
        // 原始库存模块属性
        public decimal TotalInventoryValue
        {
            get => _totalInventoryValue;
            set => SetProperty(ref _totalInventoryValue, value);
        }
        
        public double InventoryTurnover
        {
            get => _inventoryTurnover;
            set => SetProperty(ref _inventoryTurnover, value);
        }
        
        public int TotalInventoryItems
        {
            get => _totalInventoryItems;
            set => SetProperty(ref _totalInventoryItems, value);
        }
        
        // 实时数据更新间隔（毫秒）
        private const int DATA_UPDATE_INTERVAL = 5000;
        
        public DashboardViewModel()
        {
            // 初始化模拟数据
            InitializeMockData();
            
            // 启动数据更新定时器
            _dataUpdateTimer = new DispatcherTimer();
            _dataUpdateTimer.Interval = TimeSpan.FromMilliseconds(DATA_UPDATE_INTERVAL);
            _dataUpdateTimer.Tick += (s, e) => UpdateMockData();
            _dataUpdateTimer.Start();
        }
        
        /// <summary>
        /// 初始化模拟数据
        /// </summary>
        private void InitializeMockData()
        {
            Random random = new Random();
            
            // 业务接待模块
            TodayReceptions = random.Next(10, 50);
            PendingBusinesses = random.Next(5, 20);
            BusinessCompletionRate = random.NextDouble() * 30 + 70; // 70%-100%
            
            // 财务结算模块
            TodayRevenue = random.Next(10000, 50000) + (decimal)random.NextDouble();
            PendingSettlement = random.Next(5000, 20000) + (decimal)random.NextDouble();
            MonthRevenue = random.Next(200000, 800000) + (decimal)random.NextDouble();
            
            // 备件管理模块
            TotalSpareParts = random.Next(1000, 5000);
            LowStockAlerts = random.Next(0, 20);
            TodayConsumptions = random.Next(20, 100);
            
            // 车间管理模块
            RepairingVehicles = random.Next(5, 30);
            WorkshopCompletionRate = random.NextDouble() * 40 + 60; // 60%-100%
            TodayCompletedRepairs = random.Next(10, 40);
            
            // 领导查询模块
            OverallEfficiency = random.NextDouble() * 20 + 80; // 80%-100%
            ProfitMargin = random.Next(10, 30) + (decimal)random.NextDouble(); // 10%-30%
            CustomerSatisfaction = random.Next(85, 100); // 85%-100%
            
            // 系统管理模块
            OnlineUsers = random.Next(5, 50);
            SystemAlerts = random.Next(0, 10);
            SystemStatus = "正常";
            
            // 原始库存模块
            TotalInventoryValue = random.Next(500000, 2000000) + (decimal)random.NextDouble();
            InventoryTurnover = random.NextDouble() * 5 + 1; // 1-6
            TotalInventoryItems = random.Next(2000, 10000);
        }
        
        /// <summary>
        /// 更新模拟数据（模拟实时变化）
        /// </summary>
        private void UpdateMockData()
        {
            Random random = new Random();
            
            // 业务接待模块（小幅度随机变化）
            TodayReceptions += random.Next(-2, 3);
            PendingBusinesses += random.Next(-1, 2);
            BusinessCompletionRate += (random.NextDouble() - 0.5) * 5;
            BusinessCompletionRate = Math.Max(0, Math.Min(100, BusinessCompletionRate));
            
            // 财务结算模块（小幅度随机变化）
            TodayRevenue += (decimal)(random.NextDouble() - 0.5) * 1000;
            PendingSettlement += (decimal)(random.NextDouble() - 0.5) * 500;
            
            // 备件管理模块（小幅度随机变化）
            TotalSpareParts += random.Next(-10, 11);
            LowStockAlerts += random.Next(-2, 3);
            TodayConsumptions += random.Next(-1, 2);
            
            // 车间管理模块（小幅度随机变化）
            RepairingVehicles += random.Next(-2, 3);
            WorkshopCompletionRate += (random.NextDouble() - 0.5) * 3;
            WorkshopCompletionRate = Math.Max(0, Math.Min(100, WorkshopCompletionRate));
            TodayCompletedRepairs += random.Next(-1, 2);
            
            // 领导查询模块（小幅度随机变化）
            OverallEfficiency += (random.NextDouble() - 0.5) * 2;
            OverallEfficiency = Math.Max(0, Math.Min(100, OverallEfficiency));
            ProfitMargin += (decimal)(random.NextDouble() - 0.5) * 2;
            ProfitMargin = Math.Max(0, Math.Min(50, ProfitMargin));
            
            // 系统管理模块（小幅度随机变化）
            OnlineUsers += random.Next(-3, 4);
            SystemAlerts += random.Next(-1, 2);
            
            // 原始库存模块（小幅度随机变化）
            TotalInventoryValue += (decimal)(random.NextDouble() - 0.5) * 5000;
            InventoryTurnover += (random.NextDouble() - 0.5) * 0.5;
            InventoryTurnover = Math.Max(0.1, Math.Min(10, InventoryTurnover));
            
            // 确保所有数值为非负
            EnsureNonNegativeValues();
        }
        
        /// <summary>
        /// 确保所有数值为非负
        /// </summary>
        private void EnsureNonNegativeValues()
        {
            TodayReceptions = Math.Max(0, TodayReceptions);
            PendingBusinesses = Math.Max(0, PendingBusinesses);
            TodayRevenue = Math.Max(0, TodayRevenue);
            PendingSettlement = Math.Max(0, PendingSettlement);
            TotalSpareParts = Math.Max(0, TotalSpareParts);
            LowStockAlerts = Math.Max(0, LowStockAlerts);
            TodayConsumptions = Math.Max(0, TodayConsumptions);
            RepairingVehicles = Math.Max(0, RepairingVehicles);
            TodayCompletedRepairs = Math.Max(0, TodayCompletedRepairs);
            OnlineUsers = Math.Max(0, OnlineUsers);
            SystemAlerts = Math.Max(0, SystemAlerts);
            TotalInventoryValue = Math.Max(0, TotalInventoryValue);
            TotalInventoryItems = Math.Max(0, TotalInventoryItems);
        }
    }
}