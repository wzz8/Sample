using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Input;
using MaterialDesignThemes.Wpf;

namespace Login_Sample.ViewModels
{
    public class SparePartsManagementViewModel : INotifyPropertyChanged
    {
        // 统计卡片属性
        private int _totalSpareParts;
        public int TotalSpareParts
        {
            get { return _totalSpareParts; }
            set { _totalSpareParts = value; OnPropertyChanged(); }
        }

        private int _totalInventory;
        public int TotalInventory
        {
            get { return _totalInventory; }
            set { _totalInventory = value; OnPropertyChanged(); }
        }

        private int _todayInbound;
        public int TodayInbound
        {
            get { return _todayInbound; }
            set { _todayInbound = value; OnPropertyChanged(); }
        }

        private int _todayOutbound;
        public int TodayOutbound
        {
            get { return _todayOutbound; }
            set { _todayOutbound = value; OnPropertyChanged(); }
        }

        // 导航相关属性和命令
        private object _currentView;
        public object CurrentView
        {
            get { return _currentView; }
            set { _currentView = value; OnPropertyChanged(); }
        }

        // 构造函数
        public SparePartsManagementViewModel()
        {
            // 初始化统计数据
            InitializeDashboardData();
        }

        // 初始化仪表盘数据
        private void InitializeDashboardData()
        {
            // 这里可以从数据库获取真实数据
            // 暂时使用模拟数据
            TotalSpareParts = 1250;
            TotalInventory = 5870;
            TodayInbound = 32;
            TodayOutbound = 18;
        }

        // 属性变更通知实现
        public event PropertyChangedEventHandler PropertyChanged;
        protected virtual void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}