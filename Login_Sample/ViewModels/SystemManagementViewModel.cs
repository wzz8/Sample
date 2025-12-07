using System.ComponentModel;
using System.Runtime.CompilerServices;
using System.Windows.Input;
using MaterialDesignThemes.Wpf;

namespace Login_Sample.ViewModels
{
    public class SystemManagementViewModel : INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler? PropertyChanged;

        private void OnPropertyChanged([CallerMemberName] string? propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }

        // 构造函数
        public SystemManagementViewModel()
        {
            // 初始化数据或命令
        }

        // 这里可以添加系统管理相关的属性和命令
    }
}