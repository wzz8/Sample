using System.Windows;
using System.Windows.Controls;
using Login_Sample.ViewModels;

namespace Login_Sample.Views
{
    /// <summary>
    /// InsuranceManagementView.xaml 的交互逻辑
    /// </summary>
    public partial class InsuranceManagementView : UserControl
    {
        public InsuranceManagementView()
        {
            InitializeComponent();
        }

        /// <summary>
        /// 数据网格双击事件处理
        /// </summary>
        private void DataGrid_MouseDoubleClick(object sender, System.Windows.Input.MouseButtonEventArgs e)
        {
            var dataGrid = sender as DataGrid;
            if (dataGrid != null && dataGrid.SelectedItem != null)
            {
                var viewModel = this.DataContext as InsuranceManagementViewModel;
                if (viewModel != null)
                {
                    // 这里可以打开详情窗口或执行其他编辑操作
                    viewModel.ViewClaimCommand.Execute(dataGrid.SelectedItem);
                }
            }
        }
    }
}