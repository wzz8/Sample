using System.Windows;
using test.ViewModels;

namespace test
{
    /// <summary>
    /// RegisterWindow.xaml 的交互逻辑
    /// </summary>
    public partial class RegisterWindow : Window
    {
        public RegisterWindow()
        {
            InitializeComponent();
            this.DataContext = new RegisterViewModel();
        }
    }
}