﻿using Common.Utilities;
using System.ComponentModel.DataAnnotations;
using System.Reflection;


namespace Common.Exceptions
{
    public static class EnumExtensions
    {
        public static IEnumerable<T> GetEnumValues<T>(this T input) where T : struct
        {
            if (!typeof(T).IsEnum)
                throw new NotSupportedException();

            return Enum.GetValues(input.GetType()).Cast<T>();
        }

        public static IEnumerable<T> GetEnumFlags<T>(this T input) where T : struct, Enum
        {
            foreach (var value in Enum.GetValues(typeof(T)))
            {
                if (input.HasFlag((Enum)(object)value))
                {
                    yield return (T)value;
                }
            }
        }

        public static string ToDisplay(this Enum value, DisplayProperty property = DisplayProperty.Name)
        {
            Assert.NotNull(value, nameof(value));

            var attribute = value.GetType().GetField(value.ToString())?
                .GetCustomAttributes<DisplayAttribute>(false).FirstOrDefault();

            if (attribute == null)
                return value.ToString();

            var propValue = attribute.GetType().GetProperty(property.ToString())?.GetValue(attribute, null);
            return propValue?.ToString() ?? value.ToString();
        }

        public static Dictionary<int, string> ToDictionary(this Enum value)
        {
            return Enum.GetValues(value.GetType()).Cast<Enum>().ToDictionary(p => Convert.ToInt32(p), q => ToDisplay(q));
        }
    }

    public enum DisplayProperty
    {
        Description,
        GroupName,
        Name,
        Prompt,
        ShortName,
        Order
    }


}
