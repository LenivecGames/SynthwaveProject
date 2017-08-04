using System;
using System.Globalization;

namespace LiteDB.Editor
{
    class BsonValueToStringConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, System.Globalization.CultureInfo culture)
        {
            if (value == null)
            {
                return string.Empty;
            }

            if (value is BsonValue)
            {
                var bsonValue = value as BsonValue;
                if (bsonValue.IsDocument)
                {
                    return "[Document]";
                }
                else if (bsonValue.IsArray)
                {
                    return "[Array]";
                }
                else if (bsonValue.IsBinary)
                {
                    return "[Binary]";
                }
                else
                {
                    return bsonValue.ToString();
                }
            }
            else
            {
                throw new Exception("Cannot convert non BSON value");
            }
        }

        public object ConvertBack(object value, Type targetType, object parameter, System.Globalization.CultureInfo culture)
        {
            if (value == null)
            {
                return null;
            }

            return new BsonValue(value as string);
        }
    }
}