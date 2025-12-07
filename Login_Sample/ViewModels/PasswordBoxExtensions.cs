using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;

namespace test.ViewModels
{
    public static class PasswordBoxExtensions
    {
        /// <summary>
        /// PasswordChangedCommand 附加属性
        /// </summary>
        public static readonly DependencyProperty PasswordChangedCommandProperty = 
            DependencyProperty.RegisterAttached(
                "PasswordChangedCommand", 
                typeof(ICommand), 
                typeof(PasswordBoxExtensions),
                new PropertyMetadata(null, OnPasswordChangedCommandChanged));

        /// <summary>
        /// GetPasswordChangedCommand 方法
        /// </summary>
        public static ICommand GetPasswordChangedCommand(DependencyObject obj)
        {
            return (ICommand)obj.GetValue(PasswordChangedCommandProperty);
        }

        /// <summary>
        /// SetPasswordChangedCommand 方法
        /// </summary>
        public static void SetPasswordChangedCommand(DependencyObject obj, ICommand value)
        {
            obj.SetValue(PasswordChangedCommandProperty, value);
        }

        /// <summary>
        /// 当 PasswordChangedCommand 属性变化时调用
        /// </summary>
        private static void OnPasswordChangedCommandChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            if (d is PasswordBox passwordBox)
            {
                // 移除旧事件处理程序
                passwordBox.PasswordChanged -= OnPasswordChanged;
                
                // 添加新事件处理程序
                if (e.NewValue is ICommand command)
                {
                    passwordBox.PasswordChanged += OnPasswordChanged;
                }
            }
        }

        /// <summary>
        /// PasswordChanged 事件处理程序
        /// </summary>
        private static void OnPasswordChanged(object sender, RoutedEventArgs e)
        {
            if (sender is PasswordBox passwordBox)
            {
                ICommand command = GetPasswordChangedCommand(passwordBox);
                if (command != null && command.CanExecute(passwordBox.Password))
                {
                    command.Execute(passwordBox.Password);
                }
            }
        }
    }
}