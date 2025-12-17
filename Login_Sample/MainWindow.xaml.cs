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
        private bool _isMouseDown;

        public MainWindow()
        {
            DataContext = new MainViewModel();
            InitializeComponent();
        }

        private void Sidebar_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            _isMouseDown = true;
            ((Border)sender).CaptureMouse();
        }

        private void Sidebar_MouseMove(object sender, MouseEventArgs e)
        {
            if (_isMouseDown)
            {
                // 获取当前鼠标位置相对于窗口的坐标
                Point mousePosition = e.GetPosition(this);
                // 获取窗口中心点
                double windowCenterX = this.ActualWidth / 2;

                // 获取MainViewModel
                MainViewModel viewModel = (MainViewModel)DataContext;
                
                // 如果鼠标位置在窗口左半部分，侧边栏在左侧
                if (mousePosition.X < windowCenterX)
                {
                    viewModel.SidebarOnLeft = true;
                }
                // 如果鼠标位置在窗口右半部分，侧边栏在右侧
                else
                {
                    viewModel.SidebarOnLeft = false;
                }
            }
        }

        private void Sidebar_MouseLeftButtonUp(object sender, MouseButtonEventArgs e)
        {
            _isMouseDown = false;
            ((Border)sender).ReleaseMouseCapture();
        }
    }
}
