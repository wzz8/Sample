using Login_Sample.ViewModels;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;

namespace Login_Sample
{
    /// <summary>
    /// MainWindow.xaml 的交互逻辑
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            DataContext = new MainViewModel();
            InitializeComponent();
        }

        private void Expander_MouseDoubleClick(object sender, MouseButtonEventArgs e)
        {
            Expander expander = sender as Expander;
            if (expander != null)
            {
                expander.IsExpanded = !expander.IsExpanded;
                e.Handled = true;
            }
        }
    }
}