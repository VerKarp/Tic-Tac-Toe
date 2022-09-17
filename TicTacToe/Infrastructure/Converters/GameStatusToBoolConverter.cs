using System;
using System.Globalization;
using System.Windows.Data;
using TicTacToe.Models.Enums;

namespace TicTacToe.Infrastructure.Converters
{
    internal class GameStatusToBoolConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            if (value is not GameStatus gameStatus) return null;

            if (gameStatus == GameStatus.GameOn) return true;
            return false;
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            if (value is not bool b) return null;

            if (b == true) return GameStatus.GameOn;
            return GameStatus.GameOff;
        }
    }
}