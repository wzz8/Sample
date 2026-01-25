using System.ComponentModel;
using System.Runtime.CompilerServices;
using System.Windows.Input;
using MaterialDesignThemes.Wpf;

namespace Login_Sample.ViewModels
{
    public class CustomerCareViewModel : INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler? PropertyChanged;

        private void OnPropertyChanged([CallerMemberName] string? propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }

        // 构造函数
        public CustomerCareViewModel()
        {
            // 初始化数据或命令
        }

        // 客户关怀相关属性和命令
        // 关怀管理操作，包括提醒设置、提醒发送、提醒记录等
    }
}