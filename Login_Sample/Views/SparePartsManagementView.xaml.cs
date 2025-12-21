using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows; 
using System.Windows.Input;

namespace Login_Sample.Views
{
    public partial class SparePartsManagementView
    {
        public SparePartsManagementView()
        {
            InitializeComponent();
        }

        private void Card_MouseDown(object sender, MouseButtonEventArgs e)
        {
            // 获取卡片的Tag值，即导航目标页面
            string pageName = ((FrameworkElement)sender).Tag.ToString();

            // 获取MainWindow实例
            var mainWindow = Application.Current.MainWindow as MainWindow;
            if (mainWindow != null)
            {
                // 获取MainViewModel实例
                var viewModel = mainWindow.DataContext as ViewModels.MainViewModel;
                if (viewModel != null && viewModel.NavigateCommand.CanExecute(pageName))
                {
                    // 调用导航命令
                    viewModel.NavigateCommand.Execute(pageName);
                }
            }
        }
    }
}