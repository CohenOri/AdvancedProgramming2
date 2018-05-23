using System;
using System.Windows.Data;
using System.Globalization;
using System.Windows.Media;



using ImageService.Model;


namespace Image_Service_GUI.ViewModel
{
    class MessageTypeToBackgroundConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            if (targetType != typeof(Brush))
            {
                throw new InvalidOperationException("Must convert to a brush!");
            }
            MessageTypeEnum msgType = (MessageTypeEnum)value;
            switch (msgType)
            {
                case MessageTypeEnum.WARNING:
                    return new SolidColorBrush(Colors.Yellow);
                case MessageTypeEnum.FAIL:
                    return new SolidColorBrush(Colors.Red);
                case MessageTypeEnum.INFO:
                    return new SolidColorBrush(Colors.Green);
                default:
                    return new SolidColorBrush(Colors.White);
            }
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
}
