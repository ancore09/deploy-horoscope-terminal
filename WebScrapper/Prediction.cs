namespace WebScrapper;

public class Prediction
{
    public string? Zodiac { get; set; }
    public List<int> GoodDays { get; set; }
    public List<int> NeutralDays { get; set; }
    public List<int> BadDays { get; set; }
    
    public Prediction()
    {
        GoodDays = new();
        NeutralDays = new();
        BadDays = new();
    }

    public void Print()
    {
        Console.WriteLine($"{Zodiac}:");
        Console.ForegroundColor = ConsoleColor.Green;
        Console.WriteLine($"Благоприятные дни: {string.Join(", ", GoodDays)}");
        Console.ResetColor();
        Console.WriteLine($"Нейтральные дни: {string.Join(", ", NeutralDays)}");
        Console.ForegroundColor = ConsoleColor.Red;
        Console.WriteLine($"Неблагоприятные дни: {string.Join(", ", BadDays)}");
        Console.ResetColor();
    }
    
    public void PrintDayPrediction(int day)
    {
        if (GoodDays.Contains(day))
        {
            Console.ForegroundColor = ConsoleColor.Green;
            Console.WriteLine("Благоприятный день");
            Console.ResetColor();
        }
        else if (NeutralDays.Contains(day))
        {
            Console.WriteLine("Нейтральный день");
        }
        else if (BadDays.Contains(day))
        {
            Console.ForegroundColor = ConsoleColor.Red;
            Console.WriteLine("Неблагоприятный день");
            Console.ResetColor();
        }
        else
        {
            Console.WriteLine("No prediction");
        }
    }
}