using System.ComponentModel;
using System.Runtime.CompilerServices;

namespace Login_Sample.ViewModels
{
    public class CustomerOpinionViewModel : INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler? PropertyChanged;

        private void OnPropertyChanged([CallerMemberName] string? propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }

        // 构造函数
        public CustomerOpinionViewModel()
        {
            // 初始化数据或命令
        }

        // 客户意见相关属性和命令
        // 意见管理操作，包括意见查看、处理、状态更新等
    }
}