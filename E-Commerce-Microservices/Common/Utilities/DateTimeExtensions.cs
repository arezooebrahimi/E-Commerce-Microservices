using System.Globalization;

namespace Common.Utilities
{
    public static class DateTimeExtensions
    {
        public static string ToPersianDate(this DateTime date) =>
            new PersianCalendar().GetYear(date).ToString("0000") + "/" +
            new PersianCalendar().GetMonth(date).ToString("00") + "/" +
            new PersianCalendar().GetDayOfMonth(date).ToString("00");
    }
}
