using System;
using System.Globalization;
using System.Windows;
using System.Windows.Data;
using Login_Sample.ViewModels;

namespace Login_Sample.Converters
{
    public class DashboardDataTypeToVisibilityConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            if (value == null || parameter == null)
                return Visibility.Collapsed;

            var dataType = (DashboardDataType)value;
            var targetTypeStr = parameter.ToString();

            if (targetTypeStr == "TechnicianOverview" && dataType == DashboardDataType.TechnicianOverview)
                return Visibility.Visible;
            else if (targetTypeStr == "VehicleTask" && dataType == DashboardDataType.VehicleTask)
                return Visibility.Visible;

            return Visibility.Collapsed;
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
}
