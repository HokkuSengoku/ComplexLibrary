
using Casting;

short p;
do
{
    Console.Write("Enter p: ");
    var input = Console.ReadLine();
    if (short.TryParse(input, out p))
        break;
}
while (true);

var weekDayMeaning = (WeekDay)p;
var bookAttMeaning = (BookAttributes)p;
Console.WriteLine(weekDayMeaning);
Console.WriteLine(bookAttMeaning);