using HtmlAgilityPack;
using WebScrapper;

bool nextMonth = false;
string zodiac = "";
bool today = false;
bool calendar = false;

if (args.Any(x => x.Contains("-next")))
    nextMonth = true;
if (args.Any(x => x.Contains("-zodiac")))
    zodiac = args.First(x => x.Contains("-zodiac")).Split("=")[1];
if (args.Any(x => x.Contains("-today")))
    today = true;
if (args.Any(x => x.Contains("-calendar")))
    calendar = true;

if (today && string.IsNullOrEmpty(zodiac))
{
    Console.WriteLine("Не указан знак зодиака");
    return;
}

if (calendar && string.IsNullOrEmpty(zodiac))
{
    Console.WriteLine("Не указан знак зодиака");
    return;
}

string url = nextMonth ? "https://deployhoroscope.ru/next-month" : "https://deployhoroscope.ru/";

var web = new HtmlWeb();

var document = web.Load(url);

var nodes = document.DocumentNode.SelectNodes("/html/body/div[1]/div[3]/table/tbody/tr[position()>0 and position()<14]");

List<Prediction> predictions = new List<Prediction>();

foreach (var htmlNode in nodes)
{
    var prediction = new Prediction();
    var children = htmlNode.ChildNodes.ToList();
    prediction.Zodiac = children[1].InnerText.Trim();
    prediction.GoodDays = children[3].InnerText.Trim().Split().Select(int.Parse).ToList();
    prediction.NeutralDays = children[5].InnerText.Trim().Split().Select(int.Parse).ToList();
    prediction.BadDays = children[7].InnerText.Trim().Split().Select(int.Parse).ToList();
    predictions.Add(prediction);
}

if (today)
{
    var todayPrediction = predictions.First(x => x.Zodiac == zodiac);
    var todayDate = DateTime.Today.Day;
    todayPrediction.PrintDayPrediction(todayDate);
}
else if (!string.IsNullOrWhiteSpace(zodiac) && !calendar)
{
    var prediction = predictions.First(x => x.Zodiac == zodiac);
    prediction.Print();
} else if (calendar)
{
    var prediction = predictions.First(x => x.Zodiac == zodiac);
    Calendar.Print(prediction, nextMonth);
}
else
{
    foreach (var prediction in predictions)
    {
        prediction.Print();
    }
}

