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
    /// SparePartsSupplierView.xaml 的交互逻辑
    /// </summary>
    public partial class SparePartsSupplierView : UserControl
    {
        public SparePartsSupplierView()
        {
            InitializeComponent();
        }

        // 处理数据表格的选择变化事件
        private void DataGrid_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (DataContext is ViewModels.SparePartsSupplierViewModel viewModel)
            {
                if (e.AddedItems.Count > 0)
                {
                    viewModel.SelectedSupplier = e.AddedItems[0] as Data.SparePartsSupplier;
                }
            }
        }
    }
}