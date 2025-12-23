using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace Login_Sample.Views
{
    /// <summary>
    /// BusinessReceptionView.xaml 的交互逻辑
    /// </summary>
    public partial class BusinessReceptionView : UserControl
    {
        public BusinessReceptionView()
        {
            InitializeComponent();
        }

        // 卡片点击事件处理程序
        private void Card_MouseDown(object sender, MouseButtonEventArgs e)
        {
            if (sender is MaterialDesignThemes.Wpf.Card card && card.Tag is string pageName)
            {
                // 获取MainViewModel实例
                if (DataContext is ViewModels.MainViewModel mainViewModel)
                {
                    // 调用导航命令
                    mainViewModel.NavigateCommand.Execute(pageName);
                }
            }
        }
    }
}