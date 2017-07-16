﻿using System;
using System.Globalization;
using Xamarin.Forms;

namespace ArcTMDb.Converters
{
    public class ItemVisibilityEventArgsConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            var itemVisibilityEventArgs = value as ItemVisibilityEventArgs;
            if (itemVisibilityEventArgs == null)
                throw new ArgumentException("Expected value to be of type ItemTappedEventArgs", nameof(value)); //Mudar mensagem

            return itemVisibilityEventArgs.Item;
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
}
