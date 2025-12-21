using System;
using System.Globalization;
using System.Windows.Data;

namespace Login_Sample.Converters
{
    /// <summary>
    /// 将字符串转换为布尔值的转换器
    /// </summary>
    public class StringToBooleanConverter : IValueConverter
    {
        /// <summary>
        /// 将字符串转换为布尔值
        /// </summary>
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            if (value is string stringValue)
            {
                // 当字符串为"借用中"时返回true，否则返回false
                // 这样可以控制归还日期DatePicker的可用性
                return stringValue == "借用中";
            }
            return false;
        }

        /// <summary>
        /// 将布尔值转换为字符串（未实现）
        /// </summary>
        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
}