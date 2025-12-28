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
        /// <param name="value">要转换的字符串值</param>
        /// <param name="targetType">目标类型</param>
        /// <param name="parameter">转换参数</param>
        /// <param name="culture">区域信息</param>
        /// <returns>转换后的布尔值</returns>
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            if (value == null)
                return false;
                
            string strValue = value.ToString().Trim().ToLower();
            
            // 定义表示true的字符串
            string[] trueValues = { "true", "1", "yes", "是", "y", "t" };
            
            // 定义表示false的字符串
            string[] falseValues = { "false", "0", "no", "否", "n", "f" };
            
            // 检查是否匹配true值
            if (trueValues.Contains(strValue))
                return true;
                
            // 检查是否匹配false值
            if (falseValues.Contains(strValue))
                return false;
                
            // 默认返回false
            return false;
        }
        
        /// <summary>
        /// 将布尔值转换为字符串
        /// </summary>
        /// <param name="value">要转换的布尔值</param>
        /// <param name="targetType">目标类型</param>
        /// <param name="parameter">转换参数</param>
        /// <param name="culture">区域信息</param>
        /// <returns>转换后的字符串</returns>
        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            if (value is bool boolValue)
                return boolValue ? "true" : "false";
                
            return "false";
        }
    }
}