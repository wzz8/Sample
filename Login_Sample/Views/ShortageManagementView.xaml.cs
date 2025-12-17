using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;

namespace Login_Sample.Views
{
    /// <summary>
    /// ShortageManagementView.xaml 的交互逻辑
    /// </summary>
    public partial class ShortageManagementView : UserControl
    {
        public ShortageManagementView()
        {
            InitializeComponent();
        }

        /// <summary>
        /// 双击数据行处理不同状态的操作
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void DataGrid_MouseDoubleClick(object sender, MouseButtonEventArgs e)
        {
            if (DataContext is ViewModels.ShortageManagementViewModel viewModel)
            {
                if (viewModel.SelectedShortageItem != null)
                {
                    // 根据当前状态执行不同操作
                    switch (viewModel.SelectedShortageItem.Status)
                    {
                        case "已到货":
                            if (MessageBox.Show("确认通知客户吗？", "操作确认", MessageBoxButton.YesNo, MessageBoxImage.Question) == MessageBoxResult.Yes)
                            {
                                viewModel.NotifyCustomerCommand.Execute(null);
                            }
                            break;
                        case "已通知":
                            if (MessageBox.Show("确认处理缺件吗？", "操作确认", MessageBoxButton.YesNo, MessageBoxImage.Question) == MessageBoxResult.Yes)
                            {
                                viewModel.ProcessShortageCommand.Execute(null);
                            }
                            break;
                        case "已确认":
                            MessageBox.Show("当前状态为已确认，等待备件计划员采购", "提示", MessageBoxButton.OK, MessageBoxImage.Information);
                            break;
                        case "已订货":
                            MessageBox.Show("当前状态为已订货，等待备件到货", "提示", MessageBoxButton.OK, MessageBoxImage.Information);
                            break;
                        case "已处理":
                            MessageBox.Show("该缺件已处理完成", "提示", MessageBoxButton.OK, MessageBoxImage.Information);
                            break;
                    }
                }
            }
        }
    }
}