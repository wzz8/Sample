using Login_Sample.ViewModels;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using System.Windows.Media;

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

        private void ExpanderButton_MouseDoubleClick(object sender, MouseButtonEventArgs e)
        {
            Button button = sender as Button;
            if (button != null)
            {
                // 找到父级 Expander
                Expander expander = FindParent<Expander>(button);
                if (expander != null)
                {
                    expander.IsExpanded = !expander.IsExpanded;
                    e.Handled = true;
                }
            }
        }

        // 辅助方法：查找视觉树中的父级元素
        private T FindParent<T>(DependencyObject child) where T : DependencyObject
        {
            DependencyObject parentObject = VisualTreeHelper.GetParent(child);
            if (parentObject == null) return null;

            if (parentObject is T parent) return parent;
            return FindParent<T>(parentObject);
        }
    }
}