using System;
using System.Text.RegularExpressions;
using System.Windows.Controls;
using System.Windows.Input;

namespace Login_Sample.Views
{
    /// <summary>
    /// SparePartsSaleView.xaml 的交互逻辑
    /// </summary>
    public partial class SparePartsSaleView : UserControl
    {
        public SparePartsSaleView()
        {
            InitializeComponent();
        }
        
        /// <summary>
        /// 只允许输入数字
        /// </summary>
        /// <param name="sender">事件发送者</param>
        /// <param name="e">事件参数</param>
        private void NumericOnlyPreviewTextInput(object sender, TextCompositionEventArgs e)
        {
            var regex = new Regex("^[0-9]+$");
            e.Handled = !regex.IsMatch(e.Text);
        }
    }
}