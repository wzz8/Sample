using System.ComponentModel;
using System.Runtime.CompilerServices;
using System.Windows.Input;
using MaterialDesignThemes.Wpf;

namespace Login_Sample.ViewModels
{
    public class FinancialSettlementViewModel : INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler? PropertyChanged;

        private void OnPropertyChanged([CallerMemberName] string? propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }

        // 构造函数
        public FinancialSettlementViewModel()
        {
            // 初始化数据或命令
        }

        // 这里可以添加财务结算相关的属性和命令
    }
}