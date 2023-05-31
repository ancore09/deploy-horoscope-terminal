namespace WebScrapper;

public class Calendar
{
    public static void Print(Prediction prediction, bool nextMonth)
    {
        // get current month
        var month = nextMonth ? (DateTime.Today.Month + 1) % 12 : DateTime.Today.Month;
        var year = nextMonth ? DateTime.Today.Year + (month == 1 ? 1 : 0) : DateTime.Today.Year;

        // get first day of month
        var firstDayOfMonth = new DateTime(year, month, 1);

        // get last day of month
        var lastDayOfMonth = firstDayOfMonth.AddMonths(1).AddDays(-1);

        // get first day of week

        var firstDayOfWeek = firstDayOfMonth;
            
        // get last day of week

        var lastDayOfWeek = lastDayOfMonth;
            
        // get number of weeks in month

        var weeksInMonth = (lastDayOfWeek - firstDayOfWeek).Days / 7 + 1;

        // print calendar

        Console.WriteLine($"{"Пн",3}{"Вт",3}{"Ср",3}{"Чт",3}{"Пт",3}{"Сб",3}{"Вс",3}");

        for (int i = 0; i < weeksInMonth; i++)
        {
            for (int j = 0; j < 7; j++)
            {
                if (i == 0 && j == 0)
                {
                    var dow= firstDayOfWeek.DayOfWeek == DayOfWeek.Sunday ? 7 : (int)firstDayOfWeek.DayOfWeek - 1;
                    var str = new string(' ', 3*dow);
                    Console.Write(str);
                }
                var day = firstDayOfWeek.AddDays(i * 7 + j);
                var dayNumber = day.Day;
                var dayOfWeek = day.DayOfWeek;
                var dayOfWeekNumber = (int) dayOfWeek;
                var dayOfWeekNumberInMonth = dayOfWeekNumber == 0 ? 7 : dayOfWeekNumber;
                var dayOfWeekNumberInMonthString = dayOfWeekNumberInMonth.ToString();
                var dayNumberString = dayNumber.ToString();
                var dayNumberStringWithSpaces = dayNumberString.Length == 1 ? $"  {dayNumberString}" : $" {dayNumberString}";
                var dayNumberStringWithSpacesAndColor = dayNumberStringWithSpaces;
                if (prediction.GoodDays.Contains(dayNumber))
                {
                    dayNumberStringWithSpacesAndColor = dayNumberStringWithSpacesWithColor(dayNumberStringWithSpaces, ConsoleColor.Green);
                }
                else if (prediction.NeutralDays.Contains(dayNumber))
                {
                    dayNumberStringWithSpacesAndColor = dayNumberStringWithSpacesWithColor(dayNumberStringWithSpaces, ConsoleColor.White);
                }
                else if (prediction.BadDays.Contains(dayNumber))
                {
                    dayNumberStringWithSpacesAndColor = dayNumberStringWithSpacesWithColor(dayNumberStringWithSpaces, ConsoleColor.Red);
                }
                Console.Write($"{dayNumberStringWithSpacesAndColor}{(dayOfWeekNumberInMonth == 7 ? "\n" : "")}");
                if (day.Day == lastDayOfMonth.Day)
                {
                    Console.ResetColor();
                    Console.WriteLine("\n");
                    break;
                }
            }
        }

        string dayNumberStringWithSpacesWithColor(string dayNumberStringWithSpaces, ConsoleColor color)
        {
            Console.ForegroundColor = color;
            return dayNumberStringWithSpaces;
        }
    } 
}