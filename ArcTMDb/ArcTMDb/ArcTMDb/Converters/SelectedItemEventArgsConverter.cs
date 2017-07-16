using System;
using System.Globalization;
using Xamarin.Forms;

namespace ArcTMDb.Converters
{
    public class SelectedItemEventArgsConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            var SelectedItemEventArgs = value as SelectedItemChangedEventArgs;
            if (SelectedItemEventArgs == null)
                throw new ArgumentException("Expected value to be of type ItemTappedEventArgs", nameof(value)); //Mudar mensagem

            return SelectedItemEventArgs.SelectedItem;
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
}
