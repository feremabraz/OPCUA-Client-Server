using System;
using System.Globalization;
using Avalonia.Data.Converters;

namespace OPCUA_Client_Desktop.Views;

public class BoolToConnectionStatusConverter : IValueConverter
{
    public object Convert(object? value, Type targetType, object? parameter, CultureInfo culture)
    {
        if (value is bool isConnected)
        {
            return isConnected ? "Connected" : "Disconnected";
        }
        return string.Empty;
    }

    public object? ConvertBack(object? value, Type targetType, object? parameter, CultureInfo culture)
    {
        throw new NotImplementedException();
    }
}
