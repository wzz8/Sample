using System.ComponentModel;
using System.Runtime.CompilerServices;

namespace Login_Sample.ViewModels
{
    public class InsuranceAgentViewModel : INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler? PropertyChanged;

        private void OnPropertyChanged([CallerMemberName] string? propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }

        // 构造函数
        public InsuranceAgentViewModel()
        {
            // 初始化数据或命令
        }

        // 保险代理相关属性和命令
        // 保险记录管理操作，包括记录添加、编辑、删除、统计等
    }
}