using System;
using System.Globalization;
using System.Windows;
using System.Windows.Data;

namespace Login_Sample.Views
{
    /// <summary>
    /// 将对象是否为null转换为Visibility的转换器
    /// </summary>
    public class NullToVisibilityConverter : IValueConverter
    {
        /// <summary>
        /// 将对象是否为null转换为Visibility
        /// </summary>
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            bool isVisible = value != null;
            
            // 检查是否需要反转结果
            if (parameter is string param && param.Equals("True", StringComparison.OrdinalIgnoreCase))
            {
                isVisible = !isVisible;
            }
            
            return isVisible ? Visibility.Visible : Visibility.Collapsed;
        }
        
        /// <summary>
        /// 反向转换（未实现）
        /// </summary>
        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
}